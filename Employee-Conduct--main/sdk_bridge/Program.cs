using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

internal sealed record BridgeOptions(
    string DeviceIp,
    int DevicePort,
    int MachineNumber,
    string DeviceId,
    int Password,
    string ServerUrl,
    string HeartbeatUrl,
    int PollSeconds,
    bool MarkRead
);

internal sealed record PunchPayload(
    string user_id,
    string timestamp,
    int verify_mode,
    string device_id,
    int machine_number,
    int terminal_machine_number,
    string protocol,
    string raw
);

internal static class Program
{
    private static readonly HttpClient Client = new()
    {
        Timeout = TimeSpan.FromSeconds(15)
    };
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = null,
        DictionaryKeyPolicy = null,
        WriteIndented = false
    };

    public static async Task<int> Main(string[] args)
    {
        BridgeOptions options = ParseOptions(args);
        string sentPath = Path.Combine(AppContext.BaseDirectory, $"sent-{Sanitize(options.DeviceId)}.txt");
        HashSet<string> sent = LoadSentKeys(sentPath);

        Console.WriteLine("============================================================");
        Console.WriteLine("HRMS SDK Biometric Bridge - Device ID: " + options.DeviceId);
        Console.WriteLine("Connecting to biometric device " + options.DeviceIp + ":" + options.DevicePort);
        Console.WriteLine("Syncing punches to: " + options.ServerUrl);
        Console.WriteLine("Machine number: " + options.MachineNumber + ", Password: " + options.Password);
        Console.WriteLine("============================================================");

        while (true)
        {
            bool connected = false;
            try
            {
                connected = sbxpc.SBXPCDLL.ConnectTcpip(
                    options.MachineNumber,
                    options.DeviceIp,
                    options.DevicePort,
                    options.Password
                );

                if (!connected)
                {
                    Console.WriteLine("[CRITICAL] SDK connection failed: " + LastSdkError(options.MachineNumber));
                    await Task.Delay(TimeSpan.FromSeconds(options.PollSeconds));
                    continue;
                }

                Console.WriteLine("[SDK] Connected. Reading general attendance logs...");
                await PostHeartbeat(options);
                List<PunchPayload> newPunches = ReadPunches(options)
                    .Where(p => sent.Add(BuildKey(p)))
                    .ToList();

                if (newPunches.Count == 0)
                {
                    Console.WriteLine("[SDK] No new punches found.");
                }
                else
                {
                    foreach (PunchPayload punch in newPunches)
                    {
                        Console.WriteLine($"[PUNCH] user={punch.user_id} time={punch.timestamp} verify={punch.verify_mode}");
                    }

                    if (await PostPunches(options.ServerUrl, newPunches))
                    {
                        SaveSentKeys(sentPath, sent);
                    }
                    else
                    {
                        foreach (PunchPayload punch in newPunches)
                        {
                            sent.Remove(BuildKey(punch));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CRITICAL] Bridge error: " + ex.Message);
            }
            finally
            {
                if (connected)
                {
                    try
                    {
                        sbxpc.SBXPCDLL.Disconnect(options.MachineNumber);
                    }
                    catch
                    {
                        // The SDK can throw during disconnect after network loss; the next loop reconnects.
                    }
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(options.PollSeconds));
        }
    }

    private static IEnumerable<PunchPayload> ReadPunches(BridgeOptions options)
    {
        bool readOk = sbxpc.SBXPCDLL.ReadGeneralLogData(options.MachineNumber, options.MarkRead ? (byte)1 : (byte)0);
        if (!readOk)
        {
            Console.WriteLine("[SDK] ReadGeneralLogData failed: " + LastSdkError(options.MachineNumber));
            yield break;
        }

        while (sbxpc.SBXPCDLL.GetGeneralLogData(
            options.MachineNumber,
            out int terminalMachineNumber,
            out int enrollNumber,
            out int enrollMachineNumber,
            out int verifyMode,
            out int year,
            out int month,
            out int day,
            out int hour,
            out int minute,
            out int second
        ))
        {
            DateTime punchTime;
            try
            {
                punchTime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local);
            }
            catch
            {
                Console.WriteLine($"[SDK] Ignored invalid timestamp for user {enrollNumber}: {year}-{month}-{day} {hour}:{minute}:{second}");
                continue;
            }

            string raw = JsonSerializer.Serialize(new
            {
                terminalMachineNumber,
                enrollNumber,
                enrollMachineNumber,
                verifyMode,
                year,
                month,
                day,
                hour,
                minute,
                second
            });

            yield return new PunchPayload(
                user_id: enrollNumber.ToString(),
                timestamp: punchTime.ToString("o"),
                verify_mode: verifyMode,
                device_id: options.DeviceId,
                machine_number: enrollMachineNumber,
                terminal_machine_number: terminalMachineNumber,
                protocol: "bridge",
                raw: raw
            );
        }
    }

    private static async Task<bool> PostPunches(string serverUrl, List<PunchPayload> punches)
    {
        string json = JsonSerializer.Serialize(punches, JsonOptions);
        using HttpResponseMessage response = await Client.PostAsync(
            serverUrl,
            new StringContent(json, Encoding.UTF8, "application/json")
        );
        string body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("[HTTP] Push failed: " + (int)response.StatusCode + " " + body);
            Console.WriteLine("[HTTP] Payload: " + json);
            return false;
        }

        try
        {
            using JsonDocument document = JsonDocument.Parse(body);
            if (document.RootElement.TryGetProperty("invalid", out JsonElement invalid) && invalid.GetInt32() > 0)
            {
                Console.WriteLine("[HTTP] Server rejected part of the batch: " + body);
                Console.WriteLine("[HTTP] Payload: " + json);
                return false;
            }
        }
        catch
        {
            Console.WriteLine("[HTTP] Could not parse server response, keeping punches unsent: " + body);
            return false;
        }

        Console.WriteLine("[HTTP] Synced " + punches.Count + " punch(es): " + body);
        return true;
    }

    private static async Task PostHeartbeat(BridgeOptions options)
    {
        try
        {
            Dictionary<string, object> heartbeat = new()
            {
                ["device_id"] = options.DeviceId,
                ["serial_number"] = options.DeviceId,
                ["device_ip"] = options.DeviceIp,
                ["device_port"] = options.DevicePort,
                ["machine_number"] = options.MachineNumber,
                ["protocol"] = "sdk_bridge"
            };
            string json = JsonSerializer.Serialize(heartbeat, JsonOptions);
            using HttpResponseMessage response = await Client.PostAsync(
                options.HeartbeatUrl,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            string body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("[HEARTBEAT] Connected heartbeat accepted.");
            }
            else
            {
                Console.WriteLine("[HEARTBEAT] Failed: " + (int)response.StatusCode + " " + body);
                Console.WriteLine("[HEARTBEAT] Payload: " + json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[HEARTBEAT] Failed: " + ex.Message);
        }
    }

    private static BridgeOptions ParseOptions(string[] args)
    {
        Dictionary<string, string> values = new(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < args.Length; i++)
        {
            string key = args[i];
            if (!key.StartsWith("--", StringComparison.Ordinal))
            {
                continue;
            }

            if (key.Equals("--mark-read", StringComparison.OrdinalIgnoreCase))
            {
                values[key] = "true";
                continue;
            }

            if (i + 1 < args.Length)
            {
                values[key] = args[++i];
            }
        }

        string deviceIp = Get(values, "--ip", "BIOMETRIC_DEVICE_IP", "192.168.0.102");
        int devicePort = GetInt(values, "--port", "BIOMETRIC_DEVICE_PORT", 4370);
        string deviceId = Get(values, "--device-id", "BIOMETRIC_DEVICE_ID", "hrms-device-01");
        int machineNumber = GetInt(values, "--machine-number", "BIOMETRIC_MACHINE_NUMBER", ParseTrailingNumber(deviceId, 1));
        int password = GetInt(values, "--password", "BIOMETRIC_DEVICE_PASSWORD", 0);
        string serverUrl = Get(values, "--server-url", "BIOMETRIC_SERVER_URL", "http://127.0.0.1:8000/api/attendance/biometric-punch/");
        string heartbeatUrl = Get(values, "--heartbeat-url", "BIOMETRIC_HEARTBEAT_URL", BuildHeartbeatUrl(serverUrl));
        int pollSeconds = Math.Max(1, GetInt(values, "--poll-seconds", "BIOMETRIC_POLL_SECONDS", 5));
        bool markRead = values.ContainsKey("--mark-read") || IsTruthy(Environment.GetEnvironmentVariable("BIOMETRIC_MARK_READ"));

        return new BridgeOptions(deviceIp, devicePort, machineNumber, deviceId, password, serverUrl, heartbeatUrl, pollSeconds, markRead);
    }

    private static string Get(Dictionary<string, string> values, string key, string envName, string fallback)
    {
        if (values.TryGetValue(key, out string? fromArgs) && !string.IsNullOrWhiteSpace(fromArgs))
        {
            return fromArgs;
        }

        string? fromEnv = Environment.GetEnvironmentVariable(envName);
        return string.IsNullOrWhiteSpace(fromEnv) ? fallback : fromEnv;
    }

    private static int GetInt(Dictionary<string, string> values, string key, string envName, int fallback)
    {
        string raw = Get(values, key, envName, fallback.ToString());
        return int.TryParse(raw, out int parsed) ? parsed : fallback;
    }

    private static int ParseTrailingNumber(string value, int fallback)
    {
        string digits = new(value.Reverse().TakeWhile(char.IsDigit).Reverse().ToArray());
        return int.TryParse(digits, out int parsed) ? parsed : fallback;
    }

    private static bool IsTruthy(string? value)
    {
        return value is not null && new[] { "1", "true", "yes", "y" }.Contains(value.Trim(), StringComparer.OrdinalIgnoreCase);
    }

    private static string BuildHeartbeatUrl(string serverUrl)
    {
        if (serverUrl.Contains("/biometric-punch/", StringComparison.OrdinalIgnoreCase))
        {
            return serverUrl.Replace("/biometric-punch/", "/biometric-heartbeat/", StringComparison.OrdinalIgnoreCase);
        }

        Uri uri = new(serverUrl);
        return new Uri(uri, "/api/attendance/biometric-heartbeat/").ToString();
    }

    private static string LastSdkError(int machineNumber)
    {
        return sbxpc.SBXPCDLL.GetLastError(machineNumber, out int code) ? $"SDK error {code}" : "unknown SDK error";
    }

    private static string BuildKey(PunchPayload punch)
    {
        return $"{punch.device_id}|{punch.user_id}|{punch.timestamp}|{punch.verify_mode}";
    }

    private static HashSet<string> LoadSentKeys(string path)
    {
        if (!File.Exists(path))
        {
            return new HashSet<string>(StringComparer.Ordinal);
        }

        return File.ReadLines(path).Where(line => !string.IsNullOrWhiteSpace(line)).ToHashSet(StringComparer.Ordinal);
    }

    private static void SaveSentKeys(string path, HashSet<string> sent)
    {
        File.WriteAllLines(path, sent.OrderBy(line => line, StringComparer.Ordinal));
    }

    private static string Sanitize(string value)
    {
        foreach (char invalid in Path.GetInvalidFileNameChars())
        {
            value = value.Replace(invalid, '_');
        }

        return string.IsNullOrWhiteSpace(value) ? "device" : value;
    }
}

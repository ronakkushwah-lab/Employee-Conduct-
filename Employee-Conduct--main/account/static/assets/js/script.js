/* preloader script */
var loader = document.getElementById("preloader");
window.addEventListener("load", function(){
    loader.style.display = "none";
})

/* sidebar menuitem toggle script */
$("#menu").metisMenu();

/* sidebar responsive script */
$("#btn").click(function(){
    $(".wrapper").toggleClass("active");
    $(".sidebar_wrapper .toggle-icon").removeClass("ms-auto");
    $(".sidebar_wrapper .toggle-icon").addClass("m-auto");
});

/* dashboard charts script */
$('#chart1').sparkline([5,8,7,10,9,10,8,6,4,6,8,7,6,8], {
    type: 'bar',
    height: '35',
    barWidth: '3',
    resize: true,
    barSpacing: '3',
    barColor: '#fff'
});
$("#chart2").sparkline([0,5,3,7,5,10,3,6,5,10], {
    type: 'line',
    width: '80',
    height: '40',
    lineWidth: '2',
    lineColor: '#fff',
    fillColor: 'transparent',
    spotColor: '#fff',
});
$("#chart3").sparkline([2,3,4,5,4,3,2,3,4,5,6,5,4,3,4,5], {
   type: 'discrete',
   width: '75',
   height: '40',
   lineColor: '#fff',
   lineHeight: 22
});
$(document).ready(function(){
    var ctx = document.getElementById("chart4").getContext('2d');

    var gradientStroke5 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke5.addColorStop(0, '#8e2de2');
    gradientStroke5.addColorStop(1, '#4a00e0');

    var gradientStroke6 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke6.addColorStop(0, ' #ee0979');
    gradientStroke6.addColorStop(0.5, '#ff6a00');

    var gradientStroke7 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke7.addColorStop(0, ' #00b09b');
    gradientStroke7.addColorStop(0.5, '#96c93d');

    var myChart = new Chart(ctx, {
      type: 'pie',
      data: {
        labels: ["Employees", "Clients", "Projects"],
        datasets: [{
            backgroundColor: [
                gradientStroke5,
                gradientStroke6,
                gradientStroke7
            ],
            hoverBackgroundColor: [
                gradientStroke5,
                gradientStroke6,
                gradientStroke7
            ],
    		data: [{{ employee_count }}, {{ clients_count  }},{{ projects_count }}]
            }]
        },
        options: {
            maintainAspectRatio: false,
            legend: {
                display: false
            },
            tooltips: {
    			displayColors:false
            }
        }
    });
});

/* pagination on table */
$(document).ready(function(){
    $('#myTable').DataTable();
});

/* download button script */
$('#download-salary-pdf').click(function(e){
    var opt = ({
        margin: 0.5,
        filename: 'Salary.pdf',
        image: { 
            type: 'png'
        },
        html2canvas: { 
            scale: 1 
        },
        jsPDF: { 
            unit: 'in', 
            format: 'letter', 
            orientation: 'portrait' 
        }
    });
    e.preventDefault();
    var element = document.getElementById('print-salary');
    html2pdf(element, opt);
});
$('#download-invoice-pdf').click(function(e){
    var opts = ({
        margin: 0.5,
        filename: 'Invoice.pdf',
        image: { 
            type: 'png'
        },
        html2canvas: { 
            scale: 1 
        },
        jsPDF: { 
            unit: 'in', 
            format: 'letter', 
            orientation: 'portrait' 
        }
    });
    e.preventDefault();
    var elements = document.getElementById('print-invoice');
    html2pdf(elements, opts);
});


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


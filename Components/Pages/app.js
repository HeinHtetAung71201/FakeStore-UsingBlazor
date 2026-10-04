$(".sidebar-btn").click(function(){
    $(".sideBar").animate({marginLeft:0})
});
$(".closebtn").click(function(){
    $(".sideBar").animate({
        marginLeft:"-100%"
    })

})
$(".counter").counterUp({
    delay: 10,
    time: 1000,
});

function link(url){
    setTimeout(() => {
        location.href=(`${url}`);
    }, 500);
}


//chartjs
let dateArr=['JUN','FEB','MAR','APR','MAY','JUN','JUL','AUG','SEP','OCT','NOV','DEC']
let orders=[40,20,10,22,10,30,21,25,45,85,71,72]
let visitors=[10,9,2,1,5,8,41,2,20,25,11,12]
var ctx = document.getElementById('myChart').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: dateArr,
        datasets: [{
            label:'orders',
            data: orders,
            // backgroundColor: '#0d6efdb5',
            borderColor: '#0d6efd',
            borderWidth: 1,
            
        },
        {
            label:'visitors',
            data: visitors,
            // backgroundColor: '#a7ceaa',
            borderColor:'#d56762',
            borderWidth: 1,
        }
    ],
       
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            },
            
            
        },
        elements: {
            line: {
                tension: 0.2
            }
        },
        legend:{
          display:true,
          position:'top',

          label:{
            color:'#333',
            font:{
                fontWeight:'500px'
            }
          },
          
          
        },
        maintainAspectRatio:false,
    }
});
// chart.canvas.parentNode.style.height = '508px';
// chart.canvas.parentNode.style.width = '128px';

//userImg
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
  })

//doughnutChart
let order=[10,15,25,90,70,100,28];
let orderedPlaces=["MDY","YGN","MYW","SGG","POL","YMB","SBO"];
var ctx = document.getElementById('doughnutChart').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: orderedPlaces,
        datasets: [{
            label: orderedPlaces, 
            data: order,
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)',
                'rgba(255, 159, 4, 0.2)',
                
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)',
                'rgba(255, 159, 4, 1)',
            ],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        },
        legend:{
            display:true,
            position:'bottom',
  
            label:{
              color:'#333',
              font:{
                  fontWeight:'500px'
              }
            },
            
            
          },
          maintainAspectRatio:false,
    }
});


var barColors = ["red", "green", "blue", "orange", "brown"];
var revenueData = [];

$(document).ready(function () {
    //     //Ajax
    $.ajax({
        url: 'https://localhost:7219/api/Order/GetWeeklyRevenue',
        type: 'get',
        success: function (data) {
            data.forEach(function (val, i) {
                revenueData.push(val);
            })
            BestSellerProduct();
        },
        error: function (err) {

        }
    })

})



function BestSellerProduct() {

    var days = revenueData.map(function (value) {
        return value.gunAdi;
    })
    var totalRevenue = revenueData.map(function (value) {
        return value.toplamCiro;
    })

    console.log(days);
    console.log(totalRevenue);
    const ctx2 = document.getElementById('myChart2');

    new Chart(ctx2, {
        type: 'bar',
        data: {
            labels: days,
            datasets: [{
                label: 'Top 10 Ürünler',
                data: totalRevenue,
                backgroundColor: barColors,
                borderWidth: 2

            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true

                }
            }
        }
    });
}

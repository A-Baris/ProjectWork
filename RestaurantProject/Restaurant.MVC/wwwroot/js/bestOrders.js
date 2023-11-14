$(document).ready(function () {
    getBestOrders();
});

function getBestOrders() {
    $.ajax({
        url: 'https://localhost:7219/api/Order/GetBestOrders',
        dataType: 'json',
        success: function (data) {
            displayBestOrders(data);
            // Handle the success response
            console.log(data);
            // You can update your UI or perform other actions with the data

            // For example, append the data to a div with id 'result'
            $('#result').html(JSON.stringify(data));
        },
        error: function (error) {
            // Handle the error
            console.error(error);
        }
    });
}

function displayBestOrders(bestOrdersData) {
    var tableBody = $("#dataTable tbody");

    tableBody.empty(); // Clear the table body.

    bestOrdersData.forEach(function (product) {

      

            var row = $("<tr>");
            row.append($("<td>").text(product.productName));
            row.append($("<td>").text(product.toplamSiparis));

            tableBody.append(row);
        
    });
}


//var productData = [];
//$(document).ready(function () {
//    //     //Ajax
//    $.ajax({
//        url: 'https://localhost:7219/api/Order/GetBestOrders',
//        type: 'get',
//        success: function (data) {
//            data.forEach(function (val, i) {
//                productData.push(val);
//            })
//            BestSellerProduct();
//        },
//        error: function (err) {

//        }
//    });

//});








//function BestSellerProduct() {
//    var barColors = ["red", "green", "blue", "orange", "brown", "yellow", "black"];

//    var products = productData.map(function (value) {
//        return value.productName;
//    })
//    var salesQuantity = productData.map(function (value) {
//        return value.toplamSiparis;
//    })

//    console.log(products);
//    console.log(salesQuantity);
//    const ctx = document.getElementById('myChart');

//    new Chart(ctx, {
//        type: 'bar',
//        data: {
//            labels: products,
//            datasets: [{
//                label: 'Top 10 Ürünler',
//                data: salesQuantity,
//                backgroundColor: barColors,
//                borderWidth: 2

//            }]
//        },
//        options: {
//            scales: {
//                y: {
//                    beginAtZero: true

//                }
//            }
//        }
//    });
//}

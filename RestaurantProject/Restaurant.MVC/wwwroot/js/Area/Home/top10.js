$(document).ready(function () {
    populateMenuOptions();
});

function populateMenuOptions() {
    $.ajax({
        url: 'https://localhost:7219/api/Menu/GetAllMenus',
        dataType: 'json',
        success: function (data) {
            var menuSelect = $('#menuSelect');

            // Clear existing options
            menuSelect.empty();

            // Add default option
            menuSelect.append($('<option>', {
                value: '',
                text: 'Select a Menu'
            }));

            // Add menu options
            data.forEach(function (menu) {
                menuSelect.append($('<option>', {
                    value: menu.menuName,
                    text: menu.menuName
                }));
            });
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function fetchAndDisplayTopProducts() {
    var selectedMenu = $('#menuSelect').val();

    if (!selectedMenu) {
        alert('Please select a menu.');
        return;
    }

    $.ajax({
        url: `https://localhost:7219/api/order/getbestorders?menuName=${selectedMenu}`,
        dataType: 'json',
        success: function (data) {
            displayTopProducts(data);
            console.log(data);
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function displayTopProducts(topProductsData) {
    var tableBody = $("#dataTable tbody");

    tableBody.empty(); // Clear the table body.

    topProductsData.forEach(function (product) {
        var row = $("<tr>");
        row.append($("<td>").text(product.productName));
        row.append($("<td>").text(product.toplamSatis));

        tableBody.append(row);
    });
}
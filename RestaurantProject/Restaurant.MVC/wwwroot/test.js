﻿
$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7219/api/product/getproducts",
        type: "get",
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            console.log(err);
        }

    });


});
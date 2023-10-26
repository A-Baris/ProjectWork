//$(document).ready(function () {
//    // Event handler for form 1
//    $('#form1').submit(function (event) {
//        event.preventDefault(); // Prevent the default form submission
//        var formData = $(this).serialize();

//        // Send data to the first API endpoint
//        $.ajax({
//            url: 'https://localhost:7219/api/reservation/postreservation', // URL for the first form
//            type: 'POST',
//            data: formData,
//            success: function (data) {
//                // Handle the success response for the first form
//            },
//            error: function (err) {
//                // Handle errors for the first form
//            }
//        });
//    });

//    // Event handler for form 2
//    $('#form2').submit(function (event) {
//        event.preventDefault(); // Prevent the default form submission
//        var formData = $(this).serialize();

//        // Send data to the second API endpoint
//        $.ajax({
//            url: 'https://localhost:7219/api/reservation/postreservation', // URL for the second form
//            type: 'POST',
//            data: formData,
//            success: function (data) {
//                // Handle the success response for the second form
//            },
//            error: function (err) {
//                // Handle errors for the second form
//            }
//        });
//    });
//});


$(document).ready(function () {
    $('#submitButton').click(function () {
        var formData1 = $('#form1').serialize();
        var formData2 = $('#form2').serialize();

        // Combine the data from both forms
        var combinedData = formData1 + '&' + formData2;

        // Send the combined data to the desired endpoint
        $.ajax({
            url: 'https://localhost:7219/api/reservation/postreservation', // URL for the combined action
            type: 'POST',
            data: combinedData,
            success: function (data) {
                // Handle the success response for the combined form
            },
            error: function (err) {
                // Handle errors for the combined form
            }
        });
    });
});
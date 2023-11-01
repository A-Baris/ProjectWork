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
$('#form1').submit(function (event) {
    event.preventDefault(); // Prevent the default form submission

    var formData = $(this).serialize();
    // Send the form data to the API using a POST request
    $.ajax({
        url: 'https://localhost:7219/api/reservation/postreservation',
        type: 'POST',
        data: formData,
        success: function (data) {
            console.log('Reservation created successfully:', data);
            // Optionally, you can display a success message or redirect the user.
            $('#form1')[0].reset();
        },
        error: function (err) {
            console.error('Error creating reservation:', err);
            // Optionally, you can display an error message.
        }
    });
});
});

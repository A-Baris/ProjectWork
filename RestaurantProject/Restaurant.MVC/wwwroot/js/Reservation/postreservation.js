$(document).ready(function () {
    $('#form1').submit(function (event) {
        event.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: 'https://localhost:7219/api/reservation/postreservation',
            type: 'POST',
            data: formData,
            success: function (data) {
                console.log('Reservation created now:', data);
                $('#form1')[0].reset();
                var message = "Rezervasyonunuz Başarılı Şekilde Oluşturuldu";
                displaySuccessMessage(message);

               
                setTimeout(function () {
                    window.location.href = '/reservation/userreservation';
                }, 5000);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error creating reservation:', xhr);

                var errorMessage = "An error occurred while processing your request.";

                if (xhr.status === 400) {
                   
                        errorMessage = "Bad Request: The server could not understand the request.";
                    } else if (xhr.status === 401) {
                        errorMessage = "Unauthorized: You are not authorized to perform this action.";
                    } else if (xhr.status === 403) {
                        errorMessage = "Forbidden: Access to the requested resource is forbidden.";
                    } else if (xhr.status === 404) {
                        errorMessage = "Not Found: The requested resource was not found.";
                    } else if (xhr.status === 409) {
                        errorMessage = "Seçilen Tarihte Rezervasyonlarımız Dolu!";
                    } else if (xhr.status === 500) {
                        errorMessage = "Internal Server Error: An unexpected error occurred on the server.";
                    }

                console.log('Error message to be displayed:', errorMessage);
                displayErrorMessage(errorMessage);
            }
        });
    });

  
    function displaySuccessMessage(message) {

        $('#successMessageContainer').html('<div class="alert alert-success">' + message + '</div>');
        $('#successMessageContainer').show();
    }

   
    function displayErrorMessage(errorMessage) {
        
        $('#errorMessageContainer').html('<div class="alert alert-danger">' + errorMessage + '</div>');
        $('#errorMessageContainer').show();
    }
});
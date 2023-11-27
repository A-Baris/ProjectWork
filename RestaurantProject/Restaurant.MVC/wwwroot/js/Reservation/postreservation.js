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
                }, 10000);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Rezervasyon oluştururken Hata oluştu!', xhr);

                var errorMessage;

                if (xhr.status === 400) {

                    errorMessage = "Rezervasyon Tarihi geçmiş tarih veya gün içi tarihte gerçekleşemez.\n Gün içinde rezervasyonlar yalnız Telefon iletişimi ile yapılır.";
                } else if (xhr.status === 401) {
                    errorMessage = "Sayfa için yetkiniz yok";
                } else if (xhr.status === 409) {
                    errorMessage = "Seçilen Tarihte Rezervasyonlarımız Dolu!";
                } else if (xhr.status === 500) {
                    errorMessage = xhr.textStatus;
                }

                console.log('Error message to be displayed:', errorMessage);
                displayErrorMessage(errorMessage);
                setTimeout(function () {
                    window.location.href = '/reservation/userreservation';
                }, 15000);
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
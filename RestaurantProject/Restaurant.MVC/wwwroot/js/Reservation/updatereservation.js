$(document).ready(function () {
    $('#updateForm').submit(function (event) {
        event.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: 'https://localhost:7219/api/reservation/UpdateReservation',
            type: 'PUT',
            data: formData,
            success: function (data) {
                console.log('Reservation updated now:', data);
                $('#updateForm')[0].reset();
                var message = "İşlem Başarılı\n(Rezervasyonlarım'a Yönlendiriliyorsunuz)";

                displaySuccessMessage(message);

                setTimeout(function () {
                    window.location.href = 'https://localhost:7152/userprofile/myreservation';
                }, 4000);



            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Rezervasyon oluştururken Hata oluştu!', xhr);

                var errorMessage;

                if (xhr.status === 400) {
                    errorMessage = "Rezervasyon Tarihi geçmiş tarih veya gün içi tarihte gerçekleşemez.\n Gün içinde rezervasyonlar yalnız Telefon iletişimi ile yapılır.";
                } else if (xhr.status === 409) {
                    errorMessage = "Seçilen Tarihte Rezervasyonlarımız Dolu!";
                } else if (xhr.status === 500) {
                    errorMessage = "Sunucu Kaynaklı Hata";
                }

                console.log('Error message to be displayed:', errorMessage);
                displayErrorMessage(errorMessage);
            }
        });
    });

    function displayErrorMessage(errorMessage) {
        $('#errorMessageContainer').html('<div class="alert alert-danger">' + errorMessage + '</div>');
        $('#errorMessageContainer').show();

    }

    function displaySuccessMessage(message) {
        $('#successMessageContainer').html('<div class="alert alert-success">' + message + '</div>');
        $('#successMessageContainer').show();
    }
});
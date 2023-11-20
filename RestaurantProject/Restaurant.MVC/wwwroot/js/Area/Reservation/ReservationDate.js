$(document).ready(function () {
    $("#fetchReservations").click(function () {
        var selectedDate = $("#dateInput").val();

        $.ajax({
            url: "https://localhost:7219/api/reservation/GetReservationDate",
            type: "get",
            data: { date: selectedDate },
            success: function (data) {
                displayReservationData(data);
            },
            error: function (err) {
                console.log(err);
            }
        });
    });
});

function displayReservationData(reservationData) {
    var tableBody = $("#reservationTable tbody");

    tableBody.empty(); 

    reservationData.forEach(function (reservation) {
        var row = $("<tr>");
        row.append($("<td>").text(reservation.reservationDate));
        row.append($("<td>").text(reservation.reservationStatus));
        row.append($("<td>").text(reservation.name));
        row.append($("<td>").text(reservation.surname));
        row.append($("<td>").text(reservation.guestNumber));
        row.append($("<td>").text(reservation.description));
        tableBody.append(row);
    });
}
$(document).ready(function () {
    $("#fetchReservations").click(function () {
        var selectedDate = $("#dateInput").val();

        $.ajax({
            url: "https://localhost:7219/api/reservation/GetReservationDates",
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

    tableBody.empty(); // Clear the table body.

    reservationData.forEach(function (reservation) {
        var row = $("<tr>");
        row.append($("<td>").text(reservation.reservationDate));
        row.append($("<td>").text(reservation.reservationStatus.toString()));
        row.append($("<td>").text(reservation.name.toString()));
        row.append($("<td>").text(reservation.surname.toString()));
        row.append($("<td>").text(reservation.description));
        tableBody.append(row);
    });
}
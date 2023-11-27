$(document).ready(function () {
    getDebits();
});

function getDebits() {
    $.ajax({
        url: 'https://localhost:7219/api/Accounting/GetDebits',
        dataType: 'json',
        success: function (data) {
            displayDebits(data);
           
            console.log(data);
       
            $('#result').html(JSON.stringify(data));
        },
        error: function (error) {
         
            console.error(error);
        }
    });
}

function displayDebits(debitData) {
    var tableBody = $("#dataTable tbody");
    

    tableBody.empty(); 

    debitData.forEach(function (debit) {

        

        var row = $("<tr>");
        row.append($("<td>").text(debit.companyName));
        row.append($("<td>").text(debit.kalanborc));

        tableBody.append(row);

    });
 

}
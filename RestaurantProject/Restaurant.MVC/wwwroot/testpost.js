$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:7219/api/category/getcategories',
        type: 'GET',
        success: function (data) {
            console.log('Data received:', data);

            var categoryDropdown = $('#CategoryId');
            categoryDropdown.empty(); // Clear existing options

            $.each(data, function (index, category) {
                console.log('Adding option:', category.categoryName);
                categoryDropdown.append($('<option>', {
                    value: category.id,
                    text: category.categoryName
                }));
            });
        },
        error: function (err) {
            console.error('Error fetching data for the Category dropdown: ' + err);
        }
    });

    $('#productForm').submit(function (event) {
        event.preventDefault(); // Prevent the default form submission

        // Get the selected CategoryID
        var selectedCategoryId = $('#CategoryId option:selected').val();

        // Modify the form data to include the Category ID
        var formData = $(this).serialize() + '&CategoryId=' + selectedCategoryId;

        // Send the form data to the API using a POST request
        $.ajax({
            url: 'https://localhost:7219/api/product/postproduct',
            type: 'POST',
            data: formData,
            success: function (data) {
                console.log('Product created successfully:', data);
                // Optionally, you can display a success message or redirect the user.
                $('#productForm')[0].reset();
            },
            error: function (err) {
                console.error('Error creating product:', err);
                // Optionally, you can display an error message.
            }
        });
    });
});
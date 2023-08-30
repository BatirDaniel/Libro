$(document).ready(function () {

    //REGISTER users
    $('#registerUserForm').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: "/Identity/Create",
            type: "POST",
            data: formData,
            async: false,
            success: function (response) {
                if (response.toast) {
                    var svg = response.toast.svg;
                    var message = response.toast.message;

                    showToast(svg, message)
                }
            },
            error: function (xhr) {
                if (xhr.responseJSON && xhr.responseJSON.toast) {
                    var svg = xhr.responseJSON.toast.svg;
                    var message = xhr.responseJSON.toast.message;

                    showToast(svg, message)
                }
            }
        });
    })
})
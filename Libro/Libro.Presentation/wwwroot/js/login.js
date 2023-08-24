$(document).ready(function () {
    $('#signInForm').submit(function (event) {
        var formData = $(this).serialize();
        $.ajax({
            url: "/Auth/SignIn",
            type: "POST",
            data: formData,
            success: function (response) {
                if (response.redirectUrl) {
                    window.location.href = response.redirectUrl;
                }

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
        })
        event.preventDefault();
    });
});


$(document).ready(function () {
    var pathArray = window.location.pathname.split('/');
    var userId = pathArray[pathArray.length - 1];

    $.ajax({
        url: "/Identity/GetUserDetails/" + userId,
        type: "GET",
        dataType: "json",
        success: function (response) {
            $('#name').html(response.Name);
            $('#email').html(response.Email)
            $('#role').html(response.Role)
            $('#joined').html(response.Joined)
            $('#issuesAssigned').html(response.NumberOfIssuesAssigned)
            $('#issuesCreated').html(response.NumberOfIssuesAdded)
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
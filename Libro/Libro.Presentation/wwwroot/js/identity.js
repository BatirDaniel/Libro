$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/roles/GetAllRoles",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#IdUserType");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.userType));
                });
            }
        }
    });
});

$(document).ready(function () {
    $("#registerForm").validate()
});
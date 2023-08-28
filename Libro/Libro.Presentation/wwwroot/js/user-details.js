$(document).ready(function () {
    var pathArray = window.location.pathname.split('/');
    var userId = pathArray[pathArray.length - 1];

    //GET: /Identity/GetUserById/userId
    $.ajax({
        url: "/Identity/GetUserById/" + userId,
        type: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data, function (key, value) {
                var inputField = $("#" + key);
                if (inputField.length > 0) {
                    inputField.val(value);
                }

                $('#Firstname').val(data.Name.split(' ')[0])
                $('#Lastname').val(data.Name.split(' ')[1])
            });

            var role = data.Role;

            $.ajax({
                type: 'GET',
                url: '/roles/GetAllRoles',
                dataType: 'json',
                success: function (data) {
                    if (data && data.length > 0) {

                        var dropdown = $('#updateRole');
                        dropdown.empty();

                        if (role) {
                            dropdown.append($('<option></option>')
                                .attr('value', role.Id)
                                .text(role.Name));
                        }

                        data.forEach(function (item) {
                            if (item.Id !== role.Id) {
                                dropdown.append($('<option></option>')
                                    .attr('value', item.Id)
                                    .text(item.Name));
                            }
                        });
                    }
                }
            });

            $("#updateForm").validate({
                submitHandler: function (event) {
                    event.preventDefault();

                    var formData = $(this).serialize();

                    $.ajax({
                        url: '/Identity/Update',
                        type: "POST",
                        data: formData,
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
                }
            });

            //$("#updateForm input, #updateForm select").on("input change", function () {
            //    var isFormValid = $("#updateForm").valid();
            //    $("#submitButton").prop("disabled", !isFormValid);
            //});
        }
    });
})
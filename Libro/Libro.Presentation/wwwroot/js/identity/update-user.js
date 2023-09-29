function updateUserDetails() {
    var pathArray = window.location.pathname.split('/');
    var userId = pathArray[pathArray.length - 1];

    $('#checkStatus').change(function () {
        var isChecked = $(this).prop('checked');
        $('#textStatus').text(isChecked ? 'Enabled' : 'Disabled');
    });

    //GET: user by id
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

                $('#checkStatus').prop('checked', !data.IsArchieved);
                $('#textStatus').text(!data.IsArchieved ? 'Enable' : 'Disable');
            });

            var role = data.Role;

            //GET: roles
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

            //POST: update user
            $("#updateUserForm").submit(function (e) {
                e.preventDefault();

                var selectedRoleId = $("#updateRole").val();
                $("#RoleId").val(selectedRoleId);

                var formFields = $(this).serializeArray();

                for (var i = 0; i < formFields.length; i++) {
                    if (formFields[i].name === "IsArchieved") {
                        formFields[i].value = $('#checkStatus').prop('checked') ? 'false' : 'true';
                        break;
                    }
                }

                var formData = $.param(formFields);

                $.ajax({
                    url: '/Identity/Update',
                    type: "POST",
                    data: formData + '&Id=' + userId,
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
                });
            })
        }
    });
}
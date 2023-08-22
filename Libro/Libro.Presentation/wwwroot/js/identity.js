$(document).ready(function () {

    //GET: roles list
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

    //VALIDATION: register form validation
    $("#registerForm").validate({
        submitHandler: function (event) {
            event.preventDefault();

            var formData = $(this).serialize();
            var actionUrl = form.attr('action');

            $.ajax({
                url: actionUrl,
                type: "POST",
                data: formData,
                success: function (response) {
                    $("#staticModal").modal("hide");

                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'User has been saved',
                        showConfirmButton: false,
                        timer: 1500
                    })
                },
                error: function (error) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                    })
                }
            });
        }
    });

    //GET: users list
    var table = $('#usersTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        scrollX: true,
        "ajax": {
            "url": "/Identity/GetUsers",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "language": {
            "emptyTable": "No record found.",
            "processing":
                '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
        },
        "columns": [
            {
                "data": "id",
                title: "Id",
                name: "id",
                visible: false,
                searchable: false
            },
            {
                "data": "name",
                title: "Name",
                name: "name",
                autoWidth: true,
                visible: true,
                orderable: false,
                filter: false
            },
            {
                "data": "email",
                title: "Email",
                name: "email",
                autoWidth: true,
                visible: true,
                searchable: true
            },
            {
                "data": "username",
                title: "Username",
                name: "username",
                autoWidth: true,
                visible: true,
                searchable: true
            },
            {
                "data": "telephone",
                title: "Telephone",
                name: "telephone",
                autoWidth: true,
                "visible": true,
                "searchable": true
            },
            {
                "data": "role",
                title: "Role",
                name: "role",
                autoWidth: true,
                visible: true,
                orderable: false,
                filter: false
            },
            {
                "data": null,
                filter: false,
                innerHeight: "200px",
                innerWidth: "200px",
                orderable: false,
                "render": function (data, type, row) {
                    return `<button id="drop" class="dropdown-button text-gray-900 bg-gray-100 hover:bg-gray-200 focus:ring-4
                                focus:outline-none focus:ring-gray-100 font-medium rounded-lg text-sm px-5 py-2.5 text-center 
                                inline-flex items-center dark:focus:ring-gray-500" type="button">
                                <i class="bi bi-three-dots"></i>
                            </button>
                            `;
                }
            }
        ]
    });
    
    //CONTEXT MENU: options (edit, delete) for all users
    $(document).contextMenu({
        selector: '.dropdown-button',
        trigger: 'left',
        items: {
            "edit": {
                name: 'Edit',
                icon: 'edit',
                callback: function (key, options) {

                    let row = table.row(options.$trigger.closest("tr"));
                    let userId = row.data().id;

                    $.ajax({
                        url: "/Identity/GetUserById",
                        type: "GET",
                        data: { userId: userId },
                        dataType: "json",
                        success: function (data) {

                            var modal = document.getElementById("staticModalUpdate");
                            var backdrop = document.getElementById("backdrop");
                            modal.classList.remove("hidden")
                            backdrop.classList.remove("hidden")

                            $("#firstname").val(data.name.split(' ').at(0));
                            $("#lastname").val(data.name.split(' ').at(1));
                            $("#username").val(data.userName);
                            $("#email").val(data.email);
                            $("#password").val("");
                            $("#telephone").val(data.telephone);

                            //ROLE: Need to implement role from get response => Role manager

                            $('#cancelModal, #closeModal').on('click', function () { //Closing tailwind modal
                                modal.classList.add("hidden")
                                backdrop.classList.add("hidden")
                            })

                            $("#updateForm").validate({
                                submitHandler: function (event) {
                                    event.preventDefault();

                                    var formData = $(this).serialize();

                                    $.ajax({
                                        url: "/Identity/Update",
                                        type: "POST",
                                        data: formData,
                                        success: function (response) {

                                            Swal.fire({
                                                position: 'top-end',
                                                icon: 'success',
                                                title: 'User has been updated',
                                                showConfirmButton: false,
                                                timer: 1500
                                            })

                                            table.ajax.reload(); //TABLE will reload data
                                        },
                                        error: function (error) {

                                            Swal.fire({
                                                icon: 'error',
                                                title: 'Oops...',
                                                text: 'Something went wrong!',
                                            })
                                        }
                                    });
                                }
                            });
                        }
                    });
                }
            },
            "delete": {
                name: 'Delete',
                icon: 'delete',
                callback: function (key, options) {

                    let row = table.row(options.$trigger.closest("tr"));
                    let userId = row.data().id;

                    Swal.fire({
                        title: 'Are you sure?',
                        text: "You won't be able to revert this!",
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Confirm'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $.ajax({
                                url: `/Identity/Delete`,
                                type: "DELETE",
                                data: { userId: userId },
                                success: function (response) {
                                    Swal.fire(
                                        'Deleted!',
                                        'User has been deleted.',
                                        'success'
                                    )
                                    table.ajax.reload();  //TABLE will reload data
                                },
                                error: function () {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Oops...',
                                        text: 'Something went wrong!',
                                    })
                                }
                            });
                        }
                    })
                }
            },
        }
    });
});


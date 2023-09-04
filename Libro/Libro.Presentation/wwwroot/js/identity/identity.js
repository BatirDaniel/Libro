$(document).ready(function () {

    //GET: users
    var table = $('#usersTable').DataTable({
        "processing": true,
        "serverSide": true,
        scrollX: true,
        "ajax": {
            "url": "/Identity/GetUsers",
            "type": "POST",
            xhrFields: {
                withCredentials: true
            },
            "datatype": "json"
        },

        "language": {
            "emptyTable": "No record found.",
            "processing":
                '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
        },
        "columns": [
            { "data": "Id", title: "Id", name: "Id", visible: false, searchable: false, filter: false },
            { "data": "Username", title: "Username", name: "Username", autoWidth: true },
            { "data": "Email", title: "Email", name: "Email", autoWidth: true },
            { "data": "Name", title: "Name", name: "Name", autoWidth: true },
            { "data": "Telephone", title: "Telephone", name: "Telephone", autoWidth: true },
            { "data": "Role", title: "Role", name: "Role", autoWidth: true, visible: true, filter: false, orderable: false },
            {
                "data": "IsArchieved", title: "Status", name: "IsArchieved", autoWidth: true,
                "render": function (data, type, row) {
                    if (data === true || data === "true") {
                        return `<span class="px-4 py-1 ml-auto text-sm font-medium tracking-wide text-yellow-500 bg-yellow-100 rounded-full">
                                  Disabled
                                </span>`;
                    } else {
                        return `<span class="px-4 py-1 ml-auto text-sm font-medium tracking-wide text-green-600 bg-green-200 rounded-full">
                                  Enabled
                                </span>`;
                    }
                }
            },
            {
                "data": null, filter: false, innerHeight: "200px", innerWidth: "200px", orderable: false,
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

    table.ajax.reload();

    //CONTEXT MENU: options (edit, delete, details) for all users
    $(document).contextMenu({
        selector: '.dropdown-button',
        trigger: 'left',
        className: 'bg-white w-60 border border-gray-300 rounded-lg flex flex-col text-sm w-auto text-gray-500 shadow-lg',
        build: function ($triggerElement, e) {
            let row = table.row($triggerElement.closest("tr"));
            if (row) {
                return {
                    items: {
                        "edit": {
                            name: 'Edit',
                            className: 'text-sm',
                            icon: 'bi bi-pencil-square',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let userId = row.data().Id;

                                //GET: edit user page
                                $.ajax({
                                    url: '/user/edit/' + userId,
                                    success: function (response) {

                                        window.location.href = '/user/edit/' + userId;

                                        if (response.toast) {
                                            var svg = response.toast.svg;
                                            var message = response.toast.message;

                                            showToast(svg, message)
                                        }
                                    },
                                    error: function (xhr) {

                                        if (xhr.redirectUrl) {
                                            window.location.href = response.redirectUrl;
                                        }

                                        if (xhr.responseJSON && xhr.responseJSON.toast) {
                                            var svg = xhr.responseJSON.toast.svg;
                                            var message = xhr.responseJSON.toast.message;

                                            showToast(svg, message)
                                        }
                                    }
                                });
                            }
                        },
                        "delete": {
                            name: 'Delete',
                            className: 'text-sm',
                            icon: 'bi bi-person-dash',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let userId = row.data().Id;

                                Swal.fire({
                                    title: 'Are you sure?',
                                    text: "You won't be able to revert this!",
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#16a34a',
                                    cancelButtonColor: ' #c4c4c4',
                                    confirmButtonText: 'Confirm'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        $.ajax({
                                            url: `/Identity/Delete/` + userId,
                                            type: "DELETE",
                                            success: function (response) {
                                                if (response.toast) {
                                                    var svg = response.toast.svg;
                                                    var message = response.toast.message;

                                                    showToast(svg, message)
                                                }

                                                table.ajax.reload(); //Reloading table
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
                                })
                            }
                        },
                        "datails": {
                            name: 'Details',
                            className: 'text-sm',
                            icon: 'bi bi-info-circle',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let userId = row.data().Id;

                                //GET: details user page
                                $.ajax({
                                    url: '/user/details/' + userId,
                                    success: function (response) {

                                        window.location.href = '/user/details/' + userId;

                                        if (response.toast) {
                                            var svg = response.toast.svg;
                                            var message = response.toast.message;

                                            showToast(svg, message)
                                        }
                                    },
                                    error: function (xhr) {

                                        if (xhr.redirectUrl) {
                                            window.location.href = response.redirectUrl;
                                        }

                                        if (xhr.responseJSON && xhr.responseJSON.toast) {
                                            var svg = xhr.responseJSON.toast.svg;
                                            var message = xhr.responseJSON.toast.message;

                                            showToast(svg, message)
                                        }
                                    }
                                });
                            }
                        }
                    }
                }
            }
        }
    });

    $('#createUser').on('click', function () {
        //GET: roles
        $.ajax({
            type: 'GET',
            url: '/roles/GetAllRoles',
            dataType: 'json',
            success: function (data) {
                if (data && data.length > 0) {
                    var dropdown = $('#addRole');
                    dropdown.empty();

                    $.each(data, function (index, item) {
                        dropdown.append($('<option></option>')
                            .attr('value', item.Id)
                            .text(item.Name));
                    });
                }
            }
        });
    })

    //POST: create user
    $('#registerUserForm').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: "/Identity/Create",
            type: "POST",
            data: formData,
            async: false,
            success: function (response) {

                window.location.reload();

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
});

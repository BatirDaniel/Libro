$(document).ready(function () {

    //GET: roles list
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

    //VALIDATION: register form validation
    $("#registerForm").validate({
        submitHandler: function (event) {
            event.preventDefault();

            var formData = $(this).serialize();

            $.ajax({
                url: "/Identity/Create",
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

        "language": {
            "emptyTable": "No record found.",
            "processing":
                '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
        },
        "columns": [
            {
                "data": "Id",
                title: "Id",
                name: "Id",
                visible: false,
                searchable: false
            },
            {
                "data": "Name",
                title: "Name",
                name: "Name",
                autoWidth: true,
                visible: true,
                orderable: false,
                filter: false
            },
            {
                "data": "Email",
                title: "Email",
                name: "Email",
                autoWidth: true,
                visible: true,
                searchable: true
            },
            {
                "data": "Username",
                title: "Username",
                name: "Username",
                autoWidth: true,
                visible: true,
                searchable: true
            },
            {
                "data": "Telephone",
                title: "Telephone",
                name: "Telephone",
                autoWidth: true,
                "visible": true,
                "searchable": true
            },
            {
                "data": "IsArchieved",
                title: "Status",
                name: "IsArchieved",
                autoWidth: true,
                visible: true,
                orderable: false,
                filter: false,
                "render": function (data, type, row) {
                    if (data === true || data === "true") {
                        return `<span class="px-4 py-1 ml-auto text-sm font-medium tracking-wide text-yellow-500 bg-yellow-100 rounded-full">
                                  Disabled
                                </span>`;
                    } else{
                        return `<span class="px-4 py-1 ml-auto text-sm font-medium tracking-wide text-green-600 bg-green-200 rounded-full">
                                  Enabled
                                </span>`;
                    }
                }
            },
            {
                "data": "Role",
                title: "Role",
                name: "Role",
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
        className: 'bg-white w-60 border border-gray-300 rounded-lg flex flex-col text-sm w-auto text-gray-500 shadow-lg',
        build: function ($triggerElement, e) {
            let row = table.row($triggerElement.closest("tr"));
            if (row) {
                let isArchived = row.data().IsArchieved;
                let archiveName = isArchived ? "Enable" : "Disable";

                return {
                    items: {
                        "edit": {
                            name: 'Edit',
                            className: 'text-sm',
                            icon: 'bi bi-pencil-square',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let userId = row.data().Id;

                                window.location.href = '/details/' + userId;
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
                        "archive": {
                            name: archiveName,
                            className: 'text-sm',
                            icon: 'bi bi-archive-fill',
                            callback: function (key, options) {
                                let row = table.row(options.$trigger.closest("tr"));
                                let userId = row.data().Id;

                                $.ajax({
                                    url: "/Identity/UpdateStatusUser/" + userId,
                                    type: "POST",
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
                        }
                    }
                }
            }
        }
    });
});

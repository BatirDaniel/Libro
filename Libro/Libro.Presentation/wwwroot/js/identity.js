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
    $("#registerForm").validate();

    //GET: users list
    $('#usersTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        scrollX: true,
        "ajax": {
            "url": "/Identity/GetUsers",
            "type": "POST",
            "datatype": "json",
            "async": false
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

    $(document).contextMenu({
        selector: '.dropdown-button',
        trigger: 'left',
        callback: function (key, options) {
            var userId = $(this).data("user-id");
            var m = "clicked: " + key + " for user with ID: " + userId;
            window.console && console.log(m) || alert(m);
        },
        items: {
            "edit": { name: "Edit", icon: "edit" },
            "delete": { name: "Delete", icon: "delete" }
        }
    });
});
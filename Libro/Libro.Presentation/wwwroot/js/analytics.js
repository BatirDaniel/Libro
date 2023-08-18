$(document).ready(function () {
    $('#usersTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Identity/GetUsers",
            "type": "POST",
            "datatype": "json",
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
                title: "Fullname",
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
                orderable: false,
                "render": function (data, type, row) {
                    return `<button id="dropdownDefaultButton" aria-expanded="false" data-dropdown-toggle="dropdown" class="text-gray " type="button">
                                <i class="bi bi-three-dots"></i>
                            </button>
                            <div id="dropdown" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-44 dark:bg-gray-700">
                                <ul class="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
                                  <li>
                                    <i class="bi bi-pencil-square mr-3"></i>
                                    <a class="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Edit</a>
                                  </li>
                                  <li>
                                    <i class="bi bi-pencil-square mr-3"></i>
                                    <a class="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Delete</a>
                                  </li>
                                </ul>
                            </div>
                            <script src="https://cdnjs.cloudflare.com/ajax/libs/flowbite/1.8.0/flowbite.min.js"></script>`;
                }
            }
        ]
    });
});
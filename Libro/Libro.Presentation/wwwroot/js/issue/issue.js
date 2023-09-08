$(document).ready(function () {

    var table = $('#possTable').DataTable({
        "processing": true,
        "serverSide": true,
        scrollX: true,
        "ajax": {
            url: "/Issue/GetPOSsFromForIssues",
            type: "POST",
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
        columns: [
            { "data": "Id", title: "Id", name: "Id", visible: false, searchable: false, filter: false },
            { "data": "Name", title: "Name", name: "Name", autoWidth: true },
            { "data": "Telephone", title: "Telephone", name: "Telephone", autoWidth: true },
            { "data": "Cellphone", title: "Cellphone", name: "Cellphone", autoWidth: true },
            { "data": "Address", title: "Address", name: "Address", autoWidth: true },
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
    })

    //CONTEXT MENU: options (edit, delete, details) for all pos
    $(document).contextMenu({
        selector: '.dropdown-button',
        trigger: 'left',
        className: 'bg-white w-60 border border-gray-300 rounded-lg flex flex-col text-sm w-auto text-gray-500 shadow-lg',
        build: function ($triggerElement, e) {
            let row = table.row($triggerElement.closest("tr"));
            if (row) {
                let status = row.data().Status;

                return {
                    items: {
                        "edit": {
                            name: 'Create issue',
                            className: 'text-sm',
                            icon: 'bi bi-pencil-square',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let posId = row.data().Id;

                                //GET: create issue page
                                $.ajax({
                                    url: '/issue/create/' + posId,
                                    success: function (response) {

                                        window.location.href = '/issue/create/' + posId;

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
                        }, /// MUST TO DO
                        "details": {
                            name: 'Details',
                            className: 'text-sm',
                            icon: 'bi bi-info-circle',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let posId = row.data().Id;

                                $.ajax({
                                    url: '/pos/edit/' + posId,
                                    success: function (response) {

                                        window.location.href = '/pos/details/' + posId;

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
});
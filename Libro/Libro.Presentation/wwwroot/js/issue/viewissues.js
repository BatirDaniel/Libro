function getViewIssues() {
    var table = $('#issuestable').DataTable({
        "processing": true,
        "serverSide": true,
        scrollX: true,
        "ajax": {
            url: "/Issue/GetIssues",
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
            { "data": "POSName", title: "POS Name", name: "POSName", autoWidth: true },
            { "data": "CreatedBy", title: "Created By", name: "CreatedBy", autoWidth: true },
            {
                "data": "CreationDate", title: "Creation Date", name: "CreationDate", autoWidth: true,
                "render": function (data, type, row) {
                    return formatDate(data);
                }
            },
            { "data": "IssueType", title: "Issue type", name: "IssueType", autoWidth: true },
            { "data": "Status", title: "Status", name: "Status", autoWidth: true },
            { "data": "AssignedTo", title: "Assigned To", name: "AssignedTo", autoWidth: true },
            { "data": "Memo", title: "Memo", name: "Memo", autoWidth: true },
            { "data": "Priority", title: "Priority", name: "Priority", autoWidth: true },
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

    //custom date format
    function formatDate(inputDate) {
        const months = [
            'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
            'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'
        ];

        const dateParts = inputDate.split('T')[0].split('-');
        const year = parseInt(dateParts[0]);
        const month = parseInt(dateParts[1]);
        const day = parseInt(dateParts[2]);

        if (isNaN(year) || isNaN(month) || isNaN(day)) {
            return 'Invalid Date';
        }

        return `${day} ${months[month - 1]} ${year}`;
    }

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
                            name: 'Create log',
                            className: 'text-sm',
                            icon: 'bi bi-pencil-square',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let issueId = row.data().Id;
                                let url = '/log/create/' + issueId;

                                //GET: create issue page
                                $.ajax({
                                    url: url,
                                    success: function (response) {

                                        history.pushState(null, null, url);
                                        spaLoad(url, "GET", null, null);

                                        if (response.toast) {
                                            var svg = response.toast.svg;
                                            var message = response.toast.message;

                                            showToast(svg, message)
                                        }
                                    },
                                    error: function (xhr) {

                                        if (xhr.redirectUrl) {
                                            history.pushState(null, null, xhr.redirectUrl);
                                            spaLoad(xhr.redirectUrl, "GET", null, null);
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
                        /// MUST TO DO
                        "details": {
                            name: 'Details',
                            className: 'text-sm',
                            icon: 'bi bi-info-circle',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let issueId = row.data().Id;
                                let url = '/issue/details/' + issueId;

                                $.ajax({
                                    url: url,
                                    success: function (response) {

                                        history.pushState(null, null, url);
                                        spaLoad(url, "GET", null, null);

                                        if (response.toast) {
                                            var svg = response.toast.svg;
                                            var message = response.toast.message;

                                            showToast(svg, message)
                                        }
                                    },
                                    error: function (xhr) {

                                        if (xhr.redirectUrl) {
                                            history.pushState(null, null, xhr.redirectUrl);
                                            spaLoad(xhr.redirectUrl, "GET", null, null);
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
}
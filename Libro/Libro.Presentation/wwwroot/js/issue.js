$(document).ready(function () {

    //TABLE: issues


    //MUST TO DO :::::



    var table = $('#issuesTable').DataTable({
        "processing": true,
        "serverSide": true,
        scrollX: true,
        "ajax": {
            url: "/POS/GetPOSs",
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
            { "data": "Address", title: "Address", name: "Address", autoWidth: true },
            { "data": "Status", title: "Status", name: "Status", autoWidth: true },
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

    //$.ajax({
    //    type: "GET",
    //    url: "/issuetypes/GetAllIssuesTypes",
    //    dataType: "json",
    //    success: function (data) {
    //        if (data && data.length > 0) {
    //            var dropdown = $("#IdType");
    //            dropdown.empty();
    //            $.each(data, function (index, item) {
    //                dropdown.append($("<option></option>")
    //                    .attr("value", item.id)
    //                    .text(item.issuestypes));
    //            });
    //        }
    //    }
    //});

    //$.ajax({
    //    type: "GET",
    //    url: "/pos/GetAllPos",
    //    dataType: "json",
    //    success: function (data) {
    //        if (data && data.length > 0) {
    //            var dropdown = $("#IdPos");
    //            dropdown.empty();
    //            $.each(data, function (index, item) {
    //                dropdown.append($("<option></option>")
    //                    .attr("value", item.id)
    //                    .text(item.name));
    //            });
    //        }
    //    }
    //});
    //$.ajax({
    //    type: "GET",
    //    url: "/issuetypes/GetAllIssuesSubtypes",
    //    dataType: "json",
    //    success: function (data) {
    //        if (data && data.length > 0) {
    //            var dropdown = $("#IdStatus");
    //            dropdown.empty();
    //            $.each(data, function (index, item) {
    //                dropdown.append($("<option></option>")
    //                    .attr("value", item.id)
    //                    .text(item.status));
    //            });
    //        }
    //    }
    //});

    //$.ajax({
    //    type: "GET",
    //    url: "/status/GetAllStatuses",
    //    dataType: "json",
    //    success: function (data) {
    //        if (data && data.length > 0) {
    //            var dropdown = $("#IdStatus");
    //            dropdown.empty();
    //            $.each(data, function (index, item) {
    //                dropdown.append($("<option></option>")
    //                    .attr("value", item.id)
    //                    .text(item.status));
    //            });
    //        }
    //    }
    //});

    //$.ajax({
    //    type: "GET",
    //    url: "/Identity/GetEmployees",
    //    dataType: "json",
    //    success: function (data) {
    //        if (data && data.length > 0) {
    //            var dropdown = $("#IdAssigned");
    //            dropdown.empty();
    //            $.each(data, function (index, item) {
    //                dropdown.append($("<option></option>")
    //                    .attr("value", item.id)
    //                    .text(item.name));
    //            });
    //        }
    //    }
    //});
});
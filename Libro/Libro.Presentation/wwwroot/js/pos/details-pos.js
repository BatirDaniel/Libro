$(document).ready(function () {
    var pathArray = window.location.pathname.split('/');
    var posId = pathArray[pathArray.length - 1];

    //GET: pos details
    $.ajax({
        url: "/POS/GetPOSDetails/" + posId,
        type: "GET",
        dataType: "json",
        success: function (response) {
            $('#name').html(response.Name);
            $('#telephone').html(response.Telephone);
            $('#address').html(response.Address);
            $('#city').html(response.City.Name);
            $('#model').html(response.Model);
            $('#brand').html(response.Brand);
            $('#connectionType').html(response.ConnectionType.Type);
            $('#morningOpening').html(convertTo12HourFormat(response.MorningOpening));
            $('#morningClosing').html(convertTo12HourFormat(response.MorningClosing));
            $('#afternoonOpening').html(convertTo12HourFormat(response.AfternoonOpening));
            $('#afternoonClosing').html(convertTo12HourFormat(response.AfternoonClosing));
            $('#insertionDate').html(formatDate(response.InserDate));
        }
    });

    $('#issuesTable').DataTable({
        "processing": true,
        "serverSide": true,
        "scrollX": true,
        "ajax": {
            url: "/POS/GetIssuesOfThePOS/" + posId,
            type: "POST",
            data: function (d) {
                d.search.value = $('#searchBox').val();
            }
        },
        "language": {
            "emptyTable": "No record found.",
            "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span>'
        },
        "columns": [
            { "data": "Id", "title": "Id", "name": "Id", "visible": false, "searchable": false, "orderable": false },
            { "data": "POSName.Name", "title": "POS Name", "name": "POSName.Name", "autoWidth": true },
            { "data": "CreatedBy.Name", "title": "Created by", "name": "CreatedBy.Name", "autoWidth": true },
            { "data": "DateCreated", "title": "Date", "name": "DateCreated", "autoWidth": true },
            { "data": "IssueType.Name", "title": "Type", "name": "IssueType.Name", "autoWidth": true },
            { "data": "Status.Status_Name", "title": "Status", "name": "Status.Status_Name", "autoWidth": true },
            { "data": "AssignedTo.Name", "title": "Assigned to", "name": "AssignedTo.Name", "autoWidth": true },
            { "data": "Memo", "title": "Memo", "name": "Memo", "autoWidth": true },
            {
                "data": null, "filter": false, "innerHeight": "200px", "innerWidth": "200px", "orderable": false,
                "render": function (data, type, row) {
                    return `<button id="drop" class="dropdown-button text-gray-900 bg-gray-100 hover:bg-gray-200 focus:ring-4 focus:outline-none focus:ring-gray-100 font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center dark:focus:ring-gray-500" type="button"><i class="bi bi-three-dots"></i></button>`;
                }
            }
        ],
        "order": [[3, 'desc']],
        "searching": true
    });
})

function convertTo12HourFormat(time) {
    const [hour, minute] = time.split(':');
    let period = 'AM';
    let hour12 = parseInt(hour);

    if (hour12 >= 12) {
        period = 'PM';
        if (hour12 > 12) {
            hour12 -= 12;
        }
    }

    return `${hour12}:${minute} ${period}`;
}

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
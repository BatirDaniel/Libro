 function createIssue() {
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
            $('#insertionDate').html(formatDate(response.InserDate));
        }
    });

    //GET: issue types
    $.ajax({
        type: "GET",
        url: "/IssuesTypes/GetAllIssueTypes",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#IssueType");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.Id)
                        .text(item.Name));
                });
            }
        }
    });

    //GET: statuses
    $.ajax({
        type: "GET",
        url: "/Status/GetAllStatuses",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#Status");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.Id)
                        .text(item.Type));
                });
            }
        }
    });

    //GET: priorities
    $.ajax({
        type: "GET",
        url: "/Issue/GetAllPriorities",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#Priority");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.Id)
                        .text(item.Name));
                });
            }
        }
    });

    //GET: workers by function
    $.ajax({
        type: "GET",
        url: "/Issue/GetAllUsersForIssue",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#UsersAssigned");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.Id)
                        .text(item.Name));
                });
            }
        }
    });

    $('#issueForm').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: "/Issue/Create",
            type: "POST",
            data: formData + "&IdPos=" + posId,
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
    });
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
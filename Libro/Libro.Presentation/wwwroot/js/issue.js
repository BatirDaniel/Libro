$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/issuetypes/GetAllIssuesTypes",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#IdType");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.issuestypes));
                });
            }
        }
    });

    $.ajax({
        type: "GET",
        url: "/pos/GetAllPos",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#IdPos");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.name));
                });
            }
        }
    });
    $.ajax({
        type: "GET",
        url: "/issuetypes/GetAllIssuesSubtypes",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#IdStatus");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.status));
                });
            }
        }
    });

    $.ajax({
        type: "GET",
        url: "/status/GetAllStatuses",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#IdStatus");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.status));
                });
            }
        }
    });

    $.ajax({
        type: "GET",
        url: "/Identity/GetEmployees",
        dataType: "json",
        success: function (data) {
            if (data && data.length > 0) {
                var dropdown = $("#IdAssigned");
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.name));
                });
            }
        }
    });

    $("#issueForm").validate();
});
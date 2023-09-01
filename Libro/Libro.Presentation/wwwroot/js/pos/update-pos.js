$(document).ready(function () {
    var pathArray = window.location.pathname.split('/');
    var posId = pathArray[pathArray.length - 1];

    //GET: get POS by id
    $.ajax({
        url: "/POS/GetPOSById/" + posId,
        type: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data, function (key, value) {
                var inputField = $("#" + key);
                if (inputField.length > 0) {
                    inputField.val(value);
                }
            });

            var city = data.City;
            var connectionType = data.ConnectionType;

            //GET: connection types
            $.ajax({
                type: 'GET',
                url: '/ConnectionTypes/GetConnectionTypes',
                dataType: 'json',
                success: function (data) {
                    if (data && data.length > 0) {

                        var dropdown = $('#updateConnectionType');
                        dropdown.empty();
                        data.forEach(function (item) {
                            if (connectionType.Id === item.Id) {
                                dropdown.append($('<option></option>')
                                    .attr('value', item.Id)
                                    .text(item.Type));
                            }
                        })

                        data.forEach(function (item) {
                            if (item.Id !== connectionType.Id) {
                                dropdown.append($('<option></option>')
                                    .attr('value', item.Id)
                                    .text(item.Type));
                            }
                        });
                    }
                }
            });

            //GET: cities
            $.ajax({
                type: 'GET',
                url: '/Cities/GetCities',
                dataType: 'json',
                success: function (data) {
                    if (data && data.length > 0) {

                        var dropdown = $('#updateCity');
                        dropdown.empty();

                        data.forEach(function (item) {
                            if (city.Id === item.Id) {
                                dropdown.append($('<option></option>')
                                    .attr('value', item.Id)
                                    .text(item.Name));
                            }
                        })

                        data.forEach(function (item) {
                            if (item.Id !== city.Id) {
                                dropdown.append($('<option></option>')
                                    .attr('value', item.Id)
                                    .text(item.Name));
                            }
                        });
                    }
                }
            });

            //POST: update POS
            $('#updatePosForm').submit(function (e) {
                e.preventDefault();

                var formData = $(this).serialize();

                $.ajax({
                    url: '/POS/Update',
                    type: "POST",
                    data: formData + '&Id=' + posId,
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
    });
});
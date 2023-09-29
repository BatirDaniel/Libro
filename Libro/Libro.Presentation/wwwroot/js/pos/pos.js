function getPOSs() {
    //TABLE: POSs
    var table = $('#possTable').DataTable({
        "processing": true,
        "serverSide": true,
        scrollX: true,
        "ajax": {
            url: "/POS/GetPOSs",
            type: "POST",
            datatype: "json",
            xhrFields: {
                withCredentials: true
            }
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
            {
                "data": "Status", title: "Status", name: "Status", autoWidth: true, orderable: false,
                "render": function (data, type, row) {
                    if (data != 0) {
                        return `<span class="px-4 py-1 ml-auto text-sm font-medium tracking-wide text-red-500 bg-red-100 rounded-full">`
                            + data + ` active issues
                                </span>`;
                    } else {
                        return `<span class="px-4 py-1 ml-auto text-sm font-medium tracking-wide text-green-600 bg-green-200 rounded-full">
                                  No issues
                                </span>`;
                    }
                }
            },
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
    });

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
                            name: 'Edit',
                            className: 'text-sm',
                            icon: 'bi bi-pencil-square',
                            callback: function (key, options) {
                                let row = table.row(options.$trigger.closest("tr"));
                                let posId = row.data().Id;
                                let url = '/pos/edit/' + posId;

                                //GET: edit pos page
                                $.ajax({
                                    url: url,
                                    success: function (response) {

                                        history.replaceState(null, null, url);
                                        spaLoad(url, "GET", null, null);

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
                        },
                        "delete": {
                            name: 'Delete',
                            className: 'text-sm',
                            icon: 'bi bi-trash3-fill',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let posId = row.data().Id;

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

                                        //DELETE: pos
                                        $.ajax({
                                            url: `/POS/Delete/` + posId,
                                            type: "DELETE",
                                            success: function (response) {
                                                if (response.toast) {
                                                    var svg = response.toast.svg;
                                                    var message = response.toast.message;

                                                    showToast(svg, message)
                                                }

                                                table.ajax.reload(); //Reloading table
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
                        "details": {
                            name: 'Details',
                            className: 'text-sm',
                            icon: 'bi bi-info-circle',
                            callback: function (key, options) {

                                let row = table.row(options.$trigger.closest("tr"));
                                let posId = row.data().Id;
                                let url = '/pos/details/' + posId;

                                $.ajax({
                                    url: url,
                                    success: function (response) {
                                        history.replaceState(null, null, url);
                                        spaLoad(url, "GET", null, null);

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
}

function createPOS() {
    $('#createPOS').on('click', function () {
        //GET: cities
        $.ajax({
            type: 'GET',
            url: '/Cities/GetCities',
            datatype: 'json',
            success: function (data) {
                if (data && data.length > 0) {
                    var dropdown = $('#addCity');
                    dropdown.empty();

                    $.each(data, function (index, item) {
                        dropdown.append($('<option></option>')
                            .attr('value', item.Id)
                            .text(item.Name));
                    });
                }
            }
        });

        //GET: connections types
        $.ajax({
            type: 'GET',
            url: '/ConnectionTypes/GetConnectionTypes',
            datatype: 'json',
            success: function (data) {
                if (data && data.length > 0) {
                    var dropdown = $('#addConnectionType');
                    dropdown.empty();

                    $.each(data, function (index, item) {
                        dropdown.append($('<option></option>')
                            .attr('value', item.Id)
                            .text(item.Type));
                    });
                }
            }
        });
    });

    //POST: create pos
    $('#createPosForm').submit(function (e) {
        e.preventDefault();

        var selectedDays = [];
        $('input[name="DaysClosed"]:checked').each(function () {
            selectedDays.push($(this).val());
        });

        var daysClosed = selectedDays.join(' ');

        var formData = $(this).serialize();
        formData = formData.replace(/(^|&)DaysClosed=[^&]*/g, '');

        $.ajax({
            url: "/Pos/Create",
            type: "POST",
            data: formData + '&DaysClosed=' + daysClosed,
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
    });
}
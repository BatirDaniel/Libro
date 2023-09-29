function getIssueDetails() {
    var pathArray = window.location.pathname.split('/');
    var id = pathArray[pathArray.length - 1];

    //GET: pos details
    $.ajax({
        url: "/POS/GetPOSDetailsByIssue/" + id,
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
}
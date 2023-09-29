function getUserDetails() {
    var pathArray = window.location.pathname.split('/');
    var userId = pathArray[pathArray.length - 1];

    //GET: user details
    $.ajax({
        url: "/Identity/GetUserDetails/" + userId,
        type: "GET",
        dataType: "json",
        success: function (response) {
            $('#name').html(response.Name)
            $('#nameP').html(response.Name)
            $('#email').html(response.Email)
            $('#role').html(response.Role)
            $('#roleP').html(response.Role)
            $('#joined').html(response.Joined)
            $('#issuesAssigned').html(response.NumberOfIssuesAssigned)
            $('#issuesCreated').html(response.NumberOfIssuesAdded)
        }
    });
}
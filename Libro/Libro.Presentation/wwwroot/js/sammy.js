import Sammy from '../../node_modules/s'

var app = Sammy(function () {
    $.get('', function (context) {
        $.ajax({
            url: '', // Ruta trebuie să fie aceeași ca în $.get
            type: 'GET',
            success: function (data) {
                $('#main').html(data);
            },
        });
    });

    $.get('/issue/create/:posId', function (context) {
        $.ajax({
            url: '/Issue/CreateIssue/' + context.params.posId,
            type: 'GET',
            success: function (data) {
                $('#main').html(data);
            },
        });
    });

    $.get('#/issue', function (context) {
        $.ajax({
            url: '/Issue/Issue',
            type: 'GET',
            success: function (data) {
                $('#main').html(data);
            },
        });
    });

    $.get('#/pos', function (context) {
        $.ajax({
            url: '/Pos/Pos',
            type: 'GET',
            success: function (data) {
                $('#main').html(data);
            },
        });
    });

    this.run('#/');
})

$('#pageclick').on('click', function () {
    app.runRoute('get', '#/pos');
})

$("#pageclick").on('click', function () {
    $.ajax({
        url: '@Url.Action("Pos", "Pos")',
        type: 'GET',
        dataType: 'html',
        success: function (data) {
            $("#main-content").html(data);
        },
        error: function () {
            alert('Eroare la încărcarea conținutului.');
        }
    });
});
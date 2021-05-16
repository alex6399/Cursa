$(document).ready(function () {
    var table = $("#userDatatable").DataTable({
        initComplete: function () {
            this.api().columns().every(function () {
                var that = this;

                $('input', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        },
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Users/GetUsers",
            "type": "POST",
            "datatype": "json"
        },
        "language": {
            "url": '../language/Russian.json'
        },
        "search": {
            "caseInsensitive": false
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [4],
                "orderable": false
            }
        ],
        "columns": [
            {"data": "id", "name": "Id", "autoWidth": true},
            {"data": "fullName", "name": "FullName", "autoWidth": true},
            {"data": "email", "name": "Email", "autoWidth": true},
            {"data": "phoneNumber", "name": "PhoneNumber", "autoWidth": true},
            {
                "render": function (data, type, row) {
                    return '<a href="/Users/Details/' + row.id + '"  >\n' +
                        '<svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="currentColor" className="bi bi-person-lines-fill"\n' +
                        '     viewBox="0 0 16 16">\n' +
                        '    <path\n' +
                        '        d="M6 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5 6s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H1zM11 3.5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1h-4a.5.5 0 0 1-.5-.5zm.5 2.5a.5.5 0 0 0 0 1h4a.5.5 0 0 0 0-1h-4zm2 3a.5.5 0 0 0 0 1h2a.5.5 0 0 0 0-1h-2zm0 3a.5.5 0 0 0 0 1h2a.5.5 0 0 0 0-1h-2z"/>\n' +
                        '</svg> </a>';
                }
            }
        ]
    });
    $('#userDatatable tfoot th.search').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Поиск" class="form-control form-control-sm" />');
    });
});



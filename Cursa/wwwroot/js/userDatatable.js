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
            }
        ],
        "columns": [
            {"data": "id", "name": "Id", "autoWidth": true},
            {"data": "fullName", "name": "FullName", "autoWidth": true},
            {"data": "email", "name": "Email", "autoWidth": true},
            {"data": "phoneNumber", "name": "PhoneNumber", "autoWidth": true}
        ]
    });
    $('#userDatatable tfoot th.search').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Поиск" class="form-control form-control-sm" />');
    });
});



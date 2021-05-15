$(document).ready(function () {
    var table = $("#employeeDatatable").DataTable({
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
            "url": "/Employees/GetEmployee",
            "type": "POST",
            "datatype": "json"
            ,
            "data":{
                "projectId":projectId
            }
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
            {"data": "phone", "name": "Phone", "autoWidth": true},
            {"data": "departmentName", "name": "DepartmentName", "autoWidth": true}
        ]
    });
    $('#employeeDatatable tfoot th.search').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Поиск" class="form-control form-control-sm" />');
    });
    //TODO нужно представить данные в виде ссылок
    
    //TODO узнать про кэш VS,Rider
    
    // TODO узнать про добавление миграций из Rider


    // $('#employeeDatatable thead tr').clone(true).appendTo( '#employeeDatatable thead' );
    // $('#employeeDatatable thead tr:eq(1) th').each( function (i) {
    //     var title = $(this).text();
    //     $(this).html( '<input type="text" placeholder="Search '+title+'" />' );});
});

let projectId = 21;

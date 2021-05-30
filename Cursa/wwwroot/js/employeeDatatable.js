$(document).ready(function () {
    var token = $('input[name="__RequestVerificationToken"]').val();
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
            "data": {
                "__RequestVerificationToken": token
            }
        },
        "language": {
            "url": '/language/Russian.json'
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
            {"data": "phone", "name": "Phone", "autoWidth": true},
            {"data": "departmentName", "name": "DepartmentName", "autoWidth": true},
            {
                "render": function (data, type, row) {
                    return '<a href="/Employees/Details/' + row.id + '"  >\n' +
                        '<svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="currentColor" className="bi bi-person-lines-fill"\n' +
                        '     viewBox="0 0 16 16">\n' +
                        '    <path\n' +
                        '        d="M6 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5 6s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H1zM11 3.5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1h-4a.5.5 0 0 1-.5-.5zm.5 2.5a.5.5 0 0 0 0 1h4a.5.5 0 0 0 0-1h-4zm2 3a.5.5 0 0 0 0 1h2a.5.5 0 0 0 0-1h-2zm0 3a.5.5 0 0 0 0 1h2a.5.5 0 0 0 0-1h-2z"/>\n' +
                        '</svg> </a>';
                }
            }
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



// let link="<div class=\"btn-group\">\n" +
//     "  <button type=\"button\" class=\"btn btn-danger dropdown-toggle\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n" +
//     "    Action\n" +
//     "  </button>\n" +
//     "  <div class=\"dropdown-menu\">\n" +
//     "    <a class=\"dropdown-item\" href=\"#\">Action</a>\n" +
//     "    <a class=\"dropdown-item\" href=\"#\">Another action</a>\n" +
//     "    <a class=\"dropdown-item\" href=\"#\">Something else here</a>\n" +
//     "    <div class=\"dropdown-divider\"></div>\n" +
//     "    <a class=\"dropdown-item\" href=\"#\">Separated link</a>\n" +
//     "  </div>\n" +
//     "</div>";
//
// let a='<a href="/Employees/Details/' + row.id + '"  >\n' +
// '<svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="currentColor" className="bi bi-person-lines-fill"\n' +
// '     viewBox="0 0 16 16">\n' +
// '    <path\n' +
// '        d="M6 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm-5 6s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H1zM11 3.5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1h-4a.5.5 0 0 1-.5-.5zm.5 2.5a.5.5 0 0 0 0 1h4a.5.5 0 0 0 0-1h-4zm2 3a.5.5 0 0 0 0 1h2a.5.5 0 0 0 0-1h-2zm0 3a.5.5 0 0 0 0 1h2a.5.5 0 0 0 0-1h-2z"/>\n' +
// '</svg> </a>      <a href="/Employees/Edit?id='+ row.id+'"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil" viewBox="0 0 16 16">\n' +
// '  <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z"/>\n' +
// '</svg></a>'
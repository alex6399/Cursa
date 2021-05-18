// $(document).ready(function () {
//     $('#subprojectDatatable').dataTable({
//     });
// });
$(document).ready(function () {
    let table = $('#subprojectDatatable').DataTable({
        "scrollXInner": true,
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
            "url": "/SubProjects/FindSubProjects",
            "type": "POST",
            "datatype": "json",
            "data": {
                "projectId": projectId
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
            },
            {
                "targets": [1,8],
                "orderable": false
            }
        ],
        "columns": [
            {"data": "id", "name": "Id", "autoWidth": true},
            {
                "data": "projectName", "name": "ProjectName", "autoWidth": true
            },
            {
                "data": "name", "name": "Name", "autoWidth": true,
                "render": function (data, type, row) {
                    if (type === "display" || type === "filter") {
                        return '<a href="/Products/GetProductsForSubProject?subProjectId=' +// TODO добавить ссылку на документы
                            row.id +
                            '" >' +
                            row.name +
                            "</a>";
                    }
                    return data;
                }
            },
            {"data": "code", "name": "Code", "autoWidth": true},
            {"data": "employee.fullName", "name": "employee.fullName", "width": "250px"},
            {"data": "statusName", "name": "StatusName", "autoWidth": true},
            {
                "data": "contract", "name": "contract", "autoWidth": true
                // ,
                // // "defaultContent":"Данные не указаны",
                // "render": function (data, type, row) {
                //     if (data != null) {
                //         if (type === "display" || type === "filter") {
                //             return '<a href="/SubProjects/GetSubProject?projectId=' +// TODO почему-то работал и через слэш GetSubProjectById/projectId
                //                 row.id +
                //                 '" >' +
                //                 row.contract.name +
                //                 "</a>";
                //         }
                //         return data;
                //     }
                //     if (!data) {
                //         return "Не указано";
                //     }
                // }
            },
            {"data": "description", "name": "Description", "autoWidth": true},
            {
                "render": function (data, type, row) {
                    return '<a href="/SubProjects/Details/' + row.id + '"  ><svg xmlns="http://www.w3.org/2000/svg" width="27" height="27" fill="currentColor" class="bi bi-card-checklist" viewBox="0 0 21 21">\n' +
                        '  <path d="M14.5 3a.5.5 0 0 1 .5.5v9a.5.5 0 0 1-.5.5h-13a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h13zm-13-1A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h13a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 14.5 2h-13z"/>\n' +
                        '  <path d="M7 5.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 1 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0zM7 9.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 0 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0z"/>\n' +
                        '</svg></a>';
                }
            }
        ]
    });
    $('#subprojectDatatable tfoot th.search').each(function () {
        let title = $(this).text();
        $(this).html('<input type="text" placeholder="Поиск" class="form-control form-control-sm" />');
    });
});

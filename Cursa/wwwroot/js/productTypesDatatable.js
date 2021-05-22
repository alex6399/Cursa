﻿$(document).ready(function () {
    $('#productTypesDatatable').DataTable({


        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/ProductTypes/GetProductTypes",
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
            ,
            {
                "targets": [3],
                "orderable": false
            }
        ],
        "columns": [
            {"data": "id", "name": "Id", "autoWidth": true},
            {
                "data": "name", "name": "Name", "autoWidth": true
            },

            {
                "data": "productTypeName", "name": "productTypeName", "autoWidth": true
            },
            {
                "render": function (data, type, row) {
                    return '<a href="/ProductTypes/Details/' + row.id + '"  ><svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="currentColor" class="bi bi-card-checklist" viewBox="0 0 16 16">\n' +
                        '  <path d="M14.5 3a.5.5 0 0 1 .5.5v9a.5.5 0 0 1-.5.5h-13a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h13zm-13-1A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h13a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 14.5 2h-13z"/>\n' +
                        '  <path d="M7 5.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 1 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0zM7 9.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 0 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0z"/>\n' +
                        '</svg></a>';
                }
            }
        ]
    });
});

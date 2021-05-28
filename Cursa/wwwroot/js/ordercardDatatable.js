// $(document).ready(function () {
//     $('#orderCardDatatable').dataTable({
//     });
// });
$(document).ready(function () {
    $('#ordercardDatatable').DataTable({
        // initComplete: function () {
        //     this.api().columns().every(function () {
        //         var that = this;
        //
        //         $('input', this.footer()).on('keyup change clear', function () {
        //             if (that.search() !== this.value) {
        //                 that
        //                     .search(this.value)
        //                     .draw();
        //             }
        //         });
        //     });
        // },

        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/OrderCards/FindCardOrdersForProduct",
            "type": "POST",
            "datatype": "json"
            ,
            "data": {
                "productId": productId
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
            }
            ,
            {
                "targets": [5],
                "orderable": false
            }
        ],
        "columns": [
            {"data": "id", "name": "Id", "autoWidth": true},
            {
                "data": "product.name", "name": "product.name", "autoWidth": true
            },
            {
                "data": "name", "name": "Name", "autoWidth": true,
                "render": function (data, type, row) {
                    if (type === "display" || type === "filter") {
                        return '<a href="/Modules/GetModulesForCardOrder?cardOrderId=' +
                            row.id +
                            '" >' +
                            row.name +
                            "</a>";
                    }
                    return data;
                }
            },
            {
                "data": "number", "name": "Number", "autoWidth": true
            },
            {
                "data": "employeeName", "name": "EmployeeName", "autoWidth": true
            },
            {
                "render": function (data, type, row) {
                    return '<a href="/OrderCards/Details/' + row.id + '"  ><svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="currentColor" class="bi bi-card-checklist" viewBox="0 0 16 16">\n' +
                        '  <path d="M14.5 3a.5.5 0 0 1 .5.5v9a.5.5 0 0 1-.5.5h-13a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h13zm-13-1A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h13a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 14.5 2h-13z"/>\n' +
                        '  <path d="M7 5.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 1 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0zM7 9.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 0 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0z"/>\n' +
                        '</svg></a>';
                }
            }
            // {"data": "serialNum", "name": "SerialNum", "autoWidth": true}
            // {"data": "certifiedNum", "name": "CertifiedNum", "width": "250px"},
            // {"data": "orderDate", "name": "OrderDate", "autoWidth": true},
            // {
            //     "data": "contract.name", "name": "contract.name", "autoWidth": true,
            //     // "defaultContent":"Данные не указаны",
            //     "render": function (data, type, row) {
            //         if (data != null) {
            //             if (type === "display" || type === "filter") {
            //                 return '<a href="/SubProjects/GetSubProject?projectId=' +// TODO почему-то работал и через слэш GetSubProjectById/projectId
            //                     row.id +
            //                     '" >' +
            //                     row.contract.name +
            //                     "</a>";
            //             }
            //             return data;
            //         }
            //         if (!data) {
            //             return "Не указано";
            //         }
            //     }
            // },
            // {"data": "manufacturingDate", "name": "ManufacturingDate", "autoWidth": true},
            // {"data": "shippedDate", "name": "ShippedDate", "autoWidth": true},
            // {
            //     "render": function (data, type, row) {
            //         return '<a href="/Products/Details/' + row.id + '"  ><svg xmlns="http://www.w3.org/2000/svg" width="27" height="27" fill="currentColor" class="bi bi-card-checklist" viewBox="0 0 16 16">\n' +
            //             '  <path d="M14.5 3a.5.5 0 0 1 .5.5v9a.5.5 0 0 1-.5.5h-13a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h13zm-13-1A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h13a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 14.5 2h-13z"/>\n' +
            //             '  <path d="M7 5.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 1 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0zM7 9.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 0 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0z"/>\n' +
            //             '</svg></a>';
            //     }
            // }
        ]
    });
});

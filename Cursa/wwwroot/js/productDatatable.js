// $(document).ready(function () {
//     $('#subprojectDatatable').dataTable({
//     });
// });
$(document).ready(function () {
    let table = $('#productDatatable').DataTable({
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
            "url": "/Products/FindProductsForSubproject",
            "type": "POST",
            "datatype": "json"
            ,
            "data": {
                "subProjectId": subProjectId
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
                "targets": [1, 8],
                "orderable": false
            }
        ],
        "columns": [
            {"data": "id", "name": "Id", "autoWidth": true},
            {
                "data": "subProject.name", "name": "subProject.name", "autoWidth": true
            },
            {
                "data": "name", "name": "Name", "autoWidth": true,
                "render": function (data, type, row) {
                    if (type === "display" || type === "filter") {
                        return '<a href="/OrderCards/GetOrderCardsForProduct?productId=' +
                            row.id +
                            '" >' +
                            row.name +
                            "</a>";
                    }
                    return data;
                }
            },
            {"data": "serialNum", "name": "SerialNum", "autoWidth": true},
            {"data": "certifiedNum", "name": "CertifiedNum", "width": "250px"},
            {
                "data": "orderDate", "name": "OrderDate", "autoWidth": true,
                "render": function (data, type, row) {
                    if (data === null) {
                        return "<span class=\"empty\">*Не указано</span>";
                    }
                    if (type === "display") {
                        return moment(Date.parse(row.orderDate)).format('DD.MM.YYYY'); //DD.MM.YYYY HH:mm
                    }
                    return data;
                }
            },
            {
                "data": "manufacturingDate", "name": "ManufacturingDate", "autoWidth": true,
                "render": function (data, type, row) {
                    if (data === null) {
                        return "<span class=\"empty\">*Не изготовлено</span>";
                    }
                    if (type === "display") {
                        return moment(Date.parse(row.manufacturingDate)).format('DD.MM.YYYY'); //DD.MM.YYYY HH:mm
                    }
                    return data;
                }
            },
            {
                "data": "shippedDate", "name": "ShippedDate", "autoWidth": true,
                "render": function (data, type, row) {
                    if (type === "display") {
                        if (data === null) {
                            return "<span class=\"empty\">*Не отгружено</span>";
                        }
                        return moment(Date.parse(row.shippedDate)).format('DD.MM.YYYY'); //DD.MM.YYYY HH:mm
                    }
                    return data;
                }
            },
            {
                "render": function (data, type, row) {
                    return '<a href="/Products/Details/' + row.id + '"  ><svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="currentColor" class="bi bi-card-checklist" viewBox="0 0 16 16">\n' +
                        '  <path d="M14.5 3a.5.5 0 0 1 .5.5v9a.5.5 0 0 1-.5.5h-13a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h13zm-13-1A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h13a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 14.5 2h-13z"/>\n' +
                        '  <path d="M7 5.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 1 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0zM7 9.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5zm-1.496-.854a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0l-.5-.5a.5.5 0 0 1 .708-.708l.146.147 1.146-1.147a.5.5 0 0 1 .708 0z"/>\n' +
                        '</svg></a>';
                }
            }
        ]
    });
    $('#productDatatable tfoot th.search').each(function () {
        let title = $(this).text();
        $(this).html('<input type="text" placeholder="Поиск" class="form-control form-control-sm" />');
    });
});

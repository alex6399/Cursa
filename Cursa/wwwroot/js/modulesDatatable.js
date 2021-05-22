$(document).ready(function () {
    let table = $('#modulesDatatable').DataTable({
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
        "scrollX": true,
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Modules/FindModulesForCardOrder",
            "type": "POST",
            "datatype": "json",
            "data": {
                "orderId": orderId
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
                "targets": [7],
                "orderable": false
            }
        ],
        "columns": [
            {"data": "id", "name": "Id", "autoWidth": true},
            {
                "data": "moduleType.name", "name": "ModuleType.Name", "autoWidth": true
                // ,
                // "render": function (data, type, row) {
                //     if (type === "display" || type === "filter") {
                //         return '<a href="/Products/GetProductsForSubProject?subProjectId=' +
                //             row.id +
                //             '" >' +
                //             row.moduleType.name +
                //             "</a>";
                //     }
                //     return data;
                // }
            },
            // {"data": "destinationOrderCardNumber", "name": "DestinationOrderCardNumber", "autoWidth": true},
            {"data": "serialNumber", "name": "SerialNumber", "autoWidth": true,
                "render": function (data, type, row) {
                    if (type === "display") {
                        if (data === null) {
                            return "<span class=\"empty\">*Не указано</span>";
                        }
                        return data;
                    }
                    return data;
                }},
            {
                "data": "place", "name": "Place", "autoWidth": true

            },
            {
                "data": "actualOrderCardId", "name": "ActualOrderCardId", "autoWidth": true
                //,
                // "render": function (data, type, row) {
                //     if (type === "display") {
                //         return moment(Date.parse(row.endDate)).format('DD.MM.YYYY'); //DD.MM.YYYY HH:mm
                //     }
                //     return data;
                // }
            },
            {
                "data": "manufacturingData", "name": "ManufacturingData", "autoWidth": true,
                "render": function (data, type, row) {
                    if (type === "display") {
                        if (data === null) {
                            return "<span class=\"empty\">*Не указано</span>";
                        }
                        return moment(Date.parse(row.manufacturingData)).format('DD.MM.YYYY');
                    }
                    return data;
                }
            },
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true,
                "render": function (data, type, row) {
                    if (type === "display") {
                        if (data === null) {
                            return "<span class=\"empty\">*Не указано</span>";
                        }
                        return moment(Date.parse(row.createdDate)).format('DD.MM.YYYY');
                    }
                    return data;
                }
            },
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
    $('#modulesDatatable tfoot th.search').each(function () {
        let title = $(this).text();
        $(this).html('<input type="text" placeholder="Поиск" class="form-control form-control-sm" />');
    });
});

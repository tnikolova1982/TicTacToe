var Role = (function () {
    var table;
    function initTable() {
        table = $('#RolesTable-js').DataTable({
            fixedHeader: true,
            "language": {
                "lengthMenu": "Резултати на страница _MENU_ ",
                "zeroRecords": "Няма резултати",
                "info": "Страница _PAGE_ от _PAGES_",
                "infoEmpty": "Няма немерени резултати",
                "infoFiltered": "(филтрирани резултати от _MAX_ )",
                "paginate": {
                    first: '<i class="fa fa-angle-double-left">',
                    last: '<i class="fa fa-angle-double-right">',
                    next: '<i class="fa fa-angle-right">',
                    previous: '<i class="fa fa-angle-left">'
                }
            },
            "pagingType": "full_numbers",
            "pageLength": 10,
            orderCellsTop: true,
            "initComplete": function () {
                $('.top').prepend('<div class="countResults"> Общо: ' +
                    this.api().page.info().recordsTotal +
                    '</div>');

                $('.top').append('<div class="manipulateTableRecords d-flex p-3 my-3 rounded box-shadow"></div>');

                //if (Site.EditRoleRights() === "true") {
                $('.manipulateTableRecords').prepend('<span class="editable" id="editRow"><i class="fas fa-pencil-alt fa-lg" aria-hidden="true"></i></span>');
            },

            "dom": '<"top"ilp>rt<"bottom"><"clear">',
            destroy: true
        });

        $.each($('.filterColumn', table.table().header()), function () {
            var column = table.column($(this).index());

            $('input', this).on('keyup change', function () {
                if (column.search() !== this.value) {
                    column
                        .search(this.value)
                        .draw();
                }
            });
        });

        $('#RolesTable-js tbody').on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });
    }

    function bindEvent() {
        $(document).re('click', '.add-new-role-js', function () {
            window.location.href = $.link("Upsert", "ManageRole");
        });

        $(document).re('click', '#editRow', function () {
            var selectedRow = $('#RolesTable-js').find('tr.selected');
            if (selectedRow !== undefined && selectedRow !== "") {
                var id = $(selectedRow).find('#IdColumn').text().trim();

                if (id !== undefined && id !== '') {
                    window.location.href = $.link("Upsert", "ManageRole") + "?id=" + id;
                }
            }
        });
    }
    return {
        BindEvent: bindEvent,
        InitTable: initTable
    }

})();

$(document).ready(function () {
    Role.BindEvent();

    $('#SearchForm').find('button[type="submit"]').trigger('click');
});
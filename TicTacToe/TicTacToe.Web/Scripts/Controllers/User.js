var User = (function () {
    var table;
    function initTable() {
        table = $('#UsersTable-js').DataTable({
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
                // if (Site.EditUserRights() === "true") {

                $('.manipulateTableRecords').prepend('<span class="editable" id="editRow"><i class="fas fa-pencil-alt fa-lg" aria-hidden="true"></i></span>');
                // if (Site.DeactivateUserRights() === "true") {

                $('.manipulateTableRecords').prepend('<span class="editable" id="deleteRow"><i class="fas fa-times-circle fa-lg" aria-hidden="true"></i></span>');
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

        $('#UsersTable-js tbody').on('click', 'tr', function () {
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
        $(document).re('click', '.add-new-user-js', function () {
            window.location.href = $.link("CreateUser", "ManageUser");
        });

        $(document).re('click', '#editRow', function () {
            var selectedRow = $('#UsersTable-js').find('tr.selected');
            if (selectedRow !== undefined && selectedRow !== "") {
                var id = $(selectedRow).find('#IdColumn').text().trim();

                if (id !== undefined && id !== '') {
                    window.location.href = $.link("EditUser", "ManageUser") + "?id=" + id;
                }
            }
        });

        $(document).re('click', '#deleteRow', function () {
            var selectedRow = $('#UsersTable-js').find('tr.selected');

            Notification.ConfirmDialog("Изтриване", "Сигурни ли сте, че искате да изтриете този ред?", function () {
                if (selectedRow !== undefined && selectedRow !== "") {
                    var id = $(selectedRow).find('#IdColumn').text().trim();

                    if (id !== undefined && id !== '') {
                        $.ajax({
                            url: $.link("Delete", "ManageUser"),
                            data: { id: id },
                            type: "POST",
                            success: function (e) {
                                selectedRow.hide('slow', function () {
                                    selectedRow.remove();
                                });

                                $('#SearchForm').find('button[type="submit"]').trigger('click');

                                Notification.ShowMessage(e.message, e.type);
                            },
                            fail: function (e) {
                                Notification.ShowMessage(e.message, e.type);
                            }
                        });
                    }
                }
            });

        });
    }

    return {
        BindEvent: bindEvent,
        InitTable: initTable
    }

})();

$(document).ready(function () {
    User.BindEvent();

    $('#SearchForm').find('button[type="submit"]').trigger('click');
});
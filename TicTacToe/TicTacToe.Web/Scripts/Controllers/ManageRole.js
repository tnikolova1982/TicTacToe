var ManageRole = (function () {

    function initTable() {
        $("#RoleActivitiesTable").DataTable({
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
            "paging": true,
            "pageLength": 10,
            columnDefs: [
                {
                    targets: 0,
                    orderable: false
                }
            ],
            orderCellsTop: true,
            "initComplete": function () {
                $('.top').prepend('<div class="countResults"> Общо: ' +
                    this.api().page.info().recordsTotal +
                    '</div>');
            },
            "dom": '<"top"ilp>rt<"bottom"><"clear">',
            destroy: true
        });
    }

    function bindEvent() {
        $(document).re('change', 'input[type=checkbox].select-all-checkboxes-js', function () {

            var all = $(this).closest('table').find('tbody input[type=checkbox]');

            if ($(this).prop('checked')) {
                all.prop("checked", true);
            } else {
                all.prop("checked", false);
            }
        });

        $(document).re('click', 'tbody input[type=checkbox]', function (e) {
            CheckOrUncheckTheHeaderCheckBox($(this));
        });

        $('#RoleActivitiesTable').on('draw.dt', function () {
            CheckOrUncheckTheHeaderCheckBox($(this));
        });

        function CheckOrUncheckTheHeaderCheckBox(currentSelector) {

            var checkedInputsLength = currentSelector.closest('table').find('tbody input[type=checkbox]:checked').length;
            var allCheckboxLength = currentSelector.closest('table').find('tbody input[type=checkbox]').length;

            if (checkedInputsLength === allCheckboxLength) {
                $('input[type=checkbox].select-all-checkboxes-js').prop("checked", true);
            }
            else {
                $('input[type=checkbox].select-all-checkboxes-js').prop("checked", false);
            }
        }

        // Handle form submission event
        $(document).re('click', '#role-btn-submit', function (e) {
            e.preventDefault();

            var table = $('#RoleActivitiesTable').DataTable();

            var form = $("#UpsertRole");

            $("#appendInputsForSubmit").empty();

            // Encode a set of form elements from all pages as an array of names and values
            var params = table.$('input[type=checkbox]:checked');

            // Iterate over all form elements
            $.each(params, function () {
                // If element doesn't exist in DOM
                if (!$.contains(document, this)) {
                    // Create a hidden element

                    $("#appendInputsForSubmit").append(
                        $('<input>')
                            .attr('id', 'ActivitiesIds_Index')
                            .attr('type', 'hidden')
                            .attr('name', 'ActivitiesIds.Index')
                            .val(this.name.match(/[^[\]]+(?=])/g)[0])
                    );

                    $("#appendInputsForSubmit").append(
                        $('<input>')
                            .attr('type', 'hidden')
                            .attr('name', this.name)
                            .val(this.value)
                    );
                }
            });

            $(form).submit();
        });
    }

    return {
        InitTable: initTable,
        BindEvent: bindEvent
    }
})();

$(document).ready(function () {
    ManageRole.InitTable();
    ManageRole.BindEvent();

    $('.check-box-header').removeClass("sorting_asc");
});
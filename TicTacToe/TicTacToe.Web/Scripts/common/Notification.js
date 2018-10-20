var Notification = (function () {

    function modalFooter() {
        return '<div class="modal-footer">' +
            '<button type="button" class="btn btn-primary btn-ok-js">Да</button>' +
            '<button type="button" class="btn btn-secondary btn-cancel-js" data-dismiss="modal">Не</button>' +
            '</div>';
    }

    function createTemplate(title, message, dialogCssClass, id, shouldAddFooter) {
        var footer = modalFooter();

        if (!shouldAddFooter) {
            footer = "";
        }

        return '<div class="modal" id="' + id + '" tabindex="-1" role="dialog" data-backdrop="static">' +
            '<div class="modal-dialog ' + dialogCssClass + '" role="document">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<h5 class="modal-title">' + title + '</h5>' +
            '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
            '<span aria-hidden="true">&times;</span>' +
            '</button>' +
            '</div>' +

            '<div class="modal-body">' +
            '<p>' + message + '</p>' +
            '</div>' +
            footer +
            '</div>' +
            '</div>' +
            '</div>';
    }

    function confirmDialog(title, message, successCallback, cancelCallback, dialogCssClass, id, shouldAddFooter) {
        shouldAddFooter = (shouldAddFooter === false) ? false : true;

        // prepare variables
        id = id !== "" && id !== undefined ? id : "modal";
        dialogCssClass = dialogCssClass != '' && dialogCssClass != undefined ? dialogCssClass : '';

        // toggle modal
        $(function () {
            $('#' + id).modal("toggle");
        });

        var template = createTemplate(title, message, dialogCssClass, id, shouldAddFooter);
        $("body").append(template);

        $('#' + id).on('click', 'button[type="button"]', function (e) {
            if ($(this).hasClass('btn-ok-js') && typeof successCallback === 'function') {
                successCallback();
                $(this).closest(".modal").find('.close').click();
            }

            if ($(this).hasClass('btn-cancel-js')) {
                if (typeof cancelCallback === 'function') {
                    cancelCallback();
                }
                $(this).closest(".modal").find('.close').click();
            }

            $('#' + id).remove();
        });

        // When closing modal dialog with Esc key removes the modal
        $('#' + id).on('hidden.bs.modal', function () {
            $('#' + id).remove();
        });

    }

    function openPopup(action, contoller, title, data, successCallback, cancelCallback, dialogCssClass, id) {
        var path = $.link(action, contoller);

        $.get(path, data).done(function (data) {
            confirmDialog(title, data, successCallback, cancelCallback, "modal-lg", id, false);
        }, function () {
            $.validator.unobtrusive.parse(document);
        });
    }

    function showMessage(message, type) {
        var statusDiv = $("#statusMessage");

        var statusTemplate = '<div class="alert alert-' + type + ' alert-dismissable fade in""><button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>' + message + '</div>';

        statusDiv.append(statusTemplate);

        $(".alert").fadeTo(3000, 500).slideUp(500, function () {
            $(".alert").slideUp(500);
            $('.alert').remove();
        });
    }

    function showError(request) {
        if (request) {

            if (!request.suppressErrors) {

                if (request.statusText === "canceled" && request.status === 0) {
                    return;
                }

                // Catch unhandled ajax exceptions
                var message = "Грешка при работа с приложението!";
                var messageType = "danger";

                switch (request.status) {

                    case 401:
                        {
                            message = "Нямате права за това действие или вашата сесия е изтекла!";
                            messageType = "warning";
                            break;
                        }
                    case 404:
                        {
                            message = "Страницата не е намерена!";
                            messageType = "warning";
                            break;
                        }
                }

                showMessage(message, messageType);
            }
        }
    }

    function handleAjaxMessages() {
        $(document).ajaxError(function (event, request) {
            showError(request);
        });
    }

    function init() {
        handleAjaxMessages();

        $(document).re('click', '.nav-link,.dropdown-item,#exitCurentPage-js', function (e) {
            if (!$(e.target).hasClass('dropdown-toggle') && !$(e.target).hasClass('section')) {
                var hasForm = $('#AssaignedConcessionUpdateForm').length > 0 || $('#ConcessionaireProcedureUpsertForm').length > 0 || $('#UpsertPageForm').length > 0 || $('#UpsertPublicationsForm').length > 0 || $('#ManageConcederForm').length > 0 || $('#UpsertUser').length > 0 || $('#ManageLotForm').length > 0 || $('#AssaignedConcessionCorrectionUpdateForm').length > 0 || $('#ManageAmendedConcessionUpsertForm').length > 0;
                if (hasForm || $(e.target).hasClass('btn')) {
                    e.preventDefault();

                    Notification.ConfirmDialog("Изход", "Незапазените промени ще бъдат изгубени. <br/> Сигурни ли сте, че искате да напуснете страницата? ", function () {
                        window.location.href = $(e.target).prop('href');
                    });
                }
            }
        });
    }

    return {
        ConfirmDialog: confirmDialog,
        OpenModal: openPopup,
        ShowMessage: showMessage,
        Init: init
    }
})();

jQuery.extend({
    removeTableRow: function (element) {
        Notification.ConfirmDialog("Изтриване", "Сигурни ли сте, че искате да изтриете този ред?", function () {
            element.hide('slow', function () {
                element.remove();
            });
        });
    }
});

$(document).ready(function () {
    Notification.Init();

    var message = Cookie.GetCookie("X-Message");

    if (message != undefined && message != '') {
        var strings = message.split('|');
        Notification.ShowMessage(strings[0], strings[1]);

        Cookie.DeleteCookie("X-Message");

        $(document).off('click', '.nav-link,.dropdown-item,#exitCurentPage-js');
    }
});
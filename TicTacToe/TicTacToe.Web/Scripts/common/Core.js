jQuery.fn.extend({
    re: function (event, selector, callback) {
        $(this).off(event, selector);
        $(this).on(event, selector, callback);
    }
});

jQuery.extend({ link: Site.getPathToActionMethod });

jQuery.extend({
    randomIndex: function () {
        return Math.random().toString(36).substr(2, 5);
    },
});

function bindValidation() {
    jQuery.validator.methods.number = function (value) {
        if (value) {
            try {
                var number = value.replace(',', '.');
                if (isNaN(number)) {
                    return false;
                } else {
                    return true;
                }
            } catch (ex) {
                return false;
            }
        }
        return true;
    };

    jQuery.validator.methods.date = function (value, element) {
        return this.optional(element) || kendo.parseDate(value, "dd.MM.yyyy", "bg-BG") !== null;
    };
}

$(document).ready(function () {
    bindValidation();

    $(".disable-inputs-js :input").attr("disabled", true);
});
var GameTicTacToe = (function () {
    function bindEvent() {

        $(document).re('click', '#startFirstBtn', function (e) {
            $("#submit-action").val(this.value);
        });

        $(document).re('click', '#startSecondBtn', function (e) {
            $("#submit-action").val(this.value);
        });

        $(document).re('click', '.button-options-js', function (e) {

            if (Site.GetGameLatterX() === $("#UserLetterId").val()) {
                this.textContent = 'X'
                var inputValue = $(this).prev('input');
                inputValue.val('X');

                var inputIsEnabled = $(this).prev('input').prev('input');
                inputIsEnabled.val(false);
            }
            else {
                this.textContent = 'O'
            }

            $("#submit-action").val(this.value);
        });
    }

    return {
        BindEvent: bindEvent
    }
})();

$(document).ready(function () {
    GameTicTacToe.BindEvent();
});
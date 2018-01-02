/// <reference path="typings/bootbox/index.d.ts"/>
var Dialog = (function () {
    function Dialog() {
    }
    Dialog.prototype.alert = function (message) {
        bootbox.alert("<h3><strong class=\"text-danger\">" + message + "</strong></he>");
        //Stop the action of link or button.
        return false;
    };
    Dialog.prototype.info = function (message) {
        bootbox.alert("<h3><strong class=\"text-info\">" + message + "</strong></he>");
        //Stop the action of link or button.
        return false;
    };
    Dialog.prototype.confirm = function (message, link) {
        bootbox.confirm("<h3><strong class=\"text-danger\">" + message + "</strong></he>", function (result) {
            if (result) {
                window.location.href = link.href;
            }
        });
        //Stop the action of link or button.
        return false;
    };
    return Dialog;
}());
var dialog = new Dialog();
//# sourceMappingURL=Dialog.js.map
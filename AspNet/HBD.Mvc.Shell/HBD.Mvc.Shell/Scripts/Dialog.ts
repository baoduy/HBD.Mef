/// <reference path="typings/bootbox/index.d.ts"/>

class Dialog {
    alert(message: string) {
        bootbox.alert(`<h3><strong class="text-danger">${message}</strong></he>`);

        //Stop the action of link or button.
        return false;
    }

    info(message: string) {
        bootbox.alert(`<h3><strong class="text-info">${message}</strong></he>`);

        //Stop the action of link or button.
        return false;
    }

    confirm(message: string, link:HTMLLinkElement) {
        bootbox.confirm(`<h3><strong class="text-danger">${message}</strong></he>`,
            result => {
                if (result) {
                    window.location.href = link.href;
                }
            });
        //Stop the action of link or button.
        return false;
    }
}

let dialog = new Dialog();
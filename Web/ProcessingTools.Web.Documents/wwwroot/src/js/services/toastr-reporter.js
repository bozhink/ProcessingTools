'use strict';

module.exports = function ToastrReporter(toastr) {
    if (!toastr) {
        throw 'Toastr is required';
    }

    function raiseMessage(res) {
        switch (res.type) {
            case 'success':
                toastr.success(res.message);
                break;

            case 'info':
                toastr.info(res.message);
                break;

            case 'warning':
                toastr.warning(res.message);
                break;

            default:
                toastr.error(res.message);
        }
    }

    return {
        raiseMessage: raiseMessage
    };
}
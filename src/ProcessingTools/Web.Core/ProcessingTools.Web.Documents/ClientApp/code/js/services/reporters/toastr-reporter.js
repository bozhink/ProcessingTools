'use strict';

module.exports = function ToastrReporter(toastr) {
    if (!toastr) {
        throw 'Toastr is required';
    }

    function report(type, message) {
        switch (type) {
            case 'success':
                toastr.success(message);
                break;

            case 'info':
                toastr.info(message);
                break;

            case 'warning':
                toastr.warning(message);
                break;

            default:
                toastr.error(message);
        }
    }

    return {
        report: report
    };
};

'use strict';

module.exports = function MessageReporter() {
    function report(message, type) {
        switch (type) {
            case 'success':
                // TODO
                break;

            case 'info':
                // TODO
                break;

            case 'warning':
                // TODO
                break;

            default:
                // TODO
        }
    }

    return {
        report: report
    };
};
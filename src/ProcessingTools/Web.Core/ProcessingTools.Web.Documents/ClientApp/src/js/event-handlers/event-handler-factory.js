module.exports = function eventHandlerFactory(window) {
    function createEventHandler(callback) {
        if (!callback) {
            throw 'Callback function is required';
        }

        if (typeof (callback) !== 'function') {
            throw 'Callback should be a valid function';
        }

        return function (event) {
            var e = event || window.event;
            e.stopPropagation();
            e.preventDefault();
            callback(e);
            return false;
        };
    }

    return {
        create: createEventHandler
    };
};

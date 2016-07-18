(function (window, document) {
    'use strict';

    function keyDownEventListener(event) {
        var e = event || window.event;

        // Ctrl + s
        if (e.ctrlKey && e.which === 83) {
            e.preventDefault();
            window.save();
            return false;
        }

        // Ctrl + r
        if (e.ctrlKey && e.which === 82) {
            e.preventDefault();
            window.get();
            return false;
        }
    }

    document.addEventListener("keydown", keyDownEventListener);

}(window, document));

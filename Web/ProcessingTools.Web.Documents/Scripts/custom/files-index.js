(function (window, document) {
    'use strict';

    function keydownEventHandler(event) {
        var e = event || window.event,
            anchorElement,
            href;

        // U
        if (e.which === 85) {
            anchorElement = document.getElementById('upload-file-link');
            if (anchorElement) {
                href = anchorElement.href || null;
                if (href) {
                    document.location = href;
                }
            }

            return false;
        }
    }

    window.addEventListener('keydown', keydownEventHandler, false);
}(window, document));
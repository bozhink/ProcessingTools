(function (window, document) {
    'use strict';

    function redirect(anchorId) {
        var id = anchorId || null,
            anchorElement,
            href;
        if (id) {
            anchorElement = document.getElementById(id);
            if (anchorElement) {
                href = anchorElement.href || null;
                if (href) {
                    document.location = href;
                }
            }
        }
    }

    function keydownEventHandler(event) {
        var e = event || window.event;

        // U
        if (e.which === 85) {
            redirect('upload-file-link');
            return false;
        }

        // Page Up
        if (e.which === 33) {
            redirect('page-next');
            return false;
        }

        // Page Down
        if (e.which === 34) {
            redirect('page-prev');
            return false;
        }

        // End
        if (e.which === 35) {
            redirect('page-last');
            return false;
        }

        // Home
        if (e.which === 36) {
            redirect('page-first');
            return false;
        }
    }

    window.addEventListener('keydown', keydownEventHandler, false);
}(window, document));
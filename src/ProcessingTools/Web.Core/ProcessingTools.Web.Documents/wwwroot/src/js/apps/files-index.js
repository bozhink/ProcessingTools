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
            e.stopPropagation();
            e.preventDefault();
            redirect('upload-file-link');
            return false;
        }

        // Page Up
        if (e.which === 33) {
            e.stopPropagation();
            e.preventDefault();
            redirect('page-next');
            return false;
        }

        // Page Down
        if (e.which === 34) {
            e.stopPropagation();
            e.preventDefault();
            redirect('page-prev');
            return false;
        }

        // End
        if (e.which === 35) {
            e.stopPropagation();
            e.preventDefault();
            redirect('page-last');
            return false;
        }

        // Home
        if (e.which === 36) {
            e.stopPropagation();
            e.preventDefault();
            redirect('page-first');
            return false;
        }
    }

    window.addEventListener('keydown', keydownEventHandler, false);
}(window, document));

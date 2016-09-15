(function (window, document) {
    'use strict';

    window.toolboxEventHandlers = (function () {

        function clickMinimizeButtonEventHandler(event) {
            var e = event || window.event,
                target = e.target,
                toolbox,
                body;
            e.stopPropagation();
            e.preventDefault();

            if (target) {
                try {
                    toolbox = target.parentNode.parentNode.parentNode;
                    if (toolbox instanceof HTMLElement) {
                        body = toolbox.querySelector('.panel-body');
                    }

                    if (body instanceof HTMLElement) {
                        body.style.display = 'none';
                        toolbox.style.height = '60px';
                    }
                } catch (ex) {
                    console.error(ex);
                }
            }
        }

        function clickMaximizeButtonEventHandler(event) {
            var e = event || window.event,
                target = e.target,
                toolbox,
                body;
            e.stopPropagation();
            e.preventDefault();

            if (target) {
                try {
                    toolbox = target.parentNode.parentNode.parentNode;
                    if (toolbox instanceof HTMLElement) {
                        body = toolbox.querySelector('.panel-body');
                    }

                    if (body instanceof HTMLElement) {
                        body.style.display = 'block';
                        toolbox.style.height = '400px';
                    }
                } catch (ex) {
                    console.error(ex);
                }
            }
        }

        function clickCloseButtonEventHandler(event) {
            var e = event || window.event,
                target = e.target,
                toolbox;
            e.stopPropagation();
            e.preventDefault();

            if (target) {
                try {
                    toolbox = target.parentNode.parentNode.parentNode;
                    if (toolbox instanceof HTMLElement) {
                        toolbox.parentNode.removeChild(toolbox);
                        document.body.style.cursor = 'auto';
                    }
                } catch (ex) {
                    console.error(ex);
                }
            }
        }

        return {
            clickMinimizeButtonEventHandler,
            clickMaximizeButtonEventHandler,
            clickCloseButtonEventHandler
        };

    }());

}(window, document));

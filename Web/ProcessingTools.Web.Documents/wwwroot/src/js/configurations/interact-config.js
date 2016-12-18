'use strict';

export function InteractJSConfig(window, interact) {
    if (!window) {
        throw 'Window object is required';
    }

    if (!interact) {
        throw 'Interact is required';
    }

    function resizeMoveEventHandler(event) {
        var e = event || window.event,
            target = e.target,
            x = (parseFloat(target.getAttribute('data-x')) || 0),
            y = (parseFloat(target.getAttribute('data-y')) || 0);

        // update the element's style
        target.style.width = event.rect.width + 'px';
        target.style.height = event.rect.height + 'px';

        // translate when resizing from top or left edges
        x += event.deltaRect.left;
        y += event.deltaRect.top;

        target.style.webkitTransform = target.style.transform = 'translate(' + x + 'px,' + y + 'px)';

        target.setAttribute('data-x', x);
        target.setAttribute('data-y', y);
        //target.textContent = Math.round(event.rect.width) + '×' + Math.round(event.rect.height);
    }

    function dragEndEventHandler(event) {
        //var textEl = event.target.querySelector('p');

        //textEl && (textEl.textContent =
        //  'moved a distance of '
        //  + (Math.sqrt(event.dx * event.dx +
        //               event.dy * event.dy) | 0) + 'px');
    }

    function dragMoveEventHandler(event) {
        var e = event || window.event,
            target = e.target,
            // keep the dragged position in the data-x/data-y attributes
            x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
            y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

        // translate the element
        target.style.webkitTransform = target.style.transform = 'translate(' + x + 'px, ' + y + 'px)';

        // update the position attributes
        target.setAttribute('data-x', x);
        target.setAttribute('data-y', y);
    }

    function registerDragabbleBehavior(selector) {
        interact(selector)
            .draggable({
                inertia: true,
                restrict: {
                    //restriction: "parent",
                    endOnly: true,
                    elementRect: {
                        top: 0,
                        left: 0,
                        bottom: 1,
                        right: 1
                    }
                },
                autoScroll: true,
                onmove: dragMoveEventHandler,
                onend: dragEndEventHandler
            })
            .resizable({
                preserveAspectRatio: false,
                edges: {
                    left: true,
                    right: true,
                    bottom: true,
                    top: true
                }
            })
            .on('resizemove', resizeMoveEventHandler);
    }

    return {
        registerDragabbleBehavior: registerDragabbleBehavior
    };
}
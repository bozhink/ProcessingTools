window.articlePreviewActions = (function ($) {
    function toggleMarkTitle() {
        $('mark').hover(function () {
            $(this).attr('title', function () {
                return $(this).attr('elem-name');
            })
        }, function () {
            $(this).removeAttr('title');
        });
    }

    function genrateCoordinatesList(selector) {
        var sectionId = 'coordinates-list',
            $aside = $(selector),
            $section = $('<section>').addClass('panel panel-primary').addClass('draggable').attr('id', sectionId),
            $list = $('<div>').addClass('list-group'),
            $closeButton = $('<button>')
                .addClass('btn btn-primary')
                .text('×')
                .on('click', function () {
                    $('#' + sectionId).remove()
                }),
            $minimizeButton = $('<button>')
                .addClass('btn btn-primary')
                .text('_')
                .on('click', function () {
                    $('#' + sectionId + ' .panel-body').css('display', 'none');
                    $('#' + sectionId).css('height', '60px');
                }),
            $maximizeButton = $('<button>')
                .addClass('btn btn-primary')
                .text('+')
                .on('click', function () {
                    $('#' + sectionId + ' .panel-body').css('display', 'block')
                    $('#' + sectionId).css('height', '400px');
                }),
            toolbox = {
                title: 'Coordinates'
            };

        $('.named-content.geo-json').each(function () {
            var $that = $(this),
                id = $that.attr('id'),
                coordinates = JSON.parse($that.attr('specific-use'))['coordinates'];

            $('<a>')
                .addClass('list-group-item')
                .attr('href', '#' + id)
                .text(coordinates.toString())
                .on('click', listAnchorClickListener)
                .appendTo($list);
        });

        // https://css-tricks.com/snippets/css/a-guide-to-flexbox/
        // Panel heading
        $('<div>')
            .addClass('panel-heading')
            .addClass('toolbox-titlebar')
            .append($('<span>')
                .addClass('titlebar-text')
                .text(toolbox.title))
            .append($('<span>')
                .addClass('titlebar-buttons')
                .append($minimizeButton)
                .append($maximizeButton)
                .append($closeButton))
            .appendTo($section);

        // Panel body
        $('<div>')
            .addClass('panel-body')
            .append($list)
            .appendTo($section);

        $section.appendTo($aside);
    }

    function listAnchorClickListener(event) {
        var $that = $(this),
            $target = $($that.attr('href'));

        $('html, body').animate({
            scrollTop: $target.offset().top - 250 + 'px'
        }, 'fast');

        $target.addClass('selected-text-to-scroll');
        setTimeout(function () {
            $target.removeClass('selected-text-to-scroll');
        }, 1500);
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
                onmove: dragMoveListener,
                onend: dragEndListener
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
            .on('resizemove', resizeMoveListener);
    }

    function dragMoveListener(event) {
        var target = event.target,
            // keep the dragged position in the data-x/data-y attributes
            x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
            y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

        // translate the element
        target.style.webkitTransform = target.style.transform = 'translate(' + x + 'px, ' + y + 'px)';

        // update the position attributes
        target.setAttribute('data-x', x);
        target.setAttribute('data-y', y);
    }

    function dragEndListener(event) {
        //var textEl = event.target.querySelector('p');

        //textEl && (textEl.textContent =
        //  'moved a distance of '
        //  + (Math.sqrt(event.dx * event.dx +
        //               event.dy * event.dy) | 0) + 'px');
    }

    function resizeMoveListener(event) {
        var target = event.target,
            x = (parseFloat(target.getAttribute('data-x')) || 0),
            y = (parseFloat(target.getAttribute('data-y')) || 0);

        // update the element's style
        target.style.width = event.rect.width + 'px';
        target.style.height = event.rect.height + 'px';

        // translate when resizing from top or left edges
        x += event.deltaRect.left;
        y += event.deltaRect.top;

        target.style.webkitTransform = target.style.transform =
            'translate(' + x + 'px,' + y + 'px)';

        target.setAttribute('data-x', x);
        target.setAttribute('data-y', y);
        //target.textContent = Math.round(event.rect.width) + '×' + Math.round(event.rect.height);
    }

    return {
        toggleMarkTitle,
        genrateCoordinatesList,
        registerDragabbleBehavior
    }

}(jQuery));

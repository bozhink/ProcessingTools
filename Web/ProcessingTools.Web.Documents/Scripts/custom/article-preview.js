var articlePreviewActions = (function ($) {

    function toggleMarkTitle() {
        $('mark').hover(function () {
            $(this).attr('title', function () {
                return $(this).attr('elem-name');
            })
        }, function () {
            $(this).removeAttr('title');
        });
    }

    function genrateCoordinatesList(targetBoxId) {
        targetBoxId = '' + targetBoxId;
        if (targetBoxId.length < 1) {
            throw 'Invalid target box id!';
        }

        if (targetBoxId.substr(0, 1) !== '#') {
            targetBoxId = '#' + targetBoxId;
        }

        var sectionId = 'coordinates-list',
            $aside = $(targetBoxId),
            $section = $('<section>').addClass('panel panel-primary').addClass('draggable').attr('id', sectionId),
            $list = $('<div>').addClass('list-group'),
            $closeButton;

        $('.named-content.geo-json').each(function () {
            var $that = $(this),
                id = $that.attr('id'),
                coordinates = JSON.parse($that.attr('specific-use'))['coordinates'];

            $('<a>')
                .addClass('list-group-item')
                .attr('href', '#' + id)
                .text(coordinates.toString())
                .on('click', function () {
                    var $that = $(this),
                        $target = $($that.attr('href'));

                    $('html, body').animate({
                        scrollTop: $target.offset().top - 250 + 'px'
                    }, 'fast');

                    $target.addClass('selected-text-to-scroll');
                    setTimeout(function () {
                        $target.removeClass('selected-text-to-scroll');
                    }, 1500);
                })
                .appendTo($list);
        });

        $closeButton = $('<span>')
            .append($('<button>')
                .addClass('btn btn-primary btn')
                .text('×')
                .on('click', function () {
                    $('#' + sectionId).remove()
                }));

        // https://css-tricks.com/snippets/css/a-guide-to-flexbox/
        $('<div>')
            .addClass('panel-heading')
            .css({
                'display': 'flex',
                'justify-content': 'space-between',
                'align-items': 'center'
            })
            .append($('<span>').text('Coordinates'))
            .append($closeButton)
            .appendTo($section);

        $('<div>')
            .addClass('panel-body')
            .append($list)
            .appendTo($section);

        $section.appendTo($aside);
    }

    function registerDragabbleBehavior(selector) {
        interact(selector)
            .draggable({

                inertia: true,

                restrict: {
                    //restriction: "parent",
                    //restriction: "window",
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

                onend: function (event) {
                    //var textEl = event.target.querySelector('p');

                    //textEl && (textEl.textContent =
                    //  'moved a distance of '
                    //  + (Math.sqrt(event.dx * event.dx +
                    //               event.dy * event.dy) | 0) + 'px');
                }
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
            .on('resizemove', function (event) {
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
            });

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
    }

    return {
        toggleMarkTitle,
        genrateCoordinatesList,
        registerDragabbleBehavior
    }

}(jQuery));

articlePreviewActions.toggleMarkTitle();
articlePreviewActions.genrateCoordinatesList('#aside-main-box');
articlePreviewActions.registerDragabbleBehavior('.draggable');
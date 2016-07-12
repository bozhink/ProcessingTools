(function (window, saveLinkAddress) {
    const AUTOSAVE_TIME_MILLISECONDS = 60000,
        MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS = 5000,
        LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY_PREVIEW',
        LAST_SAVED_HASH_KEY = 'LAST_SAVED_HASH_KEY_PREVIEW';

    window.documentPreviewActions = (function ($) {
        var content = getContent(),
            contentHash = CryptoJS.SHA1(content).toString();

        sessionStorage.setItem(LAST_SAVED_HASH_KEY, contentHash);

        function getContent() {
            return $('#article').html();
        }

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
                $section,
                $list = $('<div>').addClass('list-group'),
                $closeButton = $('<button>')
                    .addClass('btn btn-primary')
                    .text('×')
                    .on('click', function () {
                        $('#' + sectionId).remove();
                        document.body.style.cursor = 'auto';
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

            // Remove all coordinates list toolboxes yet present.
            $('#' + sectionId).remove();

            // Create new coordinates list toolbox.
            $section = $('<section>').addClass('panel panel-primary').addClass('draggable').attr('id', sectionId);

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

        function keyDownEventListener(event) {
            var e = event || window.event;

            // Ctrl + s
            if (e.ctrlKey && e.which == 83) {
                e.preventDefault();
                window.save();
                return false;
            }
        }

        function addBalloon(selector, contentSelector) {
            contentSelector = contentSelector || '';

            $(selector).hover(function () {
                var $that = $(this),
                    rid = $that.attr('href');

                $('<div>').addClass('custom-tooltiptext')
                    .text($(rid + contentSelector).text())
                    .appendTo($that);

                $that.addClass('custom-tooltip');
            }, function () {
                $(this)
                    .removeClass('custom-tooltip')
                    .find('.custom-tooltiptext')
                    .remove();
            });
        }

        window.save = function (quietMode) {
            var lastSavedTime = sessionStorage.getItem(LAST_SAVED_TIME_KEY),
                lastSavedHash = sessionStorage.getItem(LAST_SAVED_HASH_KEY),
                content, contentHash;

            function getTimeToNextPossibleSave() {
                return MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS - (new Date() - new Date(lastSavedTime));
            }

            function doSaveRequest(content) {
                jsonRequester.put(saveLinkAddress, {
                    data: {
                        content: content
                    }
                })
                .then(function (res) {
                    if (res.Status === 'OK') {
                        sessionStorage.setItem(LAST_SAVED_HASH_KEY, contentHash);
                        if ('Message' in res) {
                            toastr.success(res.Message);
                        }
                    } else {
                        if ('Message' in res) {
                            toastr.error(res.Message);
                        } else {
                            toastr.error(JSON.stringify(res));
                        }
                    }
                })
                .catch(function (err) {
                    if ('Message' in err) {
                        toastr.error(err.Message);
                    } else {
                        toastr.error(JSON.stringify(err));
                    }
                });

                sessionStorage.setItem(LAST_SAVED_TIME_KEY, new Date());
            }

            if (!lastSavedTime || getTimeToNextPossibleSave() < 0) {
                content = getContent();
                contentHash = CryptoJS.SHA1(content).toString();

                if (contentHash !== lastSavedHash) {
                    doSaveRequest(content);
                } else {
                    if (!quietMode) {
                        toastr.info('Content will not be saved because it is not modified.');
                    }
                }
            } else {
                if (!quietMode) {
                    toastr.info('Wait ' + (getTimeToNextPossibleSave() / 1000.0) + 's before save.');
                }
            }
        }

        // Autosave
        //setInterval(function () {
        //    window.save(true);
        //}, AUTOSAVE_TIME_MILLISECONDS);

        document.addEventListener("keydown", keyDownEventListener);

        return {
            toggleMarkTitle,
            genrateCoordinatesList,
            registerDragabbleBehavior,
            addBalloon
        }
    }(window.jQuery));
}(window, saveLinkAddress));

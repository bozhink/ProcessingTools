(function (window, document) {
    const
        LAST_GET_TIME_KEY = 'LAST_GET_TIME_KEY_PREVIEW',
        LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY_PREVIEW',
        CONTENT_HASH_KEY = 'CONTENT_HASH_KEY_PREVIEW';

    var sessionStorage = window.sessionStorage,
        interactConfig = new window.InteractJSConfig(),
        jsonRequester = new window.JsonRequester(),
        documentController = new window.DocumentController(sessionStorage, LAST_GET_TIME_KEY, LAST_SAVED_TIME_KEY, CONTENT_HASH_KEY, jsonRequester),
        sha1 = window.CryptoJS.SHA1;

    interactConfig.registerDragabbleBehavior('.draggable');

    documentController.registerGetAction(function (content) {
        var contentHash,
            articleElement = document.getElementById('article');
        if (content) {
            articleElement.innerHTML = content;
            contentHash = sha1(articleElement.innerHTML).toString();
            sessionStorage.setItem(CONTENT_HASH_KEY, contentHash);
        }
    });

    // Fetch content
    window.get();

    documentController.registerSaveAction(function () {
        return document.getElementById('article').innerHTML;
    });

    // TODO
    window.documentPreviewActions = (function ($) {
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

        // Selection tag
        function foo() {
            var selection = window.getSelection().getRangeAt(0),
                selectedText = selection.extractContents(),
                span = document.createElement("span");
            span.style.backgroundColor = "yellow";
            span.appendChild(selectedText);
            selection.insertNode(span);
        }

        function tagLink() {
            var selection = window.getSelection().getRangeAt(0),
                selectedText = selection.extractContents(),
                tag = document.createElement("a");

            tag.setAttribute('href', selectedText);
            tag.setAttribute('target', '_blank');
            tag.setAttribute('type', 'simple');

            tag.setAttribute('xlink:type', 'simple');
            tag.setAttribute('elem-name', 'ext-link');
            tag.setAttribute('xmlns:xlink', 'http://www.w3.org/1999/xlink');

            tag.appendChild(selectedText);

            tag.setAttribute('href', tag.innerText);
            tag.setAttribute('xlink:href', tag.innerText);

            selection.insertNode(tag);
        }

        function tag(tagName, elemName, className) {
            var selection = window.getSelection().getRangeAt(0),
                selectedText = selection.extractContents(),
                tag = document.createElement(tagName);

            tag.setAttribute('elem-name', elemName);

            if (!className) {
                tag.setAttribute('class', elemName);
            } else {
                tag.setAttribute('class', className);
            }

            tag.appendChild(selectedText);
            selection.insertNode(tag);
        }

        function tagInSpan(elemName, className) {
            tag('span', elemName, className);
        }

        function tagInMark(elemName, className) {
            tag('mark', elemName, className);
        }

        function tagInDiv(elemName, className) {
            tag('div', elemName, className);
        }

        return {
            genrateCoordinatesList,
            addBalloon,
            foo,
            tagLink,
            tagInSpan,
            tagInMark,
            tagInDiv
        }
    }(window.jQuery));

    window.documentPreviewActions.addBalloon('a.bibr');
    window.documentPreviewActions.addBalloon('a.fig', ' .caption');
    window.documentPreviewActions.addBalloon('a.table', ' .caption');
}(window, document));

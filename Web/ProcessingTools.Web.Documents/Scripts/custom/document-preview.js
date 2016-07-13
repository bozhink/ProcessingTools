(function (window, document) {
    const AUTOSAVE_TIME_MILLISECONDS = 60000,
        MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS = 5000,
        LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY_PREVIEW',
        LAST_SAVED_HASH_KEY = 'LAST_SAVED_HASH_KEY_PREVIEW';

    var content = '' + window.documentContent,
        contentHash = window.CryptoJS.SHA1(content).toString(),
        sessionStorage = window.sessionStorage,
        interactConfig = new window.InteractJSConfig(),
        jsonRequester = new window.JsonRequester(),
        documentSaveController = new window.DocumentSaveController(sessionStorage, LAST_SAVED_TIME_KEY, LAST_SAVED_HASH_KEY, jsonRequester);

    sessionStorage.setItem(LAST_SAVED_HASH_KEY, contentHash);

    window.documentContent = ''; // Clear unused content

    interactConfig.registerDragabbleBehavior('.draggable');

    documentSaveController.registerSaveAction(function () {
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

        return {
            genrateCoordinatesList,
            addBalloon
        }
    }(window.jQuery));

    window.documentPreviewActions.addBalloon('a.bibr');
    window.documentPreviewActions.addBalloon('a.fig', ' .caption');
    window.documentPreviewActions.addBalloon('a.table', ' .caption');
}(window, document));

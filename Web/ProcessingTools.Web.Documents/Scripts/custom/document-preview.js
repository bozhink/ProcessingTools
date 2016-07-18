(function (window, document, $) {
    'use strict';

    const
        LAST_GET_TIME_KEY = 'LAST_GET_TIME_KEY_PREVIEW',
        LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY_PREVIEW',
        CONTENT_HASH_KEY = 'CONTENT_HASH_KEY_PREVIEW';

    var sessionStorage = window.sessionStorage,
        interactConfig = new window.InteractJSConfig(),
        jsonRequester = new window.JsonRequester(),
        documentController = new window.DocumentController(sessionStorage, LAST_GET_TIME_KEY, LAST_SAVED_TIME_KEY, CONTENT_HASH_KEY, jsonRequester),
        sha1 = window.CryptoJS.SHA1;

    window.getLinkAddress = document.getElementById('get-link').href;
    window.saveLinkAddress = document.getElementById('save-link').href;

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

    documentController.registerSaveAction(function () {
        return document.getElementById('article').innerHTML;
    });

    // Fetch content
    window.get();

    function listAnchorClickEventHandler(event) {
        var $that = $(event.target),
            $target = $($that.attr('href'));

        $('html, body').animate({
            scrollTop: $target.offset().top - 250 + 'px'
        }, 'fast');

        $target.addClass('selected-text-to-scroll');
        setTimeout(function () {
            $target.removeClass('selected-text-to-scroll');
        }, 1500);
    }

    function genrateCoordinatesList(selector) {
        var sectionId = 'coordinates-list',
            $aside = $(selector),
            $section,
            $list = $('<div>').addClass('list-group'),
            $closeButton = $('<button>')
                .addClass('btn btn-primary')
                .addClass('close-button')
                .text('×')
                .on('click', function () {
                    $('#' + sectionId).remove();
                    document.body.style.cursor = 'auto';
                }),
            $minimizeButton = $('<button>')
                .addClass('btn btn-primary')
                .addClass('minimize-button')
                .text('_')
                .on('click', function () {
                    $('#' + sectionId + ' .panel-body').css('display', 'none');
                    $('#' + sectionId).css('height', '60px');
                }),
            $maximizeButton = $('<button>')
                .addClass('btn btn-primary')
                .addClass('maximize-button')
                .text('+')
                .on('click', function () {
                    $('#' + sectionId + ' .panel-body').css('display', 'block');
                    $('#' + sectionId).css('height', '400px');
                }),
            toolbox = {
                title: 'Coordinates',
                coordinates: []
            };

        // Remove all coordinates list toolboxes yet present.
        $('#' + sectionId).remove();

        // Create new coordinates list toolbox.
        $section = $('<div>')
            .addClass('panel panel-primary')
            .addClass('draggable')
            .attr('id', sectionId);

        $('.named-content.geo-json').each(function (i, element) {
            var $that = $(element),
                id = $that.attr('id'),
                coordinates = JSON.parse($that.attr('specific-use')).coordinates;

            $('<a>')
                .addClass('list-group-item')
                .addClass('coordinate-item')
                .attr('id', 'coordinate-item-' + i)
                .attr('href', '#' + id)
                .text(coordinates.toString())
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

        $('.coordinate-item').on('click', listAnchorClickEventHandler);
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
            tagElement = document.createElement(tagName);

        tagElement.setAttribute('elem-name', elemName);

        if (!className) {
            tagElement.setAttribute('class', elemName);
        } else {
            tagElement.setAttribute('class', className);
        }

        tagElement.appendChild(selectedText);
        selection.insertNode(tagElement);
    }

    function tagInSpan(elemName, className) {
        tag('span', elemName, className);
    }

    function tagInMark(elemName, className) {
        tag('mark', elemName, className);
    }

    // Event handlers
    function genrateCoordinatesListEventHandler() {
        genrateCoordinatesList('#aside-main-box');
    }

    function getContentEventHandler() {
        window.get();
    }

    function saveContentEventHandler() {
        window.save();
    }

    function emailThisPageEventHandler() {
        window.location = 'mailto:?body=' + window.location.href;
    }

    function fooEventHandler() {
        foo();
    }

    function tagLinkEventHandler() {
        tagLink();
    }

    function tagCoordinateEventHandler() {
        tagInSpan('locality-coordinates');
    }

    function tagbibliographyElement(event) {
        var elementName = event.target.id.toString().substr(10);
        tagInMark(elementName);
    }

    // Events registration
    document.getElementById('save-button').onclick = saveContentEventHandler;
    document.getElementById('refresh-button').onclick = getContentEventHandler;
    document.getElementById('window-coordinates').onclick = genrateCoordinatesListEventHandler;
    document.getElementById('menu-item-refresh').onclick = getContentEventHandler;
    document.getElementById('menu-item-email-page').onclick = emailThisPageEventHandler;
    document.getElementById('menu-item-foo').onclick = fooEventHandler;
    document.getElementById('menu-item-tag-link').onclick = tagLinkEventHandler;
    document.getElementById('menu-item-tag-coordinate').onclick = tagCoordinateEventHandler;
    document.getElementById('menu-item-bibliography').onclick = tagbibliographyElement;

    function addBalloon(selector, contentSelector) {
        contentSelector = contentSelector || '';

        $(selector)
            .hover(function (event) {
                var $that = $(event.target),
                    rid = $that.attr('href');

                $('<div>')
                    .addClass('custom-tooltiptext')
                    .text($(rid + contentSelector).text())
                    .appendTo($that);

                $that.addClass('custom-tooltip');
            }, function (event) {
                $(event.target)
                    .removeClass('custom-tooltip')
                    .find('.custom-tooltiptext')
                    .remove();
            });
    }

    addBalloon('.xref.bibr');
    addBalloon('.xref.fig', ' .caption');
    addBalloon('.xref.table', ' .caption');

}(window, document, window.jQuery));

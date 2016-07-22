(function (window, document, $) {
    'use strict';

    const
        LAST_GET_TIME_KEY = 'LAST_GET_TIME_KEY_PREVIEW',
        LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY_PREVIEW',
        CONTENT_HASH_KEY = 'CONTENT_HASH_KEY_PREVIEW',
        CONTENT_ELEMENT_ID = 'article',
        GET_LINK_ID = 'get-link',
        SAVE_LINK_ID = 'save-link',
        SAVE_BUTTON_ID = 'save-button',
        REFRESH_BUTTON_ID = 'refresh-button',
        MAIN_ASIDE_ID = 'aside-main-box';

    var sessionStorage = window.sessionStorage,
        interactConfig = new window.InteractJSConfig(),
        jsonRequester = new window.JsonRequester(),
        documentController = new window.DocumentController(sessionStorage, LAST_GET_TIME_KEY, LAST_SAVED_TIME_KEY, CONTENT_HASH_KEY, jsonRequester),
        sha1 = window.CryptoJS.SHA1,
        template = new window.Template('../../../Content/Templates');

    interactConfig.registerDragabbleBehavior('.draggable');

    function addBalloon(selector, contentSelector) {
        contentSelector = contentSelector || '';

        $(selector)
            .hover(function (event) {
                var $that = $(event.target),
                    rid = $that.attr('href');

                $('<div>')
                    .attr('role', 'tooltip')
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

    function getContentCallback() {
        $('#' + CONTENT_ELEMENT_ID + ' .custom-tooltiptext').remove();
        return document.getElementById(CONTENT_ELEMENT_ID).innerHTML;
    }

    function setContentCallback(content) {
        var contentHash,
            articleElement = document.getElementById(CONTENT_ELEMENT_ID);
        if (content) {
            articleElement.innerHTML = content;
            contentHash = sha1(articleElement.innerHTML).toString();
            sessionStorage.setItem(CONTENT_HASH_KEY, contentHash);
        }
    }

    function getAfterAction() {
        addBalloon('.xref.bibr');
        addBalloon('.xref.fig', ' .caption');
        addBalloon('.xref.table', ' .caption');
    }

    window.getLinkAddress = document.getElementById(GET_LINK_ID).href;
    documentController.registerGetAction(setContentCallback, getAfterAction);

    window.saveLinkAddress = document.getElementById(SAVE_LINK_ID).href;
    documentController.registerSaveAction(getContentCallback);

    // Fetch content
    window.get();

    // Coordinates window
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

        tag.setAttribute('href', selectedText.toString().trim());
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

    function tag(tagName, elemName, className, attributes) {
        var selection = window.getSelection().getRangeAt(0),
            selectedText = selection.extractContents(),
            tagElement = document.createElement(tagName),
            attribute;

        tagElement.setAttribute('elem-name', elemName);

        if (!className) {
            tagElement.setAttribute('class', elemName);
        } else {
            tagElement.setAttribute('class', className);
        }

        if (attributes) {
            console.log(JSON.stringify(attributes));

            for (attribute in attributes) {
                console.log(attribute);
                console.log(attributes[attribute]);
                tagElement.setAttribute(attribute, attributes[attribute]);
            }
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

    function tagInXref(rid, refType) {
        var elemName = 'xref',
            className = elemName + ' ' + refType,
            attributes = {
                'rid': rid,
                'ref-type': refType,
                'href': '#' + rid
            };

        tag('a', elemName, className, attributes);
    }

    // Event handlers
    function tagBibliographicCitation(event) {
        var rid = event.target.getAttribute('rid').toString();
        tagInXref(rid, 'bibr');
    }

    function genrateCoordinatesListEventHandler() {
        genrateCoordinatesList('#' + MAIN_ASIDE_ID);
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

    function tagbibliographyElementEventHandler(event) {
        var elementName = event.target.id.toString().substr(10);
        tagInMark(elementName);
    }

    function tagBibliographicCitationEventHandler(event) {
        var $aside = $('#' + MAIN_ASIDE_ID),
            $target = $(event.target),
            $supermenu = $('#supermenu'),
            $menu = $('<menu>')
                .addClass('manual-tag-menu')
                .attr('id', 'menu-bibliographic-citations')
                .attr('label', $target.text());

        $('#manual-mode-notifier').remove();

        $('<div>')
            .addClass('mode-notifier')
            .attr('id', 'manual-mode-notifier')
            .text($target.text())
            .appendTo($aside);

        $('.ref').each(function (i, element) {
            var $element = $(element);

            $('<menuitem>')
                .attr('id', 'ref-' + i)
                .attr('rid', $element.attr('id'))
                .attr('label', $element.text().trim())
                .appendTo($menu);
        });

        $menu.appendTo($supermenu);

        $menu.on('click', tagBibliographicCitation);
    };

    // Events registration
    document.getElementById(SAVE_BUTTON_ID).onclick = saveContentEventHandler;
    document.getElementById(REFRESH_BUTTON_ID).onclick = getContentEventHandler;
    document.getElementById('window-coordinates').onclick = genrateCoordinatesListEventHandler;
    document.getElementById('menu-item-refresh').onclick = getContentEventHandler;
    document.getElementById('menu-item-email-page').onclick = emailThisPageEventHandler;
    document.getElementById('menu-item-foo').onclick = fooEventHandler;
    document.getElementById('menu-item-tag-link').onclick = tagLinkEventHandler;
    document.getElementById('menu-item-tag-coordinate').onclick = tagCoordinateEventHandler;
    document.getElementById('menu-item-bibliography').onclick = tagbibliographyElementEventHandler;

    document.getElementById('tag-bibliographic-citations-menu-item').onclick = tagBibliographicCitationEventHandler;

}(window, document, window.jQuery));

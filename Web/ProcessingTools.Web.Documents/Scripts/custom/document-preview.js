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
        template = new window.Template('../../../Content/Templates'),
        mainAside = document.getElementById(MAIN_ASIDE_ID);

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
    function listAnchorClickEventListener(event) {
        const TEXT_TO_SCROLL_CLASS_NAME = 'selected-text-to-scroll';
        var e = event || window.event,
            href = e.target.getAttribute('href'),
            $target = $(href);

        $('html, body').animate({
            scrollTop: $target.offset().top - 250 + 'px'
        }, 'fast');

        $target.addClass(TEXT_TO_SCROLL_CLASS_NAME);
        setTimeout(function () {
            $target.removeClass(TEXT_TO_SCROLL_CLASS_NAME);
        }, 1500);
    }

    function getCoordinates() {
        var result = [];
        $('.named-content.geo-json').each(function (i, element) {
            var $that = $(element),
                id = $that.attr('id'),
                coordinates = JSON.parse($that.attr('specific-use')).coordinates;

            result.push({
                id: id,
                index: i,
                latitude: coordinates[1],
                longitude: coordinates[0]
            });
        });

        return result;
    }

    function genrateCoordinatesListToolbox(selector) {
        var toolboxSelector = '#coordinates-list-toolbox',
            $aside = $(selector),
            toolbox = {
                title: 'Coordinates',
                coordinates: getCoordinates()
            };

        // Remove all coordinates list toolboxes yet present.
        $(toolboxSelector).remove();

        template.get('coordinates-toolbox')
            .then(function (template) {
                $('<div>')
                    .html(template(toolbox))
                    .appendTo($aside);
            })
            .then(function () {
                $('.coordinate-item').on('click', listAnchorClickEventListener);

                $(toolboxSelector + ' .minimize-button').on('click', function () {
                    $(toolboxSelector + ' .panel-body').css('display', 'none');
                    $(toolboxSelector).css('height', '60px');
                });

                $(toolboxSelector + ' .maximize-button').on('click', function () {
                    $(toolboxSelector + ' .panel-body').css('display', 'block');
                    $(toolboxSelector).css('height', '400px');
                });

                $(toolboxSelector + ' .close-button').on('click', function () {
                    $(toolboxSelector).remove();
                    document.body.style.cursor = 'auto';
                });
            });
    }

    function genrateCoordinatesMapToolbox(selector) {
        var toolboxSelector = '#coordinates-map-toolbox',
            $aside = $(selector),
            toolbox = {
                title: 'Map',
                coordinates: getCoordinates()
            };

        // Remove all coordinates list toolboxes yet present.
        $(toolboxSelector).remove();

        template.get('coordinates-map')
            .then(function (template) {
                var i,
                    len,
                    map,
                    coordinate = [],
                    coordinates = toolbox.coordinates,
                    leaflet = window.L,
                    $div = $('<div>');

                // TODO: appendTo
                $div.html(template({ title: toolbox.title })).appendTo($aside);

                map = leaflet.map('coordinates-map').setView([0.0, 0.0], 0);

                leaflet.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
                    attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                }).addTo(map);


                for (i = 0, len = coordinates.length; i < len; i += 1) {
                    coordinate = [coordinates[i].latitude, coordinates[i].longitude];
                    leaflet.marker(coordinate)
                        .bindPopup(JSON.stringify(coordinate))
                        .addTo(map);
                }
            })
            .then(function () {
                $(toolboxSelector + ' .minimize-button').on('click', function () {
                    $(toolboxSelector + ' .panel-body').css('display', 'none');
                    $(toolboxSelector).css('height', '60px');
                });

                $(toolboxSelector + ' .maximize-button').on('click', function () {
                    $(toolboxSelector + ' .panel-body').css('display', 'block');
                    $(toolboxSelector).css('height', '400px');
                });

                $(toolboxSelector + ' .close-button').on('click', function () {
                    $(toolboxSelector).remove();
                    document.body.style.cursor = 'auto';
                });
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

    // Event listeners

    // TODO
    function clickMinimizeMaximizeCloseButtonEventListener(event) {
        var e = event || window.event,
            target = e.target,
            toolbox;
        e.stopPropagation();
        e.preventDefault();

        function getToolbox(button) {
            var toolbox, header, body;
            if (button) {
                header = button.parentNode.parentNode;
                toolbox = header.parentNode;
                body = toolbox.lastElementChild;

                return {
                    toolbox: toolbox,
                    header: header,
                    body: body
                };
            }

            return null;
        }

        if (!target) {
            return false;
        }

        if (target.classList.contains('minimize-button')) {
            toolbox = getToolbox(target);
            if (toolbox) {
                toolbox.body.style = 'display: none';
                toolbox.toolbox.style = 'height: 60px'
            }

            return false;
        }

        if (target.classList.contains('maximize-button')) {
            toolbox = getToolbox(target);
            if (toolbox) {
                toolbox.body.style = 'display: block';
                toolbox.toolbox.style = 'height: 400px';
            }

            return false;
        }

        if (target.classList.contains('close-button')) {
            toolbox = getToolbox(target);
            if (toolbox) {
                toolbox.toolbox.parentNode.removeChild(toolbox.toolbox);
            }

            return false;
        }
    }

    function tagBibliographicCitation(event) {
        var e = event || window.event,
            rid = e.target.getAttribute('rid');
        tagInXref(rid, 'bibr');
    }

    function genrateCoordinatesListToolboxEventListener() {
        genrateCoordinatesListToolbox('#' + MAIN_ASIDE_ID);
    }

    function genrateCoordinatesMapToolboxEventListener() {
        genrateCoordinatesMapToolbox('#' + MAIN_ASIDE_ID);
    }

    function getContentEventListener() {
        window.get();
    }

    function saveContentEventListener() {
        window.save();
    }

    function emailThisPageEventListener() {
        window.location = 'mailto:?body=' + window.location.href;
    }

    function fooEventListener() {
        foo();
    }

    function tagLinkEventListener() {
        tagLink();
    }

    function tagCoordinateEventListener() {
        tagInSpan('locality-coordinates');
    }

    function tagbibliographyElementEventListener(event) {
        var e = event || window.event,
            elementName = e.target.id.toString().substr(10);
        tagInMark(elementName);
    }

    function tagBibliographicCitationEventListener(event) {
        var e = event || window.event,
            $aside = $('#' + MAIN_ASIDE_ID),
            $target = $(e.target),
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
    document
        .getElementById(SAVE_BUTTON_ID)
        .addEventListener('click', saveContentEventListener, false);
    document
        .getElementById(REFRESH_BUTTON_ID)
        .addEventListener('click', getContentEventListener, false);
    document
        .getElementById('window-coordinates')
        .addEventListener('click', genrateCoordinatesListToolboxEventListener, false);
    document
        .getElementById('window-map')
        .addEventListener('click', genrateCoordinatesMapToolboxEventListener, false);
    document
        .getElementById('menu-item-refresh')
        .addEventListener('click', getContentEventListener, false);
    document
        .getElementById('menu-item-email-page')
        .addEventListener('click', emailThisPageEventListener, false);
    document
        .getElementById('menu-item-foo')
        .addEventListener('click', fooEventListener, false);
    document
        .getElementById('menu-item-tag-link')
        .addEventListener('click', tagLinkEventListener, false);
    document
        .getElementById('menu-item-tag-coordinate')
        .addEventListener('click', tagCoordinateEventListener, false);
    document
        .getElementById('menu-item-bibliography')
        .addEventListener('click', tagbibliographyElementEventListener, false);

    document
        .getElementById('tag-bibliographic-citations-menu-item')
        .addEventListener('click', tagBibliographicCitationEventListener, false);

}(window, document, window.jQuery));

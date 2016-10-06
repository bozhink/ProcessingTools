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

    var app = window.app,
        sessionStorage = window.sessionStorage,
        interactConfig = new app.configurations.InteractJSConfig(),
        jsonRequester = new app.services.JsonRequester($),
        documentController = new app.controllers.DocumentController(sessionStorage, LAST_GET_TIME_KEY, LAST_SAVED_TIME_KEY, CONTENT_HASH_KEY, jsonRequester),
        sha1 = window.CryptoJS.SHA1,
        mainAside = document.getElementById(MAIN_ASIDE_ID);

    interactConfig.registerDragabbleBehavior('.draggable');

    function createBaloon(event, contentSelector) {
        var e = event || window.event,
            rid = e.target.getAttribute('href'),
            $contentNode = $(rid),
            $aside = $(mainAside),
            $baloon,
            content;

        contentSelector = contentSelector || '';
        if (contentSelector === '') {
            content = $contentNode.text();
        } else {
            content = $contentNode.find(contentSelector).text();
        }

        $aside.find('.balloon').remove();

        $baloon = $('<div>')
            .attr('role', 'balloon')
            .addClass('balloon')
            .text(content)
            .css({
                'top': (e.clientY + 10) + 'px',
                'left': (e.clientX - 20) + 'px'
            });

        $baloon.appendTo($aside);
    }

    function removeBalloon() {
        var $aside = $(mainAside);
        $aside.find('.balloon').remove();
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

    window.getLinkAddress = document.getElementById(GET_LINK_ID).href;
    documentController.registerGetAction(setContentCallback);

    window.saveLinkAddress = document.getElementById(SAVE_LINK_ID).href;
    documentController.registerSaveAction(getContentCallback);

    // Fetch content
    window.get();

    // Event handlers
    function tagBibliographicCitation(event) {
        var e = event || window.event,
            rid = e.target.getAttribute('rid');
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagInXref(rid, 'bibr');
    }

    function tagAppendicesCitation(event) {
        var e = event || window.event,
            rid = e.target.getAttribute('rid');
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagInXref(rid, 'app');
    }

    function tagSupplMaterialsCitation(event) {
        var e = event || window.event,
            rid = e.target.getAttribute('rid');
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagInXref(rid, 'supplementary-material');
    }

    function tagTablesCitation(event) {
        var e = event || window.event,
            rid = e.target.getAttribute('rid');
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagInXref(rid, 'table');
    }

    function tagFiguresCitation(event) {
        var e = event || window.event,
            rid = e.target.getAttribute('rid');
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagInXref(rid, 'fig');
    }

    function getContentEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.get();
    }

    function saveContentEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.save();
    }

    function emailThisPageEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.location = 'mailto:?body=' + window.location.href;
    }

    function fooEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.foo();
    }

    function tagLinkEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagLink();
    }

    function tagCoordinateEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagInSpan('locality-coordinates');
    }

    function tagAbbrevEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tag('abbr', 'abbrev');
    }

    function tagAbbrevDefEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagInSpan('p');
        window.htmlSelectionTagger.tagInSpan('def');
    }

    function tagbibliographyElementEventHandler(event) {
        var e = event || window.event,
            elementName = e.target.id.toString().substr(10);
        e.stopPropagation();
        e.preventDefault();
        window.htmlSelectionTagger.tagInMark(elementName);
    }

    // Manual tag menu event handlers
    function createManualTagMenuEventHandlerFactory(menuName, callback) {
        var $aside = $('#' + MAIN_ASIDE_ID),
            $supermenu = $('#supermenu'),
            $menu;

        // Remove notification
        $('#manual-mode-notifier').remove();
        $('<div>')
            .addClass('mode-notifier')
            .attr('id', 'manual-mode-notifier')
            .text(menuName)
            .appendTo($aside);

        // Create menu
        $('.manual-tag-menu').remove();
        $menu = $('<menu>')
            .addClass('manual-tag-menu')
            .attr('id', 'menu-bibliographic-citations')
            .attr('label', menuName);

        // Populate the menu
        callback($menu);

        // Attach the menu to DOM
        $menu.appendTo($supermenu);
    }

    function tagBibliographicCitationMenuClickEventHandler(event) {
        var e = event || window.event,
            $target = $(e.target);

        e.stopPropagation();
        e.preventDefault();

        createManualTagMenuEventHandlerFactory($target.text(), function ($menu) {
            $('.ref').each(function (i, element) {
                var $element = $(element);
                $('<menuitem>')
                    .addClass('mi-bibr')
                    .attr('id', 'ref-' + i)
                    .attr('rid', $element.attr('id'))
                    .attr('label', $element.text().trim())
                    .appendTo($menu);
            });
        });

        return false;
    }

    function tagAppendicesCitationMenuClickEventHandler(event) {
        var e = event || window.event,
            $target = $(e.target);

        e.stopPropagation();
        e.preventDefault();

        createManualTagMenuEventHandlerFactory($target.text(), function ($menu) {
            $('.app').each(function (i, element) {
                var $element = $(element);
                $('<menuitem>')
                    .addClass('mi-app')
                    .attr('id', 'app-' + i)
                    .attr('rid', $element.attr('id'))
                    .attr('label', $element.find('.title').text().trim())
                    .appendTo($menu);
            });
        });

        return false;
    }

    function tagSupplMaterialsCitationMenuClickEventHandler(event) {
        var e = event || window.event,
            $target = $(e.target);

        e.stopPropagation();
        e.preventDefault();

        createManualTagMenuEventHandlerFactory($target.text(), function ($menu) {
            $('.supplementary-material').each(function (i, element) {
                var $element = $(element);
                $('<menuitem>')
                    .addClass('mi-suppl-material')
                    .attr('id', 'suppl-material-' + i)
                    .attr('rid', $element.attr('id'))
                    .attr('label', $element.find('.label').text().trim())
                    .appendTo($menu);
            });
        });

        return false;
    }

    function tagTablesCitationMenuClickEventHandler(event) {
        var e = event || window.event,
            $target = $(e.target);

        e.stopPropagation();
        e.preventDefault();

        createManualTagMenuEventHandlerFactory($target.text(), function ($menu) {
            $('.table-wrap').each(function (i, element) {
                var $element = $(element);
                $('<menuitem>')
                    .addClass('mi-tab')
                    .attr('id', 'tab-' + i)
                    .attr('rid', $element.attr('id'))
                    .attr('label', $element.find('.label').text().trim())
                    .appendTo($menu);
            });
        });

        return false;
    }

    function tagFiguresCitationMenuClickEventHandler(event) {
        var e = event || window.event,
            $target = $(e.target);

        e.stopPropagation();
        e.preventDefault();

        createManualTagMenuEventHandlerFactory($target.text(), function ($menu) {
            $('.fig').each(function (i, element) {
                var $element = $(element);
                $('<menuitem>')
                    .addClass('mi-fig')
                    .attr('id', 'fig-' + i)
                    .attr('rid', $element.attr('id'))
                    .attr('label', $element.find('.label').text().trim())
                    .appendTo($menu);
            });
        });

        return false;
    }

    function keyDownEventHandler(event) {
        var e = event || window.event;

        // Ctrl + Delete
        if (e.ctrlKey && e.which === 46) {
            e.stopPropagation();
            e.preventDefault();
            window.htmlSelectionTagger.clearTagsInSelection();
            return false;
        }

        // Ctrl + B
        if (e.ctrlKey && e.which === 66) {
            e.stopPropagation();
            e.preventDefault();
            window.htmlSelectionTagger.tagInBold();
            return false;
        }

        // Ctrl + I
        if (e.ctrlKey && e.which === 73) {
            e.stopPropagation();
            e.preventDefault();
            window.htmlSelectionTagger.tagInItalic();
            return false;
        }

        // Ctrl + U
        if (e.ctrlKey && e.which === 85) {
            e.stopPropagation();
            e.preventDefault();
            window.htmlSelectionTagger.tagInUnderline();
            return false;
        }

        // Ctrl + M
        if (e.ctrlKey && e.which === 77) {
            e.stopPropagation();
            e.preventDefault();
            window.htmlSelectionTagger.tagInMonospace();
            return false;
        }

        // Alt + A -> abbrev
        if (e.altKey && e.which === 65) {
            e.stopPropagation();
            e.preventDefault();
            tagAbbrevEventHandler(e);
            return false;
        }

        // Alt + D -> def/p
        if (e.altKey && e.which === 68) {
            e.stopPropagation();
            e.preventDefault();
            tagAbbrevDefEventHandler(e);
            return false;
        }

        // Alt + C -> coordinates
        if (e.altKey && e.which === 67) {
            e.stopPropagation();
            e.preventDefault();
            tagCoordinateEventHandler(e);
            return false;
        }

        // Alt + E -> ext-link
        if (e.altKey && e.which === 69) {
            e.stopPropagation();
            e.preventDefault();
            tagLinkEventHandler(e);
            return false;
        }
    }

    function mouseoverXrefEventLstener(event) {
        var e = event || window.event,
            target = e.target;

        if (target.classList.contains('xref')) {
            e.stopPropagation();
            e.preventDefault();

            if (target.classList.contains('bibr')) {
                createBaloon(e);
            }

            if (target.classList.contains('aff')) {
                createBaloon(e, '.addr-line');
            }

            if (target.classList.contains('fig')) {
                createBaloon(e, '.caption');
            }

            if (target.classList.contains('table')) {
                createBaloon(e, '.caption');
            }

            return false;
        }
    }

    function mouseoutXrefEventHandler(event) {
        var e = event || window.event,
            target = e.target;
        if (target.classList.contains('xref')) {
            e.stopPropagation();
            e.preventDefault();

            removeBalloon();

            return false;
        }
    }

    function genrateCoordinatesListToolboxEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.coordinatesToolboxes.genrateCoordinatesListToolbox('#' + MAIN_ASIDE_ID);
    }

    function genrateCoordinatesMapToolboxEventHandler(event) {
        var e = event || window.event;
        e.stopPropagation();
        e.preventDefault();
        window.coordinatesToolboxes.genrateCoordinatesMapToolbox('#' + MAIN_ASIDE_ID);
    }

    // Events registration
    $('#supermenu')
        .on('click', '.mi-bibr', tagBibliographicCitation)
        .on('click', '.mi-app', tagAppendicesCitation)
        .on('click', '.mi-suppl-material', tagSupplMaterialsCitation)
        .on('click', '.mi-tab', tagTablesCitation)
        .on('click', '.mi-fig', tagFiguresCitation);

    document
        .getElementById(SAVE_BUTTON_ID)
        .addEventListener('click', saveContentEventHandler, false);
    document
        .getElementById(REFRESH_BUTTON_ID)
        .addEventListener('click', getContentEventHandler, false);
    document
        .getElementById('window-coordinates')
        .addEventListener('click', genrateCoordinatesListToolboxEventHandler, false);
    document
        .getElementById('window-map')
        .addEventListener('click', genrateCoordinatesMapToolboxEventHandler, false);
    document
        .getElementById('menu-item-refresh')
        .addEventListener('click', getContentEventHandler, false);
    document
        .getElementById('menu-item-email-page')
        .addEventListener('click', emailThisPageEventHandler, false);
    document
        .getElementById('menu-item-foo')
        .addEventListener('click', fooEventHandler, false);
    document
        .getElementById('menu-item-tag-link')
        .addEventListener('click', tagLinkEventHandler, false);
    document
        .getElementById('menu-item-tag-coordinate')
        .addEventListener('click', tagCoordinateEventHandler, false);
    document
        .getElementById('menu-item-bibliography')
        .addEventListener('click', tagbibliographyElementEventHandler, false);

    document
        .getElementById('tag-bibliographic-citations-menu-item')
        .addEventListener('click', tagBibliographicCitationMenuClickEventHandler, false);

    document
        .getElementById('tag-appendices-citations-menu-item')
        .addEventListener('click', tagAppendicesCitationMenuClickEventHandler, false);

    document
        .getElementById('tag-suppl-materials-citations-menu-item')
        .addEventListener('click', tagSupplMaterialsCitationMenuClickEventHandler, false);

    document
        .getElementById('tag-tables-citations-menu-item')
        .addEventListener('click', tagTablesCitationMenuClickEventHandler, false);

    document
        .getElementById('tag-figures-citations-menu-item')
        .addEventListener('click', tagFiguresCitationMenuClickEventHandler, false);

    document
        .addEventListener('keydown', keyDownEventHandler, false);

    document
        .getElementById(CONTENT_ELEMENT_ID)
        .addEventListener('mouseover', mouseoverXrefEventLstener, false);

    document
        .getElementById(CONTENT_ELEMENT_ID)
        .addEventListener('mouseout', mouseoutXrefEventHandler, false);
}(window, window.document, window.jQuery));

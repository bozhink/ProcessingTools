﻿'use strict';

const
    GET_LINK_ID = 'get-link',
    SAVE_LINK_ID = 'save-link',
    SAVE_BUTTON_ID = 'save-button',
    REFRESH_BUTTON_ID = 'refresh-button',
    CONTENT_ELEMENT_ID = 'article',
    MAIN_ASIDE_ID = 'aside-main-box',
    keys = {
        lastGetTimeKey: 'LAST_GET_TIME_KEY_PREVIEW',
        lastSavedTimeKey: 'LAST_SAVED_TIME_KEY_PREVIEW',
        contentHashKey: 'CONTENT_HASH_KEY_PREVIEW'
    };


var $ = window.jQuery,
    document = window.document,
    storage = window.sessionStorage,
    toastr = window.toastr,
    sha1 = window.CryptoJS.SHA1,
    interactConfig = require('../configurations/interact-config')(window, window.interact),
    jsonRequester = require('../services/json-requester')($),
    dataService = require('../services/documents/document-content-data')(storage, keys, jsonRequester, sha1),
    reporter = require('../services/toastr-reporter')(toastr),
    documentController = require('../controllers/documents/document-controller')(dataService, reporter),
    templatesProvider = require('../services/templates-provider')($, window.handlebars || window.Handlebars, '../../../wwwroot/build/dist/templates'),
    coordinatesToolboxes = require('../components/coordinates-toolboxes')(window, $, window.L, templatesProvider),
    htmlSelectionTagger = require('../components/html-selection-tagger')(window, document),
    mainAside = document.getElementById(MAIN_ASIDE_ID),
    getUrl = document.getElementById(GET_LINK_ID).href,
    saveUrl = document.getElementById(SAVE_LINK_ID).href,
    loadContentEventHandler,
    saveContentEventHandler;

require('../configurations/toastr-config')(toastr);
interactConfig.registerDragabbleBehavior('.draggable');

loadContentEventHandler = documentController.createGetAction(getUrl, function (content) {
    var contentHash,
        articleElement = document.getElementById(CONTENT_ELEMENT_ID);
    if (content) {
        articleElement.innerHTML = content;
        contentHash = sha1(articleElement.innerHTML).toString();
        storage.setItem(keys.contentHashKey, contentHash);
    }
}, function () {
    reporter.raiseMessage({
        type: 'success',
        message: 'Content is retrieved'
    });
})(false);

saveContentEventHandler = documentController.createSaveAction(saveUrl, function () {
    $('#' + CONTENT_ELEMENT_ID + ' .custom-tooltiptext').remove();
    return document.getElementById(CONTENT_ELEMENT_ID).innerHTML;
})(false);

// Fetch content
loadContentEventHandler();

// Edit functions
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

// Event handlers
function tagBibliographicCitation(event) {
    var e = event || window.event,
        rid = e.target.getAttribute('rid');
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagInXref(rid, 'bibr');
}

function tagAppendicesCitation(event) {
    var e = event || window.event,
        rid = e.target.getAttribute('rid');
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagInXref(rid, 'app');
}

function tagSupplMaterialsCitation(event) {
    var e = event || window.event,
        rid = e.target.getAttribute('rid');
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagInXref(rid, 'supplementary-material');
}

function tagTablesCitation(event) {
    var e = event || window.event,
        rid = e.target.getAttribute('rid');
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagInXref(rid, 'table');
}

function tagFiguresCitation(event) {
    var e = event || window.event,
        rid = e.target.getAttribute('rid');
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagInXref(rid, 'fig');
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
    htmlSelectionTagger.foo();
}

function tagLinkEventHandler(event) {
    var e = event || window.event;
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagLink();
}

function tagCoordinateEventHandler(event) {
    var e = event || window.event;
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagInSpan('locality-coordinates');
}

function tagAbbrevEventHandler(event) {
    var e = event || window.event;
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tag('abbr', 'abbrev');
}

function tagAbbrevDefEventHandler(event) {
    var e = event || window.event;
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagInSpan('p');
    htmlSelectionTagger.tagInSpan('def');
}

function tagbibliographyElementEventHandler(event) {
    var e = event || window.event,
        elementName = e.target.id.toString().substr(10);
    e.stopPropagation();
    e.preventDefault();
    htmlSelectionTagger.tagInMark(elementName);
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

function setElementInEditModeEventHandler(event) {
    var e = event || window.event,
        $target = $(e.target),
        name = $target.prop('nodeName').toLowerCase();

    if (name === 'p' || name === 'td' || name === 'th') {
        e.stopPropagation();
        e.preventDefault();
        $target
            .attr('contenteditable', '')
            .addClass('in-edit');
    }
}

function unsetElementInEditModeEventHandler(event) {
    var e = event || window.event,
        $target = $(e.target),
        name = $target.prop('nodeName').toLowerCase();

    if (name === 'p' || name === 'td' || name === 'th') {
        e.stopPropagation();
        e.preventDefault();
        $target
            .removeAttr('contenteditable')
            .removeClass('in-edit');
    }
}

function unsetAllInEditModeEventHandler(event) {
    var e = event || window.event;
    e.stopPropagation();
    e.preventDefault();
    $('.in-edit')
        .removeAttr('contenteditable')
        .removeClass('in-edit');
}

function keyDownEventHandler(event) {
    var e = event || window.event;

    if (e.ctrlKey) {

        // Ctrl + S
        if (e.which === 83) {
            e.stopPropagation();
            e.preventDefault();
            saveContentEventHandler();
            return false;
        }

        // Ctrl + R
        if (e.which === 82) {
            e.stopPropagation();
            e.preventDefault();
            loadContentEventHandler();
            return false;
        }

        // Ctrl + Delete
        if (e.which === 46) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.clearTagsInSelection();
            return false;
        }

        // Ctrl + B
        if (e.which === 66) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInBold();
            return false;
        }

        // Ctrl + I
        if (e.which === 73) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInItalic();
            return false;
        }

        // Ctrl + U
        if (e.which === 85) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInUnderline();
            return false;
        }

        // Ctrl + M
        if (e.which === 77) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMonospace();
            return false;
        }
    } else if (e.altKey) {

        // Alt + A -> abbrev
        if (e.which === 65) {
            e.stopPropagation();
            e.preventDefault();
            tagAbbrevEventHandler(e);
            return false;
        }

        // Alt + D -> def/p
        if (e.which === 68) {
            e.stopPropagation();
            e.preventDefault();
            tagAbbrevDefEventHandler(e);
            return false;
        }

        // Alt + C -> coordinates
        if (e.which === 67) {
            e.stopPropagation();
            e.preventDefault();
            tagCoordinateEventHandler(e);
            return false;
        }

        // Alt + E -> ext-link
        if (e.which === 69) {
            e.stopPropagation();
            e.preventDefault();
            tagLinkEventHandler(e);
            return false;
        }

        // Bibliography
        // Alt + 1
        if (e.which === 49) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('article-title');
            return false;
        }

        // Alt + 2
        if (e.which === 50) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('chapter-title');
            return false;
        }

        // Alt + 3
        if (e.which === 51) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('trans-title');
            return false;
        }

        // Alt + 4
        if (e.which === 52) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('source');
            return false;
        }

        // Alt + 5
        if (e.which === 53) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('trans-source');
            return false;
        }

        // Alt + 6
        if (e.which === 54) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('publisher-name');
            return false;
        }

        // Alt + 7
        if (e.which === 55) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('publisher-loc');
            return false;
        }

        // Alt + 8
        if (e.which === 56) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('comment');
            return false;
        }

        // Alt + 9
        if (e.which === 57) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('person-group');
            return false;
        }

        // Alt + 0
        if (e.which === 48) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('institution');
            return false;
        }

        // Alt + f1
        if (e.which === 112) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('volume');
            return false;
        }

        // Alt + f2
        if (e.which === 113) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('issue');
            return false;
        }

        // Alt + f3
        if (e.which === 114) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('fpage');
            return false;
        }

        // Alt + f4
        if (e.which === 115) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('lpage');
            return false;
        }

        // Alt + f5
        if (e.which === 116) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('size');
            return false;
        }

        // Alt + f6
        if (e.which === 117) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('elocation-id');
            return false;
        }

        // Alt + f7
        if (e.which === 118) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('year');
            return false;
        }

        // Alt + f8
        if (e.which === 119) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('edition');
            return false;
        }

        // Alt + f9
        if (e.which === 120) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark('series');
            return false;
        }
    } else {

        // Escape
        if (e.which === 27) {
            unsetAllInEditModeEventHandler(e);
            return false;
        }
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
    coordinatesToolboxes.genrateCoordinatesListToolbox('#' + MAIN_ASIDE_ID);
}

function genrateCoordinatesMapToolboxEventHandler(event) {
    var e = event || window.event;
    e.stopPropagation();
    e.preventDefault();
    coordinatesToolboxes.genrateCoordinatesMapToolbox('#' + MAIN_ASIDE_ID);
}

$('#article')
    .on('dblclick', 'p,td,th', setElementInEditModeEventHandler)
    .on('blur', 'p,td,th', unsetElementInEditModeEventHandler);

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
    .addEventListener('click', loadContentEventHandler, false);
document
    .getElementById('window-coordinates')
    .addEventListener('click', genrateCoordinatesListToolboxEventHandler, false);
document
    .getElementById('window-map')
    .addEventListener('click', genrateCoordinatesMapToolboxEventHandler, false);
document
    .getElementById('menu-item-refresh')
    .addEventListener('click', loadContentEventHandler, false);
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
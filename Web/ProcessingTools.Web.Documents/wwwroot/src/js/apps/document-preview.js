'use strict';

const
    GET_LINK_ID = 'get-link',
    SAVE_LINK_ID = 'save-link',
    SAVE_BUTTON_ID = 'save-button',
    REFRESH_BUTTON_ID = 'refresh-button',
    CONTENT_ELEMENT_ID = 'article',
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
    eventHandlerFactory = require('../event-handlers/event-handler-factory')(window),
    eventHandlers = require('../event-handlers/documents/document-preview-event-handlers')(window, document, $, eventHandlerFactory, htmlSelectionTagger, coordinatesToolboxes),
    getUrl = document.getElementById(GET_LINK_ID).href,
    saveUrl = document.getElementById(SAVE_LINK_ID).href,
    loadContentAction,
    saveContentAction;

require('../configurations/toastr-config')(toastr);
interactConfig.registerDragabbleBehavior('.draggable');

loadContentAction = documentController.createGetAction(getUrl, false, function (content) {
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
});

saveContentAction = documentController.createSaveAction(saveUrl, false, function () {
    $('#' + CONTENT_ELEMENT_ID + ' .custom-tooltiptext').remove();
    return document.getElementById(CONTENT_ELEMENT_ID).innerHTML;
});

eventHandlers.loadContent = eventHandlerFactory.create(loadContentAction);
eventHandlers.saveContent = eventHandlerFactory.create(saveContentAction);

// Fetch content
loadContentAction();

// Event handlers
function keyDownEventHandler(event) {
    var e = event || window.event;

    if (e.ctrlKey) {

        // Ctrl + S
        if (e.which === 83) {
            return eventHandlers.saveContent(e);
        }

        // Ctrl + R
        if (e.which === 82) {
            return eventHandlers.loadContent(e);
        }

        // Ctrl + Delete
        if (e.which === 46) {
            return eventHandlers.clearTagsInSelection(e);
        }

        // Ctrl + B
        if (e.which === 66) {
            return eventHandlers.tagInBold(e);
        }

        // Ctrl + I
        if (e.which === 73) {
            return eventHandlers.tagInItalic(e);
        }

        // Ctrl + U
        if (e.which === 85) {
            return eventHandlers.tagInUnderline(e);
        }

        // Ctrl + M
        if (e.which === 77) {
            return eventHandlers.tagInMonospace(e);
        }
    } else if (e.altKey) {

        // Alt + A -> abbrev
        if (e.which === 65) {
            return eventHandlers.tagAbbrev(e);
        }

        // Alt + D -> def/p
        if (e.which === 68) {
            return eventHandlers.tagAbbrevDef(e);
        }

        // Alt + C -> coordinates
        if (e.which === 67) {
            return eventHandlers.tagCoordinate(e);
        }

        // Alt + E -> ext-link
        if (e.which === 69) {
            return eventHandlers.tagLink(e);
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
            return eventHandlers.unsetAllInEditMode(e);
        }
    }
}

$('#article')
    .on('dblclick', 'p,td,th', eventHandlers.setElementInEditMode)
    .on('blur', 'p,td,th', eventHandlers.unsetElementInEditMode);

// Events registration
$('#supermenu')
    .on('click', '.mi-bibr', eventHandlers.tagBibliographicCitation)
    .on('click', '.mi-app', eventHandlers.tagAppendicesCitation)
    .on('click', '.mi-suppl-material', eventHandlers.tagSupplMaterialsCitation)
    .on('click', '.mi-tab', eventHandlers.tagTablesCitation)
    .on('click', '.mi-fig', eventHandlers.tagFiguresCitation)
    .on('click', '.mi-move', eventHandlers.moveFloatingObject);

document
    .getElementById(SAVE_BUTTON_ID)
    .addEventListener('click', eventHandlers.saveContent, false);
document
    .getElementById(REFRESH_BUTTON_ID)
    .addEventListener('click', eventHandlers.loadContent, false);
document
    .getElementById('window-coordinates')
    .addEventListener('click', eventHandlers.genrateCoordinatesListToolbox, false);
document
    .getElementById('window-map')
    .addEventListener('click', eventHandlers.genrateCoordinatesMapToolbox, false);
document
    .getElementById('menu-item-refresh')
    .addEventListener('click', eventHandlers.loadContent, false);
document
    .getElementById('menu-item-tag-link')
    .addEventListener('click', eventHandlers.tagLink, false);
document
    .getElementById('menu-item-tag-coordinate')
    .addEventListener('click', eventHandlers.tagCoordinate, false);
document
    .getElementById('menu-item-bibliography')
    .addEventListener('click', eventHandlers.tagbibliographyElement, false);

document
    .getElementById('tag-bibliographic-citations-menu-item')
    .addEventListener('click', eventHandlers.tagBibliographicCitationMenuClick, false);

document
    .getElementById('tag-appendices-citations-menu-item')
    .addEventListener('click', eventHandlers.tagAppendicesCitationMenuClick, false);

document
    .getElementById('tag-suppl-materials-citations-menu-item')
    .addEventListener('click', eventHandlers.tagSupplMaterialsCitationMenuClick, false);

document
    .getElementById('tag-tables-citations-menu-item')
    .addEventListener('click', eventHandlers.tagTablesCitationMenuClick, false);

document
    .getElementById('tag-figures-citations-menu-item')
    .addEventListener('click', eventHandlers.tagFiguresCitationMenuClick, false);

document
    .getElementById('move-floating-objects')
    .addEventListener('click', eventHandlers.moveFloatingObjectsMenuClick, false);

document
    .addEventListener('keydown', keyDownEventHandler, false);

document
    .getElementById(CONTENT_ELEMENT_ID)
    .addEventListener('mouseover', eventHandlers.mouseoverXref, false);

document
    .getElementById(CONTENT_ELEMENT_ID)
    .addEventListener('mouseout', eventHandlers.mouseoutXref, false);
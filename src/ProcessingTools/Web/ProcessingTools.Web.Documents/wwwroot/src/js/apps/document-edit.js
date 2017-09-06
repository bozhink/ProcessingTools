'use strict';

const
    EDITOR_CONTAINER_ID = 'editor-container',
    GET_LINK_ID = 'get-link',
    SAVE_LINK_ID = 'save-link',
    SAVE_BUTTON_ID = 'save-button',
    REFRESH_BUTTON_ID = 'refresh-button',
    keys = {
        mode: 'MONACO_EDITOR_MODE',
        theme: 'MONACO_EDITOR_THEME',
        lastGetTimeKey: 'LAST_GET_TIME_KEY_EDIT',
        lastSavedTimeKey: 'LAST_SAVED_TIME_KEY_EDIT',
        contentHashKey: 'CONTENT_HASH_KEY_EDIT'
    };

var $ = window.jQuery,
    document = window.document,
    storage = window.sessionStorage,
    toastr = window.toastr,
    sha1 = window.CryptoJS.SHA1,
    jsonRequester = require('../services/json-requester')($),
    dataService = require('../services/documents/document-content-data')(storage, keys, jsonRequester, sha1),
    reporter = require('../services/toastr-reporter')(toastr),
    documentController = require('../controllers/documents/document-controller')(dataService, reporter),
    monacoEditorConfig = require('../configurations/monaco-editor-config')(window, window.require),
    eventHandlerFactory = require('../event-handlers/event-handler-factory')(window),
    eventHandlers = {},
    editor,
    mode,
    theme,
    getUrl = document.getElementById(GET_LINK_ID).href,
    saveUrl = document.getElementById(SAVE_LINK_ID).href,
    loadContentAction,
    saveContentAction;

require('../configurations/toastr-config')(toastr);

loadContentAction = documentController.createGetAction(getUrl, false, function (content) {
    var contentHash;
    if (content) {
        editor.setValue(content);
        contentHash = sha1(editor.getValue()).toString();
        storage.setItem(keys.contentHashKey, contentHash);
    }
}, function () {
    reporter.raiseMessage({
        type: 'success',
        message: 'Content is retrieved'
    });
});

saveContentAction = documentController.createSaveAction(saveUrl, false, function () {
    return editor.getValue();
});

eventHandlers.loadContent = eventHandlerFactory.create(loadContentAction);
eventHandlers.saveContent = eventHandlerFactory.create(saveContentAction);

mode = storage.getItem(keys.mode);
theme = storage.getItem(keys.theme);

monacoEditorConfig.init(document.getElementById(EDITOR_CONTAINER_ID), '../../../node_modules', mode, theme)
    .then(function (res) {
        var i, len, modes, themes;

        if (!res) {
            return;
        }

        modes = res.modes;
        themes = res.themes;
        editor = res.editor;

        // Fetch content
        loadContentAction();

        // Populate modes and themes
        if (themes) {
            len = themes.length;
            for (i = 0; i < len; i += 1) {
                $('<option />')
                    .attr({
                        selected: themes[i].selected
                    })
                    .text(themes[i].display)
                    .appendTo($('.theme-picker'));
            }

            $('.theme-picker').change(function () {
                var index = this.selectedIndex,
                    $body = $('body'),
                    $monacoEditor = $('.monaco-editor');

                theme = themes[index].themeId;
                storage.setItem(keys.theme, theme)
                monacoEditorConfig.changeTheme(editor, theme);

                if (index > 0) {

                    // Not the default theme
                    $('.navbar-fixed-bottom').removeClass('navbar-default').addClass('navbar-inverse');
                } else {
                    $('.navbar-fixed-bottom').removeClass('navbar-inverse').addClass('navbar-default');
                }

                if ($monacoEditor) {
                    $body.css({
                        'color': $monacoEditor.css('color'),
                        'background-color': $monacoEditor.css('background-color')
                    });
                }
            });
        }

        if (modes) {
            len = modes.length;
            for (i = 0; i < len; i += 1) {
                $('<option />')
                    .attr({
                        selected: modes[i].selected
                    })
                    .text(modes[i].modeId)
                    .appendTo($('.language-picker'));
            }

            $('.language-picker').change(function () {
                mode = modes[this.selectedIndex].modeId;
                storage.setItem(keys.mode, mode);
                monacoEditorConfig.changeMode(editor, mode);
            });
        }
    });

// Events registration
document
    .getElementById(SAVE_BUTTON_ID)
    .addEventListener('click', eventHandlers.saveContent, false);
document
    .getElementById(REFRESH_BUTTON_ID)
    .addEventListener('click', eventHandlers.loadContent, false);

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
    }
}

document.addEventListener('keydown', keyDownEventHandler, false);

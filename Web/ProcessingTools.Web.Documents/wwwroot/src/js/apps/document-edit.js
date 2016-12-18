'use strict';

const
    EDITOR_CONTAINER_ID = 'editor-container',
    GET_LINK_ID = 'get-link',
    SAVE_LINK_ID = 'save-link',
    SAVE_BUTTON_ID = 'save-button',
    REFRESH_BUTTON_ID = 'refresh-button',
    keys = {
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
    monacoEditorConfig = require('../configurations/monaco-editor-config')(window, window.require, window.monaco),
    editor,
    getUrl = document.getElementById(GET_LINK_ID).href,
    saveUrl = document.getElementById(SAVE_LINK_ID).href,
    loadContentEventHandler,
    saveContentEventHandler;

require('../configurations/toastr-config')(toastr);

loadContentEventHandler = documentController.createGetAction(getUrl, function (content) {
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
})(false);

saveContentEventHandler = documentController.createSaveAction(saveUrl, function () {
    return editor.getValue();
})(false);

monacoEditorConfig.init(document.getElementById(EDITOR_CONTAINER_ID), '../../../node_modules')
    .then(function (res) {
        var i, len, option, modes, themes;

        if (!res) {
            return;
        }

        modes = res.modes;
        themes = res.themes;
        editor = res.editor;

        // Fetch content
        loadContentEventHandler();

        // Populate modes and themes
        if (themes) {
            len = themes.length;
            for (i = 0; i < len; i += 1) {
                option = document.createElement('option');
                option.textContent = themes[i].display;
                option.selected = themes[i].selected;
                $(".theme-picker").append(option);
            }

            $(".theme-picker").change(function () {
                var index = this.selectedIndex,
                    $body = $('body'),
                    $monacoEditor = $('.monaco-editor');

                monacoEditorConfig.changeTheme(themes[index]);

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
                option = document.createElement('option');
                option.textContent = modes[i].modeId;
                option.selected = modes[i].selected;
                $(".language-picker").append(option);
            }

            $(".language-picker").change(function () {
                monacoEditorConfig.changeMode(modes[this.selectedIndex]);
            });
        }
    });

// Events registration
document
    .getElementById(SAVE_BUTTON_ID)
    .addEventListener('click', saveContentEventHandler, false);
document
    .getElementById(REFRESH_BUTTON_ID)
    .addEventListener('click', loadContentEventHandler, false);

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
    }
}

document.addEventListener('keydown', keyDownEventHandler, false);
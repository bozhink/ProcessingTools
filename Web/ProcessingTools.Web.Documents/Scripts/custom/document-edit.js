(function (window) {
    'use strict';

    const
        LAST_GET_TIME_KEY = 'LAST_GET_TIME_KEY_EDIT',
        LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY_EDIT',
        CONTENT_HASH_KEY = 'CONTENT_HASH_KEY_EDIT',
        EDITOR_CONTAINER_ID = 'editor-container',
        GET_LINK_ID = 'get-link',
        SAVE_LINK_ID = 'save-link',
        SAVE_BUTTON_ID = 'save-button',
        REFRESH_BUTTON_ID = 'refresh-button';

    var sessionStorage = window.sessionStorage,
        monacoEditor = new window.MonacoEditor(window, document),
        jsonRequester = new window.JsonRequester(),
        documentController = new window.DocumentController(sessionStorage, LAST_GET_TIME_KEY, LAST_SAVED_TIME_KEY, CONTENT_HASH_KEY, jsonRequester),
        sha1 = window.CryptoJS.SHA1;

    monacoEditor.init(EDITOR_CONTAINER_ID, '');

    window.getLinkAddress = document.getElementById(GET_LINK_ID).href;
    documentController.registerGetAction(function (content) {
        var contentHash;
        if (content) {
            window.editor.setValue(content);
            contentHash = sha1(window.editor.getValue()).toString();
            sessionStorage.setItem(CONTENT_HASH_KEY, contentHash);
        }
    });

    window.saveLinkAddress = document.getElementById(SAVE_LINK_ID).href;
    documentController.registerSaveAction(function () {
        return window.editor.getValue();
    });

    // Fetch content
    // Wait 2s to be sure that Monaco editor is up and working
    setTimeout(function () {
        window.get();
    }, 2000);

    // Event handlers
    function getContentEventHandler() {
        window.get();
    }

    function saveContentEventHandler() {
        window.save();
    }

    // Events registration
    document
        .getElementById(SAVE_BUTTON_ID)
        .addEventListener('click', saveContentEventHandler, false);
    document
        .getElementById(REFRESH_BUTTON_ID)
        .addEventListener('click', getContentEventHandler, false);

}(window));

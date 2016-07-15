(function (window) {
    'use strict';

    const
        LAST_GET_TIME_KEY = 'LAST_GET_TIME_KEY_EDIT',
        LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY_EDIT',
        CONTENT_HASH_KEY = 'CONTENT_HASH_KEY_EDIT';

    var sessionStorage = window.sessionStorage,
        monacoEditor = new window.MonacoEditor(window, document),
        jsonRequester = new window.JsonRequester(),
        documentController = new window.DocumentController(sessionStorage, LAST_GET_TIME_KEY, LAST_SAVED_TIME_KEY, CONTENT_HASH_KEY, jsonRequester),
        sha1 = window.CryptoJS.SHA1;

    monacoEditor.init('editor-container', '');

    documentController.registerGetAction(function (content) {
        var contentHash;
        if (content) {
            window.editor.setValue(content);
            contentHash = sha1(window.editor.getValue()).toString();
            sessionStorage.setItem(CONTENT_HASH_KEY, contentHash);
        }
    });

    documentController.registerSaveAction(function () {
        return window.editor.getValue();
    });

    // Fetch content
    // Wait 1s to be sure that Monaco editor is up and working
    setTimeout(function () {
        window.get();
    }, 1000);
}(window));

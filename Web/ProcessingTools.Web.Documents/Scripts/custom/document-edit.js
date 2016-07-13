(function (window) {
    'use strict';

    const LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY_EDIT',
        LAST_SAVED_HASH_KEY = 'LAST_SAVED_HASH_KEY_EDIT';

    var content = '' + window.documentContent,
        contentHash = window.CryptoJS.SHA1(content).toString(),
        sessionStorage = window.sessionStorage,
        monacoEditor = new window.MonacoEditor(window, document),
        jsonRequester = new window.JsonRequester(),
        documentSaveController = new window.DocumentSaveController(sessionStorage, LAST_SAVED_TIME_KEY, LAST_SAVED_HASH_KEY, jsonRequester);

    sessionStorage.setItem(LAST_SAVED_HASH_KEY, contentHash);

    window.documentContent = ''; // Clear unused content

    monacoEditor.init('editor-container', content);

    documentSaveController.registerSaveAction(function () {
        return window.editor.getValue();
    });
}(window));

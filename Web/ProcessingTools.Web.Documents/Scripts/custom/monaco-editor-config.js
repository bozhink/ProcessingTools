function MonacoEditor(window, document) {
    'use strict';

    function initEditor(containerId, content) {
        content = '' + content;

        window.require.config({
            paths: {
                'vs': '../../../node_modules/monaco-editor/min/vs'
            }
        });

        window.require(['vs/editor/editor.main'], function () {
            window.editor = window.monaco.editor.create(document.getElementById(containerId), {
                value: content,
                language: 'xml'
                //, theme: 'vs-dark'
            });
        });
    }

    return {
        init: initEditor
    };
}

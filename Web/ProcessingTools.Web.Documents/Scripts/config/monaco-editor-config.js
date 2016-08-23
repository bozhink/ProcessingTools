function MonacoEditor(window, document) {
    'use strict';

    var require = window.require,
        editor = window.editor || null;

    function initEditor(containerId, content) {
        content = '' + content;

        require.config({
            paths: {
                'vs': '../../../node_modules/monaco-editor/min/vs'
            }
        });

        require(['vs/editor/editor.main'], function () {
            editor = window.monaco.editor.create(document.getElementById(containerId), {
                value: content,
                language: 'xml'
                //, theme: 'vs-dark'
            });

            window.editor = editor;
        });

        window.addEventListener('resize', function () {
            if (editor) {
                editor.layout();
            }
        }, false);
    }

    return {
        init: initEditor
    };
}

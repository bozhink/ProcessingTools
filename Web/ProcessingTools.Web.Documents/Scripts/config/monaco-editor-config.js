function MonacoEditor(window, document) {
    'use strict';

    const
        DEFAULT_LANGUAGE = 'xml',
        DEFAULT_THEME = 'vs';

    var require = window.require,
        editor = window.editor || null,
        modes = null,
        themes = [{
            themeId: 'vs',
            display: 'Visual Studio',
            selected: true
        }, {
            themeId: 'vs-dark',
            display: 'Visual Studio Dark'
        }, {
            themeId: 'hc-black',
            display: 'High Contrast Dark'
        }];

    function initEditor(containerId, content, callback) {
        content = '' + content;

        require.config({
            paths: {
                'vs': '../../../node_modules/monaco-editor/min/vs'
            }
        });

        require(['vs/editor/editor.main'], function () {
            var monaco = window.monaco || null;

            if (!modes) {
                modes = (function () {
                    var modesIds = monaco.languages.getLanguages().map(function (lang) {
                        return lang.id;
                    });

                    modesIds.sort();

                    return modesIds.map(function (modeId) {
                        var item = {
                            modeId: modeId
                        };

                        if (modeId === DEFAULT_LANGUAGE) {
                            item.selected = true;
                        }

                        return item;
                    });
                }());
            }

            editor = monaco.editor.create(document.getElementById(containerId), {
                value: content,
                language: DEFAULT_LANGUAGE
            });

            window.editor = editor;

            if (callback) {
                callback(modes, themes);
            }
        });

        window.addEventListener('resize', function () {
            if (editor) {
                editor.layout();
            }
        }, false);
    }

    function changeMode(mode) {
        var oldModel,
            newModel,
            content,
            language = mode ? mode.modeId || DEFAULT_LANGUAGE : DEFAULT_LANGUAGE;

        if (editor) {
            content = editor.getValue();
            oldModel = editor.getModel();
            newModel = window.monaco.editor.createModel(content, language);
            editor.setModel(newModel);
            if (oldModel) {
                oldModel.dispose();
            }
        }
    }

    function changeTheme(theme) {
        var newTheme = theme ? theme.themeId || DEFAULT_THEME : DEFAULT_THEME;
        if (editor) {
            editor.updateOptions({
                'theme': newTheme
            });
        }
    }

    return {
        init: initEditor,
        changeMode: changeMode,
        changeTheme: changeTheme
    };
}

'use strict';

const
    DEFAULT_LANGUAGE = 'xml',
    DEFAULT_THEME = 'vs';

var modes = null,
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

function getModes(monaco) {
    return (function () {
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

function createEditor(monaco, containerElement, language, theme, content) {
    content = content || '';
    language = language || DEFAULT_LANGUAGE;
    theme = theme || DEFAULT_THEME;

    return monaco.editor.create(containerElement, {
        value: content.toString(),
        language: language,
        theme: theme
    });
}

export function MonacoEditorConfig(window, require, monaco) {

    function initEditor(containerElement, pathToNodeModules, language, theme, content) {
        return new Promise(function (resolve, reject) {
            var editor;

            try {
                require.config({
                    paths: {
                        'vs': pathToNodeModules + '/monaco-editor/min/vs'
                    }
                });

                require(['vs/editor/editor.main'], function () {
                    modes = getModes(monaco);

                    editor = createEditor(monaco, containerElement, language, theme, content);

                    window.addEventListener('resize', function () {
                        if (editor) {
                            editor.layout();
                        }
                    }, false);

                    if (resolve) {
                        resolve({
                            editor: editor,
                            modes: modes,
                            themes: themes
                        });
                    }
                });
            } catch (e) {
                if (reject) {
                    reject(e);
                }
            }
        });
    }

    function changeMode(editor, mode) {
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

    function changeTheme(editor, theme) {
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
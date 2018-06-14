'use strict';

const
    DEFAULT_LANGUAGE = 'xml',
    DEFAULT_THEME = 'vs';

var config = require('./require-config'),
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

module.exports = function MonacoEditorConfig(window, require) {
    function getModes(monaco, selected) {
        return (function () {
            var modesIds = monaco.languages.getLanguages().map(function (lang) {
                return lang.id;
            });

            modesIds.sort();

            return modesIds.map(function (modeId) {
                var item = {
                    modeId: modeId
                };

                if (modeId === selected) {
                    item.selected = true;
                }

                return item;
            });
        }());
    }

    function getThemes(monaco, theme) {
        var i, len;
        for (i = 0, len = themes.length; i < len; i += 1) {
            if (themes[i].themeId === theme) {
                themes[i].selected = true;
            } else {
                themes[i].selected = false;
            }
        }

        return themes;
    }

    function createEditor(monaco, containerElement, language, theme, content) {
        return monaco.editor.create(containerElement, {
            value: content.toString(),
            language: language,
            lineNumbers: 'on',
            roundedSelection: true,
            scrollBeyondLastLine: true,
            readOnly: false,
            wordWrap: 'wordWrapColumn',
            wordWrapColumn: 120,
            wordWrapMinified: true,
            wrappingIndent: 'none', // try "same", "indent" or "none"
            theme: theme
        });
    }

    function initEditor(containerElement, pathToNodeModules, language, theme, content) {
        content = content || '';
        language = language || DEFAULT_LANGUAGE;
        theme = theme || DEFAULT_THEME;

        return new Promise(function (resolve, reject) {
            var editor;

            try {
                config(require, pathToNodeModules);

                require(['vs/editor/editor.main'], function () {
                    var monaco = window.monaco;
                    modes = getModes(monaco, language);
                    themes = getThemes(monaco, theme);

                    editor = createEditor(monaco, containerElement, language, theme, content);

                    editor.addCommand(monaco.KeyCode.F10, function () {
                        editor.updateOptions({
                            wordWrap: 'none'
                        });
                    });
                    editor.addCommand(monaco.KeyCode.F11, function () {
                        editor.updateOptions({
                            wordWrap: 'wordWrapColumn'
                        });
                    });

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
            language = mode || DEFAULT_LANGUAGE;

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
        if (editor) {
            editor.updateOptions({
                'theme': (theme || DEFAULT_THEME)
            });
        }
    }

    return {
        init: initEditor,
        changeMode: changeMode,
        changeTheme: changeTheme
    };
};

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

    function initEditor(containerId, pathToNodeModules, language, theme, content) {
        content = content || '';
        language = language || DEFAULT_LANGUAGE;
        theme = theme || DEFAULT_THEME;

        return new Promise(function (resolve, reject) {
            try {
                require.config({
                    paths: {
                        'vs': pathToNodeModules + '/monaco-editor/min/vs'
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
                        value: content.toString(),
                        language: language,
                        theme: theme
                    });

                    window.editor = editor;

                    window.addEventListener('resize', function () {
                        if (editor) {
                            editor.layout();
                        }
                    }, false);

                    if (resolve) {
                        resolve({
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

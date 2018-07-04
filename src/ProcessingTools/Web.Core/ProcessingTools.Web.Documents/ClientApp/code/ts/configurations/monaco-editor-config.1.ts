const DEFAULT_LANGUAGE: string = "xml";
const DEFAULT_THEME: string = "vs";

import * as monaco from "monaco-editor/esm/vs/editor/editor.api";
import { RequireConfig } from "./require-config";
import { IEditorMode, IEditorTheme } from "../contracts/models/configuration.models";

export interface IConfiguredEditor {
    editor: monaco.editor.ICodeEditor;
    modes: Array<IEditorMode>;
    themes: Array<IEditorTheme>;
}

export interface IMonacoEditorConfig {
    // tslint:disable-next-line:max-line-length
    initEditor(containerElement: HTMLElement, pathToNodeModules: string, language: string, theme: string, content?: string): Promise<IConfiguredEditor>;

    // tslint:disable-next-line:max-line-length
    changeMode(editor: monaco.editor.ICodeEditor, mode: string): void;

    // tslint:disable-next-line:max-line-length
    changeTheme(editor: monaco.editor.ICodeEditor, theme: string): void;
}

export class MonacoEditorConfig implements IMonacoEditorConfig {

    private modes: Array<IEditorMode> = null;
    private themes: Array<IEditorTheme> = [{
        themeId: "vs",
        display: "Visual Studio",
        selected: true
    }, {
        themeId: "vs-dark",
        display: "Visual Studio Dark"
    }, {
        themeId: "hc-black",
        display: "High Contrast Dark"
    }];


    public constructor(private readonly window: Window, private readonly require: Require) {
    }

    // tslint:disable-next-line:max-line-length
    private getModes(selected: string): Array<IEditorMode> {
        let modesIds: Array<string> = monaco.languages.getLanguages()
            .map(function (language: monaco.languages.ILanguageExtensionPoint): string {
                return language.id;
            });

        modesIds.sort();

        return modesIds.map(function (modeId: string): IEditorMode {
            return {
                modeId: modeId,
                selected: modeId === selected
            };
        });
    }

    // tslint:disable-next-line:max-line-length
    private getThemes(theme?: string): Array<IEditorTheme> {
        let self: MonacoEditorConfig = this, i: number, len: number = self.themes.length;
        if (theme) {
            for (i = 0; i < len; i += 1) {
                if (self.themes[i].themeId === theme) {
                    self.themes[i].selected = true;
                } else {
                    self.themes[i].selected = false;
                }
            }
        }

        return self.themes;
    }

    // tslint:disable-next-line:max-line-length
    private createEditor(containerElement: HTMLElement, language: string, theme: string, content?: string): monaco.editor.IStandaloneCodeEditor {
        return monaco.editor.create(containerElement, {
            value: content.toString(),
            language: language,
            lineNumbers: "on",
            roundedSelection: true,
            scrollBeyondLastLine: true,
            readOnly: false,
            wordWrap: "on",
            /*wordWrapColumn: 120,*/
            wordWrapMinified: true,
            wrappingIndent: "none", // try "same", "indent" or "none"
            theme: theme
        });
    }

    // tslint:disable-next-line:max-line-length
    public initEditor(containerElement: HTMLElement, pathToNodeModules: string, language: string, theme: string, content?: string): Promise<IConfiguredEditor> {
        content = content || "";
        language = language || DEFAULT_LANGUAGE;
        theme = theme || DEFAULT_THEME;

        let self: MonacoEditorConfig = this;

        return new Promise(function (resolve: (value?: IConfiguredEditor) => void, reject: (reason?: any) => void): void {
            var editor: monaco.editor.IStandaloneCodeEditor;

            try {
                RequireConfig(self.require, pathToNodeModules);

                self.require(["vs/editor/editor.main"], function (): void {
                    self.modes = self.getModes(language);
                    self.themes = self.getThemes(theme);

                    editor = self.createEditor(containerElement, language, theme, content);

                    editor.addCommand(monaco.KeyCode.F10, function (): void {
                        editor.updateOptions({
                            wordWrap: "off"
                        });
                    }, null);

                    editor.addCommand(monaco.KeyCode.F11, function (): void {
                        editor.updateOptions({
                            wordWrap: "on"
                        });
                    }, null);

                    self.window.addEventListener("resize", function (): void {
                        if (editor) {
                            editor.layout();
                        }
                    }, false);

                    if (resolve) {
                        resolve({
                            editor: editor,
                            modes: self.modes,
                            themes: self.themes
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

    // tslint:disable-next-line:max-line-length
    public changeMode(editor: monaco.editor.ICodeEditor, mode: string): void {
        let language: string = mode || DEFAULT_LANGUAGE;

        if (editor) {
            let content: string = editor.getValue();
            let oldModel: monaco.editor.ITextModel = editor.getModel();
            let newModel: monaco.editor.ITextModel = monaco.editor.createModel(content, language);
            editor.setModel(newModel);
            if (oldModel) {
                oldModel.dispose();
            }
        }
    }

    // tslint:disable-next-line:max-line-length
    public changeTheme(editor: monaco.editor.ICodeEditor, theme: string): void {
        if (editor) {
            editor.updateOptions({
                /*theme: (theme || DEFAULT_THEME)*/ // todo: needs revision
            });
        }
    }
}

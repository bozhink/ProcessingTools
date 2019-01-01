import { IStorageKeys } from "../contracts/models/services.models";
import { IEditorMode } from "../contracts/models/configuration.models";
import { IConfiguredEditor, IMonacoEditorConfig, MonacoEditorConfig } from "../configurations/monaco-editor-config";

declare let window: Window;
declare let document: Document;
declare let $: JQueryStatic;

enum HtmlElementIds {
    EDITOR_CONTAINER_ID = "editor-container",
}

const keys: IStorageKeys = {
    mode: "MONACO_EDITOR_MODE",
    theme: "MONACO_EDITOR_THEME"
};

let requirejs: any = (window as any).require;

let monacoEditorConfig: IMonacoEditorConfig = new MonacoEditorConfig(window, requirejs);

let editor: any; // monaco.editor.ICodeEditor;

let storage: Storage = window.sessionStorage;
let mode: string = storage.getItem(keys.mode);

monacoEditorConfig.initEditor(document.getElementById(HtmlElementIds.EDITOR_CONTAINER_ID), "../../../node_modules", mode, null)
    .then(function (response: IConfiguredEditor): void {
        let i: number, len: number, modes: Array<IEditorMode>;

        if (!response) {
            return;
        }

        modes = response.modes;
        editor = response.editor;

        if (modes) {
            let $languagePicker: JQuery = $(".language-picker");
            len = modes.length;
            for (i = 0; i < len; i += 1) {
                $("<option />")
                    .attr({
                        selected: modes[i].selected
                    })
                    .text(modes[i].modeId)
                    .appendTo($languagePicker);
            }

            $languagePicker.change(function (): void {
                let index: number = (this as HTMLSelectElement).selectedIndex;
                mode = modes[index].modeId;
                storage.setItem(keys.mode, mode);
                monacoEditorConfig.changeMode(editor, mode);
            });
        }
    });

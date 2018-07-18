import { ReportType, IReporter } from "../contracts/reporters/reporter";
import { ToastrReporter } from "../services/reporters/toastr-reporter";

import { IRequesterBase } from "../contracts/http/requester-base";
import { JsonRequester } from "../services/http/json-requester";

import { IStorageKeys } from "../contracts/models/services.models";
import { IDocumentContentData } from "../contracts/documents/document-content-data";
import { DocumentContentData } from "../services/documents/document-content-data";
import { DocumentController } from "../controllers/documents.controllers";
import { SHA1 } from "crypto-js";

import { IEditorMode, IEditorTheme } from "../contracts/models/configuration.models";
import { IConfiguredEditor, IMonacoEditorConfig, MonacoEditorConfig } from "../configurations/monaco-editor-config";
import { EventHandlerFactory } from "../configurations/event.handlers.configuration";
import { ToastrConfiguration } from "../configurations/toastr-config";

declare let window: Window;
declare let document: Document;
declare let $: JQueryStatic;
declare let toastr: Toastr;

enum HtmlElementIds {
    EDITOR_CONTAINER_ID = "editor-container",
    GET_LINK_ID = "get-link",
    SAVE_LINK_ID = "save-link",
    SAVE_BUTTON_ID = "save-button",
    REFRESH_BUTTON_ID = "refresh-button"
}

let getUrl: string = (document.getElementById(HtmlElementIds.GET_LINK_ID) as HTMLAnchorElement).href;
let saveUrl: string = (document.getElementById(HtmlElementIds.SAVE_LINK_ID) as HTMLAnchorElement).href;

const keys: IStorageKeys = {
    mode: "MONACO_EDITOR_MODE",
    theme: "MONACO_EDITOR_THEME",
    lastGetTimeKey: "LAST_GET_TIME_KEY_EDIT",
    lastSavedTimeKey: "LAST_SAVED_TIME_KEY_EDIT",
    contentHashKey: "CONTENT_HASH_KEY_EDIT"
};

let requirejs: any = (window as any).require;

let eventHandlerFactory: EventHandlerFactory = new EventHandlerFactory(window);
let monacoEditorConfig: IMonacoEditorConfig = new MonacoEditorConfig(window, requirejs);

let storage: Storage = window.sessionStorage;
let jsonRequester: IRequesterBase<any> = new JsonRequester($);
let dataService: IDocumentContentData = new DocumentContentData(storage, keys, jsonRequester, SHA1);
let reporter: IReporter = new ToastrReporter(toastr);
let documentController: DocumentController = new DocumentController(dataService, reporter);

let editor: any; // monaco.editor.ICodeEditor;

ToastrConfiguration.configure(toastr);

let loadContentAction: () => void = documentController.createGetAction(getUrl, false, function (content: string): void {
    let contentHash: string;
    if (content) {
        editor.setValue(content);
        contentHash = SHA1(editor.getValue()).toString();
        storage.setItem(keys.contentHashKey, contentHash);
    }
}, function (): void {
    reporter.report(ReportType.SUCCESS, "Content is retrieved");
});

let saveContentAction: () => void = documentController.createSaveAction(saveUrl, false, () => editor.getValue());

let eventHandlers: { loadContent: (e: Event) => any; saveContent: (e: Event) => any } = {
    loadContent: eventHandlerFactory.create(loadContentAction),
    saveContent: eventHandlerFactory.create(saveContentAction)
};

let mode: string = storage.getItem(keys.mode);
let theme: string = storage.getItem(keys.theme);

monacoEditorConfig.initEditor(document.getElementById(HtmlElementIds.EDITOR_CONTAINER_ID), "../../../node_modules", mode, theme)
    .then(function (response: IConfiguredEditor): void {
        let i: number, len: number, modes: Array<IEditorMode>, themes: Array<IEditorTheme>;

        if (!response) {
            return;
        }

        modes = response.modes;
        themes = response.themes;
        editor = response.editor;

        // fetch content
        loadContentAction();

        let $body: JQuery = $("body");
        let $monacoEditor: JQuery = $(".monaco-editor");

        // populate modes and themes
        if (themes) {
            let $themePicker: JQuery = $(".theme-picker");
            if ($themePicker.length > 0) {
                len = themes.length;
                for (i = 0; i < len; i += 1) {
                    $("<option />")
                        .attr({
                            selected: themes[i].selected
                        })
                        .text(themes[i].display)
                        .appendTo($themePicker);
                }

                $themePicker.change(function (): void {
                    let index: number = (this as HTMLSelectElement).selectedIndex;
                    theme = themes[index].themeId;
                    storage.setItem(keys.theme, theme);
                    monacoEditorConfig.changeTheme(editor, theme);

                    if (index > 0) {
                        // not default theme
                        $(".navbar-fixed-bottom").removeClass("navbar-default").addClass("navbar-inverse");
                    } else {
                        // default theme
                        $(".navbar-fixed-bottom").removeClass("navbar-inverse").addClass("navbar-default");
                    }

                    if ($monacoEditor) {
                        $body.css({
                            "color": $monacoEditor.css("color"),
                            "background-color": $monacoEditor.css("background-color")
                        });
                    }
                });

            }
        }

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

// events registration
document
    .getElementById(HtmlElementIds.SAVE_BUTTON_ID)
    .addEventListener("click", eventHandlers.saveContent, false);
document
    .getElementById(HtmlElementIds.REFRESH_BUTTON_ID)
    .addEventListener("click", eventHandlers.loadContent, false);

function keyDownEventHandler(event: KeyboardEvent): any {
    let e: KeyboardEvent = event || window.event as KeyboardEvent;

    if (e.ctrlKey) {

        // ctrl + S
        if (e.which === 83) {
            return eventHandlers.saveContent(e);
        }

        // ctrl + R
        if (e.which === 82) {
            return eventHandlers.loadContent(e);
        }
    }
}

document.addEventListener("keydown", keyDownEventHandler, false);

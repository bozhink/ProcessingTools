import { IStorageKeys } from "../contracts/models/services.models";
import { IRequesterBase } from "../contracts/http/requester-base";
import { JsonRequester } from "../services/http/json-requester";

import { IEventHandlersFactory, ITemplatesProvider } from "../contracts/services";
import { IDocumentContentData, IHtmlSelectionTagger, IDocumentPreviewEventHandlers } from "../contracts/services.documents";

import { EventHandlersFactory } from "../components/event.handlers.factory"
import { DocumentPreviewEventHandlersFactory } from "../components/event.handlers.document.preview";
import { HtmlSelectionTagger } from "../components/html-selection-tagger";
import { DocumentContentData } from "../services/documents/document-content-data";
import { HandlebarsTemplatesProvider } from "../services/handlebars-templates-provider";
import { CoordinatesToolboxesControl, ICoordinatesToolboxesComponent } from "../components/coordinates-toolboxes";

import { IReporter } from "../contracts/reporters/reporter";
import { ToastrReporter } from "../services/reporters/toastr-reporter";
import { DocumentController } from "../controllers/documents.controllers";
import { ToastrConfiguration } from "../configurations/toastr-config";
import { InteractJSConfiguration } from "../configurations/interact-config";

import { INumberKeyValues } from "../contracts/models/key-value.models";


declare let window: Window;
declare let document: Document;
declare let $: JQueryStatic;
declare let interact: any/*Interact.InteractStatic*/;
declare let toastr: Toastr;

let bibliographyKeyCodes: INumberKeyValues =
{
    // alt + 1
    49: "article-title",

    // alt + 2
    50: "chapter-title",

    // alt + 3
    51: "trans-title",

    // alt + 4
    52: "source",

    // alt + 5
    53: "trans-source",

    // alt + 6
    54: "publisher-name",

    // alt + 7
    55: "publisher-loc",

    // alt + 8
    56: "comment",

    // alt + 9
    57: "person-group",

    // alt + 0
    48: "institution",

    // alt + f1
    112: "volume",

    // alt + f2
    113: "issue",

    // alt + f3
    114: "fpage",

    // alt + f4
    115: "lpage",

    // alt + f5
    116: "size",

    // alt + f6
    117: "elocation-id",

    // alt + f7
    118: "year",

    // alt + f8
    119: "edition",

    // alt + f9
    120: "series"
}

enum HtmlElementIds {
    GET_LINK_ID = "get-link",
    SAVE_LINK_ID = "save-link",
    SAVE_BUTTON_ID = "save-button",
    REFRESH_BUTTON_ID = "refresh-button",
    CONTENT_ELEMENT_ID = "article"
}

let getUrl: string = (document.getElementById(HtmlElementIds.GET_LINK_ID) as HTMLAnchorElement).href;
let saveUrl: string = (document.getElementById(HtmlElementIds.SAVE_LINK_ID) as HTMLAnchorElement).href;

const keys: IStorageKeys = {
    lastGetTimeKey: "LAST_GET_TIME_KEY_PREVIEW",
    lastSavedTimeKey: "LAST_SAVED_TIME_KEY_PREVIEW",
    contentHashKey: "CONTENT_HASH_KEY_PREVIEW"
};

let storage: Storage = window.sessionStorage;
let jsonRequester: IRequesterBase<any> = new JsonRequester($);
let dataService: IDocumentContentData = new DocumentContentData(storage, keys, jsonRequester);
let reporter: IReporter = new ToastrReporter(toastr);
let documentController: DocumentController = new DocumentController(dataService, reporter);

let templatesProvider: ITemplatesProvider = new HandlebarsTemplatesProvider("../../../build/dist/templates");
let coordinatesToolboxes: ICoordinatesToolboxesComponent = CoordinatesToolboxesControl(window, $, templatesProvider);
let eventHandlersFactory: IEventHandlersFactory = new EventHandlersFactory();
let htmlSelectionTagger: IHtmlSelectionTagger = HtmlSelectionTagger(window, document);
let eventHandlers: IDocumentPreviewEventHandlers = DocumentPreviewEventHandlersFactory(window, document, $, eventHandlersFactory, htmlSelectionTagger, coordinatesToolboxes);

ToastrConfiguration.configure(toastr);
InteractJSConfiguration.registerDragabbleBehavior(interact, ".draggable");

let loadContentAction: () => void = documentController.createGetAction(getUrl, false, function (content: string): void {
    let contentHash: string,
        articleElement: HTMLElement = document.getElementById(HtmlElementIds.CONTENT_ELEMENT_ID);
    if (content) {
        articleElement.innerHTML = content;
        dataService.initializeContent(articleElement.innerHTML);
    }
}, function (): void {
    reporter.report("success", "Content is retrieved");
});

let saveContentAction: () => void = documentController.createSaveAction(saveUrl, false, function (): string {
    $("#" + HtmlElementIds.CONTENT_ELEMENT_ID + " .custom-tooltiptext").remove();
    return document.getElementById(HtmlElementIds.CONTENT_ELEMENT_ID).innerHTML;
});

eventHandlers.loadContent = eventHandlersFactory.create(loadContentAction);
eventHandlers.saveContent = eventHandlersFactory.create(saveContentAction);

// fetch content
loadContentAction();

// event handlers
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

        // ctrl + delete
        if (e.which === 46) {
            return eventHandlers.clearTagsInSelection(e);
        }

        // ctrl + B
        if (e.which === 66) {
            return eventHandlers.tagInBold(e);
        }

        // ctrl + I
        if (e.which === 73) {
            return eventHandlers.tagInItalic(e);
        }

        // ctrl + U
        if (e.which === 85) {
            return eventHandlers.tagInUnderline(e);
        }

        // ctrl + M
        if (e.which === 77) {
            return eventHandlers.tagInMonospace(e);
        }
    } else if (e.altKey) {

        // alt + A -> abbrev
        if (e.which === 65) {
            return eventHandlers.tagAbbrev(e);
        }

        // alt + D -> def/p
        if (e.which === 68) {
            return eventHandlers.tagAbbrevDef(e);
        }

        // alt + C -> coordinates
        if (e.which === 67) {
            return eventHandlers.tagCoordinate(e);
        }

        // alt + E -> ext-link
        if (e.which === 69) {
            return eventHandlers.tagLink(e);
        }

        // bibliography
        if (e.which in bibliographyKeyCodes) {
            let elemName: string = bibliographyKeyCodes[e.which];
            let eh: (e: Event) => any = eventHandlersFactory.create(e => htmlSelectionTagger.tagInMark(elemName));
            return eh(e);
        }

    } else {

        // escape
        if (e.which === 27) {
            return eventHandlers.unsetAllInEditMode(e);
        }
    }
}

$("#article")
    .on("dblclick", "p,td,th", eventHandlers.setElementInEditMode)
    .on("blur", "p,td,th", eventHandlers.unsetElementInEditMode);

// events registration
$("#supermenu")
    .on("click", ".mi-bibr", eventHandlers.tagBibliographicCitation)
    .on("click", ".mi-app", eventHandlers.tagAppendicesCitation)
    .on("click", ".mi-suppl-material", eventHandlers.tagSupplMaterialsCitation)
    .on("click", ".mi-tab", eventHandlers.tagTablesCitation)
    .on("click", ".mi-fig", eventHandlers.tagFiguresCitation)
    .on("click", ".mi-move", eventHandlers.moveFloatingObject);

document
    .getElementById(HtmlElementIds.SAVE_BUTTON_ID)
    .addEventListener("click", eventHandlers.saveContent, false);
document
    .getElementById(HtmlElementIds.REFRESH_BUTTON_ID)
    .addEventListener("click", eventHandlers.loadContent, false);
document
    .getElementById("window-coordinates")
    .addEventListener("click", eventHandlers.generateCoordinatesListToolbox, false);
document
    .getElementById("window-map")
    .addEventListener("click", eventHandlers.generateCoordinatesMapToolbox, false);
document
    .getElementById("menu-item-refresh")
    .addEventListener("click", eventHandlers.loadContent, false);
document
    .getElementById("menu-item-tag-link")
    .addEventListener("click", eventHandlers.tagLink, false);
document
    .getElementById("menu-item-tag-coordinate")
    .addEventListener("click", eventHandlers.tagCoordinate, false);
document
    .getElementById("menu-item-bibliography")
    .addEventListener("click", eventHandlers.tagBibliographyElement, false);

document
    .getElementById("tag-bibliographic-citations-menu-item")
    .addEventListener("click", eventHandlers.tagBibliographicCitationMenuClick, false);

document
    .getElementById("tag-appendices-citations-menu-item")
    .addEventListener("click", eventHandlers.tagAppendicesCitationMenuClick, false);

document
    .getElementById("tag-suppl-materials-citations-menu-item")
    .addEventListener("click", eventHandlers.tagSupplMaterialsCitationMenuClick, false);

document
    .getElementById("tag-tables-citations-menu-item")
    .addEventListener("click", eventHandlers.tagTablesCitationMenuClick, false);

document
    .getElementById("tag-figures-citations-menu-item")
    .addEventListener("click", eventHandlers.tagFiguresCitationMenuClick, false);

document
    .getElementById("move-floating-objects")
    .addEventListener("click", eventHandlers.moveFloatingObjectsMenuClick, false);

document
    .addEventListener("keydown", keyDownEventHandler, false);

document
    .getElementById(HtmlElementIds.CONTENT_ELEMENT_ID)
    .addEventListener("mouseover", eventHandlers.mouseoverXref, false);

document
    .getElementById(HtmlElementIds.CONTENT_ELEMENT_ID)
    .addEventListener("mouseout", eventHandlers.mouseoutXref, false);

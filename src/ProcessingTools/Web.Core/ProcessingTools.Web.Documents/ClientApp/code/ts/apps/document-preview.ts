import { IStorageKeys } from "../contracts/models/services.models";
import { IRequesterBase } from "../contracts/http/requester-base";
import { JsonRequester } from "../services/http/json-requester";
import { IDocumentContentData } from "../contracts/documents/document-content-data";
import { DocumentContentData } from "../services/documents/document-content-data";
import { IReporter } from "../contracts/reporters/reporter";
import { ToastrReporter } from "../services/reporters/toastr-reporter";
import { DocumentController } from "../controllers/documents.controllers";
import { ToastrConfiguration } from "../configurations/toastr-config";
import { InteractJSConfiguration } from "../configurations/interact-config";

import { EventHandlerFactory } from "../configurations/event.handlers.configuration";
import {
    DocumentPreviewEventHandlersFactory,
    IDocumentPreviewEventHandlers
} from "../configurations/event.handlers.configuration.document.preview";

import { ITemplatesProvider } from "../contracts/services";
import { HandlebarsTemplatesProvider } from "../services/handlebars-templates-provider";
import { CoordinatesToolboxesControl, ICoordinatesToolboxesComponent } from "../components/coordinates-toolboxes";
import { HtmlSelectionTagger } from "../components/html-selection-tagger";

declare let window: Window;
declare let document: Document;
declare let $: JQueryStatic;
declare let interact: any/*Interact.InteractStatic*/;
declare let toastr: Toastr;

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
let htmlSelectionTagger: HtmlSelectionTagger = new HtmlSelectionTagger(window, document);
let coordinatesToolboxes: ICoordinatesToolboxesComponent = CoordinatesToolboxesControl(window, $, templatesProvider);
let eventHandlerFactory: EventHandlerFactory = new EventHandlerFactory(window);
let eventHandlers: IDocumentPreviewEventHandlers = DocumentPreviewEventHandlersFactory(
    window,
    document,
    $,
    eventHandlerFactory,
    htmlSelectionTagger,
    coordinatesToolboxes);

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

eventHandlers.loadContent = eventHandlerFactory.create(loadContentAction);
eventHandlers.saveContent = eventHandlerFactory.create(saveContentAction);

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
        // alt + 1
        if (e.which === 49) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("article-title");
            return false;
        }

        // alt + 2
        if (e.which === 50) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("chapter-title");
            return false;
        }

        // alt + 3
        if (e.which === 51) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("trans-title");
            return false;
        }

        // alt + 4
        if (e.which === 52) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("source");
            return false;
        }

        // alt + 5
        if (e.which === 53) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("trans-source");
            return false;
        }

        // alt + 6
        if (e.which === 54) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("publisher-name");
            return false;
        }

        // alt + 7
        if (e.which === 55) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("publisher-loc");
            return false;
        }

        // alt + 8
        if (e.which === 56) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("comment");
            return false;
        }

        // alt + 9
        if (e.which === 57) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("person-group");
            return false;
        }

        // alt + 0
        if (e.which === 48) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("institution");
            return false;
        }

        // alt + f1
        if (e.which === 112) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("volume");
            return false;
        }

        // alt + f2
        if (e.which === 113) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("issue");
            return false;
        }

        // alt + f3
        if (e.which === 114) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("fpage");
            return false;
        }

        // alt + f4
        if (e.which === 115) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("lpage");
            return false;
        }

        // alt + f5
        if (e.which === 116) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("size");
            return false;
        }

        // alt + f6
        if (e.which === 117) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("elocation-id");
            return false;
        }

        // alt + f7
        if (e.which === 118) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("year");
            return false;
        }

        // alt + f8
        if (e.which === 119) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("edition");
            return false;
        }

        // alt + f9
        if (e.which === 120) {
            e.stopPropagation();
            e.preventDefault();
            htmlSelectionTagger.tagInMark("series");
            return false;
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

import { IStringKeyValues } from "./models/key-value.models";

export interface IDocumentContentData {
    get(url: string): Promise<any>;
    initializeContent(content: string): void;
    save(url: string, content: string): Promise<any>;
}

export interface IHtmlSelectionTagger {
    clearTagsInSelection: () => void;
    tag: (tagName: string, elemName: string, className?: string, attributes?: IStringKeyValues, callback?: (e: HTMLElement) => void) => void;
    tagLink: () => void;
    tagInSpan: (elemName: string, className?: string) => void;
    tagInMark: (elemName: string, className?: string) => void;
    tagInXref: (rid: string, refType: string) => void;
    tagInBold: () => void;
    tagInItalic: () => void;
    tagInUnderline: () => void;
    tagInMonospace: () => void;
}

export interface IDocumentPreviewEventHandlers {
    tagBibliographicCitation: (e: Event) => any;
    tagAppendicesCitation: (e: Event) => any;
    tagSupplMaterialsCitation: (e: Event) => any;
    tagTablesCitation: (e: Event) => any;
    tagFiguresCitation: (e: Event) => any;
    moveFloatingObject: (e: Event) => any;
    tagLink: (e: Event) => any;
    tagCoordinate: (e: Event) => any;
    tagAbbrev: (e: Event) => any;
    tagAbbrevDef: (e: Event) => any;
    tagBibliographyElement: (e: Event) => any;
    tagBibliographicCitationMenuClick: (e: Event) => any;
    tagAppendicesCitationMenuClick: (e: Event) => any;
    tagSupplMaterialsCitationMenuClick: (e: Event) => any;
    tagTablesCitationMenuClick: (e: Event) => any;
    tagFiguresCitationMenuClick: (e: Event) => any;
    moveFloatingObjectsMenuClick: (e: Event) => any;
    setElementInEditMode: (e: Event) => any;
    unsetElementInEditMode: (e: Event) => any;
    unsetAllInEditMode: (e: Event) => any;
    clearTagsInSelection: (e: Event) => any;
    tagInBold: (e: Event) => any;
    tagInItalic: (e: Event) => any;
    tagInUnderline: (e: Event) => any;
    tagInMonospace: (e: Event) => any;
    mouseoverXref: (e: Event) => any;
    mouseoutXref: (e: Event) => any;
    generateCoordinatesListToolbox: (e: Event) => any;
    generateCoordinatesMapToolbox: (e: Event) => any;
    loadContent?: (e: Event) => any;
    saveContent?: (e: Event) => any;
}

import { IStringKeyValue } from "../models/key-value.models";

export interface IHtmlSelectionTagger {
    clearTagsInSelection: () => void;
    tag: (tagName: string, elemName: string, className?: string, attributes?: IStringKeyValue, callback?: (e: HTMLElement) => void) => void;
    tagLink: () => void;
    tagInSpan: (elemName: string, className?: string) => void;
    tagInMark: (elemName: string, className?: string) => void;
    tagInXref: (rid: string, refType: string) => void;
    tagInBold: () => void;
    tagInItalic: () => void;
    tagInUnderline: () => void;
    tagInMonospace: () => void;
}

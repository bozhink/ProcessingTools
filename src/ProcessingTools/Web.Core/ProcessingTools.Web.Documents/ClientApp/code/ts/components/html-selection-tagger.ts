import { IHtmlSelectionTagger } from "../contracts/services.documents";
import { IStringKeyValues } from "../contracts/models/key-value.models";

export function HtmlSelectionTagger(window: Window, document: Document): IHtmlSelectionTagger {
    if (window == null) {
        throw `HtmlSelectionTagger: window is null!`;
    }

    if (document == null) {
        throw `HtmlSelectionTagger: document is null!`;
    }

    function clearTagsInSelection(): void {
        let selection: Range = window.getSelection().getRangeAt(0),
            selectedText: DocumentFragment = selection.extractContents(),
            span: HTMLElement = document.createElement("span");
        span.appendChild(selectedText);

        span.innerHTML = span.innerHTML.replace(/<\/?[^!<>]+>/g, "");
        span.setAttribute("class", "cleaned-selection");

        selection.insertNode(span);
    };

    function tag(
        tagName: string,
        elemName: string,
        className?: string,
        attributes?: IStringKeyValues,
        callback?: (e: HTMLElement) => void
    ): void {
        let selection: Selection = window.getSelection(),
            setAttributes: boolean = attributes && Array.isArray(attributes),
            len: number = selection.rangeCount;

        for (let i: number = 0; i < len; i += 1) {
            let tagElement: HTMLElement = document.createElement(tagName);
            tagElement.setAttribute("elem-name", elemName);

            if (!className) {
                tagElement.setAttribute("class", elemName);
            } else {
                tagElement.setAttribute("class", className);
            }

            if (setAttributes) {
                let attribute: string;
                for (attribute in attributes) {
                    if (attribute) {
                        tagElement.setAttribute(attribute, attributes[attribute]);
                    }
                }
            }

            let range: Range = selection.getRangeAt(i);
            let selectedText: DocumentFragment = range.extractContents();
            tagElement.appendChild(selectedText);

            if (callback) {
                callback(tagElement);
            }

            range.insertNode(tagElement);
        }
    }

    function tagLink(): void {
        tag("a", "ext-link", "ext-link", {
            "target": "_blank",
            "type": "simple",
            "xlink:type": "simple",
            "xmlns:xlink": "http://www.w3.org/1999/xlink",
            "ext-link-type": "uri"
        }, function (tagElement: HTMLElement): void {
            let href: string = tagElement.innerText.trim();
            tagElement.setAttribute("href", href);
            tagElement.setAttribute("xlink:href", href);
        });
    }

    function tagInSpan(elemName: string, className?: string): void {
        tag("span", elemName, className);
    }

    function tagInMark(elemName: string, className?: string): void {
        tag("mark", elemName, className);
    }

    function tagInXref(rid: string, refType: string): void {
        let elemName: string = "xref",
            className: string = elemName + " " + refType,
            attributes: { [name: string]: string } = {
                "rid": rid,
                "ref-type": refType,
                "href": "#" + rid
            };

        tag("a", elemName, className, attributes);
    }

    function tagInBold(): void {
        tag("b", "bold", "bold");
    }

    function tagInItalic(): void {
        tag("i", "italic", "italic");
    }

    function tagInUnderline(): void {
        tag("u", "underline", "underline");
    }

    function tagInMonospace(): void {
        tag("kbd", "monospace", "monospace");
    }

    return {
        clearTagsInSelection: clearTagsInSelection,
        tag: tag,
        tagInBold: tagInBold,
        tagInItalic: tagInItalic,
        tagInMark: tagInMark,
        tagInMonospace: tagInMonospace,
        tagInSpan: tagInSpan,
        tagInUnderline: tagInUnderline,
        tagInXref: tagInXref,
        tagLink: tagLink
    };
}

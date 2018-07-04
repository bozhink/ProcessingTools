export class HtmlSelectionTagger {

    public constructor(private readonly window: Window, private readonly document: Document) {
    }

    public clearTagsInSelection(): void {
        var selection: Range = this.window.getSelection().getRangeAt(0),
            selectedText: DocumentFragment = selection.extractContents(),
            span: HTMLElement = this.document.createElement("span");
        span.appendChild(selectedText);

        span.innerHTML = span.innerHTML.replace(/<\/?[^!<>]+>/g, "");
        span.setAttribute("class", "cleaned-selection");

        selection.insertNode(span);
    }

    public tag(
        tagName: string,
        elemName: string,
        className?: string,
        attributes?: { [name: string]: string },
        callback?: (e: HTMLElement) => void
    ): void {
        let selection: Selection = this.window.getSelection();
        let setAttributes: boolean = attributes && Array.isArray(attributes);
        let len: number = selection.rangeCount;
        for (let i: number = 0; i < len; i += 1) {
            let tagElement: HTMLElement = this.document.createElement(tagName);
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

    public tagLink(): void {
        this.tag("a", "ext-link", "ext-link", {
            "target": "_blank",
            "type": "simple",
            "xlink:type": "simple",
            "xmlns:xlink": "http://www.w3.org/1999/xlink",
            "ext-link-type": "uri"
        }, function (tagElement: HTMLElement): void {
            var href: string = tagElement.innerText.trim();
            tagElement.setAttribute("href", href);
            tagElement.setAttribute("xlink:href", href);
        });
    }

    public tagInSpan(elemName: string, className?: string): void {
        this.tag("span", elemName, className);
    }

    public tagInMark(elemName: string, className?: string): void {
        this.tag("mark", elemName, className);
    }

    public tagInXref(rid: string, refType: string): void {
        var elemName: string = "xref",
            className: string = elemName + " " + refType,
            attributes: { [name: string]: string } = {
                "rid": rid,
                "ref-type": refType,
                "href": "#" + rid
            };

        this.tag("a", elemName, className, attributes);
    }

    public tagInBold(): void {
        this.tag("b", "bold", "bold");
    }

    public tagInItalic(): void {
        this.tag("i", "italic", "italic");
    }

    public tagInUnderline(): void {
        this.tag("u", "underline", "underline");
    }

    public tagInMonospace(): void {
        this.tag("kbd", "monospace", "monospace");
    }
}

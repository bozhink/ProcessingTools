import { IEventHandlersFactory } from "../contracts/services";
import { IHtmlSelectionTagger, IDocumentPreviewEventHandlers } from "../contracts/services.documents";
import { ICoordinatesToolboxesComponent } from "../components/coordinates-toolboxes";

const MAIN_ASIDE_ID: string = "aside-main-box";
const ARTICLE_FIGS_AND_TABLES: string = "article_figs_and_tables";

export function DocumentPreviewEventHandlersFactory(
    window: Window,
    document: Document,
    $: JQueryStatic,
    factory: IEventHandlersFactory,
    tagger: IHtmlSelectionTagger,
    coordinatesToolboxes: ICoordinatesToolboxesComponent
): IDocumentPreviewEventHandlers {
    function createBalloon(event: Event, contentSelector?: string): void {
        let e: any = event || window.event,
            rid: string = (e.target as HTMLElement).getAttribute("href"),
            $contentNode: JQuery = $(rid),
            $aside: JQuery = $("#" + MAIN_ASIDE_ID),
            $balloon: JQuery,
            content: string;

        contentSelector = contentSelector || "";
        if (contentSelector === "") {
            content = $contentNode.text();
        } else {
            content = $contentNode.find(contentSelector).text();
        }

        $aside.find(".balloon").remove();

        $balloon = $("<div>")
            .attr("role", "balloon")
            .addClass("balloon")
            .text(content)
            .css({
                "top": (e.clientY + 10) + "px",
                "left": (e.clientX - 20) + "px"
            });

        $balloon.appendTo($aside);
    }

    function removeBalloon(): void {
        let $aside: JQuery = $("#" + MAIN_ASIDE_ID);
        $aside.find(".balloon").remove();
    }

    // http://stackoverflow.com/questions/7215479/get-parent-element-of-a-selected-text
    function getSelectionParentElement(): Node {
        let parentElement: Node = null, selection: Selection;
        if (window.getSelection) {
            selection = window.getSelection();
            if (selection.rangeCount) {
                parentElement = selection.getRangeAt(0).commonAncestorContainer;
                if (parentElement.nodeType !== 1) {
                    parentElement = parentElement.parentNode;
                }
            }
        } else if ((selection = (document as any).selection) && selection.type !== "Control") {
            parentElement = (selection as any).createRange().parentElement();
        }

        return parentElement;
    }

    function actionsMenuFactory(menuName: string, callback: (e: JQuery) => void): void {
        let $aside: JQuery = $("#" + MAIN_ASIDE_ID),
            $supermenu: JQuery = $("#supermenu"),
            $menu: JQuery;

        $("body").removeClass("move-anchor");

        // remove notification
        $("#manual-mode-notifier").remove();
        $("<div>")
            .addClass("mode-notifier")
            .attr("id", "manual-mode-notifier")
            .text(menuName)
            .appendTo($aside);

        // create menu
        $(".manual-tag-menu").remove();
        $menu = $("<menu>")
            .addClass("manual-tag-menu")
            .attr("label", menuName);

        // populate the menu
        callback($menu);

        // attach the menu to DOM
        $menu.appendTo($supermenu);
    }

    return {
        tagBibliographicCitation: factory.create(function (e: Event): any {
            let rid: string = (e.target as HTMLElement).getAttribute("rid");
            tagger.tagInXref(rid, "bibr");
        }),

        tagAppendicesCitation: factory.create(function (e: Event): any {
            let rid: string = (e.target as HTMLElement).getAttribute("rid");
            tagger.tagInXref(rid, "app");
        }),

        tagSupplMaterialsCitation: factory.create(function (e: Event): any {
            let rid: string = (e.target as HTMLElement).getAttribute("rid");
            tagger.tagInXref(rid, "supplementary-material");
        }),

        tagTablesCitation: factory.create(function (e: Event): any {
            let rid: string = (e.target as HTMLElement).getAttribute("rid");
            tagger.tagInXref(rid, "table");
        }),

        tagFiguresCitation: factory.create(function (e: Event): any {
            let rid: string = (e.target as HTMLElement).getAttribute("rid");
            tagger.tagInXref(rid, "fig");
        }),

        moveFloatingObject: factory.create(function (e: Event): any {
            let $selection: JQuery,
                $anchorElement: JQuery,
                rid: string = (e.target as HTMLElement).getAttribute("rid");

            if (!rid) {
                return false;
            }

            function getAnchorElement($selection: JQuery, closestSelector: string | Element | JQuery<HTMLElement>): JQuery | null {
                let $anchor: JQuery;

                if (!$selection || !closestSelector) {
                    return null;
                }

                try {
                    $anchor = $selection.closest(closestSelector);
                    if ($anchor.length > 0 && $anchor.closest("." + ARTICLE_FIGS_AND_TABLES).length < 1) {
                        return $anchor;
                    }
                } catch (ex) {
                    // skip
                }

                return null;
            }

            try {
                $selection = $(getSelectionParentElement()) as JQuery;

                $anchorElement = getAnchorElement($selection, ".table-wrap") ||
                    getAnchorElement($selection, "figure.fig") ||
                    getAnchorElement($selection, ".title") ||
                    getAnchorElement($selection, "p.p") ||
                    null;

                if ($anchorElement != null) {
                    $anchorElement.after($("#" + rid));
                    $(e.target).remove();
                }
            } catch (ex) {
                // skip
            }

            return false;
        }),

        tagLink: factory.create(function (): void {
            tagger.tagLink();
        }),

        tagCoordinate: factory.create(function (): void {
            tagger.tagInSpan("locality-coordinates");
        }),

        tagAbbrev: factory.create(function (): void {
            tagger.tag("abbr", "abbrev");
        }),

        tagAbbrevDef: factory.create(function (): void {
            tagger.tagInSpan("p");
            tagger.tagInSpan("def");
        }),

        tagBibliographyElement: factory.create(function (e: Event): any {
            let elementName: string = (e.target as HTMLElement).id.substr(10);
            tagger.tagInMark(elementName);
        }),

        tagBibliographicCitationMenuClick: factory.create(function (e: Event): any {
            let $target: JQuery = $(e.target as HTMLElement);
            actionsMenuFactory($target.text(), function ($menu: JQuery): void {
                $("[elem-name=ref]").each(function (i: number, element: HTMLElement): void {
                    let $element: JQuery = $(element);
                    $("<menuitem>")
                        .addClass("mi-bibr")
                        .attr("id", "ref-" + i)
                        .attr("rid", $element.attr("id"))
                        .attr("label", $element.text().trim())
                        .appendTo($menu);
                });
            });
        }),

        tagAppendicesCitationMenuClick: factory.create(function (e: Event): any {
            let $target: JQuery = $(e.target as HTMLElement);
            actionsMenuFactory($target.text(), function ($menu: JQuery): void {
                $("[elem-name=app]").each(function (i: number, element: HTMLElement): void {
                    let $element: JQuery = $(element);
                    $("<menuitem>")
                        .addClass("mi-app")
                        .attr("id", "app-" + i)
                        .attr("rid", $element.attr("id"))
                        .attr("label", $element.find(".title").text().trim())
                        .appendTo($menu);
                });
            });
        }),

        tagSupplMaterialsCitationMenuClick: factory.create(function (e: Event): any {
            let $target: JQuery = $(e.target as HTMLElement);
            actionsMenuFactory($target.text(), function ($menu: JQuery): void {
                $("[elem-name=supplementary-material]").each(function (i: number, element: HTMLElement): void {
                    let $element: JQuery = $(element);
                    $("<menuitem>")
                        .addClass("mi-suppl-material")
                        .attr("id", "suppl-material-" + i)
                        .attr("rid", $element.attr("id"))
                        .attr("label", $element.find(".label").text().trim())
                        .appendTo($menu);
                });
            });
        }),

        tagTablesCitationMenuClick: factory.create(function (e: Event): any {
            let $target: JQuery = $(e.target as HTMLElement);
            actionsMenuFactory($target.text(), function ($menu: JQuery): void {
                $("[elem-name=table-wrap]").each(function (i: number, element: HTMLElement): void {
                    let $element: JQuery = $(element);
                    $("<menuitem>")
                        .addClass("mi-tab")
                        .attr("id", "tab-" + i)
                        .attr("rid", $element.attr("id"))
                        .attr("label", $element.find(".label").text().trim())
                        .appendTo($menu);
                });
            });
        }),

        tagFiguresCitationMenuClick: factory.create(function (e: Event): any {
            let $target: JQuery = $(e.target as HTMLElement);
            actionsMenuFactory($target.text(), function ($menu: JQuery): void {
                $("[elem-name=fig]").each(function (i: number, element: HTMLElement): void {
                    let $element: JQuery = $(element);
                    $("<menuitem>")
                        .addClass("mi-fig")
                        .attr("id", "fig-" + i)
                        .attr("rid", $element.attr("id"))
                        .attr("label", $element.find(".label").text().trim())
                        .appendTo($menu);
                });
            });
        }),

        moveFloatingObjectsMenuClick: factory.create(function (e: Event): any {
            let $target: JQuery = $(e.target as HTMLElement);
            actionsMenuFactory($target.text(), function ($menu: JQuery): void {
                let $articleFigsAndTables: JQuery = $("." + ARTICLE_FIGS_AND_TABLES);

                $articleFigsAndTables.find("[elem-name=fig]").each(function (i: number, element: HTMLElement): void {
                    let $element: JQuery = $(element);
                    $("<menuitem>")
                        .addClass("mi-move")
                        .attr("id", "mi-move-fig-" + i)
                        .attr("rid", $element.attr("id"))
                        .attr("label", $element.find(".label").text().trim())
                        .appendTo($menu);
                });

                $articleFigsAndTables.find("[elem-name=table-wrap]").each(function (i: number, element: HTMLElement): void {
                    let $element: JQuery = $(element);
                    $("<menuitem>")
                        .addClass("mi-move")
                        .attr("id", "mi-move-table-" + i)
                        .attr("rid", $element.attr("id"))
                        .attr("label", $element.find(".label").text().trim())
                        .appendTo($menu);
                });

                $("body").addClass("move-anchor");
            });
        }),

        setElementInEditMode: function (event: Event): any {
            let e: Event = event || window.event,
                $target: JQuery = $(e.target as HTMLElement),
                name: string = $target.prop("nodeName").toLowerCase();

            if (name === "p" || name === "td" || name === "th") {
                e.stopPropagation();
                e.preventDefault();
                $target
                    .attr("contenteditable", "")
                    .addClass("in-edit");
            }
        },

        unsetElementInEditMode: function (event: Event): any {
            let e: Event = event || window.event,
                $target: JQuery = $(e.target as HTMLElement),
                name: string = $target.prop("nodeName").toLowerCase();

            if (name === "p" || name === "td" || name === "th") {
                e.stopPropagation();
                e.preventDefault();
                $target
                    .removeAttr("contenteditable")
                    .removeClass("in-edit");
            }
        },

        unsetAllInEditMode: function (event: Event): any {
            let e: Event = event || window.event;
            e.stopPropagation();
            e.preventDefault();
            $(".in-edit")
                .removeAttr("contenteditable")
                .removeClass("in-edit");
        },

        clearTagsInSelection: factory.create(tagger.clearTagsInSelection),

        tagInBold: factory.create(tagger.tagInBold),

        tagInItalic: factory.create(tagger.tagInItalic),

        tagInUnderline: factory.create(tagger.tagInUnderline),

        tagInMonospace: factory.create(tagger.tagInMonospace),

        mouseoverXref: function (event: Event): any {
            let e: Event = event || window.event,
                target: HTMLElement = e.target as HTMLElement;

            if (target.classList.contains("xref")) {
                e.stopPropagation();
                e.preventDefault();

                if (target.classList.contains("bibr")) {
                    createBalloon(e);
                }

                if (target.classList.contains("aff")) {
                    createBalloon(e, ".addr-line");
                }

                if (target.classList.contains("fig")) {
                    createBalloon(e, ".caption");
                }

                if (target.classList.contains("table")) {
                    createBalloon(e, ".caption");
                }

                return false;
            }
        },

        mouseoutXref: function (event: Event): any {
            let e: Event = event || window.event,
                target: HTMLElement = e.target as HTMLElement;
            if (target.classList.contains("xref")) {
                e.stopPropagation();
                e.preventDefault();

                removeBalloon();

                return false;
            }
        },

        generateCoordinatesListToolbox: factory.create(function (): void {
            coordinatesToolboxes.generateCoordinatesListToolbox("#" + MAIN_ASIDE_ID);
        }),

        generateCoordinatesMapToolbox: factory.create(function (): void {
            coordinatesToolboxes.generateCoordinatesMapToolbox("#" + MAIN_ASIDE_ID);
        })
    };
}

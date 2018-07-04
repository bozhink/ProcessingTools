import * as leaflet from "leaflet";
import { ITemplatesProvider } from "../contracts/services";

interface ICoordinate {
    id?: string;
    index?: number;
    latitude: number;
    longitude: number;
}

interface ICoordinatesToolboxViewModel {
    title: string;
    coordinates: Array<ICoordinate>;
}

export interface ICoordinatesToolboxesComponent {
    generateCoordinatesListToolbox: (selector: string) => void;
    generateCoordinatesMapToolbox: (selector: string) => void;
}

export function CoordinatesToolboxesControl(
    window: Window,
    $: JQueryStatic,
    templatesProvider: ITemplatesProvider
): ICoordinatesToolboxesComponent {

    function clickMinimizeButtonEventHandler(event: Event): any {
        var e: Event = event || window.event,
            target: HTMLElement = e.target as HTMLElement,
            toolbox: HTMLElement,
            body: HTMLElement;

        e.stopPropagation();
        e.preventDefault();

        if (target) {
            try {
                toolbox = target.parentNode.parentNode.parentNode as HTMLElement;
                if (toolbox instanceof HTMLElement) {
                    body = toolbox.querySelector(".panel-body");
                }

                if (body instanceof HTMLElement) {
                    body.style.display = "none";
                    toolbox.style.height = "60px";
                }
            } catch (ex) {
                // skip
            }
        }
    }

    function clickMaximizeButtonEventHandler(event: Event): any {
        var e: Event = event || window.event,
            target: HTMLElement = e.target as HTMLElement,
            toolbox: HTMLElement,
            body: HTMLElement;

        e.stopPropagation();
        e.preventDefault();

        if (target) {
            try {
                toolbox = target.parentNode.parentNode.parentNode as HTMLElement;
                if (toolbox instanceof HTMLElement) {
                    body = toolbox.querySelector(".panel-body");
                }

                if (body instanceof HTMLElement) {
                    body.style.display = "block";
                    toolbox.style.height = "400px";
                }
            } catch (ex) {
                // skip
            }
        }
    }

    function clickCloseButtonEventHandler(event: Event): any {
        var e: Event = event || window.event,
            target: HTMLElement = e.target as HTMLElement,
            toolbox: HTMLElement;

        e.stopPropagation();
        e.preventDefault();

        if (target) {
            try {
                toolbox = target.parentNode.parentNode.parentNode as HTMLElement;
                if (toolbox instanceof HTMLElement) {
                    toolbox.parentNode.removeChild(toolbox);
                    document.body.style.cursor = "auto";
                }
            } catch (ex) {
                // skip
            }
        }
    }

    function listAnchorClickEventHandler(event: Event): any {
        const TEXT_TO_SCROLL_CLASS_NAME: string = "selected-text-to-scroll";
        var e: Event = event || window.event,
            target: HTMLElement = e.target as HTMLElement,
            href: string,
            $target: JQuery;

        e.stopPropagation();
        e.preventDefault();

        if (!target) {
            return false;
        }

        if (target.classList.contains("coordinate-item")) {
            href = target.getAttribute("href");
            $target = $(href);

            $("html, body").animate({
                scrollTop: $target.offset().top - 250 + "px"
            }, "fast");

            $target.addClass(TEXT_TO_SCROLL_CLASS_NAME);

            setTimeout(function (): void {
                $target.removeClass(TEXT_TO_SCROLL_CLASS_NAME);
            }, 1500);

            return false;
        }
    }

    function getCoordinates(): Array<ICoordinate> {
        var result: Array<ICoordinate> = [];
        $(".named-content.geo-json").each(function (i: number, element: HTMLElement): void {
            var $this: JQuery = $(element),
                id: string = $this.attr("id"),
                coordinates: Array<number> = JSON.parse($this.attr("specific-use")).coordinates;

            result.push({
                id: id,
                index: i,
                latitude: coordinates[1],
                longitude: coordinates[0]
            });
        });

        return result;
    }

    function generateCoordinatesListToolbox(selector: string): void {
        var toolboxSelector: string = "#coordinates-list-toolbox",
            $aside: JQuery = $(selector),
            toolbox: ICoordinatesToolboxViewModel = {
                title: "Coordinates",
                coordinates: getCoordinates()
            };

        // remove all coordinates list toolboxes yet present.
        $(toolboxSelector).remove();

        templatesProvider.get("coordinates-toolbox")
            .then(function (template: (vm: any) => string): void {
                $("<div>")
                    .html(template(toolbox))
                    .appendTo($aside);
            })
            .then(function (): void {
                $(toolboxSelector + " .panel-body .coordinates-list").on("click", listAnchorClickEventHandler);

                $(toolboxSelector + " .minimize-button").on("click", clickMinimizeButtonEventHandler);
                $(toolboxSelector + " .maximize-button").on("click", clickMaximizeButtonEventHandler);
                $(toolboxSelector + " .close-button").on("click", clickCloseButtonEventHandler);
            });
    }

    function generateCoordinatesMapToolbox(selector: string): void {
        var toolboxSelector: string = "#coordinates-map-toolbox",
            $aside: JQuery = $(selector),
            toolbox: ICoordinatesToolboxViewModel = {
                title: "Map",
                coordinates: getCoordinates()
            };

        // remove all coordinates list toolboxes yet present.
        $(toolboxSelector).remove();

        templatesProvider.get("coordinates-map")
            .then(function (template: (vm: any) => string): void {
                let coordinates: Array<ICoordinate> = toolbox.coordinates;
                let $div: JQuery = $("<div>");

                $div.html(template({
                    title: toolbox.title
                })).appendTo($aside);

                let map: leaflet.Map = leaflet.map("coordinates-map", {
                    center: [0.0, 0.0],
                    zoom: 0,
                    worldCopyJump: true
                });

                leaflet.tileLayer("http://{s}.tile.osm.org/{z}/{x}/{y}.png", {
                    attribution: `&copy; <a href="http://osm.org/copyright" target="_blank">OpenStreetMap</a> contributors`
                }).addTo(map);

                let len: number = coordinates.length;
                for (let i: number = 0; i < len; i += 1) {
                    let coordinate: leaflet.LatLng = new leaflet.LatLng(coordinates[i].latitude, coordinates[i].longitude);
                    leaflet.marker(coordinate)
                        .bindPopup(`<a class="coordinate-item" href="#${coordinates[i].id}">${JSON.stringify(coordinate)}</a>`)
                        .addTo(map);
                }
            })
            .then(function (): void {
                $(toolboxSelector).on("click", ".coordinate-item", listAnchorClickEventHandler);

                $(toolboxSelector + " .minimize-button").on("click", clickMinimizeButtonEventHandler);
                $(toolboxSelector + " .maximize-button").on("click", clickMaximizeButtonEventHandler);
                $(toolboxSelector + " .close-button").on("click", clickCloseButtonEventHandler);
            });
    }

    return {
        generateCoordinatesListToolbox,
        generateCoordinatesMapToolbox
    };
}

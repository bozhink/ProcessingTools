export class InteractJSConfiguration {

    private static resizeMoveEventHandler(event: Event): any {
        let e: any = event || window.event,
            target: HTMLElement = e.target as HTMLElement,
            x: number = (parseFloat(target.getAttribute("data-x")) || 0),
            y: number = (parseFloat(target.getAttribute("data-y")) || 0);

        // update the element's style
        target.style.width = e.rect.width + "px";
        target.style.height = e.rect.height + "px";

        // translate when resizing from top or left edges
        x += e.deltaRect.left;
        y += e.deltaRect.top;

        target.style.webkitTransform = target.style.transform = "translate(" + x + "px," + y + "px)";

        target.setAttribute("data-x", x.toString());
        target.setAttribute("data-y", y.toString());

        // target.textContent = Math.round(e.rect.width) + "×" + Math.round(e.rect.height);
    }

    private static dragEndEventHandler(event: Event): any {
        // let textEl = event.target.querySelector("p");

        // textEl && (textEl.textContent =
        //   "moved a distance of "
        //   + (Math.sqrt(event.dx * event.dx +
        //               event.dy * event.dy) | 0) + "px");
    }

    private static dragMoveEventHandler(event: Event): any {
        let e: any = event || window.event,
            target: HTMLElement = e.target as HTMLElement,

            // keep the dragged position in the data-x/data-y attributes
            x: number = (parseFloat(target.getAttribute("data-x")) || 0) + e.dx,
            y: number = (parseFloat(target.getAttribute("data-y")) || 0) + e.dy;

        // translate the element
        target.style.webkitTransform = target.style.transform = "translate(" + x + "px, " + y + "px)";

        // update the position attributes
        target.setAttribute("data-x", x.toString());
        target.setAttribute("data-y", y.toString());
    }

    public static registerDragabbleBehavior(interact: any/*Interact.InteractStatic*/, selector: string | HTMLElement): any {
        interact(selector as string)
            .draggable({
                inertia: true,
                restrict: {
                    /*restriction: "parent",*/
                    endOnly: true,
                    elementRect: {
                        top: 0,
                        left: 0,
                        bottom: 1,
                        right: 1
                    }
                },
                autoScroll: true,
                onmove: InteractJSConfiguration.dragMoveEventHandler,
                onend: InteractJSConfiguration.dragEndEventHandler
            })
            .resizable({
                preserveAspectRatio: false,
                edges: {
                    left: true,
                    right: true,
                    bottom: true,
                    top: true
                }
            })
            .on("resizemove", InteractJSConfiguration.resizeMoveEventHandler);
    }
}

export class EventHandlerFactory {
    public constructor(private readonly window: Window) {
    }

    public create(callback: (e: Event) => any): (e: Event) => any {
        if (!callback) {
            throw `Callback function is required`;
        }

        if (typeof callback !== "function") {
            throw `Callback should be a valid function`;
        }

        let self: EventHandlerFactory = this;

        return function (event: Event): any {
            var e: Event = event || self.window.event;
            e.stopPropagation();
            e.preventDefault();
            return callback(e) || false;
        };
    }
}

export interface IKeyCombinationCallbacks {
    name?: string;
    keyCodes: Array<number>;
    keydown: () => any;
    keyup: () => any;
}

export class KeyCombination {
    private combinations: Array<IKeyCombinationCallbacks> = [];
    private pressedKeyCodes: Array<number> = [];
    private lastKeyupCallback: any;

    public constructor(private readonly window: Window) {
    }

    private activate(): void {
        $(document).keydown(this.keydownEventHandler);
        $(document).keyup(this.keyupEventHandler);
    }

    private keydownEventHandler(event: any): void {
        let self: KeyCombination = this;
        let e: KeyboardEvent = (event || self.window.event) as KeyboardEvent;

        if (self.pressedKeyCodes.indexOf(e.keyCode) < 0) {
            self.pressedKeyCodes.push(e.keyCode);

            let numberOfKeyCodes: number = self.pressedKeyCodes.length;
            for (let i: number = 0; i < numberOfKeyCodes; i += 1) {
                let numberOfCombinations: number = self.combinations.length;
                for (let k: number = 0; k < numberOfCombinations; k += 1) {
                    if (self.combinations[k].keyCodes[i] === self.pressedKeyCodes[i]) {
                        if (self.combinations[k].keyCodes.toString() === self.pressedKeyCodes.toString()) {
                            e.preventDefault();
                            self.pressedKeyCodes.length = 0;
                            self.lastKeyupCallback = self.combinations[k].keyup;
                            self.combinations[k].keydown();
                        }

                        break;
                    }
                }
            }
        }
    }

    private keyupEventHandler(): void {
        let self: KeyCombination = this;
        self.pressedKeyCodes.length = 0;
        if (self.lastKeyupCallback != null) {
            self.lastKeyupCallback();
        }
    }

    public setCallbackToCombination(combinations: Array<IKeyCombinationCallbacks>): void {
        let self: KeyCombination = this;
        let found: boolean = false;

        let numberOfNewCombinations: number = combinations.length;
        for (let k: number = 0; k < numberOfNewCombinations; k += 1) {
            found = false;

            let numberOfCombinations: number = self.combinations.length;
            for (let i: number = 0; i < numberOfCombinations; i += 1) {
                if (self.combinations[i].keyCodes.toString() === combinations[k].keyCodes.toString()) {
                    self.combinations[i].keydown = combinations[k].keydown;
                    found = true;
                }
            }

            if (!found) {
                self.combinations.push(combinations[k]);
            }
        }

        this.activate();
    }
}

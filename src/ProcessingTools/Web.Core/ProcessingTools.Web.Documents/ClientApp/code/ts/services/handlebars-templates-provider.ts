import { ITemplatesProvider } from "../contracts/services";

export interface IHandlebars {
    compile: (c: string) => (vm: any) => string;
}

export class HandlebarsTemplatesProvider implements ITemplatesProvider {

    private readonly $: JQueryStatic;
    private readonly handlebars: IHandlebars;
    private readonly baseAddress: string;
    private readonly extension: string;
    private readonly cache: { [name: string]: (x: any) => string } = {};

    public constructor($: JQueryStatic, handlebars: IHandlebars, baseAddress: string, extension?: string) {
        if (!$) {
            throw `JQuery is null`;
        }

        if (!handlebars) {
            throw `Handlebars is required`;
        }

        if (!baseAddress) {
            throw `BaseAddress is null`;
        }

        if (!extension) {
            throw `Extension is null`;
        }

        this.$ = $;
        this.handlebars = handlebars;
        this.baseAddress = baseAddress || "~/templates";
        this.extension = extension || "handlebars";
    }

    public get(name: string): Promise<(vm: any) => string> {
        let self: HandlebarsTemplatesProvider = this;
        return new Promise(function (resolve: (value?: (vm: any) => string) => void, reject: (reason?: any) => void): void {
            let url: string = `${self.baseAddress}/${name}.${self.extension}`;

            try {
                if (self.cache[name]) {
                    resolve(self.cache[name]);
                    return;
                }

                self.$.get(url, function (html: string): void {
                    let template: (vm: any) => string = self.handlebars.compile(html);
                    self.cache[name] = template;
                    resolve(template);
                });
            } catch (e) {
                reject(e);
            }
        });
    }
}

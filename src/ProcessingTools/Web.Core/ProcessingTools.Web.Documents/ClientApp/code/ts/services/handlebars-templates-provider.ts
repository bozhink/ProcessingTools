import { ITemplatesProvider } from "../contracts/services";

declare let $: JQueryStatic;

export class HandlebarsTemplatesProvider implements ITemplatesProvider {

    private readonly baseAddress: string;
    private readonly extension: string;
    private readonly cache: { [name: string]: (x: any) => string } = {};

    public constructor(baseAddress: string, extension?: string) {
        if (!baseAddress) {
            throw `BaseAddress is null`;
        }

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

                $.get(url, function (html: string): void {
                    let template: (vm: any) => string = Handlebars.compile(html);
                    self.cache[name] = template;
                    resolve(template);
                });
            } catch (e) {
                reject(e);
            }
        });
    }
}

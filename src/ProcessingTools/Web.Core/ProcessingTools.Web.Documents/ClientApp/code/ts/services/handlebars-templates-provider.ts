import { ITemplatesProvider } from "../contracts/services";

export class HandlebarsTemplatesProvider implements ITemplatesProvider {

    private readonly $: JQueryStatic;
    private readonly handlebars: { compile: (x: string) => string };
    private readonly baseAddress: string;
    private readonly extension: string;
    private readonly cache: { [name: string]: string } = {};


    public constructor($: JQueryStatic, handlebars: { compile: (x: string) => string }, baseAddress: string, extension: string) {
        if (!$) {
            throw `JQuery is null`;
        }

        if (!handlebars) {
            throw `Handlebars is null`;
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

    public async get(name: string): Promise<string> {
        let self: HandlebarsTemplatesProvider = this;
        if (self.cache[name]) {
            return self.cache[name];
        }

        let url: string = `${self.baseAddress}/${name}.${self.extension}`;

        try {
            let html: string = await self.$.get(url);
            let template: string = self.handlebars.compile(html);
            self.cache[name] = template;

            return template;

        } catch (e) {
            return "";
        }
    }
}
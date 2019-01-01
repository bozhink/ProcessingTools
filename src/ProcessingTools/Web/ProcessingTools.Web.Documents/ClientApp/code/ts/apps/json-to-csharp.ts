import { JsonToCSharpConverter } from "./json-to-csharp-core";

declare var $: JQueryStatic;

$("#btn-generate").on("click", function (): void {
    let text: string = $("#text-content").val() as string;
    let $outputClasses: JQuery = $("#csharp-classes");
    try {
        let obj: any = JSON.parse(text);
        let converter: JsonToCSharpConverter = new JsonToCSharpConverter();
        let resultContent: string = converter.generateCSharpClasses(obj);
        $outputClasses.val(resultContent);
    } catch (e) {
        $outputClasses.val(e);
    }
});

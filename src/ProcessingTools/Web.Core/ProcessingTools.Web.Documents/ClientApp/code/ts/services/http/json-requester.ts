import { HttpMethod, IRequesterBase, IRequestModel } from "../../contracts/http/requester-base";
import { ContentType, IJsonRequester } from "../../contracts/http/json-requester";

export class JsonRequester implements IJsonRequester<any>, IRequesterBase<any> {
    private jQuery: JQueryStatic;

    public constructor(jQuery: JQueryStatic) {
        this.jQuery = jQuery;
    }

    public send(method: (string | HttpMethod), url: string, options?: IRequestModel): Promise<any> {
        var headers: any, data: any, promise: Promise<any>, $: JQueryStatic = this.jQuery;

        if (!url) {
            throw "URL is required";
        }

        if (!method) {
            throw "Method is not specified";
        }

        method = method.toString().toUpperCase();
        options = options || {} as IRequestModel;
        headers = options.headers || {};
        data = options.data || {};

        promise = new Promise(function (resolve: (value?: any) => void, reject: (reason?: any) => void): void {
            $.ajax({
                url: url,
                method: method,
                contentType: ContentType,
                headers: headers,
                data: JSON.stringify(data),
                success: function (response: any): any {
                    resolve(response);
                },
                error: function (error: any): any {
                    reject(error);
                }
            });
        });

        return promise;
    }

    public delete(url: string, options?: IRequestModel): Promise<any> {
        return this.send(HttpMethod.DELETE, url, options);
    }

    public get(url: string, options?: IRequestModel): Promise<any> {
        return this.send(HttpMethod.GET, url, options);
    }

    public head(url: string, options?: IRequestModel): Promise<any> {
        return this.send(HttpMethod.HEAD, url, options);
    }

    public patch(url: string, options?: IRequestModel): Promise<any> {
        return this.send(HttpMethod.PATCH, url, options);
    }

    public post(url: string, options?: IRequestModel): Promise<any> {
        return this.send(HttpMethod.POST, url, options);
    }

    public put(url: string, options?: IRequestModel): Promise<any> {
        return this.send(HttpMethod.PUT, url, options);
    }
}

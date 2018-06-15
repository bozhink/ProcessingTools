import { HttpMethod, IRequesterBase, IRequestModel } from "../../contracts/http/requester-base";
import { ContentType, IJsonRequester } from "../../contracts/http/json-requester";

export class JsonRequester implements IJsonRequester<any> {
    private jQuery: JQueryStatic;

    public constructor(jQuery: JQueryStatic) {
        this.jQuery = jQuery;
    }

    public send(method: (string | HttpMethod), url: string, options?: IRequestModel): Promise<any> {
        var headers: any, data: any;

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

        return this.jQuery.ajax({
            url: url,
            method: method,
            contentType: ContentType,
            headers: headers,
            data: JSON.stringify(data)
        });
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

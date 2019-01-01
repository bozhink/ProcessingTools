import { HttpMethod, IRequesterBase, IRequestModel, INgRequestModel } from "../../contracts/http/requester-base";
import { ContentType, IJsonRequester } from "../../contracts/http/json-requester";

export class NgJsonRequester implements IJsonRequester<any>, IRequesterBase<any> {
    private readonly $http: (request: INgRequestModel) => Promise<any>;

    public constructor($http: (request: INgRequestModel) => Promise<any>) {
        if ($http == null) {
            throw `$http is null`;
        }

        this.$http = $http;
    }

    public send(method: (string | HttpMethod), url: string, options?: IRequestModel): Promise<any> {
        let headers: any, data: any, request: INgRequestModel, promise: Promise<any>;

        if (!url) {
            throw "URL is required";
        }

        if (!method) {
            throw "Method is not specified";
        }

        method = method.toString().toUpperCase();
        options = options || {} as IRequestModel;

        headers = options.headers || {};
        headers["Content-Type"] = ContentType;

        data = options.data || {};

        request = {
            method: method,
            url: url,
            headers: headers,
            data: data
        };

        promise = this.$http(request);

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

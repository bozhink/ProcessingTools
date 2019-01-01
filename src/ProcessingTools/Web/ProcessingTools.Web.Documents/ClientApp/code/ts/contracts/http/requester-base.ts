export enum HttpMethod {
    GET = "GET",
    HEAD = "HEAD",
    PATCH = "PATCH",
    POST = "POST",
    PUT = "PUT",
    DELETE = "DELETE"
}

export interface IRequestModel {
    headers?: any;
    data: any;
}

export interface INgRequestModel {
    method: string;
    url: string;
    headers?: any;
    data?: any;
}

export interface IRequesterBase<T> {
    send: (method: (string | HttpMethod), url: string, options?: IRequestModel) => Promise<T>;
    delete: (url: string, options?: IRequestModel) => Promise<T>;
    get: (url: string, options?: IRequestModel) => Promise<T>;
    head: (url: string, options?: IRequestModel) => Promise<T>;
    patch: (url: string, options?: IRequestModel) => Promise<T>;
    post: (url: string, options?: IRequestModel) => Promise<T>;
    put: (url: string, options?: IRequestModel) => Promise<T>;
}

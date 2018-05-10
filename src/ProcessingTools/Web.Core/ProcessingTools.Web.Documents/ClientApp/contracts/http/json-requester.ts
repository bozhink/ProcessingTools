import { IRequesterBase, IRequestModel } from "./requester-base";

export const ContentType: string = "application/json";

export interface IJsonRequester<T> extends IRequesterBase<T> {
}

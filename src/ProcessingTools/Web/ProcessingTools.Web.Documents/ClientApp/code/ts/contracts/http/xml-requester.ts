import { IRequesterBase, IRequestModel } from "./requester-base";

export const ContentType: string = "application/xml";

export interface IXmlRequester<T> extends IRequesterBase<T> {
}

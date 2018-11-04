const MINIMAL_TIME_SPAN_BETWEEN_SEQUENTIAL_SAVES_MILLISECONDS: number = 5000;
const MINIMAL_TIME_SPAN_BETWEEN_SEQUENTIAL_GETS_MILLISECONDS: number = 5000;

import { enc, SHA1 } from "crypto-js";

import { IStorageKeys, IMessageResponse, MessageType } from "../../contracts/models/services.models";
import { IRequesterBase } from "../../contracts/http/requester-base";
import { IDocumentContentData } from "../../contracts/documents/document-content-data";

export class DocumentContentData implements IDocumentContentData {

    private storage: Storage;
    private keys: IStorageKeys;
    private requester: IRequesterBase<any>;

    public constructor(storage: Storage, keys: IStorageKeys, requester: IRequesterBase<any>) {
        if (!storage) {
            throw `Storage is null`;
        }

        if (!keys) {
            throw `Storage keys are null`;
        }

        if (!requester) {
            throw `Requester is null`;
        }

        this.storage = storage;
        this.keys = keys;
        this.requester = requester;
    }

    private getTimeToNextPossibleSave(lastSavedTime: string): number {
        return MINIMAL_TIME_SPAN_BETWEEN_SEQUENTIAL_SAVES_MILLISECONDS - (new Date().getTime() - new Date(lastSavedTime).getTime());
    }

    private getTimeToNextPossibleGet(lastGetTime: string): number {
        return MINIMAL_TIME_SPAN_BETWEEN_SEQUENTIAL_GETS_MILLISECONDS - (new Date().getTime() - new Date(lastGetTime).getTime());
    }

    private makeResponse(type: (string | MessageType), message: string): IMessageResponse {
        return {
            type: type,
            message: message
        };
    }

    private getMessage(obj: any): string {
        if (obj == null) {
            return "";
        }

        if (obj.responseJSON && obj.responseJSON.Message) {
            return obj.responseJSON.Message;
        }

        if (obj.responseText) {
            return obj.responseText;
        }

        try {
            return JSON.stringify(obj);
        } catch (e) {
            return "Cannot process message object";
        }
    }

    public get(url: string): Promise<any> {
        if (!url) {
            throw `URL is null`;
        }

        let self: DocumentContentData = this;

        return new Promise(function (resolve: (x: string) => void, reject: (x: IMessageResponse) => void): void {
            let lastGetTime: string = self.storage.getItem(self.keys.lastGetTimeKey);

            if (!lastGetTime || self.getTimeToNextPossibleGet(lastGetTime) < 0) {
                self.requester.post(url)
                    .then(function (data: any): void {
                        let content: string = data.content || data.Content;
                        self.storage.setItem(self.keys.lastGetTimeKey, new Date().toString());
                        resolve(content);
                    })
                    .catch(function (err: any): void {
                        self.storage.setItem(self.keys.lastGetTimeKey, new Date().toString());
                        reject(self.makeResponse(MessageType.ERROR, self.getMessage(err)));
                    });
            } else {
                let remainingTimeToNextGetInSeconds: number = self.getTimeToNextPossibleGet(lastGetTime) / 1000.0;
                reject(self.makeResponse(MessageType.WARNING, `Wait ${remainingTimeToNextGetInSeconds}s before get.`));
            }
        });
    }

    public initializeContent(content: string): void {
        let contentHash: string = enc.Hex.stringify(SHA1(content));
        let self: DocumentContentData = this;
        self.storage.setItem(self.keys.contentHashKey, contentHash);
    }

    public save(url: string, content: string): Promise<any> {
        if (!url) {
            throw `URL is null`;
        }

        let self: DocumentContentData = this;

        return new Promise(function (resolve: (x: IMessageResponse) => void, reject: (x: IMessageResponse) => void): void {

            let lastSavedTime: string = self.storage.getItem(self.keys.lastSavedTimeKey);
            let lastSavedHash: string = self.storage.getItem(self.keys.contentHashKey);

            if (!lastSavedTime || self.getTimeToNextPossibleSave(lastSavedTime) < 0) {
                let contentHash: string = enc.Hex.stringify(SHA1(content));

                if (contentHash !== lastSavedHash) {
                    self.requester.put(url, {
                        data: {
                            content: content
                        }
                    }).then(function (res: any): void {
                        self.storage.setItem(self.keys.lastSavedTimeKey, new Date().toString());
                        if (res.status === 1 || res.Status === 1) {
                            self.storage.setItem(self.keys.contentHashKey, contentHash);
                            resolve(self.makeResponse(MessageType.SUCCESS, self.getMessage(res)));
                        } else {
                            reject(self.makeResponse(MessageType.ERROR, self.getMessage(res)));
                        }
                    }).catch(function (err: any): void {
                        self.storage.setItem(self.keys.lastSavedTimeKey, new Date().toString());
                        reject(self.makeResponse(MessageType.ERROR, self.getMessage(err)));
                    });
                } else {
                    reject(self.makeResponse(MessageType.INFO, `Content will not be saved because it is not modified.`));
                }
            } else {
                let remainingTimeToNextSaveInSeconds: number = self.getTimeToNextPossibleSave(lastSavedTime) / 1000.0;
                reject(self.makeResponse(MessageType.WARNING, `Wait ${remainingTimeToNextSaveInSeconds}s before save.`));
            }
        });
    }
}

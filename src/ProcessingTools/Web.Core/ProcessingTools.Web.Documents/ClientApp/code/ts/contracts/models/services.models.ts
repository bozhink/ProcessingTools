export enum MessageType {
    INFO = "info",
    SUCCESS = "success",
    WARNING = "warning",
    ERROR = "error"
}

export interface IStorageKeys {
    lastSavedTimeKey?: string;
    contentHashKey?: string;
    lastGetTimeKey?: string;
}

export interface IMessageResponse {
    type: (string | MessageType);
    message: string;
}

export enum MessageType {
    INFO = "info",
    SUCCESS = "success",
    WARNING = "warning",
    ERROR = "error"
}

export interface IStorageKeys {
    mode?: string;
    theme?: string;
    lastSavedTimeKey?: string;
    contentHashKey?: string;
    lastGetTimeKey?: string;
}

export interface IMessageResponse {
    type: (string | MessageType);
    message: string;
}

export interface ISearchString {
    searchString: string;
}

export enum ReportType {
    INFO = "info",
    SUCCESS = "success",
    WARNING = "warning",
    ERROR = "error"
}

export interface IReportMessage {
    type: ReportType;
    message: string;
}

export interface IReporter {
    raiseMessage: (message: IReportMessage) => void;
}

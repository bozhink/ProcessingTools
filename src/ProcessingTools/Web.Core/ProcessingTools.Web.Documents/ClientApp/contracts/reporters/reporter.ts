export enum ReportType {
    INFO = "INFO",
    SUCCESS = "SUCCESS",
    WARNING = "WARNING",
    ERROR = "ERROR"
}

export interface IReporter {
    report: (type: (string | ReportType), message: string) => object;

}

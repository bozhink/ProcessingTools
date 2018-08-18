import { IDocumentContentData } from "../contracts/documents/document-content-data";
import { ReportType, IReporter } from "../contracts/reporters/reporter";
import { IMessageResponse } from "../contracts/models/services.models";

export class DocumentController {

    public constructor(
        private readonly dataService: IDocumentContentData,
        private readonly reporter: IReporter
    ) {
    }

    public get(url: string, quietMode: boolean, setContentCallback: (c: string) => void, done?: () => void): void {
        let self: DocumentController = this;

        if (!setContentCallback || typeof setContentCallback !== "function") {
            throw "setContentCallback function is required";
        }

        self.dataService.get(url)
            .then(function (response: string): void {
                setContentCallback(response);
            })
            .then(function (): void {
                if (done && typeof done === "function") {
                    done();
                }
            })
            .catch(function (error: any): void {
                self.reportError(self.reporter, quietMode, error);
            });
    }

    public save(url: string, quietMode: boolean, getContentCallback: () => string, done?: () => void): void {
        let self: DocumentController = this;

        if (!getContentCallback || typeof getContentCallback !== "function") {
            throw "getContentCallback function is required";
        }

        self.dataService.save(url, getContentCallback())
            .then(function (response: IMessageResponse): void {
                if (!quietMode && response != null) {
                    self.reporter.report(response.type.toUpperCase(), response.message);
                }
            })
            .then(function (): void {
                if (done && typeof done === "function") {
                    done();
                }
            })
            .catch(function (error: any): void {
                self.reportError(self.reporter, quietMode, error);
            });
    }

    public createSaveAction(url: string, quietMode: boolean, getContentCallback: () => string, done?: () => void): () => void {
        let self: DocumentController = this;

        return function (): void {
            self.save(url, quietMode, getContentCallback, done);
        };
    }

    public createGetAction(url: string, quietMode: boolean, setContentCallback: (c: string) => void, done?: () => void): () => void {
        let self: DocumentController = this;

        return function (): void {
            self.get(url, quietMode, setContentCallback, done);
        };
    }

    private reportError(reporter: IReporter, quietMode: boolean, error: any): void {
        if (!quietMode && error != null && reporter != null) {
            let reportType: (string | ReportType) = ReportType.ERROR;
            let message: string = JSON.stringify(error);

            if (typeof error.type !== "undefined" && typeof error.message !== "undefined") {
                reportType = (error as IMessageResponse).type.toUpperCase();
                message = (error as IMessageResponse).message;
            }

            reporter.report(reportType, message);
        }
    }
}

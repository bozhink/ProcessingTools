import { IDocumentContentData } from "../contracts/documents/document-content-data";
import { ReportType, IReporter } from "../contracts/reporters/reporter";

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
                if (!quietMode) {
                    self.reporter.report(ReportType.ERROR, error);
                }
            });
    }

    public save(url: string, quietMode: boolean, getContentCallback: () => string, done?: () => void): void {
        let self: DocumentController = this;

        if (!getContentCallback || typeof getContentCallback !== "function") {
            throw "getContentCallback function is required";
        }

        self.dataService.save(url, getContentCallback())
            .then(function (response: string): void {
                if (!quietMode) {
                    self.reporter.report(ReportType.SUCCESS, response);
                }
            })
            .then(function (): void {
                if (done && typeof done === "function") {
                    done();
                }
            })
            .catch(function (error: any): void {
                if (!quietMode) {
                    self.reporter.report(ReportType.ERROR, error);
                }
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
}

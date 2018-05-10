import { ReportType, IReporter } from "../../contracts/reporters/reporter";

export class MessageReporter implements IReporter {
    public report(type: (string | ReportType), message: string): object {
        return null;
    }
}

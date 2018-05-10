import { ReportType, IReportMessage, IReporter } from "../../contracts/reporters/reporter";

export class ToastrReporter implements IReporter {
    private toastr: Toastr;

    public constructor(toastr: Toastr) {
        if (toastr == null) {
            throw "Toastr is null";
        }

        this.toastr = toastr;
    }

    public raiseMessage(message: IReportMessage): void {
        if (message != null) {
            switch (message.type) {
                case ReportType.SUCCESS:
                    toastr.success(message.message);
                    break;

                case ReportType.INFO:
                    toastr.info(message.message);
                    break;

                case ReportType.WARNING:
                    toastr.warning(message.message);
                    break;

                default:
                    toastr.error(message.message);
            }
        }
    }
}

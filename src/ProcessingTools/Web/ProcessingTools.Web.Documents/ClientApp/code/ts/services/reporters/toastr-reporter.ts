import { ReportType, IReporter } from "../../contracts/reporters/reporter";

export class ToastrReporter implements IReporter {
    private toastr: Toastr;

    public constructor(toastr: Toastr) {
        if (toastr == null) {
            throw "Toastr is null";
        }

        this.toastr = toastr;
    }

    public report(type: (string | ReportType), message: string): object {
        if (message != null) {
            switch (type.toString().toUpperCase()) {
                case ReportType.SUCCESS:
                    return this.toastr.success(message);

                case ReportType.INFO:
                    return this.toastr.info(message);

                case ReportType.WARNING:
                    return this.toastr.warning(message);

                default:
                    return this.toastr.error(message);
            }
        }

        return null;
    }
}

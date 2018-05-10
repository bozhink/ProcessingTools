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
                    return toastr.success(message);

                case ReportType.INFO:
                    return toastr.info(message);

                case ReportType.WARNING:
                    return toastr.warning(message);

                default:
                    return toastr.error(message);
            }
        }

        return null;
    }
}

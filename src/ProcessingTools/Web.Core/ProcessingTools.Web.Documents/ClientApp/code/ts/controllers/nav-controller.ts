export class NavigationController {
    public activePage: string;
    public pages: Array<any>;

    public constructor(private readonly $location: ng.ILocationService, pages: Array<any>) {
        if (!Array.isArray(pages)) {
            throw "Invalid pages array";
        }

        this.activePage = "/";
        this.pages = pages;
    }

    public getActivePage(): string {
        let path: string = this.$location.path();
        this.activePage = path;
        return path;
    }
}

import { IRequesterBase } from "../contracts/http/requester-base";
import { ISearchStringService } from "../contracts/services";
import { ReportType, IReporter } from "../contracts/reporters/reporter";
import { IDataSet } from "../contracts/models/data.common.models";

import {
    IBlackListItem,
    IBlackListItems,
    IBlackListItemsResponseModel,
    ITaxonRank,
    ITaxa,
    ITaxaResponseModel
} from "../contracts/models/data.bio.models";

import { BlackListItem, TaxonRank } from "../models/data.bio.models";

export interface IBioDataController<T> {
    items: Array<T>;
    textArea: string;
    searchString: string;
    addItem: () => void;
    removeItem: (id: string) => void;
    clearList: () => void;
    search: (url: string) => void;
    submitItems: (url: string) => void;
}

export class BlackListController implements IBioDataController<IBlackListItem> {

    public items: Array<IBlackListItem>;
    public textArea: string;
    public searchString: string;

    public constructor(
        private readonly dataSet: IDataSet<IBlackListItem>,
        private readonly searchService: ISearchStringService,
        private readonly jsonRequester: IRequesterBase<any>,
        private readonly reporter: IReporter
    ) {
        this.items = dataSet.data;
        this.textArea = "";
        this.searchString = "";
    }

    public addItem(): void {
        let self: BlackListController = this;

        let items: any, text: string = self.textArea || "";
        text = text.replace(/\s+/g, " ").trim();
        if (text === "") {
            return;
        }

        text = text.replace(/(\S+)\s+/g, "$1\n");
        items = text.split("\n");

        self.dataSet.addMulti(items, (e: any) => new BlackListItem(e));

        self.textArea = "";
    }

    public removeItem(id: string): void {
        this.dataSet.remove(id);
    }

    public clearList(): void {
        this.dataSet.removeAll();
    }

    public search(url: string): void {
        let self: BlackListController = this;

        let searchString: string = self.searchString || "";
        searchString = searchString.replace(/\s+/g, " ").trim();

        if (!url || searchString === "") {
            return;
        }

        self.searchService.search(url, searchString)
            .then(function (response: IBlackListItemsResponseModel): void {
                if (response.status === 200) {
                    self.dataSet.addMulti(response.data.items, (e: any) => new BlackListItem(e.Content));
                } else {
                    self.reporter.report(ReportType.ERROR, response.status.toString());
                }
            }).catch(function (error: string): void {
                self.reporter.report(ReportType.ERROR, error);
            });
    }

    public submitItems(url: string): void {
        let self: BlackListController = this;

        if (!url) {
            return;
        }

        let items: Array<IBlackListItem> = self.dataSet.data.slice(0);
        let data: IBlackListItems = { items: items };

        self.jsonRequester.post(url, { data: data })
            .then(function (): void {
                self.dataSet.removeAll();
                self.reporter.report(ReportType.SUCCESS, "Done");
            }).catch(function (error: string): void {
                self.reporter.report(ReportType.ERROR, error);
            });
    }
}

export class TaxaRanksController implements IBioDataController<ITaxonRank> {

    public items: Array<ITaxonRank>;
    public textArea: string;
    public searchString: string;

    public constructor(
        private readonly dataSet: IDataSet<ITaxonRank>,
        private readonly searchService: ISearchStringService,
        private readonly jsonRequester: IRequesterBase<any>,
        private readonly reporter: IReporter
    ) {
        this.items = dataSet.data;
        this.textArea = "";
        this.searchString = "";
    }

    public addItem(): void {
        let self: TaxaRanksController = this;

        let text: string = self.textArea || "";
        text = text.replace(/[^\w\-]+/g, " ").trim();
        if (text === "") {
            return;
        }

        text = text.replace(/(\S+\s+\S+)\s+/g, "$1\n");
        let pairs: Array<any> = text.split("\n");

        self.dataSet.addMulti(pairs, function (element: string): ITaxonRank {
            let pair: Array<string> = element.split(" ");
            if (!pair || pair.length !== 2) {
                return null;
            }

            return new TaxonRank(pair[0], pair[1]);
        });

        self.textArea = "";
    }

    public removeItem(id: string): void {
        this.dataSet.remove(id);
    }

    public clearList(): void {
        this.dataSet.removeAll();
    }

    public search(url: string): void {
        let self: TaxaRanksController = this;
        let searchString: string = self.searchString || "";
        searchString = searchString.replace(/\s+/g, " ").trim();
        if (!url || searchString === "") {
            return;
        }

        self.searchService.search(url, searchString)
            .then(function (response: ITaxaResponseModel): void {
                if (response.status === 200) {
                    self.dataSet.addMulti(response.data.taxa, (e: ITaxonRank) => new TaxonRank(e.taxonName, e.rank));
                } else {
                    self.reporter.report(ReportType.ERROR, response.status.toString());
                }
            }).catch(function (error: string): void {
                self.reporter.report(ReportType.ERROR, error);
            });
    }

    public submitItems(url: string): void {
        let self: TaxaRanksController = this;

        if (!url) {
            return;
        }

        let taxa: Array<ITaxonRank> = self.dataSet.data.slice(0);
        let data: ITaxa = { taxa: taxa };

        self.jsonRequester.post(url, { data: data })
            .then(function (): void {
                self.dataSet.removeAll();
                self.reporter.report(ReportType.SUCCESS, "Done");
            }).catch(function (error: string): void {
                self.reporter.report(ReportType.ERROR, error);
            });
    }
}

import { IDataModel } from "./data.models";

export interface ITaxonRank extends IDataModel {
    name: string;
    rank: string;
}

export interface ITaxonRanks {
    items: Array<ITaxonRank>;
}

export interface IBlackListItem extends IDataModel {
    content: string;
}

export interface IBlackListItems {
    items: Array<IBlackListItem>;
}

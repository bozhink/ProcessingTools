import { IDataModel } from "./data.models";

export interface ITaxonRank extends IDataModel {
    taxonName: string;
    rank: string;
}

export interface IBlackListItem extends IDataModel {
    content: string;
}

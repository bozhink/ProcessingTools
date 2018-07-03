import { IDataModel } from "./data.models";

export interface ITaxonRank extends IDataModel {
    taxonName: string;
    rank: string;
}

export interface ITaxa {
    taxa: Array<ITaxonRank>;
}

export interface ITaxaResponseModel {
    data: ITaxa;
    status: (number | string);
}

export interface IBlackListItem extends IDataModel {
    content: string;
}

export interface IBlackListItems {
    items: Array<IBlackListItem>;
}

export interface IBlackListItemsResponseModel {
    data: IBlackListItems;
    status: (number | string);
}

export interface IDataModel {
    getHash: () => string;
}

export interface ITaxonRank extends IDataModel {
    id: string;
    taxonName: string;
    rank: string;
}

export interface IBlackListItem extends IDataModel {
    id: string;
    content: string;
}

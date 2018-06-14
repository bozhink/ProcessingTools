import { IDataModel } from "./data.models";

export interface IDataSet<T extends IDataModel> {
    data: Array<T>;
    add: (item: T) => void;
    addMulti: (items: Array<T>, map: any) => void;
    remove: (id: string) => void;
    removeAll: () => void;
}

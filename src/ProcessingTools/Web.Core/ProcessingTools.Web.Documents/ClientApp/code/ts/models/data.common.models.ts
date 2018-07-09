import { IDataModel } from "../contracts/models/data.models";
import { IDataSet } from "../contracts/models/data.common.models";

export class DataSet<T extends IDataModel> implements IDataSet<T> {
    public data: Array<T>;
    private id: number;

    public constructor() {
        this.id = 0;
        this.data = [];
    }

    private nextId(): number {
        this.id += 1;
        return this.id;
    }

    public add(item: T): void {
        let i: number, len: number, currentItem: T, hash: string;

        if (!item) {
            return;
        }

        if (!item.getHash || typeof (item.getHash) !== "function") {
            throw `Item to add should have function "getHash"`;
        }

        len = this.data.length;
        if (len > 0) {
            hash = item.getHash();
            for (i = 0; i < len; i += 1) {
                currentItem = this.data[i];
                if (hash === currentItem.getHash()) {
                    return;
                }
            }
        }

        item.id = this.nextId().toString();
        this.data.push(item);
    }

    public addMulti(items: Array<T>, map: any): void {
        let i: number, len: number, item: T;
        if (!items) {
            return;
        }

        if (!Array.isArray(items)) {
            items = [items];
        }

        if (!map || typeof (map) !== "function") {
            map = function (x: T): T {
                return x;
            };
        }

        len = items.length;
        for (i = 0; i < len; i += 1) {
            item = items[i];
            if (!item) {
                break;
            }

            this.add(map(item));
        }
    }

    public remove(id: string): void {
        let i: number, len: number;
        if (id) {
            len = this.data.length;
            for (i = 0; i < len; i += 1) {
                if (this.data[i].id === id) {
                    this.data.splice(i, 1);
                    break;
                }
            }
        }
    }

    public removeAll(): void {
        this.data.splice(0, this.data.length);
    }
}

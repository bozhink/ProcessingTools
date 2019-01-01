import { ITaxonRank, IBlackListItem } from "../contracts/models/data.bio.models";

export class TaxonRank implements ITaxonRank {
    public id: string;
    public name: string;
    public rank: string;

    public constructor(name: string, rank: string) {
        name = name ? name.replace(/\s+/g, "") : "";
        if (name.length < 1) {
            throw "'Null or whitespace taxon name";
        }

        rank = rank ? rank.replace(/\s+/g, "").toLowerCase() : "";
        if (rank.length < 1) {
            throw "Null or whitespace";
        }

        this.id = undefined;
        this.name = name;
        this.rank = rank;
    }

    public getHash(): string {
        return `${this.name}${this.rank}`;
    }
}

export class BlackListItem implements IBlackListItem {
    public id: string;
    public content: string;

    public constructor(content: string) {
        content = content ? content.replace(/\s+/g, "") : "";
        if (content.length < 1) {
            throw "Null or whitespace content";
        }

        this.id = null;
        this.content = content;
    }

    public getHash(): string {
        return this.content;
    }
}

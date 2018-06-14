import { ITaxonRank, IBlackListItem } from "../../../code/ts/contracts/models/data.bio.models";
import { TaxonRank, BlackListItem } from "../../../code/ts/models/data.bio.models";
import { expect } from "chai";

// tslint:disable-next-line:max-line-length
describe("TS TaxonRank tests", function (): void {

    // tslint:disable-next-line:max-line-length
    it("Expect valid initialization to not throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank("name", "rank");
        }).to.not.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect valid initialization to set correctly taxon name", function (): void {
        let name: string = "name";
        let rank: string = "rank";
        let model: ITaxonRank = new TaxonRank(name, rank);
        expect(model).to.have.property("taxonName");
        expect(model.taxonName).to.be.equal(name);
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with null taxon name to throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank(null, "rank");
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with undefined taxon name to throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank(undefined, "rank");
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with empty taxon name to throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank("", "rank");
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with white-space taxon name to throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank("  ", "rank");
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect valid initialization to set correctly rank", function (): void {
        let name: string = "name";
        let rank: string = "rank";
        let model: ITaxonRank = new TaxonRank(name, rank);
        expect(model).to.have.property("rank");
        expect(model.rank).to.be.equal(rank);
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with null rank to throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank("name", null);
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with undefined rank to throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank("name", undefined);
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with empty rank to throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank("name", "");
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with white-space rank to throw", function (): void {
        expect(function (): void {
            let model: ITaxonRank = new TaxonRank("name", "  ");
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect valid initialization to return object with getHash method", function (): void {
        let name: string = "name";
        let rank: string = "rank";
        let model: ITaxonRank = new TaxonRank(name, rank);
        expect(model.getHash).to.not.be.equal(undefined);
        expect(model.getHash).is.a("function");
        expect(() => model.getHash()).to.not.throw();
        expect(model.getHash()).is.a("string");
    });
});

// tslint:disable-next-line:max-line-length
describe("TS BlackListItem tests", function (): void {

    // tslint:disable-next-line:max-line-length
    it("Expect valid initialization to not throw", function (): void {
        expect(function (): void {
            let model: IBlackListItem = new BlackListItem("content");
        }).to.not.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect valid initialization to set correctly content", function (): void {
        let content: string = "content";
        let model: IBlackListItem = new BlackListItem(content);
        expect(model).to.have.property("content");
        expect(model.content).to.be.equal(content);
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with null content to throw", function (): void {
        expect(function (): void {
            let model: IBlackListItem = new BlackListItem(null);
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with undefined content to throw", function (): void {
        expect(function (): void {
            let model: IBlackListItem = new BlackListItem(undefined);
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with empty content to throw", function (): void {
        expect(function (): void {
            let model: IBlackListItem = new BlackListItem("");
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect initialization with white-space content to throw", function (): void {
        expect(function (): void {
            let model: IBlackListItem = new BlackListItem("   ");
        }).to.throw();
    });

    // tslint:disable-next-line:max-line-length
    it("Expect valid initialization to return object with getHash method", function (): void {
        let content: string = "content";
        let model: IBlackListItem = new BlackListItem(content);
        expect(model.getHash).to.not.be.equal(undefined);
        expect(model.getHash).is.a("function");
        expect(() => model.getHash()).to.not.throw();
        expect(model.getHash()).is.a("string");
    });
});

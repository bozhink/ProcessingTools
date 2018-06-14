import { IDataSet } from "../code/ts/contracts/models/data.common.models";
import { DataSet } from "../code/ts/models/data.common.models";
import { expect } from "chai";
import * as sinon from "sinon";

// tslint:disable-next-line:max-line-length
describe("DataSet tests", function (): void {

    // tslint:disable-next-line:max-line-length
    describe("Initialization", function (): void {

        // tslint:disable-next-line:max-line-length
        it("Expect default initialization to not throw", function (): void {
            expect(function (): void {
                let dataSet: IDataSet<any> = new DataSet();
            }).to.not.throw();
        });

        // tslint:disable-next-line:max-line-length
        it("Expect default initialization to return DataSet object with empty data array", function (): void {
            let dataSet: IDataSet<any> = new DataSet();
            expect(dataSet).to.have.property("data");
            expect(Array.isArray(dataSet.data)).to.be.equal(true);
            expect(dataSet.data.length).to.be.equal(0);
        });

        // tslint:disable-next-line:max-line-length
        it("Expect default initialization to return DataSet object with add function", function (): void {
            let dataSet: IDataSet<any> = new DataSet();
            expect(dataSet).to.have.property("add");
            expect(dataSet.add).to.be.a("function");
        });

        // tslint:disable-next-line:max-line-length
        it("Expect default initialization to return DataSet object with addMulti function", function (): void {
            let dataSet: IDataSet<any> = new DataSet();
            expect(dataSet).to.have.property("addMulti");
            expect(dataSet.addMulti).to.be.a("function");
        });

        // tslint:disable-next-line:max-line-length
        it("Expect default initialization to return DataSet object with remove function", function (): void {
            let dataSet: IDataSet<any> = new DataSet();
            expect(dataSet).to.have.property("remove");
            expect(dataSet.remove).to.be.a("function");
        });

        // tslint:disable-next-line:max-line-length
        it("Expect default initialization to return DataSet object with removeAll function", function (): void {
            let dataSet: IDataSet<any> = new DataSet();
            expect(dataSet).to.have.property("removeAll");
            expect(dataSet.removeAll).to.be.a("function");
        });
    });

    // tslint:disable-next-line:max-line-length
    describe("Add items to DataSet", function (): void {

        // tslint:disable-next-line:max-line-length
        it("Expect addition of single valid item to DataSet to add it correctly with valid id", function (): void {
            let item: any = {
                a: 1,
                getHash: () => 1
            };

            let spy: sinon.SinonSpy = sinon.spy(item, "getHash");

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item);
            }).to.not.throw();

            expect(spy.callCount).to.be.equal(0);

            expect(dataSet.data.length).to.equal(1);

            let addedItem: any = dataSet.data[0];
            expect(addedItem.a).to.equal(item.a);

            expect(addedItem).to.have.property("id");
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of two valid items with different hash to DataSet to add them correctly with valid id", function (): void {
            let item1: any = {
                a: 1,
                getHash: () => 1
            };
            let item2: any = {
                a: 2,
                getHash: () => 2
            };

            let spy1: sinon.SinonSpy = sinon.spy(item1, "getHash");
            let spy2: sinon.SinonSpy = sinon.spy(item2, "getHash");

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item1);
            }).to.not.throw();

            expect(function (): void {
                dataSet.add(item2);
            }).to.not.throw();

            expect(spy1.callCount).to.be.equal(1);
            expect(spy2.callCount).to.be.equal(1);

            expect(dataSet.data.length).to.equal(2);

            let addedItem1: any = dataSet.data[0];
            expect(addedItem1.a).to.equal(item1.a);

            let addedItem2: any = dataSet.data[1];
            expect(addedItem2.a).to.equal(item2.a);

            expect(addedItem1).to.have.property("id");
            expect(addedItem2).to.have.property("id");

            expect(addedItem1.id).not.to.be.equal(addedItem2.id);
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of three valid items with different hash to DataSet to add them correctly with valid id", function (): void {
            let item1: any = {
                a: 1,
                getHash: () => 1
            };
            let item2: any = {
                a: 2,
                getHash: () => 2
            };
            let item3: any = {
                a: 3,
                getHash: () => 3
            };

            let spy1: sinon.SinonSpy = sinon.spy(item1, "getHash");
            let spy2: sinon.SinonSpy = sinon.spy(item2, "getHash");
            let spy3: sinon.SinonSpy = sinon.spy(item3, "getHash");

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item1);
            }).to.not.throw();

            expect(function (): void {
                dataSet.add(item2);
            }).to.not.throw();

            expect(function (): void {
                dataSet.add(item3);
            }).to.not.throw();

            expect(spy1.callCount).to.be.equal(2);
            expect(spy2.callCount).to.be.equal(2);
            expect(spy3.callCount).to.be.equal(1);

            expect(dataSet.data.length).to.equal(3);

            let addedItem1: any = dataSet.data[0];
            expect(addedItem1.a).to.equal(item1.a);

            let addedItem2: any = dataSet.data[1];
            expect(addedItem2.a).to.equal(item2.a);

            let addedItem3: any = dataSet.data[2];
            expect(addedItem3.a).to.equal(item3.a);

            expect(addedItem1).to.have.property("id");
            expect(addedItem2).to.have.property("id");
            expect(addedItem3).to.have.property("id");

            expect(addedItem1.id).not.to.be.equal(addedItem2.id);
            expect(addedItem1.id).not.to.be.equal(addedItem3.id);
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of two valid items with equal hash to DataSet to add only the first one", function (): void {
            let item1: any = {
                a: 1,
                getHash: () => 1
            };
            let item2: any = {
                a: 2,
                getHash: () => 1
            };

            let spy1: sinon.SinonSpy = sinon.spy(item1, "getHash");
            let spy2: sinon.SinonSpy = sinon.spy(item2, "getHash");

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item1);
            }).to.not.throw();

            expect(function (): void {
                dataSet.add(item2);
            }).to.not.throw();

            expect(spy1.callCount).to.be.equal(1);
            expect(spy2.callCount).to.be.equal(1);

            expect(dataSet.data.length).to.equal(1);

            let addedItem: any = dataSet.data[0];
            expect(addedItem.a).to.equal(item1.a);

            expect(addedItem).to.have.property("id");
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of single item with no getHash() to DataSet to throw and not to add it", function (): void {
            let item: any = {
                a: 1
            };

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item);
            }).to.throw(`Item to add should have function "getHash"`);

            expect(dataSet.data.length).to.equal(0);
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of one valid item and one item with no getHash() to DataSet to throw and to add only the first one", function (): void {
            let item1: any = {
                a: 1,
                getHash: () => 1
            };
            let item2: any = {
                a: 2
            };

            let spy1: sinon.SinonSpy = sinon.spy(item1, "getHash");

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item1);
            }).to.not.throw();

            expect(function (): void {
                dataSet.add(item2);
            }).to.throw(`Item to add should have function "getHash"`);

            expect(spy1.callCount).to.be.equal(0);

            expect(dataSet.data.length).to.equal(1);

            let addedItem: any = dataSet.data[0];
            expect(addedItem.a).to.equal(item1.a);

            expect(addedItem).to.have.property("id");
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of one item with no getHash() and one valid item to DataSet to throw and to add only the second one", function (): void {
            let item1: any = {
                a: 1
            };
            let item2: any = {
                a: 2,
                getHash: () => 2
            };

            let spy2: sinon.SinonSpy = sinon.spy(item2, "getHash");

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item1);
            }).to.throw(`Item to add should have function "getHash"`);

            expect(function (): void {
                dataSet.add(item2);
            }).to.not.throw();

            expect(spy2.callCount).to.be.equal(0);

            expect(dataSet.data.length).to.equal(1);

            let addedItem: any = dataSet.data[0];
            expect(addedItem.a).to.equal(item2.a);

            expect(addedItem).to.have.property("id");
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of two items with no getHash() to DataSet to throw and not to add them", function (): void {
            let item1: any = {
                a: 1
            };
            let item2: any = {
                a: 2
            };

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item1);
            }).to.throw(`Item to add should have function "getHash"`);

            expect(function (): void {
                dataSet.add(item2);
            }).to.throw(`Item to add should have function "getHash"`);

            expect(dataSet.data.length).to.equal(0);
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of null item to DataSet to not throw and to not add id", function (): void {
            let item: any = null;

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item);
            }).to.not.throw();

            expect(dataSet.data.length).to.equal(0);
        });

        // tslint:disable-next-line:max-line-length
        it("Expect addition of undefined item to DataSet to not throw and to not add id", function (): void {
            let item: any = undefined;

            let dataSet: IDataSet<any> = new DataSet();

            expect(function (): void {
                dataSet.add(item);
            }).to.not.throw();

            expect(dataSet.data.length).to.equal(0);
        });

        // tslint:disable-next-line:max-line-length
        it("Expect add() with no parameters to empty DataSet to not throw and to not change data.length", function (): void {
            let dataSet: any = new DataSet();

            expect(function (): void {
                dataSet.add();
            }).to.not.throw();

            expect(dataSet.data.length).to.equal(0);
        });

        // tslint:disable-next-line:max-line-length
        it("Expect add() with no parameters to not throw and to not change data.length", function (): void {
            let dataSet: any = new DataSet();

            // first add some items
            dataSet.add({
                x: 1,
                getHash: () => 1
            });

            expect(function (): void {
                dataSet.add();
            }).to.not.throw();

            expect(dataSet.data.length).to.equal(1);
        });
    });
});

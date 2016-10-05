(function () {
    'use strict';

    var expect = require('chai').expect,
        DataSet = require('../app/data/data-set.js');

    describe('DataSet tests', function () {
        describe('Initialization', function () {
            it('Expect default initialization to not throw', function () {
                expect(function () {
                    var dataSet = new DataSet();
                }).to.not.throw();
            });

            it('Expect default initialization to return DataSet object with empty data array', function () {
                var dataSet = new DataSet();
                expect(dataSet).to.have.property('data');
                expect(Array.isArray(dataSet.data)).to.be.equal(true);
                expect(dataSet.data.length).to.be.equal(0);
            });

            it('Expect default initialization to return DataSet object with add function', function () {
                var dataSet = new DataSet();
                expect(dataSet).to.have.property('add');
                expect(dataSet.add).to.be.a('function');
            });

            it('Expect default initialization to return DataSet object with addMulti function', function () {
                var dataSet = new DataSet();
                expect(dataSet).to.have.property('addMulti');
                expect(dataSet.addMulti).to.be.a('function');
            });

            it('Expect default initialization to return DataSet object with remove function', function () {
                var dataSet = new DataSet();
                expect(dataSet).to.have.property('remove');
                expect(dataSet.remove).to.be.a('function');
            });

            it('Expect default initialization to return DataSet object with removeAll function', function () {
                var dataSet = new DataSet();
                expect(dataSet).to.have.property('removeAll');
                expect(dataSet.removeAll).to.be.a('function');
            });
        });
    });

}());
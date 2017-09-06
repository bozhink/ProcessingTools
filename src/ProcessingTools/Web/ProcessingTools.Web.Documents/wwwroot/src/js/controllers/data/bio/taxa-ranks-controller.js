'use strict';

var TaxonRank = require('../../../models/data/taxon-rank');

module.exports = function TaxaRanksController(dataSet, searchService, jsonRequester, reporter) {
    var self = this;

    self.items = dataSet.data;

    self.addItem = function () {
        var pairs, text = self.textArea || '';
        text = text.replace(/[^\w\-]+/g, ' ').trim();
        if (text === '') {
            return;
        }

        text = text.replace(/(\S+\s+\S+)\s+/g, '$1\n');
        pairs = text.split('\n');

        dataSet.addMulti(pairs, function (element) {
            var pair = element.split(' ');
            if (!pair || pair.length !== 2) {
                return null;
            }

            return new TaxonRank(pair[0], pair[1]);
        });

        self.textArea = '';
    };

    self.removeItem = function (id) {
        dataSet.remove(id);
    };

    self.clearList = function () {
        dataSet.removeAll();
    };

    self.search = function (url) {
        var searchString = self.searchString || '';
        searchString = searchString.replace(/\s+/g, ' ').trim();
        if (!url || searchString === '') {
            return;
        }

        searchService.search(url, searchString)
            .then(function successCallback(response) {
                if (response.status === 200) {
                    dataSet.addMulti(response.data.Taxa, (e) => new TaxonRank(e.TaxonName, e.Rank));
                } else {
                    reporter.report(response.status, 'error')
                }
            }).catch(function (err) {
                reporter.report(err, 'error');
            });
    };

    self.submitItems = function (url) {
        var i, len, taxon, taxa = [];

        if (!url) {
            return;
        }

        len = dataSet.data.length;
        for (i = 0; i < len; i += 1) {
            taxon = dataSet.data[i];
            taxa.push({
                TaxonName: taxon.taxonName,
                Rank: taxon.rank
            });
        }

        jsonRequester.post(url, {
            data: {
                Taxa: taxa
            }
        }).then(function () {
            dataSet.removeAll();
            reporter.report('Done', 'success');
        }).catch(function (err) {
            reporter.report(err, 'error');
        });
    };
};

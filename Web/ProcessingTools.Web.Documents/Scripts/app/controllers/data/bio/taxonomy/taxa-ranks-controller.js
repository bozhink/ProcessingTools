(function (window) {
    'use strict';
    var app, controllers;

    window.app = window.app || {};
    app = window.app;

    app.controllers = app.controllers || {};
    controllers = app.controllers;

    controllers.TaxaRanksController = function TaxaRanksController(dataSet, searchService, jsonRequester) {
        var self = this,
            TaxonRank = app.models.TaxonRank;

        self.taxa = dataSet.data;

        self.addTaxa = function () {
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

        self.removeTaxon = function (id) {
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
                    }
                }).catch(function () {
                });
        };

        self.submitTaxa = function (url) {
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
            }).catch(function () {
            });
        };
    };
}(window));

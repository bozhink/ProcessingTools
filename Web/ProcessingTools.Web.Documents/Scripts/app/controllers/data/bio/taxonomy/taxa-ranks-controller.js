(function (angular, app) {
    'use strict';

    function TaxonRank(taxonName, rank) {

        taxonName = taxonName ? taxonName.replace(/\s+/g, '') : '';
        if (taxonName.length < 1) {
            throw 'Null or whitespace taxon name';
        }

        rank = rank ? rank.replace(/\s+/g, '').toLowerCase() : '';
        if (rank.length < 1) {
            throw 'Null or whitespace'
        }

        this.id = null;
        this.taxonName = taxonName;
        this.rank = rank;
    }

    TaxonRank.prototype.compare = function (taxon) {
        var self = this;
        return (self.taxonName === taxon.taxonName) && (self.rank === taxon.rank);
    }

    angular.module('taxaranksApp', [])
        .service('SearchStringService', ['$http', app.services.SearchStringService])
        .controller('TaxaRanksController', ['SearchStringService', function TaxaRanksController(searchService) {
            var self = this,
                dataSet = new app.data.DataSet();

            self.taxa = dataSet.data;

            self.addTaxa = function () {
                var pairs, text = self.textArea || '';
                text = text.replace(/[^\w-]+/g, ' ').trim();
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
            }

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
                    }, function errorCallback(response) { });
            }
        }]);
}(window.angular, window.app));
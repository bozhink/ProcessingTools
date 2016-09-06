(function (angular) {
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



    function DataSet() {
        var id = 0, dataSet = [];

        function nextId() {
            id += 1;
            return id;
        }

        function addItemToSet(item) {
            var i, len, currentItem;

            if (!item) {
                return;
            }

            if (!item.compare || typeof (item.compare) !== 'function') {
                throw 'Item to add should have function "compare"';
            }

            len = dataSet.length;
            for (i = 0; i < len; i += 1) {
                currentItem = dataSet[i];
                if (item.compare(currentItem)) {
                    return;
                }
            }

            item.id = nextId();
            dataSet.push(item);
        }

        function addMulti(items, map) {
            if (!items) {
                return;
            }

            if (!Array.isArray(items)) {
                items = [items];
            }

            if (!map || typeof (map) !== 'function') {
                map = (x) => x;
            }

            items.forEach((element) => {
                if (!element) {
                    return;
                }

                addItemToSet(map(element));
            });
        }

        function removeItem(id) {
            var i, len;
            if (id) {
                len = dataSet.length;
                for (i = 0; i < len; i += 1) {
                    if (dataSet[i].id === id) {
                        dataSet.splice(i, 1);
                        break;
                    }
                }
            }
        }

        function removeAll() {
            dataSet.splice(0, dataSet.length);
        }

        return {
            data: dataSet,
            add: addItemToSet,
            addMulti: addMulti,
            remove: removeItem,
            removeAll: removeAll
        }
    }

    angular.module('taxaranksApp', [])
        .service('SearchStringService', ['$http', function SearchStringService($http) {
            function search(url, searchString) {
                var request;
                if (!url || !searchString) {
                    throw 'Invalid input parameter';
                }

                searchString = searchString.trim();
                if (searchString.length < 1) {
                    throw 'Search string should not be empty';
                }

                request = {
                    method: 'POST',
                    url: url,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    data: {
                        searchString: searchString
                    }
                };

                return $http(request);
            }

            return {
                search: search
            };
        }])
        .controller('TaxaRanksController', ['SearchStringService', function TaxaRanksController(searchService) {
            var self = this,
                dataSet = new DataSet();

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
}(window.angular));
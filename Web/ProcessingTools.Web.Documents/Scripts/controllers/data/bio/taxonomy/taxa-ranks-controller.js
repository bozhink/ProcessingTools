(function (angular) {
    'use strict';

    var id = 0;

    function getId() {
        id += 1;
        return id;
    }

    function addToSet(array, item) {
        var i, len, currentItem;

        if (!item) {
            return;
        }

        item.taxonName = item.taxonName || '';
        item.rank = item.rank || '';
        if (item.taxonName === '' || item.rank === '') {
            return;
        }

        array = array || [];
        if (!Array.isArray(array)) {
            array = [];
        }

        len = array.length;
        for (i = 0; i < len; i += 1) {
            currentItem = array[i];
            if (currentItem.taxonName === item.taxonName && currentItem.rank === item.rank) {
                return;
            }
        }

        array.push(item);
    }

    angular.module('taxaranksApp', [])
        .controller('TaxaRanksController', function TaxaRanksController() {
            var taxaList = this;
            taxaList.taxa = [];

            taxaList.addTaxon = function () {
                var pairs, text = taxaList.textArea || '';
                if (text !== '') {
                    text = text.replace(/[^\w-]+/g, ' ')
                        .replace(/(\S+\s+\S+)\s+/g, '$1\n');
                }

                pairs = text.split('\n');

                pairs.forEach(function (element) {
                    var pair;
                    if (!element) {
                        return;
                    }

                    pair = element.split(' ');
                    if (!pair || pair.length !== 2) {
                        return;
                    }

                    addToSet(taxaList.taxa, {
                        id: getId(),
                        taxonName: pair[0],
                        rank: pair[1].toLowerCase(),
                    });
                });

                taxaList.textArea = '';
            };

            taxaList.removeTaxon = function (id) {
                var i, len, taxa = taxaList.taxa;

                if (id) {
                    len = taxa.length;
                    for (i = 0; i < len; i += 1) {
                        if (taxa[i].id === id) {
                            taxa.splice(i, 1);
                            break;
                        }
                    }
                }
            };
        });
}(window.angular));
(function (window) {
    'use strict';
    var app, models;

    window.app = window.app || {};
    app = window.app;

    app.models = app.models || {};
    models = app.models;

    function TaxonRank(taxonName, rank) {
        var self = this;

        taxonName = taxonName ? taxonName.replace(/\s+/g, '') : '';
        if (taxonName.length < 1) {
            throw 'Null or whitespace taxon name';
        }

        rank = rank ? rank.replace(/\s+/g, '').toLowerCase() : '';
        if (rank.length < 1) {
            throw 'Null or whitespace';
        }

        self.id = undefined;
        self.taxonName = taxonName;
        self.rank = rank;
    }

    TaxonRank.prototype.getHash = function () {
        var self = this;
        return `${self.taxonName}${self.rank}`;
    };

    models.TaxonRank = TaxonRank;

}(window));
(function (window) {
    'use strict';
    var app, models;

    window.app = window.app || {};
    app = window.app;

    app.models = app.models || {};
    models = app.models;

    function TaxonRank(taxonName, rank) {

        taxonName = taxonName ? taxonName.replace(/\s+/g, '') : '';
        if (taxonName.length < 1) {
            throw 'Null or whitespace taxon name';
        }

        rank = rank ? rank.replace(/\s+/g, '').toLowerCase() : '';
        if (rank.length < 1) {
            throw 'Null or whitespace';
        }

        this.id = null;
        this.taxonName = taxonName;
        this.rank = rank;
    }

    TaxonRank.prototype.compare = function (taxon) {
        var self = this;
        return (self.taxonName === taxon.taxonName) && (self.rank === taxon.rank);
    };

    models.TaxonRank = TaxonRank;

}(window));
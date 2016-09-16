(function (window) {
    'use strict';
    var app, models;

    window.app = window.app || {};
    app = window.app;

    app.models = app.models || {};
    models = app.models;

    function BlackListItem(content) {
        var self = this;

        content = content ? content.replace(/\s+/g, '') : '';
        if (content.length < 1) {
            throw 'Null or whitespace content';
        }

        self.id = undefined;
        self.content = content;
    }

    BlackListItem.prototype.getHash = function () {
        var self = this;
        return self.content;
    };

    models.BlackListItem = BlackListItem;

}(window));
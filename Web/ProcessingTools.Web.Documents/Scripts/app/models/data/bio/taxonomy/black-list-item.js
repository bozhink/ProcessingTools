(function (window) {
    'use strict';
    var app, models;

    window.app = window.app || {};
    app = window.app;

    app.models = app.models || {};
    models = app.models;

    function BlackListItem(content) {
        content = content ? content.replace(/\s+/g, '') : '';
        if (content.length < 1) {
            throw 'Null or whitespace content';
        }

        this.id = null;
        this.content = content;
    }

    BlackListItem.prototype.compare = function (item) {
        var self = this;
        return (self.content === item.content);
    };

    models.BlackListItem = BlackListItem;

}(window));
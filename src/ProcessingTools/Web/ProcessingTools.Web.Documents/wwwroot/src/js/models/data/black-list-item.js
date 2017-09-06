'use strict';

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

module.exports = BlackListItem;

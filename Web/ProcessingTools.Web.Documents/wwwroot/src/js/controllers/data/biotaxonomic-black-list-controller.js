'use strict';

var BlackListItem = require('../../models/data/black-list-item');

module.exports = function BiotaxonomicBlackListController(dataSet, searchService, jsonRequester, reporter) {
    var self = this;

    self.items = dataSet.data;

    self.addItem = function () {
        var items, text = self.textArea || '';
        text = text.replace(/\s+/g, ' ').trim();
        if (text === '') {
            return;
        }

        text = text.replace(/(\S+)\s+/g, '$1\n');
        items = text.split('\n');

        dataSet.addMulti(items, (e) => new BlackListItem(e));

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
                    dataSet.addMulti(response.data.Items, (e) => new BlackListItem(e.Content));
                } else {
                    reporter.report(response.status, 'error')
                }
            }).catch(function (err) {
                reporter.report(err, 'error');
            });
    };

    self.submitItems = function (url) {
        var i, len, item, items = [];

        if (!url) {
            return;
        }

        len = dataSet.data.length;
        for (i = 0; i < len; i += 1) {
            item = dataSet.data[i];
            items.push({
                Content: item.content
            });
        }

        jsonRequester.post(url, {
            data: {
                Items: items
            }
        }).then(function () {
            dataSet.removeAll();
            reporter.report('Done', 'success');
        }).catch(function (err) {
            reporter.report(err, 'error');
        });
    };
};
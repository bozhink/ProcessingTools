(function (window) {
    'use strict';
    var app, data;

    window.app = window.app || {};
    app = window.app;

    app.data = app.data || {};
    data = app.data;

    data.DataSet = function DataSet() {
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

            items.forEach(function (element) {
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
        };
    };
}(window));

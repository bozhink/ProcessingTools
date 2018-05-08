'use strict';

module.exports = function DataSet() {
    var id = 0,
        dataSet = [];

    function nextId() {
        id += 1;
        return id;
    }

    function addItemToSet(item) {
        var i, len, currentItem, hash;

        if (!item) {
            return;
        }

        if (!item.getHash || typeof (item.getHash) !== 'function') {
            throw 'Item to add should have function "getHash"';
        }

        len = dataSet.length;
        if (len > 0) {
            hash = item.getHash();
            for (i = 0; i < len; i += 1) {
                currentItem = dataSet[i];
                if (hash === currentItem.getHash()) {
                    return;
                }
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

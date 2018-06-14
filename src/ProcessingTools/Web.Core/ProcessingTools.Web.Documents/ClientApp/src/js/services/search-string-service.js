'use strict';

module.exports = function SearchStringService(jsonRequester) {
    if (!jsonRequester) {
        throw 'JsonRequester should not be null';
    }

    function search(url, searchString) {
        if (!url || !searchString) {
            throw 'Invalid input parameter';
        }

        searchString = searchString.trim();
        if (searchString.length < 1) {
            throw 'Search string should not be empty';
        }

        return jsonRequester.post(url, {
            data: {
                searchString: searchString
            }
        });
    }

    return {
        search: search
    };
};

(function (window) {
    'use strict';
    var app, services;

    window.app = window.app || {};
    app = window.app;

    app.services = app.services || {};
    services = app.services;

    services.SearchStringService = function SearchStringService(jsonRequester) {
        if (!jsonRequester) {
            throw 'JsonRequester should not be null';
        }

        function search(url, searchString) {
            var request;
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
}(window));

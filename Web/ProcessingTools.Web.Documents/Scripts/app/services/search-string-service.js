(function (window) {
    'use strict';
    var app, services;

    window.app = window.app || {};
    app = window.app;

    app.services = app.services || {};
    services = app.services;

    services.SearchStringService = function SearchStringService($http) {
        function search(url, searchString) {
            var request;
            if (!url || !searchString) {
                throw 'Invalid input parameter';
            }

            searchString = searchString.trim();
            if (searchString.length < 1) {
                throw 'Search string should not be empty';
            }

            request = {
                method: 'POST',
                url: url,
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    searchString: searchString
                }
            };

            return $http(request);
        }

        return {
            search: search
        };
    };
}(window));

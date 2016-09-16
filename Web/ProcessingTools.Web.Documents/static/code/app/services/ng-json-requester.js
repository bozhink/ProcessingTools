/*property
    NgJsonRequester, app, data, delete, get, headers, method, post, put,
    services, url
*/
(function (window) {
    'use strict';
    var app, services;

    window.app = window.app || {};
    app = window.app;

    app.services = app.services || {};
    services = app.services;

    services.NgJsonRequester = function NgJsonRequester($http) {
        function send(method, url, options) {
            var headers, data, request, promise;

            if (!url) {
                throw 'URL should not be null';
            }

            options = options || {};

            headers = options.headers || {};
            headers['Content-Type'] = 'application/json';

            data = options.data || undefined;

            request = {
                method: method,
                url: url,
                headers: headers,
                data: data
            };

            promise = $http(request);

            return promise;
        }

        function get(url, options) {
            return send('GET', url, options);
        }

        function post(url, options) {
            return send('POST', url, options);
        }

        function put(url, options) {
            return send('PUT', url, options);
        }

        function del(url, options) {
            return send('DELETE', url, options);
        }

        return {
            get: get,
            post: post,
            put: put,
            delete: del
        };
    };
}(window));

/*property
    JsonRequester, ajax, app, contentType, data, delete, error, get, headers,
    method, post, put, send, services, stringify, success, url
*/
(function (window) {
    'use strict';
    var app, services;

    window.app = window.app || {};
    app = window.app;

    app.services = app.services || {};
    services = app.services;

    services.JsonRequester = function ($) {
        function send(method, url, options) {
            var headers, data, promise;

            if (!url) {
                throw 'URL should not be null';
            }

            options = options || {};
            headers = options.headers || {};
            data = options.data || undefined;

            promise = new Promise(function (resolve, reject) {
                $.ajax({
                    url: url,
                    method: method,
                    contentType: 'application/json',
                    headers: headers,
                    data: JSON.stringify(data),
                    success: function (res) {
                        resolve(res);
                    },
                    error: function (err) {
                        reject(err);
                    }
                });
            });

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
            send: send,
            get: get,
            post: post,
            put: put,
            delete: del
        };
    };
}(window));

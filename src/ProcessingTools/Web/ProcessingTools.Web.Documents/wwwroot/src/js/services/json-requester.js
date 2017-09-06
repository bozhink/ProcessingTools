'use strict';

module.exports = function JsonRequester($) {
    function send(method, url, options) {
        var headers, data, promise;

        if (!url) {
            throw 'URL is required';
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

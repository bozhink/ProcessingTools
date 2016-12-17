(function (window, $) {
    'use strict';
    var app, services;

    window.app = window.app || {};
    app = window.app;

    app.services = app.services || {};
    services = app.services;

    services.TemplatesProvider = function (baseAddress, extension) {
        var handlebars = window.handlebars || window.Handlebars,
            cache = {};

        baseAddress = baseAddress || '~/templates';
        extension = extension || 'handlebars';

        function get(name) {
            var promise = new Promise(function (resolve, reject) {
                var url = `${baseAddress}/${name}.${extension}`;

                try {
                    if (cache[name]) {
                        resolve(cache[name]);
                        return;
                    }

                    $.get(url, function (html) {
                        var template = handlebars.compile(html);
                        cache[name] = template;
                        resolve(template);
                    });
                } catch (e) {
                    reject(e);
                }
            });

            return promise;
        }

        return {
            get: get
        };
    };
}(window, window.jQuery));
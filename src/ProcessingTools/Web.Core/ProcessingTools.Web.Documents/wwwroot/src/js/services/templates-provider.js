'use strict';

module.exports = function TemplatesProvider($, handlebars, baseAddress, extension) {
    var cache = {};

    if (!handlebars) {
        throw 'Handlebars is required';
    }

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

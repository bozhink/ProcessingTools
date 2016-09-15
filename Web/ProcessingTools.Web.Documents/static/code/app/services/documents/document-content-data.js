(function (window) {
    'use strict';
    var app, services;

    window.app = window.app || {};
    app = window.app;

    app.services = app.services || {};
    services = app.services;

    const
        MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS = 5000,
        MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_GETS_MILLISECONDS = 5000;

    var sha1 = window.CryptoJS.SHA1;

    services.DocumentContentData = function (sessionStorage, lastGetTimeKey, lastSavedTimeKey, contentHashKey, jsonRequester) {

        function getTimeToNextPossibleSave(lastSavedTime) {
            return MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS - (new Date() - new Date(lastSavedTime));
        }

        function getTimeToNextPossibleGet(lastGetTime) {
            return MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_GETS_MILLISECONDS - (new Date() - new Date(lastGetTime));
        }

        function makeResponse(type, message) {
            return {
                type: type,
                message: message
            };
        }

        function getMessage(obj) {
            try {
                if (obj.responseJSON && obj.responseJSON.Message) {
                    return obj.responseJSON.Message;
                }

                if (obj.responseText) {
                    return obj.responseText;
                }
            } catch (e) { }

            try {
                return JSON.stringify(obj);
            } catch (e) {
                return 'Cannot process message object';
            }
        }

        function get(url) {
            var promise = new Promise(function (resolve, reject) {
                var remainingTimeToNextGetInSeconds,
                    lastGetTime = sessionStorage.getItem(lastGetTimeKey);

                if (!lastGetTime || getTimeToNextPossibleGet(lastGetTime) < 0) {
                    jsonRequester.post(url)
                        .then(function (data) {
                            var content = data.Content;
                            sessionStorage.setItem(lastGetTimeKey, new Date());
                            resolve(content);
                        })
                        .catch(function (err) {
                            sessionStorage.setItem(lastGetTimeKey, new Date());
                            reject(makeResponse('error', getMessage(err)));
                        });
                } else {
                    remainingTimeToNextGetInSeconds = getTimeToNextPossibleGet(lastGetTime) / 1000.0;
                    reject(makeResponse('warning', 'Wait ' + remainingTimeToNextGetInSeconds + 's before get.'));
                }
            });

            return promise;
        }

        function save(url, content) {
            var promise = new Promise(function (resolve, reject) {
                var remainingTimeToNextSaveInSeconds,
                    contentHash,
                    lastSavedTime = sessionStorage.getItem(lastSavedTimeKey),
                    lastSavedHash = sessionStorage.getItem(contentHashKey);

                if (!lastSavedTime || getTimeToNextPossibleSave(lastSavedTime) < 0) {
                    contentHash = sha1(content).toString();

                    if (contentHash !== lastSavedHash) {
                        jsonRequester.put(url, {
                            data: {
                                content: content
                            }
                        }).then(function (res) {
                            sessionStorage.setItem(lastSavedTimeKey, new Date());
                            if (res.Status === 1) {
                                sessionStorage.setItem(contentHashKey, contentHash);
                                resolve(makeResponse('success', getMessage(res)));
                            } else {
                                reject(makeResponse('error', getMessage(res)));
                            }
                        }).catch(function (err) {
                            sessionStorage.setItem(lastSavedTimeKey, new Date());
                            reject(makeResponse('error', getMessage(err)));
                        });
                    } else {
                        reject(makeResponse('info', 'Content will not be saved because it is not modified.'));
                    }
                } else {
                    remainingTimeToNextSaveInSeconds = getTimeToNextPossibleSave(lastSavedTime) / 1000.0;
                    reject(makeResponse('warning', 'Wait ' + remainingTimeToNextSaveInSeconds + 's before save.'));
                }
            });

            return promise;
        }

        return {
            get: get,
            save: save
        };
    };
}(window));

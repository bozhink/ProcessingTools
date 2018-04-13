'use strict';

const
    MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS = 5000,
    MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_GETS_MILLISECONDS = 5000;

module.exports = function DocumentContentData(storage, keys, jsonRequester, sha1) {
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
        } catch (e) {}

        try {
            return JSON.stringify(obj);
        } catch (e) {
            return 'Cannot process message object';
        }
    }

    function get(url) {
        var promise = new Promise(function (resolve, reject) {
            var remainingTimeToNextGetInSeconds,
                lastGetTime = storage.getItem(keys.lastGetTimeKey);

            if (!lastGetTime || getTimeToNextPossibleGet(lastGetTime) < 0) {
                jsonRequester.post(url)
                    .then(function (data) {
                        var content = data.Content;
                        storage.setItem(keys.lastGetTimeKey, new Date());
                        resolve(content);
                    })
                    .catch(function (err) {
                        storage.setItem(keys.lastGetTimeKey, new Date());
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
                lastSavedTime = storage.getItem(keys.lastSavedTimeKey),
                lastSavedHash = storage.getItem(keys.contentHashKey);

            if (!lastSavedTime || getTimeToNextPossibleSave(lastSavedTime) < 0) {
                contentHash = sha1(content).toString();

                if (contentHash !== lastSavedHash) {
                    jsonRequester.put(url, {
                        data: {
                            content: content
                        }
                    }).then(function (res) {
                        storage.setItem(keys.lastSavedTimeKey, new Date());
                        if (res.Status === 1) {
                            storage.setItem(keys.contentHashKey, contentHash);
                            resolve(makeResponse('success', getMessage(res)));
                        } else {
                            reject(makeResponse('error', getMessage(res)));
                        }
                    }).catch(function (err) {
                        storage.setItem(keys.lastSavedTimeKey, new Date());
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

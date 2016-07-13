(function (window) {
    'use strict';

    const MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS = 5000;
    var toastr = window.toastr;

    window.DocumentSaveController = function (sessionStorage, lastSavedTimeKey, lastSavedHashKey, jsonRequester) {

        function getTimeToNextPossibleSave(lastSavedTime) {
            return MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS - (new Date() - new Date(lastSavedTime));
        }

        function doSaveRequest(content, contentHash) {
            function getErrorMessage(err) {
                if (!!err.Message) {
                    toastr.error(err.Message);
                } else {
                    toastr.error(JSON.stringify(err));
                }
            }

            function getSuccessMessage(res) {
                if (!!res.Message) {
                    toastr.success(res.Message);
                }
            }

            jsonRequester.put(window.saveLinkAddress, {
                data: {
                    content: content
                }
            }).then(function (res) {
                if (res.Status === 'OK') {
                    sessionStorage.setItem(lastSavedHashKey, contentHash);
                    getSuccessMessage(res);
                } else {
                    getErrorMessage(res);
                }
            }).catch(getErrorMessage);

            sessionStorage.setItem(lastSavedTimeKey, new Date());
        }

        function registerSaveAction(getContent) {
            if (!getContent) {
                throw 'getContent function is required';
            }

            window.save = function (quietMode) {
                var content,
                    contentHash,
                    remainingTimeToNextSaveInSeconds,
                    lastSavedTime = sessionStorage.getItem(lastSavedTimeKey),
                    lastSavedHash = sessionStorage.getItem(lastSavedHashKey);

                if (!lastSavedTime || getTimeToNextPossibleSave(lastSavedTime) < 0) {
                    content = getContent();
                    contentHash = window.CryptoJS.SHA1(content).toString();

                    if (contentHash !== lastSavedHash) {
                        doSaveRequest(content, contentHash);
                    } else {
                        if (!quietMode) {
                            toastr.info('Content will not be saved because it is not modified.');
                        }
                    }
                } else {
                    if (!quietMode) {
                        remainingTimeToNextSaveInSeconds = getTimeToNextPossibleSave(lastSavedTime) / 1000.0;
                        toastr.warning('Wait ' + remainingTimeToNextSaveInSeconds + 's before save.');
                    }
                }
            };
        }

        return {
            registerSaveAction: registerSaveAction
        };
    };
}(window));

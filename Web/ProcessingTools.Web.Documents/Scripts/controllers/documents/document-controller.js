(function (window) {
    'use strict';

    var toastr = window.toastr;

    window.DocumentController = function (sessionStorage, lastGetTimeKey, lastSavedTimeKey, contentHashKey, jsonRequester) {
        var data = new window.DocumentContentData(sessionStorage, lastGetTimeKey, lastSavedTimeKey, contentHashKey, jsonRequester);

        function raiseMessage(res) {
            switch (res.type) {
                case 'success':
                    toastr.success(res.message);
                    break;

                case 'info':
                    toastr.info(res.message);
                    break;

                case 'warning':
                    toastr.warning(res.message);
                    break;

                default:
                    toastr.error(res.message);
            }
        }

        function get(url, setContentCallback) {
            if (!setContentCallback) {
                throw 'setContentCallback function is required';
            }

            data.get(url)
                .then(function (res) {
                    setContentCallback(res);
                })
                .catch(function (res) {
                    raiseMessage(res);
                });
        }

        function save(url, quietMode, getContentCallback) {
            if (!getContentCallback) {
                throw 'getContentCallback function is required';
            }

            data.save(url, getContentCallback())
                .then(function (res) {
                    raiseMessage(res);
                })
                .catch(function (res) {
                    if (!quietMode) {
                        raiseMessage(res);
                    }
                });
        }

        function registerSaveAction(getContentCallback) {
            window.save = function (quietMode) {
                var url = window.saveLinkAddress;
                save(url, quietMode, getContentCallback);
            };
        }

        function registerGetAction(setContentCallback) {
            window.get = function () {
                var url = window.getLinkAddress;
                get(url, setContentCallback);
            };
        }

        return {
            get: get,
            save: save,
            registerSaveAction: registerSaveAction,
            registerGetAction: registerGetAction
        };
    };
}(window));

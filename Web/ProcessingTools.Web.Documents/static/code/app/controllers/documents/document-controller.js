(function (window) {
    'use strict';

    var toastr = window.toastr,
        app = window.app;

    window.DocumentController = function (sessionStorage, lastGetTimeKey, lastSavedTimeKey, contentHashKey, jsonRequester) {
        var dataService = new app.services.DocumentContentData(sessionStorage, lastGetTimeKey, lastSavedTimeKey, contentHashKey, jsonRequester);

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

        function get(url, setContentCallback, afterAction) {
            if (!setContentCallback) {
                throw 'setContentCallback function is required';
            }

            dataService.get(url)
                .then(function (res) {
                    setContentCallback(res);
                })
                .then(function () {
                    if (afterAction) {
                        afterAction();
                    }
                })
                .catch(function (res) {
                    raiseMessage(res);
                });
        }

        function save(url, quietMode, getContentCallback, afterAction) {
            if (!getContentCallback) {
                throw 'getContentCallback function is required';
            }

            dataService.save(url, getContentCallback())
                .then(function (res) {
                    raiseMessage(res);
                })
                .then(function () {
                    if (afterAction) {
                        afterAction();
                    }
                })
                .catch(function (res) {
                    if (!quietMode) {
                        raiseMessage(res);
                    }
                });
        }

        function registerSaveAction(getContentCallback, afterAction) {
            window.save = function (quietMode) {
                var url = window.saveLinkAddress;
                save(url, quietMode, getContentCallback, afterAction);
            };
        }

        function registerGetAction(setContentCallback, afterAction) {
            window.get = function () {
                var url = window.getLinkAddress;
                get(url, setContentCallback, afterAction);
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

'use strict';

module.exports = function DocumentController(dataService, reporter) {

    function get(url, quietMode, setContentCallback, done) {
        if (!setContentCallback) {
            throw 'setContentCallback function is required';
        }

        dataService.get(url)
            .then(function (res) {
                setContentCallback(res);
            })
            .then(function () {
                if (done) {
                    done();
                }
            })
            .catch(function (err) {
                if (!quietMode) {
                    reporter.raiseMessage(err);
                }
            });
    }

    function save(url, quietMode, getContentCallback, done) {
        if (!getContentCallback) {
            throw 'getContentCallback function is required';
        }

        dataService.save(url, getContentCallback())
            .then(function (res) {
                if (!quietMode) {
                    reporter.raiseMessage(res);
                }
            })
            .then(function () {
                if (done) {
                    done();
                }
            })
            .catch(function (err) {
                if (!quietMode) {
                    reporter.raiseMessage(err);
                }
            });
    }

    function createSaveAction(url, getContentCallback, done) {
        return function (quietMode) {
            save(url, quietMode, getContentCallback, done);
        };
    }

    function createGetAction(url, setContentCallback, done) {
        return function (quietMode) {
            get(url, quietMode, setContentCallback, done);
        };
    }

    return {
        get: get,
        save: save,
        createSaveAction: createSaveAction,
        createGetAction: createGetAction
    };
}
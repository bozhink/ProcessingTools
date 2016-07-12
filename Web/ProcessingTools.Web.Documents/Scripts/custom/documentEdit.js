(function (window) {

    const AUTOSAVE_TIME_MILLISECONDS = 60000,
MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS = 5000,
LAST_SAVED_TIME_KEY = 'LAST_SAVED_TIME_KEY',
LAST_SAVED_HASH_KEY = 'LAST_SAVED_HASH_KEY';

    initEditor('editor-container', initialContent);

    function initEditor(containerId, content) {
        var contentHash = CryptoJS.SHA1(content).toString();

        sessionStorage.setItem(LAST_SAVED_HASH_KEY, contentHash);

        require.config({
            paths: {
                'vs': '../../../node_modules/monaco-editor/min/vs'
            }
        });

        require(['vs/editor/editor.main'], function () {
            window.editor = monaco.editor.create(document.getElementById(containerId), {
                value: content,
                language: 'xml',
                // theme: 'vs-dark'
            });
        });
    }

    window.save = function (quietMode) {
        var lastSavedTime = sessionStorage.getItem(LAST_SAVED_TIME_KEY),
            lastSavedHash = sessionStorage.getItem(LAST_SAVED_HASH_KEY),
            content, contentHash;

        function getTimeToNextPossibleSave() {
            return MINIMAL_TIME_SPAN_BETWEEN_SEQUENTAL_SAVES_MILLISECONDS - (new Date() - new Date(lastSavedTime));
        }

        function doSaveRequest(content) {
            jsonRequester.put(saveLinkAddress, {
                data: {
                    content: content
                }
            })
            .then(function (res) {
                if (res.Status === 'OK') {
                    sessionStorage.setItem(LAST_SAVED_HASH_KEY, contentHash);
                    if ('Message' in res) {
                        toastr.success(res.Message);
                    }
                } else {
                    if ('Message' in res) {
                        toastr.error(res.Message);
                    } else {
                        toastr.error(JSON.stringify(res));
                    }
                }
            })
            .catch(function (err) {
                if ('Message' in err) {
                    toastr.error(err.Message);
                } else {
                    toastr.error(JSON.stringify(err));
                }
            });

            sessionStorage.setItem(LAST_SAVED_TIME_KEY, new Date());
        }

        if (!lastSavedTime || getTimeToNextPossibleSave() < 0) {

            content = window.editor.getValue();
            contentHash = CryptoJS.SHA1(content).toString();

            if (contentHash !== lastSavedHash) {
                doSaveRequest(content);
            } else {
                if (!quietMode) {
                    toastr.info('Content will not be saved because it is not modified.');
                }
            }
        } else {
            if (!quietMode) {
                toastr.info('Wait ' + (getTimeToNextPossibleSave() / 1000.0) + 's before save.');
            }
        }
    }

    // Autosave
    setInterval(function () {
        window.save(true);
    }, AUTOSAVE_TIME_MILLISECONDS);

    // Ctrl + s
    document.addEventListener("keydown", function (event) {
        var e = event || window.event;
        if (e.ctrlKey && e.which == 83) {
            e.preventDefault();
            window.save();
            return false;
        }
    });



}(window));
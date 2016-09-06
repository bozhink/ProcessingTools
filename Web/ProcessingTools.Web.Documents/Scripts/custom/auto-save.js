(function (window) {
    'use strict';

    const AUTOSAVE_TIME_MILLISECONDS = 60000;

    setInterval(function () {
        window.save(true);
    }, AUTOSAVE_TIME_MILLISECONDS);

}(window));
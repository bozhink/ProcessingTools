"use strict";

const TESTS_PATH = "ClientApp/tests";

var gulp = require("gulp");
var debug = require("gulp-debug");
var mocha = require("gulp-mocha");
var path = require("path");

module.exports.test = function () {
    return gulp.src(path.join(TESTS_PATH, "**/*.js"))
        .pipe(debug({
            title: "test"
        }))
        .pipe(mocha())
        .on("error", function () {
            this.emit("end");
        });
};

module.exports.watch = function (task) {
    return function () {
        if (task) {
            gulp.watch(path.join(TESTS_PATH, "**/*.js"), gulp.series(task));
        }
    }
}

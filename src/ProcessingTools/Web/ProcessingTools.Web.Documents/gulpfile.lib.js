"use strict";

const DIST_PATH = "wwwroot/build/dist";

var gulp = require("gulp");
var debug = require("gulp-debug");
var path = require("path");
var rename = require("gulp-rename");
var merge = require("merge-stream");
var packageJson = require("./package.json");

module.exports.deploy = function (done) {
    var stream, streams = [], name;

    for (name in packageJson.dependencies) {
        stream = gulp.src(path.join("./node_modules", name, "**/*"))
            .pipe(debug({
                title: "copy: library"
            }))
            .pipe(rename(p => {
                p.dirname = p.dirname.replace(/node_modules/, "lib")
            }))
            .pipe(gulp.dest(path.join(DIST_PATH)));

        streams.push(stream);
    }

    return merge(streams).end(done);
}

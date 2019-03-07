"use strict";

/**
 * Template paths
 */
const TEMPLATES_SRC_PATH = "ClientApp/templates";
const TEMPLATES_DIST_PATH = "wwwroot/build/dist/templates";

var gulp = require("gulp");
var debug = require("gulp-debug");
var htmlmin = require("gulp-htmlmin");
var rename = require("gulp-rename");
var merge = require("merge-stream");
var del = require("del");
var path = require("path");

var getCopyStream = (sourcePath, destinationPath) => gulp.src(path.join(sourcePath, "**/*"))
    .pipe(debug({
        title: "copy: Templates"
    }))
    .pipe(rename(p => {
        p.dirname = path.relative(sourcePath, p.dirname);
    }))
    .pipe(gulp.dest(path.join(".", destinationPath)))
    .pipe(debug({
        title: "minify: Templates"
    }))
    .pipe(htmlmin({
        collapseWhitespace: true,
        minifyCSS: true,
        minifyJS: true
    }))
    .pipe(rename(p => {
        p.basename += ".min";
    }))
    .pipe(gulp.dest(path.join(".", destinationPath)));

module.exports.build = function (done) {
    var stream = getCopyStream(TEMPLATES_SRC_PATH, TEMPLATES_DIST_PATH);

    return merge(stream).end(done);
}

module.exports.clean = function () {
    return del(TEMPLATES_DIST_PATH);
}

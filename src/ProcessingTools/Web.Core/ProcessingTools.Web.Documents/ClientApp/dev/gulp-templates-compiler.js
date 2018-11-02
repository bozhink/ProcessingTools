"use strict";

/**
 * Common paths.
 */
const OUT_PATH = "wwwroot/build/out";
const DIST_PATH = "wwwroot/build/dist";

/**
 * Template paths
 */
const TEMPLATES_SRC_PATH = "ClientApp/templates";
const TEMPLATES_DIST_PATH = DIST_PATH + "/templates";

var gulp = require("gulp");
var htmlmin = require("gulp-htmlmin");
var rename = require("gulp-rename");
var path = require("path");

module.exports.copyTemplates = function copyTemplates(done) {
    gulp.src(path.join(TEMPLATES_SRC_PATH, "**/*"))
        .pipe(gulp.dest(path.join(TEMPLATES_DIST_PATH)))
        .end(done);
}

module.exports.copyAndMinifyTemplates = function copyAndMinifyTemplates(done) {
    gulp.src(path.join(TEMPLATES_SRC_PATH, "**/*"))
        .pipe(htmlmin({
            collapseWhitespace: true,
            minifyCSS: true,
            minifyJS: true
        }))
        .pipe(rename(path => {
            path.basename += ".min";
        }))
        .pipe(gulp.dest(path.join(TEMPLATES_DIST_PATH)))
        .end(done);
}

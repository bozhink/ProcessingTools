"use strict";

/**
 * Common paths.
 */
const OUT_PATH = "wwwroot/build/out";
const DIST_PATH = "wwwroot/build/dist";

/**
 * Style paths
 */
const SASS_SRC_PATH = "ClientApp/styles/sass";
const LESS_SRC_PATH = "ClientApp/styles/less";
const CSS_SRC_PATH = "ClientApp/styles/css";
const CSS_DIST_PATH = DIST_PATH + "/css";

var gulp = require("gulp");
var rename = require("gulp-rename");
var path = require("path");
var less = require("gulp-less");
var cssmin = require("gulp-cssmin");

function renameForMinify(path) {
    path.basename += ".min";
}

module.exports.compileCss = function compileCss(done) {
    gulp.src(path.join(CSS_SRC_PATH, "**/*.css"))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)))
        .end(done);
}

module.exports.compileAndMinifyCss = function compileAndMinifyCss(done) {
    gulp.src(path.join(CSS_SRC_PATH, "**/*.css"))
        .pipe(cssmin())
        .pipe(rename(renameForMinify))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)))
        .end(done);
}

module.exports.compileSass = function compileSass(done) {
    // not supported
    done();
}

module.exports.compileAndMinifySass = function compileAndMinifySass(done) {
    // not supported
    done();
}

module.exports.compileLess = function compileLess(done) {
    gulp.src(path.join(LESS_SRC_PATH, "**/*.less"))
        .pipe(less())
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)))
        .end(done);
}

module.exports.compileAndMinifyLess = function compileAndMinifyLess(done) {
    gulp.src(path.join(LESS_SRC_PATH, "**/*.less"))
        .pipe(less())
        .pipe(cssmin())
        .pipe(rename(renameForMinify))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)))
        .end(done);
}

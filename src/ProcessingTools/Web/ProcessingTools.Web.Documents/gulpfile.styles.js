"use strict";

const SASS_SRC_PATH = "ClientApp/styles/sass";
const LESS_SRC_PATH = "ClientApp/styles/less";
const CSS_SRC_PATH = "ClientApp/styles/css";
const CSS_DIST_PATH = "wwwroot/build/dist/css";

var gulp = require("gulp");
var debug = require("gulp-debug");
var sourcemaps = require("gulp-sourcemaps");
var sass = require("gulp-sass");
var less = require("gulp-less");
var cssmin = require("gulp-cssmin");
var rename = require("gulp-rename");
var del = require("del");
var merge = require("merge-stream");
var path = require("path");

var getCssStream = (sourcePath, destinationPath) => gulp.src(path.join(sourcePath, "**/*.css"))
    .pipe(debug({
        title: "compile: CSS"
    }))
    .pipe(rename(p => {
        p.dirname = path.relative(sourcePath, p.dirname);
    }))
    .pipe(gulp.dest(path.join(".", destinationPath)))
    .pipe(debug({
        title: "minify: CSS"
    }))
    .pipe(cssmin())
    .pipe(rename(p => {
        p.basename += ".min";
    }))
    .pipe(gulp.dest(path.join(".", destinationPath)));

var getSassStream = (sourcePath, destinationPath) => gulp.src(path.join(sourcePath, "**/*.scss"))
    .pipe(debug({
        title: "compile: SASS"
    }))
    .pipe(sourcemaps.init())
    .pipe(sass().on("error", sass.logError))
    .pipe(sourcemaps.write())
    .pipe(rename(p => {
        p.dirname = path.relative(sourcePath, p.dirname);
    }))
    .pipe(gulp.dest(path.join(".", destinationPath)))
    .pipe(debug({
        title: "minify: SASS"
    }))
    .pipe(cssmin())
    .pipe(rename(p => {
        p.basename += ".min";
    }))
    .pipe(gulp.dest(path.join(".", destinationPath)));

var getLessStream = (sourcePath, destinationPath) => gulp.src(path.join(sourcePath, "**/*.less"))
    .pipe(debug({
        title: "compile: LESS"
    }))
    .pipe(sourcemaps.init())
    .pipe(less())
    .pipe(sourcemaps.write())
    .pipe(rename(p => {
        p.dirname = path.relative(sourcePath, p.dirname);
    }))
    .pipe(gulp.dest(path.join(".", destinationPath)))
    .pipe(debug({
        title: "minify: LESS"
    }))
    .pipe(cssmin())
    .pipe(rename(p => {
        p.basename += ".min";
    }))
    .pipe(gulp.dest(path.join(".", destinationPath)));

module.exports.buildCss = function (done) {
    var cssStream = getCssStream(CSS_SRC_PATH, CSS_DIST_PATH);

    return merge(cssStream).end(done);
}

module.exports.buildSass = function (done) {
    var sassStream = getSassStream(SASS_SRC_PATH, CSS_DIST_PATH);

    return merge(sassStream).end(done);
}

module.exports.buildLess = function (done) {
    var lessStream = getLessStream(LESS_SRC_PATH, CSS_DIST_PATH);
    
    return merge(lessStream).end(done);
}

module.exports.build = function (done) {
    var cssStream = getCssStream(CSS_SRC_PATH, CSS_DIST_PATH);
    var sassStream = getSassStream(SASS_SRC_PATH, CSS_DIST_PATH);
    var lessStream = getLessStream(LESS_SRC_PATH, CSS_DIST_PATH);
    
    return merge(cssStream, sassStream, lessStream).end(done);
}

module.exports.clean = function () {
    return del(CSS_DIST_PATH);
}

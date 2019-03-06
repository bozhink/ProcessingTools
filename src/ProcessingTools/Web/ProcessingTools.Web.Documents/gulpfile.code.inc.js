"use strict";

const APPS_RELATIVE_PATH = "apps";

const TS_SRC_PATH = "ClientApp/code/ts";
const JS_SRC_PATH = "ClientApp/code/js";
const JS_OUT_PATH = "wwwroot/build/out/js";
const JS_DIST_PATH = "wwwroot/build/dist/js";

var gulp = require("gulp");
var debug = require("gulp-debug");
var concat = require("gulp-concat");

var uglify = require("gulp-uglify");

var ts = require("gulp-typescript");
var tsProject = ts.createProject("./tsconfig.json");

var webpackStream = require("webpack-stream");
var webpackConfig = require("./webpack.config");

var rename = require("gulp-rename");
var merge = require("merge-stream");
var del = require("del");
var path = require("path");

var getCompileJavaScriptStream = (srcPath, outPath, distPath) => gulp.src(path.join(srcPath, "**/*.js"))
    .pipe(debug({
        title: "compile: JavaScript"
    }))
    .pipe(rename(p => {
        p.dirname = path.relative(srcPath, p.dirname);
    }))
    .pipe(gulp.dest(path.join(outPath)))
    .pipe(debug({
        title: "copy: JavaScript"
    }))
    .pipe(gulp.dest(path.join(distPath)))
    .pipe(debug({
        title: "minify: JavaScript"
    }))
    .pipe(uglify())
    .pipe(rename(p => {
        p.basename += ".min";
    }))
    .pipe(gulp.dest(path.join(distPath)));

var getCompileTypeScriptStream = (srcPath, outPath, distPath) => gulp.src(path.join(srcPath, "**/*.ts"))
    .pipe(debug({
        title: "compile: TypeScript"
    }))
    .pipe(tsProject())
    .js
    .pipe(rename(p => {
        p.dirname = path.relative(srcPath, p.dirname);
    }))
    .pipe(gulp.dest(path.join(outPath)))
    .pipe(debug({
        title: "copy: TypeScript"
    }))
    .pipe(gulp.dest(path.join(distPath)))
    .pipe(debug({
        title: "minify: TypeScript"
    }))
    .pipe(uglify())
    .pipe(rename(p => {
        p.basename += ".min";
    }))
    .pipe(gulp.dest(path.join(distPath)));

var getWebpackStream = (sourcePath, destinationPath, fileName) => gulp.src(path.join(sourcePath, fileName))
    .pipe(debug({
        title: `webpack compile: ${fileName}`
    }))
    .pipe(webpackStream({
        config: webpackConfig
    }))
    .on("error", function handleError() {
        this.emit("end");
    })
    .pipe(rename(p => {
        p.dirname = path.relative(sourcePath, p.dirname);
    }))
    .pipe(concat(path.join(destinationPath, fileName)))
    .pipe(debug({
        title: `webpack minify: ${fileName}`
    }))
    .pipe(uglify())
    .pipe(rename(p => {
        p.basename += ".min";
    }))
    .pipe(gulp.dest(path.join(destinationPath)));

module.exports.compileJavaScript = function (done) {
    var jsStream = getCompileJavaScriptStream(JS_SRC_PATH, JS_OUT_PATH, JS_DIST_PATH);

    return merge(jsStream).end(done);
}

module.exports.compileTypeScript = function (done) {
    var tsStream = getCompileTypeScriptStream(TS_SRC_PATH, JS_OUT_PATH, JS_DIST_PATH);

    return merge(tsStream).end(done);
}

module.exports.compile = function (done) {
    var jsStream = getCompileJavaScriptStream(JS_SRC_PATH, JS_OUT_PATH, JS_DIST_PATH);
    var tsStream = getCompileTypeScriptStream(TS_SRC_PATH, JS_OUT_PATH, JS_DIST_PATH);

    return merge(jsStream, tsStream).end(done);
}

module.exports.linkApp = function (fileName) {
    var sourcePath = path.join(JS_OUT_PATH, APPS_RELATIVE_PATH);
    var destinationPath = path.join(JS_DIST_PATH, APPS_RELATIVE_PATH);

    return function (done) {
        var stream = getWebpackStream(sourcePath, destinationPath, fileName);

        return merge(stream).end(done);
    }
}

module.exports.clean = function () {
    return del([
        JS_OUT_PATH,
        JS_DIST_PATH,
        tsProject.config.compilerOptions.outDir.toString()
    ]);
}

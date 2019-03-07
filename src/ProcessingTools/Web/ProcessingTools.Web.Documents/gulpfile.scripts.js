"use strict";

const TS_SRC_PATH = "ClientApp/code/ts";
const JS_SRC_PATH = "ClientApp/code/js";
const JS_OUT_PATH = "wwwroot/build/out/js";
const JS_DIST_PATH = "wwwroot/build/dist/js";

var gulp = require("gulp");
var debug = require("gulp-debug");
var path = require("path");
var rename = require("gulp-rename");
var merge = require("merge-stream");
var del = require("del");

var uglify = require("gulp-uglify");

var ts = require("gulp-typescript");
var tsProject = ts.createProject("./tsconfig.json");

var PluginError = require("plugin-error");
var log = require("fancy-log");

var webpack = require("webpack");
var webpackConfig = require("./webpack.config");

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

module.exports.webpack = function (done) {
    var wp = webpack(webpackConfig);

    wp.run(function (err, stats) {
        if (err) {
            throw new PluginError("webpack", err);
        }

        log("[webpack]", stats.toString({
            all: true,
            env: true
        }));

        done();
    });
}

module.exports.clean = function () {
    return del([
        webpackConfig.output.path,
        tsProject.config.compilerOptions.outDir.toString(),
        JS_DIST_PATH,
        JS_OUT_PATH
    ]);
}

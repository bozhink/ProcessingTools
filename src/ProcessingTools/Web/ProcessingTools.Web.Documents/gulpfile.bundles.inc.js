"use strict";

var regex = {
    css: /\.css$/,
    html: /\.(html|htm)$/,
    js: /\.js$/
};

var gulp = require("gulp");
var debug = require("gulp-debug");
var concat = require("gulp-concat");
var cssmin = require("gulp-cssmin");
var htmlmin = require("gulp-htmlmin");
var uglify = require("gulp-uglify");
var sourcemaps = require("gulp-sourcemaps");
var merge = require("merge-stream");
var del = require("del");

var bundleconfig = require("./bundleconfig.json");

function getBundles(re) {
    return bundleconfig.filter((bundle) => re.test(bundle.outputFileName));
}

module.exports.processJavaScript = function (done) {
    var streams = getBundles(regex.js).map(function (bundle) {
        var stream = gulp.src(bundle.inputFiles, { base: "." })
            .pipe(debug({
                title: "bundleconfig: Process JavaScript"
            }))
            .pipe(concat(bundle.outputFileName));

        if (bundle.sourceMap) {
            stream = stream.pipe(sourcemaps.init());
        }
        
        if (bundle.minify && bundle.minify.enabled) {
            stream = stream.pipe(uglify());
        }

        if (bundle.sourceMap) {
            stream = stream.pipe(sourcemaps.write());
        }

        stream = stream.pipe(gulp.dest("."));
        
        return stream;
    });

    return merge(streams).end(done);
}

module.exports.processCss = function (done) {
    var streams = getBundles(regex.css).map(function (bundle) {
        var stream = gulp.src(bundle.inputFiles, { base: "." })
            .pipe(debug({
                title: "bundleconfig: Process CSS"
            }))
            .pipe(concat(bundle.outputFileName));

        if (bundle.minify && bundle.minify.enabled) {
            stream = stream.pipe(cssmin());
        }
        
        stream = stream.pipe(gulp.dest("."));

        return stream;
    });

    return merge(streams).end(done);
}

module.exports.processHtml = function (done) {
    var streams = getBundles(regex.html).map(function (bundle) {
        var stream = gulp.src(bundle.inputFiles, { base: "." })
            .pipe(debug({
                title: "bundleconfig: Process HTML"
            }))
            .pipe(concat(bundle.outputFileName));
        
        if (bundle.minify && bundle.minify.enabled) {
            stream = stream.pipe(htmlmin({
                    collapseWhitespace: true,
                    minifyCSS: true,
                    minifyJS: true
                }));
        }

        stream = stream.pipe(gulp.dest("."));

        return stream;
    });

    return merge(streams).end(done);
}

module.exports.clean = function () {
    var files = bundleconfig.map((bundle) => bundle.outputFileName );
    return del(files);
}

module.exports.watch = function () {
    getBundles(regex.js).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, gulp.series("min:js"));
    });

    getBundles(regex.css).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, gulp.series("min:css"));
    });

    getBundles(regex.html).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, gulp.series("min:html"));
    });
}

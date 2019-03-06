"use strict";

/**
 * Common paths.
 */
const OUT_PATH = "wwwroot/build/out";
const DIST_PATH = "wwwroot/build/dist";

/**
 * Test paths
 */
const TESTS_PATH = "ClientApp/tests";

/**
 * Common
 */

var PluginError = require("plugin-error");
var gulp = require("gulp");
var log = require("fancy-log");
var debug = require("gulp-debug");
var del = require("del");
var mocha = require("gulp-mocha");
var path = require("path");
var webpack = require("webpack");
var bundle = require("./gulpfile.bundles.inc");
var code = require("./gulpfile.code.inc");
var styles = require("./gulpfile.styles.inc");
var templates = require("./gulpfile.templates.inc");










function cleanBuild() {
    return del([
        OUT_PATH,
        DIST_PATH
    ]);
}







/**
 * Tasks
 */

/**
 * min
 */
gulp.task("min:js", gulp.series(bundle.processJavaScript));
gulp.task("min:css", gulp.series(bundle.processCss));
gulp.task("min:html", gulp.series(bundle.processHtml));
gulp.task("min", gulp.parallel("min:js", "min:css", "min:html"));

/**
 * clean
 */
gulp.task("clean", gulp.series(
    bundle.clean,
    cleanBuild,
    code.clean,
    styles.clean,
    templates.clean
));

gulp.task("watch", gulp.series(bundle.watch));



/**
 * *************************************************************
 * 
 * Build application code.
 * 
 * *************************************************************
 */

gulp.task("webpack", gulp.series(function (callback) {
    var wp = webpack({
        // configuration
    });

    wp.run(function (error, stats) {
        if (error) {
            throw new PluginError("webpack", error);
        }

        log("[webpack]", stats.toString({
            all: true,
            env: true
        }));

        if (callback && typeof callback === "function") {
            callback();
        }
    });
}));

/**
 * Compile code.
 */
gulp.task("compile:code:js", gulp.series(code.compileJavaScript));
gulp.task("compile:code:ts", gulp.series(code.compileTypeScript));
gulp.task("compile:code", gulp.series(code.compile));


/**
 * Link apps.
 */
gulp.task("pack:app:bio:data", gulp.series(code.linkApp("bio-data-app.js")));
gulp.task("pack:app:documents:edit", gulp.series(code.linkApp("document-edit.js")));
gulp.task("pack:app:documents:preview", gulp.series(code.linkApp("document-preview.js")));
gulp.task("pack:app:index:page", gulp.series(code.linkApp("files-index.js")));
gulp.task("pack:app:tools:jsonToCSharp", gulp.series(code.linkApp("json-to-csharp.js")));
gulp.task("pack:app:tools:textEditor:monaco", gulp.series(code.linkApp("text-editor.monaco.js")));

gulp.task("link:code:apps", gulp.parallel(
    "pack:app:bio:data",
    "pack:app:documents:edit",
    "pack:app:documents:preview",
    "pack:app:index:page",
    "pack:app:tools:jsonToCSharp",
    "pack:app:tools:textEditor:monaco"
));

/**
 * Build all.
 */
gulp.task("build:templates", gulp.series(templates.build));
gulp.task("build:styles", gulp.series(styles.build));
gulp.task("build:code", gulp.series("compile:code"));


gulp.task("build", gulp.series("build:code", "build:styles", "build:templates"));

/**
 * Run tests.
 */
gulp.task("test", gulp.series(function () {
    return gulp.src(path.join(TESTS_PATH, "**/*.js"))
        .pipe(debug({
            title: "test"
        }))
        .pipe(mocha())
        .on("error", function () {
            this.emit("end");
        });
}));

gulp.task("watch-tests", gulp.series(function () {
    gulp.watch(path.join(TESTS_PATH, "**/*.js"), ["test"]);
}));

/**
 * Default task.
 */
gulp.task("default", gulp.series("clean", "build", "test"));

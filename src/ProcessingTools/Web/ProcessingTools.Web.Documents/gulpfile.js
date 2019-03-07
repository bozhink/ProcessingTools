"use strict";

const OUT_PATH = "wwwroot/build/out";
const DIST_PATH = "wwwroot/build/dist";

var gulp = require("gulp");
var del = require("del");

var bundles = require("./gulpfile.bundles");
var styles = require("./gulpfile.styles");
var scripts = require("./gulpfile.scripts");
var templates = require("./gulpfile.templates");
var tests = require("./gulpfile.tests");

gulp.task("clean", gulp.series(() => del([ OUT_PATH, DIST_PATH ])));

gulp.task("bundles:min:css", gulp.series(bundles.processCss));
gulp.task("bundles:min:js", gulp.series(bundles.processJavaScript));
gulp.task("bundles:min:html", gulp.series(bundles.processHtml));
gulp.task("bundle", gulp.parallel("bundles:min:css", "bundles:min:js", "bundles:min:html"));
gulp.task("watch:bundles", gulp.series(bundles.watch("bundles:min:css", "bundles:min:js", "bundles:min:html")));

gulp.task("compile:code:js", gulp.series(scripts.compileJavaScript));
gulp.task("compile:code:ts", gulp.series(scripts.compileTypeScript));
gulp.task("compile:code", gulp.series(scripts.compile));
gulp.task("webpack", gulp.series(scripts.webpack));

gulp.task("build:styles", gulp.series(styles.build));
gulp.task("build:scripts", gulp.series(scripts.compile, scripts.webpack));
gulp.task("build:templates", gulp.series(templates.build));
gulp.task("build", gulp.parallel("build:styles", "build:scripts", "build:templates"));
gulp.task("rebuild", gulp.series("clean", "build"));

gulp.task("test", gulp.series(tests.test));
gulp.task("watch:tests", gulp.series(tests.watch("test")));

gulp.task("default", gulp.series("clean", "build", "test"));

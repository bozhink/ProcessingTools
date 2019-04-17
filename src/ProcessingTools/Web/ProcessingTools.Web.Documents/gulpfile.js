"use strict";

var gulp = require("gulp");
var del = require("del");

var bundles = require("./gulpfile.bundles");
var styles = require("./gulpfile.styles");
var scripts = require("./gulpfile.scripts");
var templates = require("./gulpfile.templates");
var lib = require("./gulpfile.lib");
var tests = require("./gulpfile.tests");


gulp.task("clean", gulp.series(() => del([ "wwwroot/build" ])));

gulp.task("lib", gulp.series(lib.deploy));

gulp.task("bundles:min:css", gulp.series(bundles.processCss));
gulp.task("bundles:min:js", gulp.series(bundles.processJavaScript));
gulp.task("bundles:min:html", gulp.series(bundles.processHtml));
gulp.task("bundle", gulp.parallel("bundles:min:css", "bundles:min:js", "bundles:min:html"));
gulp.task("watch:bundles", gulp.series(bundles.watch("bundles:min:css", "bundles:min:js", "bundles:min:html")));

gulp.task("compile:code:js", gulp.series(scripts.compileJavaScript));
gulp.task("compile:code:ts", gulp.series(scripts.compileTypeScript));
gulp.task("compile:code", gulp.series(scripts.compile));

gulp.task("build:styles", gulp.series(styles.build));
gulp.task("build:scripts", gulp.series(scripts.compile));
gulp.task("build:templates", gulp.series(templates.build));
gulp.task("build", gulp.parallel("build:styles", "build:scripts", "build:templates"));

gulp.task("webpack", gulp.series(scripts.webpack));

gulp.task("test", gulp.series(tests.test));
gulp.task("watch:tests", gulp.series(tests.watch("test")));

gulp.task("watch", gulp.parallel("watch:bundles", "watch:tests"));

gulp.task("default", gulp.series("clean", "build", "test"));

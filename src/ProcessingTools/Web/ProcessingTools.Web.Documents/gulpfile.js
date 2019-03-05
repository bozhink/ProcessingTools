"use strict";

/**
 * Common paths.
 */
const OUT_PATH = "wwwroot/build/out";
const DIST_PATH = "wwwroot/build/dist";

/**
 * Code paths.
 */
const APPS_RELATIVE_PATH = "apps";

const TS_SRC_PATH = "ClientApp/code/ts";
const JS_SRC_PATH = "ClientApp/code/js";
const JS_OUT_PATH = OUT_PATH + "/js";
const JS_DIST_PATH = DIST_PATH + "/js";

/**
 * Style paths
 */
const SASS_SRC_PATH = "ClientApp/styles/sass";
const LESS_SRC_PATH = "ClientApp/styles/less";
const CSS_SRC_PATH = "ClientApp/styles/css";
const CSS_DIST_PATH = DIST_PATH + "/css";

/**
 * Template paths
 */
const TEMPLATES_SRC_PATH = "ClientApp/templates";
const TEMPLATES_DIST_PATH = DIST_PATH + "/templates";

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
var sourcemaps = require("gulp-sourcemaps");
var concat = require("gulp-concat");
var less = require("gulp-less");
var cssmin = require("gulp-cssmin");
var htmlmin = require("gulp-htmlmin");
var uglify = require("gulp-uglify");
var rename = require("gulp-rename");
var merge = require("merge-stream");
var del = require("del");
var mocha = require("gulp-mocha");
var ts = require("gulp-typescript");
var tsProject = ts.createProject("./tsconfig.json");
var bundleconfig = require("./bundleconfig.json");
var path = require("path");
var pump = require("pump");
var webpackStream = require("webpack-stream");
var webpack = require("webpack");
var webpackConfig = require("./webpack.config");
var Fiber = require("fibers");
var sass = require("gulp-sass");
sass.compiler = require("node-sass");

function getBundles(regexPattern) {
    return bundleconfig.filter(function (bundle) {
        return regexPattern.test(bundle.outputFileName);
    });
}

function bundleconfigProcessJavaScript(done) {
    var tasks = getBundles(regex.js).map(function (bundle) {
        return gulp.src(bundle.inputFiles, {
                base: "."
            })
            .pipe(debug({
                title: "bundleconfigProcessJavaScript"
            }))
            .pipe(concat(bundle.outputFileName))
            .pipe(uglify())
            .pipe(gulp.dest("."));
    });
    return merge(tasks).end(done);
}

function bundleconfigProcessCss(done) {
    var tasks = getBundles(regex.css).map(function (bundle) {
        return gulp.src(bundle.inputFiles, {
                base: "."
            })
            .pipe(debug({
                title: "bundleconfigProcessCss"
            }))
            .pipe(concat(bundle.outputFileName))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    });
    return merge(tasks).end(done);
}

function bundleconfigProcessHtml(done) {
    var tasks = getBundles(regex.html).map(function (bundle) {
        return gulp.src(bundle.inputFiles, {
                base: "."
            })
            .pipe(debug({
                title: "bundleconfigProcessHtml"
            }))
            .pipe(concat(bundle.outputFileName))
            .pipe(htmlmin({
                collapseWhitespace: true,
                minifyCSS: true,
                minifyJS: true
            }))
            .pipe(gulp.dest("."));
    });
    return merge(tasks).end(done);
}

function bundleconfigWatch() {
    getBundles(regex.js).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:js"]);
    });

    getBundles(regex.css).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:css"]);
    });

    getBundles(regex.html).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:html"]);
    });
}

function cleanBundle() {
    var files = bundleconfig.map(function (bundle) {
        return bundle.outputFileName;
    });

    return del(files);
}

function cleanBuild() {
    return del([
        JS_OUT_PATH,
        JS_DIST_PATH,
        CSS_DIST_PATH,
        tsProject.config.compilerOptions.outDir.toString(),
        OUT_PATH,
        DIST_PATH
    ]);
}

function copyTemplates() {
    return gulp.src(path.join(TEMPLATES_SRC_PATH, "**/*"))
        .pipe(debug({
            title: "copyTemplates"
        }))
        .pipe(rename(p => {
            p.dirname = path.relative(TEMPLATES_SRC_PATH, p.dirname);
        }))
        .pipe(gulp.dest(path.join(TEMPLATES_DIST_PATH)));
}

function copyAndMinifyTemplates() {
    return gulp.src(path.join(TEMPLATES_SRC_PATH, "**/*"))
        .pipe(debug({
            title: "copyAndMinifyTemplates"
        }))
        .pipe(htmlmin({
            collapseWhitespace: true,
            minifyCSS: true,
            minifyJS: true
        }))
        .pipe(rename(p => {
            p.dirname = path.relative(TEMPLATES_SRC_PATH, p.dirname);
            p.basename += ".min";
        }))
        .pipe(gulp.dest(path.join(TEMPLATES_DIST_PATH)));
}

function compileCss() {
    return gulp.src(path.join(CSS_SRC_PATH, "**/*.css"))
        .pipe(debug({
            title: "compileCss"
        }))
        .pipe(rename(p => {
            p.dirname = path.relative(CSS_SRC_PATH, p.dirname);
        }))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
}

function compileAndMinifyCss() {
    return gulp.src(path.join(CSS_SRC_PATH, "**/*.css"))
        .pipe(debug({
            title: "compileAndMinifyCss"
        }))
        .pipe(cssmin())
        .pipe(rename(p => {
            p.dirname = path.relative(CSS_SRC_PATH, p.dirname);
            p.basename += ".min";
        }))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
}

function compileSass() {
    return gulp.src(path.join(SASS_SRC_PATH, "**/*.scss"))
        .pipe(debug({
            title: "compileSass"
        }))
        .pipe(sourcemaps.init())
        .pipe(sass({ fiber: Fiber }).on("error", sass.logError))
        .pipe(sourcemaps.write())
        .pipe(rename(p => {
            p.dirname = path.relative(SASS_SRC_PATH, p.dirname);
        }))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
}

function compileAndMinifySass() {
    return gulp.src(path.join(SASS_SRC_PATH, "**/*.scss"))
        .pipe(debug({
            title: "compileSass"
        }))
        .pipe(sass({ fiber: Fiber, outputStyle: "compressed" }).on("error", sass.logError))
        .pipe(cssmin())
        .pipe(rename(p => {
            p.dirname = path.relative(SASS_SRC_PATH, p.dirname);
            p.basename += ".min";
        }))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
}

function compileLess() {
    return gulp.src(path.join(LESS_SRC_PATH, "**/*.less"))
        .pipe(debug({
            title: "compileLess"
        }))
        .pipe(sourcemaps.init())
        .pipe(less())
        .pipe(sourcemaps.write())
        .pipe(rename(p => {
            p.dirname = path.relative(LESS_SRC_PATH, p.dirname);
        }))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
}

function compileAndMinifyLess() {
    return gulp.src(path.join(LESS_SRC_PATH, "**/*.less"))
        .pipe(debug({
            title: "compileAndMinifyLess"
        }))
        .pipe(less())
        .pipe(cssmin())
        .pipe(rename(p => {
            p.dirname = path.relative(LESS_SRC_PATH, p.dirname);
            p.basename += ".min";
        }))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
}

function compileJavaScript() {
    return gulp.src(path.join(JS_SRC_PATH, "**/*.js"))
        .pipe(debug({
            title: "compileJavaScript"
        }))
        .pipe(rename(p => {
            p.dirname = path.relative(JS_SRC_PATH, p.dirname);
        }))
        .pipe(gulp.dest(path.join(JS_OUT_PATH)));
}

function compileTypeScript() {
    return gulp.src(path.join(TS_SRC_PATH, "**/*.ts"))
        .pipe(debug({
            title: "compileTypeScript"
        }))
        .pipe(tsProject())
        .js
        .pipe(rename(p => {
            p.dirname = path.relative(TS_SRC_PATH, p.dirname);
        }))
        .pipe(gulp.dest(path.join(JS_OUT_PATH)));
}

function copyCode() {
    return gulp.src(path.join(JS_OUT_PATH, "**/*.js"))
        .pipe(debug({
            title: "copyCode"
        }))
        .pipe(gulp.dest(JS_DIST_PATH));
}

function copyAndMinifyCode() {
    return pump([
        //gulp.src(path.join(JS_OUT_PATH, "**/*.js")),
        gulp.src([path.join(JS_OUT_PATH, "**/site.js"), path.join(JS_OUT_PATH, "**/cookie-consent.js")]),
        debug({
            title: "copyAndMinifyCode"
        }),
        uglify(),
        rename(p => {
            p.dirname = path.relative(JS_OUT_PATH, p.dirname);
            p.basename += ".min";
        }),
        gulp.dest(JS_DIST_PATH)
    ]);
}

function JsAppFactory() {
    this.createBuild = function (srcFileName, distFileName, runUglify) {
        return function () {
            var stream = gulp.src(path.join(JS_OUT_PATH, APPS_RELATIVE_PATH, srcFileName))
                .pipe(debug({
                    title: "JsAppFactory " + srcFileName
                }))
                //.pipe(named())
                .pipe(webpackStream({
                    config: webpackConfig
                }))
                .on("error", function handleError() {
                    this.emit("end");
                })
                .pipe(concat(distFileName));

            if (runUglify) {
                stream = stream.pipe(uglify());
            }

            stream = stream.pipe(gulp.dest(path.join(JS_DIST_PATH, APPS_RELATIVE_PATH)));

            return stream;
        }
    }
}

var jsAppFactory = new JsAppFactory();

var regex = {
    css: /\.css$/,
    html: /\.(html|htm)$/,
    js: /\.js$/
};

/**
 * Tasks
 */

/**
 * min
 */
gulp.task("min:js", gulp.series(bundleconfigProcessJavaScript));
gulp.task("min:css", gulp.series(bundleconfigProcessCss));
gulp.task("min:html", gulp.series(bundleconfigProcessHtml));
gulp.task("min", gulp.parallel("min:js", "min:css", "min:html"));

/**
 * clean
 */
gulp.task("clean", gulp.series(
    cleanBundle,
    cleanBuild
));

gulp.task("watch", gulp.series(bundleconfigWatch));

/**
 * *************************************************************
 * 
 * Build templates
 * 
 * *************************************************************
 */
gulp.task("build:templates", gulp.series(
    copyTemplates,
    copyAndMinifyTemplates
));

/**
 * *************************************************************
 * 
 * Build styles.
 * 
 * *************************************************************
 */

gulp.task("compile:styles", gulp.parallel(
    compileCss,
    compileSass,
    compileLess
));
gulp.task("compile:styles:min", gulp.parallel(
    compileAndMinifyCss,
    compileAndMinifySass,
    compileAndMinifyLess
));
gulp.task("build:styles", gulp.series(
    "compile:styles",
    "compile:styles:min"
));

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
gulp.task("compile:code:js", gulp.series(compileJavaScript));
gulp.task("compile:code:ts", gulp.series(compileTypeScript));

gulp.task("compile:code", gulp.series(
    "compile:code:js",
    "compile:code:ts"
));

/**
 * Copy code.
 */
gulp.task("copy:code", gulp.series("compile:code", copyCode));
gulp.task("copy:code:min", gulp.series("compile:code", copyAndMinifyCode));

/**
 * Link apps.
 */


gulp.task("pack:app:bio:data", gulp.series(jsAppFactory.createBuild("bio-data-app.js", "bio-data-app.min.js", true)));
gulp.task("pack:app:documents:edit", gulp.series(jsAppFactory.createBuild("document-edit.js", "document-edit.min.js", true)));
gulp.task("pack:app:documents:preview", gulp.series(jsAppFactory.createBuild("document-preview.js", "document-preview.min.js", true)));
gulp.task("pack:app:index:page", gulp.series(jsAppFactory.createBuild("files-index.js", "files-index.min.js", true)));
gulp.task("pack:app:tools:jsonToCSharp", gulp.series(jsAppFactory.createBuild("json-to-csharp.js", "json-to-csharp.min.js", false)));
gulp.task("pack:app:tools:textEditor:monaco", gulp.series(jsAppFactory.createBuild("text-editor.monaco.js", "text-editor.monaco.min.js", false)));

gulp.task("link:code:apps", gulp.series(
    "copy:code",
    "pack:app:bio:data",
    "pack:app:documents:edit",
    "pack:app:documents:preview",
    "pack:app:index:page",
    "pack:app:tools:jsonToCSharp",
    "pack:app:tools:textEditor:monaco"
));

/**
 * Build all code.
 */
gulp.task("build:code", gulp.series(
    "compile:code",
    "copy:code",
    "copy:code:min"
));

/**
 * Build all.
 */
gulp.task("build", gulp.series(
    "build:code",
    "build:styles",
    "build:templates"
));

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

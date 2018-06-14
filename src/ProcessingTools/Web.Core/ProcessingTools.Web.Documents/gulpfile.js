"use strict";

const APPS_RELATIVE_PATH = "apps";

const JS_SRC_PATH = "ClientApp/code/js";
const JS_DIST_PATH = "wwwroot/build/dist/js";

const TS_SRC_PATH = "ClientApp/code/ts";
const TS_DIST_PATH = "wwwroot/build/dist/js";

const CSS_SRC_PATH = "ClientApp/styles/css";
const CSS_DIST_PATH = "wwwroot/build/dist/css";

const LESS_SRC_PATH = "ClientApp/styles/less";
const LESS_DIST_PATH = "wwwroot/build/dist/css";

const SASS_SRC_PATH = "ClientApp/styles/sass";
const SASS_DIST_PATH = "wwwroot/build/dist/css";

const TEMPLATES_SRC_PATH = "ClientApp/templates";
const TEMPLATES_DIST_PATH = "wwwroot/build/dist/templates";

const TESTS_PATH = "ClientApp/tests";

var gulp = require("gulp");
var gulpUtil = require("gulp-util");
var concat = require("gulp-concat");
var less = require("gulp-less");
var cssmin = require("gulp-cssmin");
var htmlmin = require("gulp-htmlmin");
var uglify = require("gulp-uglify");
var merge = require("merge-stream");
var del = require("del");
var mocha = require("gulp-mocha");
var ts = require("gulp-typescript");
var tsProject = ts.createProject("./tsconfig.json");
var bundleconfig = require("./bundleconfig.json");
var path = require("path");
var webpackStream = require("webpack-stream");
var webpack = require("webpack");
var WebpackDevServer = require("webpack-dev-server");

function getBundles(regexPattern) {
    return bundleconfig.filter(function (bundle) {
        return regexPattern.test(bundle.outputFileName);
    });
}

function JsAppFactory() {
    this.createBuild = function (srcFileName, distFileName, uglify) {
        return function () {
            var stream = gulp.src(path.join(JS_SRC_PATH, APPS_RELATIVE_PATH, srcFileName))
                .pipe(webpackStream())
                .pipe(concat(distFileName));

            if (uglify) {
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
 * min
 */

gulp.task("min", ["min:js", "min:css", "min:html"]);

gulp.task("min:js", function () {
    var tasks = getBundles(regex.js).map(function (bundle) {
        return gulp.src(bundle.inputFiles, {
                base: "."
            })
            .pipe(concat(bundle.outputFileName))
            .pipe(uglify())
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("min:css", function () {
    var tasks = getBundles(regex.css).map(function (bundle) {
        return gulp.src(bundle.inputFiles, {
                base: "."
            })
            .pipe(concat(bundle.outputFileName))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("min:html", function () {
    var tasks = getBundles(regex.html).map(function (bundle) {
        return gulp.src(bundle.inputFiles, {
                base: "."
            })
            .pipe(concat(bundle.outputFileName))
            .pipe(htmlmin({
                collapseWhitespace: true,
                minifyCSS: true,
                minifyJS: true
            }))
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

/**
 * clean
 */

gulp.task("clean", ["clean:bundle", "clean:build"]);

gulp.task("clean:bundle", function () {
    var files = bundleconfig.map(function (bundle) {
        return bundle.outputFileName;
    });

    return del(files);
});

gulp.task("clean:build", function () {
    return del([
        JS_DIST_PATH,
        TS_DIST_PATH,
        CSS_DIST_PATH,
        LESS_DIST_PATH,
        SASS_DIST_PATH,
        tsProject.config.compilerOptions.outDir.toString()
    ]);
});

gulp.task("watch", function () {
    getBundles(regex.js).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:js"]);
    });

    getBundles(regex.css).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:css"]);
    });

    getBundles(regex.html).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:html"]);
    });
});

/**
 * Copy all templates to the distribution directory
 */
gulp.task("copy-templates", function () {
    return gulp.src(path.join(TEMPLATES_SRC_PATH, "**/*"))
        .pipe(gulp.dest(path.join(TEMPLATES_DIST_PATH)));
});

/**
 * Copy JavaScript files to the distribution directory
 */
gulp.task("copy-js", function () {
    return gulp.src(path.join(JS_SRC_PATH, "**/*.js"))
        .pipe(gulp.dest(path.join(JS_DIST_PATH)));
});

/**
 * Compile and minify LESS files
 */
gulp.task("compile-less", function () {
    return gulp.src(path.join(LESS_SRC_PATH, "**/*.less"))
        .pipe(less())
        .pipe(cssmin())
        .pipe(gulp.dest(path.join(LESS_DIST_PATH)));
});

gulp.task("webpack", function (callback) {
    webpack({
        // configuration
    }, function (error, stats) {
        if (error) {
            throw new gulpUtil.PluginError("webpack", error);
        }

        gulpUtil.log("[webpack]", stats.toString({
            // output options
        }));

        if (callback && typeof callback === "function") {
            callback();
        }
    });
});

gulp.task("webpack-dev-server", function (callback) {
    var compiler = webpack({
        // configuration
    });

    new WebpackDevServer(compiler, {
        // server and middleware options
    }).listen(9090, "localhost", function (error) {
        if (error) {
            throw new gulpUtil.PluginError("webpack-dev-server", error);
        }

        gulpUtil.log("[webpack-dev-server]", "http://localhost:9090/webpack-dev-server/index.html");

        // keep the server alive or continue?
        // if (callback && typeof callback === "function") {
        //     callback();
        // }
    })
});

/**
 * Build apps.
 */

gulp.task("build-apps", [
    "build-bio-data-app",
    "build-document-edit",
    "build-document-preview",
    "build-files-index"
]);

gulp.task("build-bio-data-app", jsAppFactory.createBuild("bio-data-app.js", "bio-data-app.min.js", false));
gulp.task("build-document-edit", jsAppFactory.createBuild("document-edit.js", "document-edit.min.js", false));
gulp.task("build-document-preview", jsAppFactory.createBuild("document-preview.js", "document-preview.min.js", false));
gulp.task("build-files-index", jsAppFactory.createBuild("files-index.js", "files-index.min.js", false));

/**
 * Build Typescript.
 */

gulp.task("build-typescript", function () {
    return tsProject.src()
        .pipe(tsProject())
        .js.pipe(gulp.dest(tsProject.config.compilerOptions.outDir.toString()));
});

/**
 * Build all code
 */
gulp.task("build", [
    "build-typescript",
    "build-apps",
    "compile-less",
    "copy-js",
    "copy-templates",
    "min"
]);

/**
 * Run tests
 */
gulp.task("test", function () {
    return gulp.src(path.join(TESTS_PATH, "**/*.js"))
        .pipe(mocha())
        .on("error", function () {
            this.emit("end");
        });
});

gulp.task("watch-tests", function () {
    gulp.watch(path.join(TESTS_PATH, "**/*.js"), ["test"]);
});

/**
 * Default task
 */
gulp.task("default", ["build", "test"]);

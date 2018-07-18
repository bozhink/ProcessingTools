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
var gulp = require("gulp");
var gulpUtil = require("gulp-util");
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
var WebpackDevServer = require("webpack-dev-server");
var webpackConfig = require("./webpack.config");

function renameForMinify(path) {
    //path.dirname += "/ciao";
    path.basename += ".min";
    //path.extname = ".md";
}

function getBundles(regexPattern) {
    return bundleconfig.filter(function (bundle) {
        return regexPattern.test(bundle.outputFileName);
    });
}

function JsAppFactory() {
    this.createBuild = function (srcFileName, distFileName, runUglify) {
        return function () {
            var stream = gulp.src(path.join(JS_OUT_PATH, APPS_RELATIVE_PATH, srcFileName))
                .pipe(webpackStream(webpackConfig))
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
        JS_OUT_PATH,
        JS_DIST_PATH,
        CSS_DIST_PATH,
        tsProject.config.compilerOptions.outDir.toString(),
        OUT_PATH,
        DIST_PATH
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
 * *************************************************************
 * 
 * Build templates
 * 
 * *************************************************************
 */

gulp.task("build:templates", [
    "compile:templates:copy",
    "compile:templates:copy:min"
]);

/**
 * Copy all templates to the distribution directory
 */
gulp.task("compile:templates:copy", function () {
    return gulp.src(path.join(TEMPLATES_SRC_PATH, "**/*"))
        .pipe(gulp.dest(path.join(TEMPLATES_DIST_PATH)));
});

/**
 * Copy and minify all templates to the distribution directory
 */
gulp.task("compile:templates:copy:min", function () {
    return gulp.src(path.join(TEMPLATES_SRC_PATH, "**/*"))
        .pipe(htmlmin({
            collapseWhitespace: true,
            minifyCSS: true,
            minifyJS: true
        }))
        .pipe(rename(renameForMinify))
        .pipe(gulp.dest(path.join(TEMPLATES_DIST_PATH)));
});

/**
 * *************************************************************
 * 
 * Build styles.
 * 
 * *************************************************************
 */

gulp.task("build:styles", [
    "compile:styles:less",
    "compile:styles:less:min",
    "compile:styles:sass",
    "compile:styles:sass:min",
    "compile:styles:css",
    "compile:styles:css:min"
]);

/**
 * Copy CSS files.
 */
gulp.task("compile:styles:css", function () {
    return gulp.src(path.join(CSS_SRC_PATH, "**/*.css"))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
});

/**
 * Minify and copy CSS files.
 */
gulp.task("compile:styles:css:min", function () {
    return gulp.src(path.join(CSS_SRC_PATH, "**/*.css"))
        .pipe(cssmin())
        .pipe(rename(renameForMinify))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
});

/**
 * Compile SASS files.
 */
gulp.task("compile:styles:sass", function () {
    // not supported
});

/**
 * Compile and minify SASS files.
 */
gulp.task("compile:styles:sass:min", function () {
    // not supported
});

/**
 * Compile LESS files.
 */
gulp.task("compile:styles:less", function () {
    return gulp.src(path.join(LESS_SRC_PATH, "**/*.less"))
        .pipe(less())
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
});

/**
 * Compile and minify LESS files.
 */
gulp.task("compile:styles:less:min", function () {
    return gulp.src(path.join(LESS_SRC_PATH, "**/*.less"))
        .pipe(less())
        .pipe(cssmin())
        .pipe(rename(renameForMinify))
        .pipe(gulp.dest(path.join(CSS_DIST_PATH)));
});

/**
 * *************************************************************
 * 
 * Build application code.
 * 
 * *************************************************************
 */

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
 * Compile code.
 */
gulp.task("compile:code", [
    "compile:code:js",
    "compile:code:ts"
]);

/**
 * Copy JavaScript files to the output directory.
 */
gulp.task("compile:code:js", function () {
    return gulp.src(path.join(JS_SRC_PATH, "**/*.js"))
        .pipe(gulp.dest(path.join(JS_OUT_PATH)));
});

/**
 * Compile Typescript files to the output directory.
 */
gulp.task("compile:code:ts", function () {
    return gulp.src(path.join(TS_SRC_PATH, "**/*.ts"))
        .pipe(tsProject())
        .js.pipe(gulp.dest(path.join(JS_OUT_PATH)));
});

/**
 * Copy code.
 */

gulp.task("copy:code", ["compile:code"], function () {
    return gulp.src(path.join(JS_OUT_PATH, "**/*.js"))
        .pipe(gulp.dest(JS_DIST_PATH));
});

gulp.task("copy:code:min", ["compile:code"], function () {
    return pump([
        //gulp.src(path.join(JS_OUT_PATH, "**/*.js")),
        gulp.src(path.join(JS_OUT_PATH, "**/site.js")),
        uglify(),
        rename(renameForMinify),
        gulp.dest(JS_DIST_PATH)
    ]);
});

/**
 * Link apps.
 */
gulp.task("link:code:apps", [
    "link:code:app:bio:data",
    "link:code:app:documents:edit",
    "link:code:app:documents:preview",
    "link:code:app:index:page"
]);

gulp.task("link:code:app:bio:data", ["compile:code", "copy:code"], jsAppFactory.createBuild("bio-data-app.js", "bio-data-app.min.js", true));
gulp.task("link:code:app:documents:edit", ["compile:code", "copy:code"], jsAppFactory.createBuild("document-edit.js", "document-edit.min.js", true));
gulp.task("link:code:app:documents:preview", ["compile:code", "copy:code"], jsAppFactory.createBuild("document-preview.js", "document-preview.min.js", true));
gulp.task("link:code:app:index:page", ["compile:code", "copy:code"], jsAppFactory.createBuild("files-index.js", "files-index.min.js", true));

/**
 * Build all code.
 */
gulp.task("build:code", [
    "compile:code",
    "copy:code",
    "copy:code:min",
    "link:code:apps"
]);

/**
 * Build all.
 */
gulp.task("build", [
    "build:code",
    "build:styles",
    "build:templates"
]);

/**
 * Run tests.
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
 * Default task.
 */
gulp.task("default", ["build", "test"]);

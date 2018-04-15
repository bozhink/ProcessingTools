"use strict";

// See https://docs.microsoft.com/en-us/aspnet/core/client-side/bundling-and-minification?tabs=netcore-cli%2Caspnetcore2x

const
    srcPath = "wwwroot/src",
    distPath = "wwwroot/build/dist",
    compilePath = "wwwroot/build/compiled",
    testsPath = "wwwroot/tests",
    paths = {
        templates: "templates",
        less: "less",
        css: "css",
        js: "js",
        apps: "js/apps",
        typescript: "typescript",
        tsdefinitions: "tsdefinitions"
    };

var gulp = require("gulp"),
    concat = require("gulp-concat"),
    less = require("gulp-less"),
    browserify = require("gulp-browserify"),
    cssmin = require("gulp-cssmin"),
    htmlmin = require("gulp-htmlmin"),
    uglify = require("gulp-uglify"),
    merge = require("merge-stream"),
    del = require("del"),
    plumber = require("gulp-plumber"),
    mocha = require("gulp-mocha"),
    ts = require("gulp-typescript"),
    bundleconfig = require("./bundleconfig.json"),
    path = require("path");

var regex = {
    css: /\.css$/,
    html: /\.(html|htm)$/,
    js: /\.js$/
};

gulp.task("min", ["min:js", "min:css", "min:html"]);
gulp.task("clean", ["clean:bundle", "clean:build"]);

gulp.task("min:js", function () {
    var tasks = getBundles(regex.js).map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
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

// See https://wildermuth.com/2017/11/19/ASP-NET-Core-2-0-and-the-End-of-Bower
// Dependency Dirs
var deps = {
    "jquery": {
        "/**": ""
    },
    "jquery-validation": {
        "/**": ""
    },
    "jquery-validation-unobtrusive": {
        "/**": ""
    },
    "bootstrap": {
        "/**": ""
    },
    // ...
};

gulp.task("copyScripts", function () {

    var streams = [];

    for (var prop in deps) {
        console.log("Prepping Scripts for: " + prop);
        for (var itemProp in deps[prop]) {
            var src = gulp.src("node_modules/" + prop + "/" + itemProp);
            var dest = gulp.dest("wwwroot/external/" + prop + "/" + deps[prop][itemProp]);
            streams.push(src.pipe(dest));
        }
    }

    return merge(streams);

});

gulp.task("clean:bundle", function () {
    var files = bundleconfig.map(function (bundle) {
        return bundle.outputFileName;
    });

    return del(files);
});

gulp.task("clean:build", function () {
    return del([compilePath, distPath]);
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

function getBundles(regexPattern) {
    return bundleconfig.filter(function (bundle) {
        return regexPattern.test(bundle.outputFileName);
    });
}

/**
 * Copy all templates to the distribution directory
 */
gulp.task("copy-templates", function () {
    return gulp.src(path.join(srcPath, paths.templates, "**/*"))
        .pipe(gulp.dest(path.join(distPath, paths.templates)));
});

/**
 * Copy JavaScript files to the distribution directory
 */
gulp.task("copy-js", function () {
    return gulp.src(path.join(srcPath, paths.js, "**/*.js"))
        .pipe(gulp.dest(path.join(distPath, paths.js)));
});

/**
 * Compile and minify LESS files
 */
gulp.task("compile-less", function () {
    return gulp.src(path.join(srcPath, paths.less, "**/*.less"))
        .pipe(less())
        .pipe(cssmin())
        .pipe(gulp.dest(path.join(distPath, paths.css)));
});

/**
 * Compile TypeScript files
 */
gulp.task("compile-typescript", function () {
    var tsResult = gulp.src(path.join(srcPath, paths.typescript, "**/*.ts"))
        .pipe(ts({
            target: "ES5",
            declarationFiles: false,
            noResolve: true,
            noImplicitAny: true
        }));

    tsResult.dts.pipe(gulp.dest(path.join(compilePath, paths.tsdefinitions)));

    return tsResult.js.pipe(gulp.dest(path.join(compilePath, paths.typescript)));
});

gulp.task("compressScripts", function () {
    return gulp
        .src([
            compilePath + "/typescript/*.js"
        ])
        .pipe(plumber())
        .pipe(concat("scripts.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest(path.join(distPath, paths.js)));
});

/**
 * Compile apps
 */
gulp.task("build-bio-data-app", function () {
    return gulp
        .src([
            path.join(srcPath, paths.apps, "bio-data-app.js")
        ])
        .pipe(browserify())
        .pipe(concat("bio-data-app.min.js"))
        //.pipe(uglify())
        .pipe(gulp.dest(path.join(distPath, paths.apps)));
});

gulp.task("build-document-edit", function () {
    return gulp.src([
            path.join(srcPath, paths.apps, "document-edit.js")
        ])
        .pipe(browserify())
        .pipe(concat("document-edit.min.js"))
        //.pipe(uglify())
        .pipe(gulp.dest(path.join(distPath, paths.apps)));
});

gulp.task("build-document-preview", function () {
    return gulp.src([
            path.join(srcPath, paths.apps, "document-preview.js")
        ])
        .pipe(browserify())
        .pipe(concat("document-preview.min.js"))
        //.pipe(uglify())
        .pipe(gulp.dest(path.join(distPath, paths.apps)));
});

gulp.task("build-files-index", function () {
    return gulp.src([
            path.join(srcPath, paths.apps, "files-index.js")
        ])
        .pipe(browserify())
        .pipe(concat("files-index.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest(path.join(distPath, paths.apps)));
});

/**
 * Build all code
 */
gulp.task("build", [
    "compile-less",
    "compile-typescript",
    "copy-js",
    "copy-templates",
    "build-document-edit",
    "build-document-preview",
    "build-files-index",
    "build-bio-data-app"]);

gulp.task("watch", function () {
    gulp.watch([srcPath], ["build"]);
});

/**
 * Run tests
 */
gulp.task("test", function () {
    return gulp.src(path.join(testsPath, "**/*.js"))
        .pipe(mocha())
        .on("error", function () {
            this.emit("end");
        });
});

gulp.task("watch-tests", function () {
    gulp.watch(path.join(testsPath, "**/*.js"), ["test"]);
});

/**
 * Default task
 */
gulp.task("default", ["build", "test"]);

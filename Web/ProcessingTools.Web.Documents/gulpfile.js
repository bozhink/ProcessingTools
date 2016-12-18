const
    srcPath = 'wwwroot/src',
    distPath = 'wwwroot/build/dist',
    compilePath = 'wwwroot/build/compiled',
    testsPath = 'wwwroot/tests',
    paths = {
        templates: 'templates',
        less: 'less',
        css: 'css',
        js: 'js',
        apps: 'js/apps',
        typescript: 'typescript',
        tsdefinitions: 'tsdefinitions'
    };

var gulp = require('gulp'),
    browserify = require('gulp-browserify'),
    cleanCSS = require('gulp-clean-css'),
    concat = require('gulp-concat'),
    less = require('gulp-less'),
    mocha = require('gulp-mocha'),
    plumber = require('gulp-plumber'),
    ts = require('gulp-typescript'),
    uglify = require('gulp-uglify'),
    del = require('del'),
    path = require('path');

/**
 * Clean build directory
 */
gulp.task('clean', function () {
    return del([compilePath, distPath]);
});

/**
 * Copy all templates to the distribution directory
 */
gulp.task('copy-templates', function () {
    return gulp.src(path.join(srcPath, paths.templates, '**/*'))
        .pipe(gulp.dest(path.join(distPath, paths.templates)));
});

/**
 * Copy JavaScript files to the distribution directory
 */
gulp.task('copy-js', function () {
    return gulp.src(path.join(srcPath, paths.js, '**/*.js'))
        .pipe(gulp.dest(path.join(distPath, paths.js)));
});

/**
 * Compile and minify LESS files
 */
gulp.task('compile-less', function () {
    return gulp.src(path.join(srcPath, paths.less, '**/*.less'))
        .pipe(less())
        .pipe(cleanCSS({
            compatibility: 'ie8'
        }))
        .pipe(gulp.dest(path.join(distPath, paths.css)));
});

/**
 * Compile TypeScript files
 */
gulp.task('compile-typescript', function () {
    var tsResult = gulp.src(path.join(srcPath, paths.typescript, '**/*.ts'))
        .pipe(ts({
            target: 'ES5',
            declarationFiles: false,
            noResolve: true,
            noImplicitAny: true
        }));

    tsResult.dts.pipe(gulp.dest(path.join(compilePath, paths.tsdefinitions)));

    return tsResult.js.pipe(gulp.dest(path.join(compilePath, paths.typescript)));
});

gulp.task('compressScripts', function () {
    return gulp
        .src([
            compilePath + '/typescript/*.js'
        ])
        .pipe(plumber())
        .pipe(concat('scripts.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest(path.join(distPath, paths.js)));
});

/**
 * Compile apps
 */
gulp.task('build-document-edit', function () {
    return gulp.src([
            path.join(srcPath, paths.apps, 'document-edit.js')
        ])
        .pipe(browserify())
        .pipe(concat('document-edit.min.js'))
        //.pipe(uglify())
        .pipe(gulp.dest(path.join(distPath, paths.apps)));
});

gulp.task('build-document-preview', function () {
    return gulp.src([
            path.join(srcPath, paths.apps, 'document-preview.js')
        ])
        .pipe(browserify())
        .pipe(concat('document-preview.min.js'))
        //.pipe(uglify())
        .pipe(gulp.dest(path.join(distPath, paths.apps)));
});

/**
 * Build all code
 */
gulp.task('build', [
    'compile-less',
    'compile-typescript',
    'copy-js',
    'copy-templates',
    'build-document-edit',
    'build-document-preview']);

gulp.task('watch', function () {
    gulp.watch([srcPath], ['build']);
});

/**
 * Run tests
 */
gulp.task('test', function () {
    return gulp.src(path.join(testsPath, '**/*.js'))
        .pipe(mocha())
        .on('error', function () {
            this.emit('end');
        });
});

gulp.task('watch-tests', function () {
    gulp.watch(path.join(testsPath, '**/*.js'), ['test']);
});

/**
 * Default task
 */
gulp.task('default', ['build', 'test']);
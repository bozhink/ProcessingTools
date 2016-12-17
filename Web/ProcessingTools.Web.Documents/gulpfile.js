const compilePath = 'js/compiled',
    dist = 'js/dist',
    tsPath = 'typescript/*.ts',
    srcPath = 'wwwroot/src',
    distPath = 'wwwroot/build/dist',
    paths = {
        templates: 'templates',
        less: 'less',
        css: 'css'
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

gulp.task('copy-templates', function () {
    return gulp.src(path.join(srcPath, paths.templates, '**/*'))
        .pipe(gulp.dest(path.join(distPath, paths.templates)));
});

gulp.task('less', function () {
    return gulp.src(path.join(srcPath, paths.less, '**/*'))
        .pipe(less())
        .pipe(cleanCSS({ compatibility: 'ie8' }))
        .pipe(gulp.dest(path.join(distPath, paths.css)));
});



gulp.task('build', ['less']);

gulp.task('continuous-build', function () {
    gulp.watch(['./static/**/*.*', '!./static/build/**/*.*'], ['build']);
});

gulp.task('test', function () {
    gulp.src('./static/code/tests/*.js')
        .pipe(mocha())
        .on('error', function () {
            this.emit('end');
        });
});

gulp.task('watch', function () {
    gulp.watch('./static/code/**/*.js', ['test']);
});


gulp.task('compressScripts', function () {
    gulp
        .src([
            compilePath + '/typescript/*.js'
        ])
        .pipe(plumber())
        .pipe(concat('scripts.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest(dist));
});

gulp.task('typescript', function () {
    var tsResult = gulp.src(tsPath)
        .pipe(ts({
            target: 'ES5',
            declarationFiles: false,
            noResolve: true,
            noImplicitAny: true
        }));
    
    tsResult.dts.pipe(gulp.dest(compilePath + '/tsdefinitions'));
    return tsResult.js.pipe(gulp.dest(compilePath + '/typescript'));
});

gulp.task('watch', function () {
    gulp.watch([tsPath], ['typescript']);
});

gulp.task('default', ['typescript', 'watch', 'compressScripts']);

gulp.task('clean', function () {
    return del([compilePath, dist]);
})




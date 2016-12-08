/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    less = less = require('gulp-less'),
    cleanCSS = require('gulp-clean-css'),
    browserify = require('gulp-browserify'),
    uglify = require('gulp-uglify'),
    minifier = require('gulp-uglify/minifier'),
    pump = require('pump'),
    mocha = require('gulp-mocha');

gulp.task('less', function () {
    gulp.src('./static/less/**/*.less')
        .pipe(less())
        .pipe(cleanCSS({ compatibility: 'ie8' }))
        .pipe(gulp.dest('./static/build/css'))
        .on('error', function () {
            this.emit('end');
        });
});

gulp.task('browserify', function () {
    return gulp.src('./static/code/**/*.js')
      .pipe(browserify())
      .pipe(gulp.dest('./static/build/js'));
});

gulp.task('compress', function (done) {
    var options = {
        preserveComments: 'license'
    };

    pump([
        gulp.src('./static/code/**/*.js'),
        //minifier(options, uglifyjs),
        uglify(),
        gulp.dest('./static/build/js')
    ],
    done);
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
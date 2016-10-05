/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    mocha = require('gulp-mocha');

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
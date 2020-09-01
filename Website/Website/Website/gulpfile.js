/// <binding BeforeBuild='default' />
const { src, dest, watch, series, parallel } = require('gulp');
const sass = require('gulp-sass');
const concat = require('gulp-concat');

const files = {
    scssPath: [//'node_modules/bootstrap/scss/bootstrap.scss',
        'app/scss/**/*.scss'],

    jsPath: ['node_modules/bootstrap/dist/js/bootstrap.min.js',
        'node_modules/jquery/dist/jquery.min.js',
        'node_modules/popper.js/dist/umd/index.min.js', 'app/js/**/*.js']
}

function scssTask() {
    return src(files.scssPath)
        .pipe(concat('style.scss'))
        .pipe(sass())
        .pipe(dest('dist'))
        //.pipe(browserSync.stream());
}

function jsTask() {
    return src(files.jsPath)
        .pipe(concat('all.js'))
        //.pipe(uglify())
        .pipe(dest('dist'))
        //.pipe(browserSync.stream());
}

exports.default = series(
    scssTask,
    jsTask
    //cacheBustTask,
    //watchTask
);
/// <binding BeforeBuild='default' />
'use strict';

var _require = require('gulp');

var src = _require.src;
var dest = _require.dest;
var watch = _require.watch;
var series = _require.series;
var parallel = _require.parallel;

var sass = require('gulp-sass');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var babel = require('gulp-babel');

var files = {
    scssPath: [//'node_modules/bootstrap/scss/bootstrap.scss',
    'app/scss/**/*.scss'],

    jsPath: ['app/js/**/*.js']
};

function scssTask() {
    return src(files.scssPath).pipe(concat('style.scss')).pipe(sass()).pipe(dest('dist'));
    //.pipe(browserSync.stream());
}

function jsGeneralTask() {
    return src(files.jsPath).pipe(concat('general.js'))
    //.pipe(babel({ presets: ["@babel/preset-env"] }))
    .pipe(uglify()).pipe(dest('dist'));
}

function jsBootstrapTask() {
    return src('node_modules/bootstrap/dist/js/**/*.js').pipe(concat('bootstrap.js')).pipe(uglify()).pipe(dest('dist'));
}

function jsPopperTask() {
    return src('node_modules/popper.js/dist/umd/index.js').pipe(concat('popper.js')).pipe(babel({ presets: ["@babel/preset-env"] })).pipe(uglify()).pipe(dest('dist'));
}

function jsJQueryTask() {
    return src('node_modules/jquery/dist/jquery.js').pipe(concat('jquery.js'))
    //.pipe(babel({ presets: ["@babel/preset-env"] }))
    .pipe(uglify()).pipe(dest('dist'));
}

function jsFontAwesomrTask() {
    return src('node_modules/@fortawesome/fontawesome-free/js/all.js').pipe(concat('fontawesome.js'))
    //.pipe(babel({ presets: ["@babel/preset-env"] }))
    .pipe(uglify()).pipe(dest('dist'));
}

exports['default'] = series(scssTask, jsGeneralTask, jsBootstrapTask, jsJQueryTask, jsPopperTask, jsFontAwesomrTask);


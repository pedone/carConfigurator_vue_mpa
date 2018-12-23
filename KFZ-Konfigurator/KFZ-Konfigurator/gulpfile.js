﻿/// <binding />
'use strict';

const { src, dest, series } = require('gulp'),
    rimraf = require('rimraf'),
    concat = require('gulp-concat'),
    cssmin = require('gulp-cssmin'),
    uglify = require('gulp-uglify'),
    babel = require('gulp-babel'),
    less = require('gulp-less'),
    browserify = require('browserify'),
    rename = require('gulp-rename'),
    source = require('vinyl-source-stream'),
    buffer = require('vinyl-buffer'),
    glob = require('glob'),
    through2 = require('through2');

const contentPath = './Content';
const contentAppPath = './Content/app';
const scriptPath = './Scripts';
const scriptAppPath = './Scripts/app';
const paths = {
    less: {
        src: contentAppPath + '/**/*.less',
        destDir: contentPath,
    },
    css: {
        src: [contentAppPath + '/**/*.css', '!' + contentAppPath + '/**/*.min.css'],
        dest: contentPath + '/out/site.css',
        minExtension: '.min.css'
    },
    babel: {
        src: [scriptAppPath + '/**/*.js', '!' + scriptAppPath + '/**/*.min.js'],
        dest: scriptPath + '/out/site.js',
        minExtension: '.min.js'
    }
};

function logFileName(name) {
    console.log(name);
    return through2.obj(function (file, _, cb) {
        console.log(file.history);
        cb(null, file);
    });
}

// LESS

function compileLess() {
    return src(paths.less.src)
        .pipe(less())
        .pipe(rename({ extension: '.css' }))
        .pipe(dest(paths.less.destDir));
}

// CSS

function cleanCSS(done) {
    rimraf(paths.css.dest, done);
}

function compileCSS() {
    return src(paths.css.src)
        .pipe(logFileName('minCSS'))
        .pipe(concat(paths.css.dest))
        .pipe(dest('.'))
        .pipe(cssmin())
        .pipe(rename({ extname: paths.css.minExtension }))
        .pipe(dest('.'));
}

// JS

//function cleanJS(done) {
//    rimraf(paths.babel.dest, done);
//}

//function bundleMinJS() {
//    return src(paths.babel.src)
//        .pipe(babel({ presets: ['@babel/env'] }))
//        .pipe(logFileName('bundleMinJS'))
//        .pipe(concat(paths.babel.dest))
//        .pipe(dest('.'))
//        .pipe(uglify())
//        .pipe(rename({ extname: paths.babel.minExtension }))
//        .pipe(dest('.'));
//}

// Bundle JS in Views
function bundleViewJs(done) {
    glob('./Views/**/*.js', { ignore: ['./**/*.bundle.js', './**/*.bundle.min.js'] }, function (er, files) {
        files.forEach(curFile => {
            browserify(curFile, { standalone: 'myBundle' })
                .transform('babelify', { presets: ["@babel/preset-env"] })
                .bundle()
                .pipe(source(curFile))
                .pipe(buffer())
                .pipe(logFileName('bundleViewJs.' + curFile))
                .pipe(rename({ extname: '.bundle.js' }))
                .pipe(dest('.'))
                .pipe(uglify())
                .pipe(rename({ extname: '.min.js' }))
                .pipe(dest('.'));
        });
        done();
    });
}

exports.bundleViewJs = bundleViewJs;
exports.cleanCSS = cleanCSS;
exports.compileCSS = series(cleanCSS, compileLess, compileCSS);

exports.compileLess = compileLess;
//exports.cleanJS = cleanJS;
//exports.compileJS = series(cleanJS, bundleMinJS);
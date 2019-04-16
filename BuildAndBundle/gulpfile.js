const { src, dest, parallel, series } = require('gulp');
const browserify = require('browserify');
const source = require('vinyl-source-stream');
// const babel = require('gulp-babel');
// const rename = require('gulp-rename');
const babelify = require('babelify');
var merge = require('merge-stream');


function build(done) {
    var jsTask = browserify({
        entries: ['src/index.js']
    })
        .transform(babelify.configure({
            presets: ["@babel/preset-env"]
        }))
        .bundle()
        .pipe(source('main.js'))
        .pipe(dest('dist'));

    var htmlTask = src("src/*.html")
        .pipe(dest("dist"));

    return merge(jsTask, htmlTask);
}

exports.default = build;
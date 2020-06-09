/// <binding />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/",
    npmroot: "./node_modules/"
};

paths.bootstrapFolder = paths.npmroot + "bootstrap/dist/";
paths.jqueryFolder = paths.npmroot + "jquery/dist/";

paths.webCssFolder = paths.webroot + "css/";
paths.webJsFolder = paths.webroot + "js/";

paths.concatCssDest = paths.webCssFolder + "site.min.css";
paths.concatJsDest = paths.webJsFolder + "site.min.js";

// Clear everything
gulp.task("clean:js", function (cb) {
    rimraf(paths.webJsFolder + "*.min.js", cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.webCssFolder + "*.min.css", cb);
});

// Min everything
gulp.task("bootstrap:min:css", function () {
    return gulp.src([paths.bootstrapFolder + "**/*.css", "!" + paths.bootstrapFolder + "**/*.min.css"])
        .pipe(concat("bootstrap.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.webCssFolder));
});

gulp.task("custom:min:css", function () {
    return gulp.src([paths.webCssFolder + "**/*.css", "!" + paths.webCssFolder + "**/*.min.css"])
        .pipe(concat("custom.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.webCssFolder));
});

gulp.task("bootstrap:min:js", function () {
    return gulp.src([paths.bootstrapFolder + "**/*.js", "!" + paths.bootstrapFolder + "**/*.min.js"])
        .pipe(concat("bootstrap.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest(paths.webJsFolder));
});

gulp.task("jquery:min:js", function () {
    return gulp.src([paths.jqueryFolder + "**/*.js", "!" + paths.jqueryFolder + "**/*.min.js"])
        .pipe(concat("jquery.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest(paths.webJsFolder));
});

gulp.task("custom:min:js", function () {
    return gulp.src([paths.webJsFolder + "**/*.js", "!" + paths.webJsFolder + "**/*.min.js"])
        .pipe(concat("custom.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest(paths.webJsFolder));
});

// Concat everything
gulp.task("concat:css", function () {
    return gulp.src([paths.webCssFolder + "*.min.css"])
        .pipe(concat("bundle.min.css"))
        .pipe(gulp.dest(paths.webCssFolder));
});

gulp.task("concat:js", function () {
    return gulp.src([paths.webJsFolder + "jquery.min.js", paths.webJsFolder + "*.min.js"])
        .pipe(concat("bundle.min.js"))
        .pipe(gulp.dest(paths.webJsFolder));
});


gulp.task("clean", gulp.series(["clean:js", "clean:css"]));
gulp.task("min", gulp.series(["jquery:min:js", "bootstrap:min:js", "custom:min:js", "bootstrap:min:css", "custom:min:css"]));
gulp.task("concat", gulp.series(["concat:css", "concat:js"]));

gulp.task("build", gulp.series(["clean", "min", "concat"]));
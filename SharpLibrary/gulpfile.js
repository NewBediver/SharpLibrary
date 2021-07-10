/// <binding />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    minify = require("gulp-babel-minify");

var paths = {
    webroot: "./wwwroot/",
    npmroot: "./node_modules/"
};

paths.bootstrapFolder = paths.npmroot + "bootstrap/";
paths.jqueryFolder = paths.npmroot + "jquery/";
paths.jqueryValidationFolder = paths.npmroot + "jquery-validation/";
paths.jqueryValidationUnobtrusiveFolder = paths.npmroot + "jquery-validation-unobtrusive/";
paths.popperFolder = paths.npmroot + "popper.js/";

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
gulp.task("custom:min:css", function () {
    return gulp.src([paths.webJsFolder + "**/*.css", "!" + paths.webJsFolder + "**/*.min.css"])
        .pipe(concat("custom.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.webCssFolder));
});

gulp.task("custom:min:js", function () {
    return gulp.src([paths.webJsFolder + "**/*.js", "!" + paths.webJsFolder + "**/*.min.js"])
        .pipe(concat("custom.min.js"))
        .pipe(minify({
            mangle: {
                keepClassName: true
            }
        }))
        .pipe(gulp.dest(paths.webJsFolder));
});

// Concat everything
gulp.task("concat:css", function () {
    return gulp.src([paths.bootstrapFolder + "dist/css/bootstrap.min.css",
                paths.webCssFolder + "**/*.min.css"])
        .pipe(concat("bundle.min.css"))
        .pipe(gulp.dest(paths.webCssFolder));
});

gulp.task("concat:js", function () {
    return gulp.src([paths.jqueryFolder + "dist/jquery.min.js",
                paths.jqueryValidationFolder + "dist/jquery.validate.min.js",
                paths.jqueryValidationUnobtrusiveFolder + "dist/jquery.validate.unobtrusive.min.js",
                paths.popperFolder + "dist/umd/popper.min.js",
                paths.bootstrapFolder + "dist/js/bootstrap.min.js",
                paths.webJsFolder + "**/*.min.css"])
        .pipe(concat("bundle.min.js"))
        .pipe(gulp.dest(paths.webJsFolder));
});


gulp.task("clean", gulp.series(["clean:js", "clean:css"]));
gulp.task("min", gulp.series(["custom:min:css", "custom:min:js"]));
gulp.task("concat", gulp.series(["concat:css", "concat:js"]));

gulp.task("build", gulp.series(["clean", "min", "concat"]));
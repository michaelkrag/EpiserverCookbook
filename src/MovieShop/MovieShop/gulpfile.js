var gulp = require('gulp');
var concat = require('gulp-concat');
var debug = require('gulp-debug');

var paths = {
    javascript: {
        src: [
            'Frontend/components/**/!(app)*.js',
            'Frontend/components/app.js'
        ],
        dest: 'Static/js',
        name: 'app.js'
    }
};

gulp.task('Build:js', function () {
    return gulp.src(paths.javascript.src)
        .pipe(debug({ title: 'unicorn:' }))
        /*.pipe(plugins.jshint())
        .pipe(plugins.jshint.reporter('jshint-stylish'))*/
        .pipe(concat(paths.javascript.name))
        .pipe(gulp.dest(paths.javascript.dest));
});
var    gulp = require('gulp'),
     concat = require('gulp-concat'),
     uglify = require('gulp-uglify'),
  minifycss =require('gulp-minify-css');

gulp.task('powerup', ['minify-js', 'minify-css']);

gulp.task('minify-js', function () {
  gulp.src('../bower_components/bootstrap/dist/bootstrap.min.js')
  gulp.src('../bower_components/jquery/dist/jquery.min.js')
  .pipe(concat('application.js'))
  .pipe(uglify())
  .pipe(gulp.dest(''))
});

gulp.task('minify-css', function () {
  gulp.src('../bower_components/bootstrap/dist/bootstrap.min.css')
  gulp.src('../bower_components/bootstrap/dist/bootstrap-theme.min.css')
  gulp.src('../bower_components/font-awesome/css/font-awesome.min.css')
  .pipe(concat('application.css'))
  .pipe(minifycss())
  .pipe(gulp.dest(''))
});
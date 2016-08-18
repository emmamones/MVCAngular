var app = angular.module("myApp", []);

app.controller('GenreController', function ($scope, $http) {

    $scope.GetGenres = function () {
        $http.get('/Movies/Genres').then(function (response) {
            $scope.modelGenre = response.data;
        });
    };

    $scope.GetGenres();
});


app.controller('MoviesController', function ($scope, $http) {

    $scope.Sort = function (column) {
        $scope.key = column;
        $scope.AscOrDesc = !$scope.AscOrDesc;
    }

    $scope.GetMoviesByGenre = function (optGenre) {
    
        $http.get('/Movies/ByGenre/' + optGenre).then(function (response) {
            $scope.Refresh;
            $scope.modelMovies = response.data;
        });
    };

   
     $scope.Edit = function (idMovie) {
    
         $http.get('/Movies/Edit/' + idMovie).then(function(response) {
            $scope.modelMovie = response.data;
        });
    };
 

});

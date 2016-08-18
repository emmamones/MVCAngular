var app = angular.module("myApp", []);

app.factory('crudServiceMovies', function ($http) {

    var crudMovies = {};

    crudMovies.getGenres = function () {
        var genres;
        genres = $http({ method: 'Get', url: '/Movies/Genres' })
            .then(function (response) {
                return response.data;
            });
        return genres;
    };

    crudMovies.getMoviesByGenre = function (optGenre) {
        var genres;
        genres = $http({ method:'Get',url:'/Movies/ByGenre',params:{id:optGenre}})
            .then(function (response) {
            return response.data;
        });
        return genres;
    };

    crudMovies.editMovie = function (optGenre) {
        $http.get('/Movies/Edit/' + optGenre).then(function (response) {
            return response.data;
        });
    };

    return crudMovies;
});


app.controller('GenreController', function ($scope, crudServiceMovies) {


    crudServiceMovies.getGenres().then(function (result) {
        $scope.modelGenres = result;
    })

});



app.controller('MoviesController', function ($scope, crudServiceMovies) {

    $scope.Sort = function (column) {
        $scope.key = column;
        $scope.AscOrDesc = !$scope.AscOrDesc;
    }

    $scope.GetMoviesByGenre = function (optGenre) {

        crudServiceMovies.getMoviesByGenre(optGenre).then(function (result) {
            $scope.modelMovies = result;
        })
    }


    $scope.Edit = function (idMovie) {

        $scope.modelMovie = crudServiceMovies.editMovie(idMovie);
    };

});

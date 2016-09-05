var app = angular.module("myApp", ["ui.bootstrap", "ui.bootstrap.datetimepicker"]);
//https://docs.angularjs.org/guide/directive
//http://plnkr.co/edit/xE8XMLozp7ufdUHfNA76?p=preview
app.directive("mydatepicker", ['$interval', 'dateFilter', function ($interval, dateFilter) {
    return {
        restrict: "E",
        scope: {
            ngModel: "=",
            dateOptions: "=",
            format: "=",
            opened: "=",
        },
        link: function ($scope, element, attrs) {
            scope.popupOpen = false;
            scope.openPopup = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                scope.popupOpen = true;
            };

            $scope.open = function ($event) {
                console.log("open");
                $event.preventDefault();
                $event.stopPropagation();
                $scope.opened = true;
            };

            $scope.clear = function () {
                $scope.ngModel = null;
            };
           // $scope.dt = dateFilter($scope.ngModel.replace('/Date(', '').replace(')/', ''), $scope.dateOptions.dateFormat);
            //element.text(dateFilter(goodDate, $scope.dateFormat));
        },
        templateUrl: 'datepicker.html'
    }
}]);

app.directive('myCurrentTime', ['$interval', 'dateFilter', function ($interval, dateFilter) {

    function link(scope, element, attrs) {
        var format,
            timeoutId;

        function updateTime() {
            element.text(dateFilter(new Date(), format));
        }

        scope.$watch(attrs.myCurrentTime, function (value) {
            format = value;
            updateTime();
        });

        element.on('$destroy', function () {
            $interval.cancel(timeoutId);
        });

        // start the UI update process; save the timeoutId for canceling
        timeoutId = $interval(function () {
            updateTime(); // update DOM
        }, 1000);
    }

    return {
        link: link
    };
}]);




app.factory('crudServiceMovies', function ($http) {

    var crudMovies = {};

    crudMovies.getGenres = function () {
        var genres;
        genres = $http({ method: 'Get', url: '/api/Genres' })
            .then(function (response) {
                return response.data;
            });
        return genres;
    };

    crudMovies.getMoviesByGenre = function (optGenre) {
        var genres;
        genres = $http({ method: 'Get', url: '/Movies/ByGenre', params: { id: optGenre } })
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


    $scope.format = 'M/d/yy h:mm:ss a';
    $scope.opened = false;

    //Datepicker
    $scope.dateOptions = {
        dateFormat: 'yyyy-MM-dd HH:mm',
        'show-weeks': false
    };

});

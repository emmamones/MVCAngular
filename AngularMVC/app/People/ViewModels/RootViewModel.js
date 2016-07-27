 
peopleModule.controller("rootViewModel", function ($scope, peopleService, $http, viewModelHelper) {

    $scope.viewModelhelper = viewModelHelper;
    $scope.peopleService = peopleService;
 
    $scope.flags = { shownFromList: false };

    var initialize = function () {
        $scope.pageheading = 'Root viewl Model ';
    }


    initialize();

});


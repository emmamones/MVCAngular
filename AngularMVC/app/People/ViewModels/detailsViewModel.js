peopleModule.controller("detailsViewModel", function ($scope, peopleService, viewModelHelper) {
    $scope.heading = 'Details from the Selected People';

    $scope.viewModelhelper = viewModelHelper;
    $scope.peopleService = peopleService;
});

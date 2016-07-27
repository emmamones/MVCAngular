peopleModule.controller("peopleHomeViewModel", function ($scope, peopleService, viewModelHelper) {
    $scope.head = 'Here is the Home view of People Section';

    $scope.viewModelhelper = viewModelHelper;
    $scope.peopleService = peopleService;


    $scope.peopleList = function () {
        viewModelHelper.navigateTo('People/List');
    }

});

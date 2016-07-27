
var appMainModule = angular.module("appMain", ["ngRoute"]);

appMainModule.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/', { templateUrl: '/Templates/People.html', controller: 'homeViewModel' });
    $routeProvider.when('/PeopleList', { templateUrl: '/Templates/PeopleList.html', controller: 'peopleViewModel' });
    $routeProvider.when('/Details', { templateUrl: '/Templates/Details.html', controller: 'detailsViewModel' });
    $routeProvider.otherwise({ redirectTo: '/' });
    $locationProvider.html5Mode(true);
});


appMainModule.controller("indexViewModel", function ($scope, $http, $location) {
    $scope.headingCaption = 'Angular Routing Primer';
});

appMainModule.controller("homeViewModel", function ($scope, $http, $location) {
    $scope.heading = 'Here is the Home view of People Section';
});

appMainModule.controller("detailsViewModel", function ($scope, $http, $location) {
    $scope.heading = 'Details view';
});


appMainModule.controller("peopleViewModel", function ($scope, $http, $location) {

    $scope.states = {
        showPersonForm: false
    };
    $scope.heading = 'This is a list of people.';
    $scope.new = {
        entity: {}
    };

    $scope.people = [
        { firstname: 'miguel', lastname: 'castro', retired: true },
        { firstname: 'brian', lastname: 'moyes', retired: true },
        { firstname: 'andrew', lastname: 'brust', retired: false },
        { firstname: 'john', lastname: 'petersen', retired: true },
        { firstname: 'gus', lastname: 'castro', retired: false }
    ];

    $scope.showPerson = function (person) {
        alert('you selected ' + person.firstname + ' ' + person.lastname);
    }

    $scope.AddUser = function (show) {
        $scope.states.showPersonForm = show;
    }
    $scope.save = function () {

        alert('you saved ' + $scope.new.entity.firstname + ' ' + $scope.new.entity.lastname + $scope.new.entity.retired);
        $scope.people.push($scope.new.entity);
        $scope.new.entity = {};
        $scope.states.showPersonForm = false;
    }

});




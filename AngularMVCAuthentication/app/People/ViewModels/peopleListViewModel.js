 
peopleModule.controller("peopleListViewModel", function ($scope, peopleService, viewModelHelper) {


    $scope.viewModelhelper = viewModelHelper;
    $scope.peopleService = peopleService;
 

    $scope.states = {
        showPersonForm: false
    };
    $scope.heading = 'This is a list of people.';
    $scope.new = {
        entity: {}
    };

    $scope.people = [
        { FirstName: 'miguel', retired: true, Email : "debra@example.com" },
        { FirstName: 'brian', Retired: true ,Email : "thorsten@example.com"},
        { FirstName: 'andrew', Retired: false ,Email : "yuhong@example.com"},
        { FirstName: 'john', Retired: true, Email : "jon@example.com"},
        { FirstName: 'gus', Retired: false , Email : "diliana@example.com"}
    ];

    $scope.showPerson = function (person) {
        alert('you selected ' + person.FirstName );
    }

    $scope.AddUser = function (show) {
        $scope.states.showPersonForm = show;
    }
    $scope.save = function () {

        alert('you saved ' + $scope.new.entity.firstname + ' ' + $scope.new.entity.Retired + ' ' + $scope.new.entity.Email);
        $scope.people.push($scope.new.entity);
        $scope.new.entity = {};
        $scope.states.showPersonForm = false;
    }

});


 
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


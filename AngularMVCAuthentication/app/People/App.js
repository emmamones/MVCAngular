
var peopleModule = angular.module("people", ["common"]);

peopleModule.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/People', {
        templateUrl: '/app/People/Views/PeopleHomeView.html',
        controller: 'peopleHomeViewModel'
    });
    $routeProvider.when('/People/List', {
        templateUrl: '/app/People/Views/PeopleListView.html',
        controller: 'peopleListViewModel'
    });
    $routeProvider.when('/People/Details', {
        templateUrl: '/app/People/Views/DetailsView.html',
        controller: 'detailsViewModel'
    });
    $routeProvider.otherwise({
        redirectTo: '/People'
    });
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});

peopleModule.factory('peopleService',
    function ($http, $location, viewModelHelper) {
        return MyApp.peopleService($http,
            $location, viewModelHelper);
    });

(function (myApp) {
    var peopleService = function ($http, $location,
        viewModelHelper) {

        var self = this;

        self.customerId = 0;

        return this;
    };
    myApp.peopleService = peopleService;
}(window.MyApp));

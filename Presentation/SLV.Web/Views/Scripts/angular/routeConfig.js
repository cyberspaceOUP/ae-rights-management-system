

var app =
    angular
    .module('MyApp', [
    'ngRoute'
])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {


        $routeProvider.when('/', {
            templateUrl: '/Home/login',
            controller: 'loginController'
        })

        .when('/Register', {
            templateUrl: '/Home/Register',
            controller: 'loginController'
        })
            .when('/Login', {
            templateUrl: '/Home/login',
            controller: 'loginController'
        })
            .when('/Index', {
            templateUrl: '/Home/login',
            controller: 'loginController'
        })
            .when('/UserList', {
            templateUrl: '/Home/UserList',
            controller: 'loginController'
        }).
        otherwise({ redirectTo: '/' });

        $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
        
    }]);
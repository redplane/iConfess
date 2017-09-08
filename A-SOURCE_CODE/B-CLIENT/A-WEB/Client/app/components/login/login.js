'use strict';

angular.module('login', [
    'ngRoute',
    'ui.bootstrap'
])
    .directive('login', function () {
        return {
            restrict: "E"
        };
    })
    .config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
        $locationProvider.hashPrefix('!');

        $routeProvider
            .when('/login', {
                controller: "LoginController",
                templateUrl: "components/login/login.html",
                resolve: {
                    style: function () {
                        /* check if already exists first - note ID used on link element*/
                        /* could also track within scope object*/
                        if (!angular.element('link#login').length) {
                            angular.element('head').append('<link id="login" href="components/login/login.css" rel="stylesheet">');
                        }
                    }
                }
            });
    }])
    .controller('LoginController', [
        '$scope',
        function ($scope) {
            $scope.msg = 'login work!';
        }]);
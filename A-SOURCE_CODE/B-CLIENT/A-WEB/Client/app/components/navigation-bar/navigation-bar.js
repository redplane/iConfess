'use strict';

angular.module('navigation-bar', ['ngRoute'])
    .directive('navigationBar', function () {
        return {
            replace: false,
            restrict: "E",
            controller: "NavigationBarController",
            templateUrl: "components/navigation-bar/navigation-bar.html"
        };
    })
    .controller('NavigationBarController', ['$scope', function ($scope) {

        $scope.alert = function(){
            alert('Hello world');
        }
    }]);
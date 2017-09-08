'use strict';

// Declare app level module which depends on views, and components
angular
    .module('api-documentation', [
        'ngRoute',
        'pascalprecht.translate',

        // Components.
        'navigation-bar',
        'sidebar',
        'main-content',
        'login',

        // Services.
        'iClipServices',

        // 3rd libraries.
        'ngAnimate',
        'toastr',

        // Providers.
        'api-interceptor'

    ])
    .config(['$locationProvider', '$routeProvider', '$translateProvider', '$httpProvider',
        function ($locationProvider, $routeProvider, $translateProvider, $httpProvider) {

            // Interceptor registration.
            $httpProvider.interceptors.push('apiInterceptor');

            // Use static files loader.
            $translateProvider.useStaticFilesLoader({
                prefix: 'assets/data/language/locale-',
                suffix: '.json'
            });

            // Url hash configuration
            $locationProvider
                .hashPrefix('!');

            // Fallback url.
            $routeProvider.otherwise({redirectTo: '/login'});

        }])
    .controller('ApiDocumentationController', ['$scope',
        function ($scope) {
        }]);

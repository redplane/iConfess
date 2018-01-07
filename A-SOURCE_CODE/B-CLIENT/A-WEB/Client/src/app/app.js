'use strict';

// Css imports.
require('../../node_modules/bootstrap/dist/css/bootstrap.css');

// AdminLTE
require('../../node_modules/admin-lte/dist/css/AdminLTE.css');
require('../../node_modules/admin-lte/dist/css/skins/skin-black.css');

require('../../node_modules/angular-toastr/dist/angular-toastr.css');

// Font awesome.
require('../../node_modules/font-awesome/css/font-awesome.css');
require('../../node_modules/angular-block-ui/dist/angular-block-ui.css');

// Import jquery lib.
require('jquery');
require('bluebird');
require('bootstrap');
require('admin-lte');

// Angular plugins declaration.
var angular = require('angular');
require('@uirouter/angularjs');
require('angular-block-ui');
require('angular-toastr');
require('angular-translate');
require('angular-translate-loader-static-files');

// Module declaration.
var ngModule = angular.module('ngApp', ['ui.router', 'blockUI', 'toastr', 'pascalprecht.translate']);
ngModule.config(function($urlRouterProvider, $translateProvider, $httpProvider, urlStates){

    // API interceptor
    $httpProvider.interceptors.push('apiInterceptor');

    // Url router config.
    $urlRouterProvider.otherwise(urlStates.dashboard.url);

    // Translation config.
    $translateProvider.useStaticFilesLoader({
        prefix: './assets/dictionary/',
        suffix: '.json'
    });

    // en-US is default selection.
    $translateProvider.use('en-US');

});

ngModule.controller('appController', function($transitions, urlStates, $scope){

    //#region Properties

    // For two-way model binding.
    $scope.model = {
        layoutClass: ''
    };

    //#endregion

    //#region Methods

    //#endregion

    //#region Watchers & events

    /*
    * Called when transition from state to state is successful.
    * */
    $transitions.onSuccess({}, function($transition){

        // Find destination of transaction.
        var destination = $transition.$to();

        if (destination.includes[urlStates.authorizedLayout.name]){
            $scope.model.layoutClass = 'skin-black layout-boxed sidebar-mini sidebar-mini-expand-feature';
            return;
        }

        if (destination.includes[urlStates.login.name]){
            $scope.model.layoutClass = 'hold-transition login-page';
            return;
        }

        $scope.model.layoutClass = 'hold-transition';
    });

    //#endregion
});

// Constants import.
require('./constants/index')(ngModule);

// Factories import.
require('./factories/index')(ngModule);

// Services import.
require('./services/index')(ngModule);

// Directive requirements.
require('./directives/index')(ngModule);

// Module requirements.
require('./modules/index')(ngModule);
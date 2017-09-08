"use strict";

angular.module('api-interceptor', [])
    .factory('apiInterceptor', ['$injector', function ($injector) {


        return {
            /*
            * Callback which is fired when request is made.
            * */
            request: function (x) {
                return x;
            },

            /*
            * Callback which is fired when request is made failingly.
            * */
            requestError: function (config) {
                return config;
            },

            /*
            * Callback which is fired when response is sent back from back-end.
            * */
            response: function (x) {
                return x;
            },

            /*
            * Callback which is fired when response is failed.
            * */
            responseError: function (x) {

                // Response is invalid.
                if (!x)
                    return x;

                let url = x.config.url;
                if (!url || url.indexOf('/api/') == -1)
                    return x;

                let toastr = $injector.get('toastr');
                let szMessage = '';
                switch (x.status){
                    case 400:
                        szMessage = 'Bad request';
                        break;
                    case 401:
                        szMessage = 'Your credential is invalid.';
                        break;
                    case 403:
                        szMessage = 'You are forbidden to access this endpoint';
                        break;
                    case 500:
                        szMessage = 'Internal server error';
                        break;
                    default:
                        szMessage = 'Unknown error';
                        break;
                }

                toastr.error(szMessage, 'Error');
                return x;
            }
        }
    }]);
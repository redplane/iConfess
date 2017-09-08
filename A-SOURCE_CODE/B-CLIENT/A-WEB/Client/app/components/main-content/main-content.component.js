'use strict';

angular.module('main-content', [
    'ngRoute'
])
    .config(['$locationProvider', '$routeProvider',
        function ($locationProvider, $routeProvider) {
        $locationProvider.hashPrefix('!');

        $routeProvider
            .when('/', {
                controller: "MainContentController",
                templateUrl: "components/main-content/main-content.component.html"
            });
    }])
    .controller('MainContentController', ['$scope', 'clipService', 'clipThumbnailService', 'clipCategorizingService',
        function ($scope, clipService, clipThumbnailService, clipCategorizingService) {

            //#region Properties

            $scope.imgUrl = [
                'https://s-media-cache-ak0.pinimg.com/736x/1b/2a/a7/1b2aa711cb88233d9494239352967d67.jpg',
                'https://scontent-ams3-1.cdninstagram.com/t51.2885-15/e35/18011702_697241200479622_8913213687993466880_n.jpg',
                'https://scontent-ams3-1.cdninstagram.com/t51.2885-15/e35/17267954_1327223343989912_6454917984224804864_n.jpg',
                'https://scontent-ams3-1.cdninstagram.com/t51.2885-15/e35/18095066_1163889850388120_1266411449853411328_n.jpg',
                'https://s-media-cache-ak0.pinimg.com/564x/c0/f4/06/c0f406556afb42605975c28846f81db2.jpg',
                'https://s-media-cache-ak0.pinimg.com/236x/61/00/89/610089e726d35848efec366bbb958413.jpg',
                'https://s-media-cache-ak0.pinimg.com/564x/ed/7e/dd/ed7edd0e5a690ea09ab4f5656ce33591.jpg',
                'https://s-media-cache-ak0.pinimg.com/236x/d7/5c/dc/d75cdca56a7d8796979129a5ccc3ac6d.jpg',
                'https://scontent-ams3-1.cdninstagram.com/t51.2885-15/e35/17333337_246519425756489_5020246659360096256_n.jpg',
                'https://s-media-cache-ak0.pinimg.com/236x/8f/d0/1d/8fd01d3b7c1cb47655b676cf7b5d2ed1.jpg'
            ];
            
            $scope.randomImg = function () {
                return $scope.imgUrl[Math.floor((Math.random() * 10) + 1)];
            }
            
            
            // Collection of slides.
            $scope.slides = [];

            // Pagination information.
            $scope.pagination = {
                page: 1,
                records: 30,
                total: 3000
            };

            /*
            * List of clips which should be displayed on the screen.
            * */
            $scope.clipsList = {
                records: [],
                total: 0
            };

            /*
            * List of clip thumbnails should be displayed on the screen.
            * */
            $scope.clipThumbnailsList = {
                records: [],
                total: 0
            };

            /*
            * Conditions which are for searching clips.
            * */
            $scope.findClipsCondition = {
                pagination:{
                    page: 1,
                    records: 30
                }
            };

            //#endregion

            //#region Methods

            /*
            * Event which is called when component has been initialized.
            * */
            $scope.init = function () {

                // Search for hot trend items.
                $scope.findClipsCondition = {};
                $scope.findClipThumbnailsCondition = {};

                $scope.findClips($scope.findClipsCondition);
                $scope.findClipThumbnails($scope.findClipThumbnailsCondition);

            };

            /*
            * Raised when a category is selected in sidebar.
            * */
            $scope.selectCategory = function(category){

                // Re-initiate condition.
                $scope.findClipsCondition = {
                    categoryIds: [category.id],
                    pagination: {
                        page: 1,
                        records: 20
                    }
                };

                $scope.findClips($scope.findClipsCondition);

            };

            /*
            * Find clips base on specific condition.
            * */
            $scope.findClips = function(condition){
                console.log(condition);
                clipCategorizingService.getClipCategorizings(condition)
                    .then(function(x){
                        let data = x.data;
                        if (!data)
                            return;

                        console.log(data);
                        $scope.clipsList = data;
                    })
                    .catch(function(x){
                        console.log(x);
                    });
            };

            /*
            * Find clip thumbnails by using specific conditions.
            * */
            $scope.findClipThumbnails = function(condition){
                clipThumbnailService.getClipThumbnails(condition)
                    .then(function(x){
                        let data = x.data;
                        if (!data)
                            return;
                        
                        $scope.clipThumbnailsList = data;
                        $scope.$applyAsync();
                    });
            };
            //#endregion
        }]);
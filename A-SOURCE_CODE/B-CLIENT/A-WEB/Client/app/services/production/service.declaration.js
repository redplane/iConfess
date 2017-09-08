/*
* Application services declaration.
* */
angular.module('iClipServices', [])
    .constant('urlsList', {

        //#region Properties

        // Service base url.
        baseUrl: 'http://localhost:27021',

        // Url which is for searching categories by using specific conditions.
        getCategories: 'api/category/search',

        // Url which is for initiating category.
        initCategory: 'api/category',

        // Url which is for searching clips by using specific conditions.
        getClips: 'api/clip/search'

        //#endregion
    });
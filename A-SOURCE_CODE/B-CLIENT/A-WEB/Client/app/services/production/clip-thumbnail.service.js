angular.module('iClipServices')
    .service('clipThumbnailService', ['urlsList','$http', function (urlsList, $http) {

        //#region Methods

        /*
        * Get clip thumbnails by using specific conditions.
        * */
        this.getClipThumbnails = function(condition){
            let url = urlsList.baseUrl + '/' + urlsList.getClipThumbnails;
            return $http.post(url, condition);
        };

        //#endregion
    }]);
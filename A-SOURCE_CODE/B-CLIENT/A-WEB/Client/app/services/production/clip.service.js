angular.module('iClipServices')
    .service('clipService', ['urlsList','$http', function (urlsList, $http) {

        //#region Methods

        /*
        * Get list of categories by using specific conditions.
        * */
        this.getClips = function(condition){
            let url = urlsList.baseUrl + '/' + urlsList.getClips;
            return $http.post(url, condition);
        };

        //#endregion
    }]);
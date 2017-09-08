angular.module('iClipServices')
    .service('clipCategorizingService', ['urlsList','$http', function (urlsList, $http) {

        //#region Methods

        /*
        * Get clip categorizing by using specific conditions.
        * */
        this.getClipCategorizings = function(condition){
            let url = urlsList.baseUrl + '/' + urlsList.getClipCategorizings;
            return $http.post(url, condition);
        }

        //#endregion

    }]);
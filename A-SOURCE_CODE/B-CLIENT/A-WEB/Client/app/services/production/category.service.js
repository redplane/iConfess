angular.module('iClipServices')
    .service('categoryService', ['urlsList','$http', function (urlsList, $http) {

        //#region Methods

        /*
        * Get list of categories by using specific conditions.
        * */
        this.getCategories = function(condition){
            let url = urlsList.baseUrl + '/' + urlsList.getCategories;
            return $http.post(url, condition);
        };

        /*
        * Init category by using specific condition.
        * */
        this.initCategory = function(category){
            let url = urlsList.baseUrl + '/' + urlsList.baseUrl;
            return $http.post(url, category);
        }

        //#endregion
    }]);
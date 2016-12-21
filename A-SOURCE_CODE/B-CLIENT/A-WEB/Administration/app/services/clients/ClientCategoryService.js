"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var FindCategoriesViewModel_1 = require("../../viewmodels/category/FindCategoriesViewModel");
var HyperlinkService_1 = require("../HyperlinkService");
var http_1 = require("@angular/http");
require("rxjs/add/operator/toPromise");
var UnixDateRange_1 = require("../../viewmodels/UnixDateRange");
/*
* Service which handles category business.
* */
var ClientCategoryService = (function () {
    // Initiate instance of category service.
    function ClientCategoryService(hyperlinkService, httpClient) {
        this._hyperlinkService = hyperlinkService;
        this._httpClient = httpClient;
    }
    // Find categories by using specific conditions.
    ClientCategoryService.prototype.findCategories = function (categorySearch) {
        // Page index should be decrease by one.
        var conditions = Object.assign({}, categorySearch);
        conditions['pagination'] = Object.assign({}, categorySearch.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;
        var requestOptions = new http_1.RequestOptions({
            headers: new http_1.Headers({
                'Content-Type': 'application/json'
            })
        });
        // Request to api to obtain list of available categories in system.
        return this._httpClient.post(this._hyperlinkService.apiFindCategory, conditions, requestOptions)
            .toPromise();
    };
    // Reset categories search conditions.
    ClientCategoryService.prototype.resetFindCategoriesConditions = function () {
        // Initiate find categories conditions.
        var conditions = new FindCategoriesViewModel_1.CategorySearchViewModel();
        if (conditions == null)
            conditions = new FindCategoriesViewModel_1.CategorySearchViewModel();
        conditions.creatorIndex = null;
        conditions.name = null;
        conditions.created = new UnixDateRange_1.UnixDateRange();
        conditions.lastModified = new UnixDateRange_1.UnixDateRange();
        return conditions;
    };
    return ClientCategoryService;
}());
ClientCategoryService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [HyperlinkService_1.HyperlinkService, http_1.Http])
], ClientCategoryService);
exports.ClientCategoryService = ClientCategoryService;
//# sourceMappingURL=ClientCategoryService.js.map
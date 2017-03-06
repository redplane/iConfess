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
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SearchCategoriesViewModel_1 = require("../../viewmodels/category/SearchCategoriesViewModel");
var ClientApiService_1 = require("../ClientApiService");
require("rxjs/add/operator/toPromise");
var UnixDateRange_1 = require("../../viewmodels/UnixDateRange");
var ClientAuthenticationService_1 = require("./ClientAuthenticationService");
/*
* Service which handles category business.
* */
var ClientCategoryService = (function () {
    // Initiate instance of category service.
    function ClientCategoryService(clientApiService, clientAuthenticationService) {
        this._clientApiService = clientApiService;
        this._clientAuthenticationService = clientAuthenticationService;
    }
    // Find categories by using specific conditions.
    ClientCategoryService.prototype.findCategories = function (categorySearch) {
        // Page index should be decrease by one.
        var conditions = Object.assign({}, categorySearch);
        conditions['pagination'] = Object.assign({}, categorySearch.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;
        return this._clientApiService.post(this._clientAuthenticationService.findClientAuthenticationToken(), this._clientApiService.apiFindCategory, null, conditions)
            .toPromise();
    };
    // Find categories by using specific conditions and delete 'em.
    ClientCategoryService.prototype.deleteCategories = function (findCategoriesConditions) {
        // Request to api to obtain list of available categories in system.
        return this._clientApiService.delete(this._clientAuthenticationService.findClientAuthenticationToken(), this._clientApiService.apiDeleteCategory, null, findCategoriesConditions)
            .toPromise();
    };
    // Change category detail information by searching its index.
    ClientCategoryService.prototype.changeCategoryDetails = function (id, category) {
        // Request to api to obtain list of available categories in system.
        return this._clientApiService.put(this._clientAuthenticationService.findClientAuthenticationToken(), this._clientApiService.apiChangeCategoryDetail, { index: id }, category)
            .toPromise();
    };
    // Initiate category into system.
    ClientCategoryService.prototype.initiateCategory = function (category) {
        // Request to api to obtain list of available categories in system.
        return this._clientApiService.post(this._clientAuthenticationService.findClientAuthenticationToken(), this._clientApiService.apiInitiateCategory, null, category)
            .toPromise();
    };
    // Reset categories search conditions.
    ClientCategoryService.prototype.resetFindCategoriesConditions = function () {
        // Initiate find categories conditions.
        var conditions = new SearchCategoriesViewModel_1.SearchCategoriesViewModel();
        if (conditions == null)
            conditions = new SearchCategoriesViewModel_1.SearchCategoriesViewModel();
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
    __metadata("design:paramtypes", [ClientApiService_1.ClientApiService,
        ClientAuthenticationService_1.ClientAuthenticationService])
], ClientCategoryService);
exports.ClientCategoryService = ClientCategoryService;
//# sourceMappingURL=ClientCategoryService.js.map
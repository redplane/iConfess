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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SearchCategoriesViewModel_1 = require("../../viewmodels/category/SearchCategoriesViewModel");
var UnixDateRange_1 = require("../../viewmodels/UnixDateRange");
/*
 * Service which handles category business.
 * */
var ClientCategoryService = (function () {
    //#endregion
    //#region Constructor
    // Initiate instance of category service.
    function ClientCategoryService(clientAuthenticationService, clientApiService) {
        this.clientAuthenticationService = clientAuthenticationService;
        this.clientApiService = clientApiService;
        //#region Properties
        // Url which is for searching for categories.
        this.urlSearchCategory = "api/category/find";
        // Url which is for deleting for categories.
        this.urlDeleteCategory = "api/category";
        // Url which is for changing for category detail.
        this.urlChangeCategoryDetail = "api/category";
        // Url which is for initiating category.
        this.urlInitiateCategory = "api/category";
    }
    //#endregion
    //Region Methods
    // Find categories by using specific conditions.
    ClientCategoryService.prototype.getCategories = function (categorySearch) {
        // Page page should be decrease by one.
        var conditions = Object.assign({}, categorySearch);
        conditions['pagination'] = Object.assign({}, categorySearch.pagination);
        conditions.pagination.page -= 1;
        if (conditions.pagination.page < 0)
            conditions.pagination.page = 0;
        return this.clientApiService.post(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlSearchCategory, null, conditions);
    };
    // Find categories by using specific conditions and delete 'em.
    ClientCategoryService.prototype.deleteCategories = function (findCategoriesConditions) {
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.delete(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlDeleteCategory, null, findCategoriesConditions);
    };
    // Change category detail information by searching its page.
    ClientCategoryService.prototype.editCategoryDetails = function (id, category) {
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.put(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlChangeCategoryDetail, { index: id }, category);
    };
    // Initiate category into system.
    ClientCategoryService.prototype.initiateCategory = function (category) {
        // Initiate url.
        var url = this.clientApiService.getBaseUrl() + "/" + this.urlInitiateCategory;
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.getTokenCode(), url, null, category);
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
    __param(0, core_1.Inject("IClientAuthenticationService")),
    __param(1, core_1.Inject("IClientApiService")),
    __metadata("design:paramtypes", [Object, Object])
], ClientCategoryService);
exports.ClientCategoryService = ClientCategoryService;
//# sourceMappingURL=ClientCategoryService.js.map
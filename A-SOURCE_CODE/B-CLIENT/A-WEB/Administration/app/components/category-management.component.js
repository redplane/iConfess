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
var CategorySearchDetailViewModel_1 = require("../viewmodels/category/CategorySearchDetailViewModel");
var ClientCategoryService_1 = require("../services/clients/ClientCategoryService");
var TimeService_1 = require("../services/TimeService");
var FindCategoriesViewModel_1 = require("../viewmodels/category/FindCategoriesViewModel");
var HyperlinkService_1 = require("../services/HyperlinkService");
var ResponseAnalyzeService_1 = require("../services/ResponseAnalyzeService");
var ClientConfigurationService_1 = require("../services/ClientConfigurationService");
var Pagination_1 = require("../viewmodels/Pagination");
var CategoryManagementComponent = (function () {
    // Initiate component with dependency injections.
    function CategoryManagementComponent(categoryService, timeService, responseAnalyzeService, configurationService) {
        this._categoryService = categoryService;
        this._timeService = timeService;
        this._responseAnalyzeService = responseAnalyzeService;
        // Find configuration service in IoC.
        this._clientConfigurationService = configurationService;
        // Initiate categories search result.
        this._categorySearchResult = new CategorySearchDetailViewModel_1.CategorySearchDetailViewModel();
    }
    // Callback is fired when a category is created to be removed.
    CategoryManagementComponent.prototype.clickRemoveCategory = function (category, deleteCategoryBox) {
        // Update category information into box.
        deleteCategoryBox.setCategory(category);
        // Open delete category confirmation box.
        deleteCategoryBox.open();
    };
    // Callback which is fired when change category box is clicked.
    CategoryManagementComponent.prototype.clickChangeCategoryInfo = function (category, changeCategoryBox) {
        // Update category information into box.
        changeCategoryBox.setCategory(category);
        // Open change category information box.
        changeCategoryBox.open();
    };
    // Callback which is fired when search button of category search box is clicked.
    CategoryManagementComponent.prototype.clickSearch = function () {
        var _this = this;
        // Freeze the find box.
        this._isLoading = true;
        // Find categories by using specific conditions.
        this._categoryService.findCategories(this._findCategoriesViewModel)
            .then(function (response) {
            // Update categories list.
            _this._categorySearchResult = response.json();
            // Unfreeze the category find box.
            _this._isLoading = false;
        })
            .catch(function (response) {
            _this._isLoading = false;
        });
    };
    // Callback which is fired when page selection is changed.
    CategoryManagementComponent.prototype.clickPageChange = function (pagination) {
        // Update pagination index.
        this._findCategoriesViewModel.pagination.index = pagination.page;
        // Call search function.
        this.clickSearch();
    };
    // This callback is fired when category management component is initiated.
    CategoryManagementComponent.prototype.ngOnInit = function () {
        // Initiate category search conditions.
        this._findCategoriesViewModel = new FindCategoriesViewModel_1.CategorySearchViewModel();
        var pagination = new Pagination_1.Pagination();
        pagination.index = 1;
        pagination.records = this._clientConfigurationService.findMaxPageRecords();
        this._findCategoriesViewModel.pagination = pagination;
    };
    return CategoryManagementComponent;
}());
CategoryManagementComponent = __decorate([
    core_1.Component({
        selector: 'category-management',
        templateUrl: './app/views/pages/category-management.component.html',
        providers: [
            ClientCategoryService_1.ClientCategoryService,
            TimeService_1.TimeService,
            HyperlinkService_1.HyperlinkService,
            ResponseAnalyzeService_1.ResponseAnalyzeService,
            ClientConfigurationService_1.ConfigurationService
        ],
    }),
    __metadata("design:paramtypes", [ClientCategoryService_1.ClientCategoryService, TimeService_1.TimeService,
        ResponseAnalyzeService_1.ResponseAnalyzeService, ClientConfigurationService_1.ConfigurationService])
], CategoryManagementComponent);
exports.CategoryManagementComponent = CategoryManagementComponent;
//# sourceMappingURL=category-management.component.js.map
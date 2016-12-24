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
var Category_1 = require("../models/Category");
var CategoryManagementComponent = (function () {
    // Initiate component with dependency injections.
    function CategoryManagementComponent(categoryService, timeService, responseAnalyzeService, configurationService) {
        this._clientCategoryService = categoryService;
        this._timeService = timeService;
        this._responseAnalyzeService = responseAnalyzeService;
        // Find configuration service in IoC.
        this._clientConfigurationService = configurationService;
        // Initiate categories search result.
        this._categorySearchResult = new CategorySearchDetailViewModel_1.CategorySearchDetailViewModel();
    }
    // Callback is fired when a category is created to be removed.
    CategoryManagementComponent.prototype.clickRemoveCategory = function (categoryDetail, deleteCategoryConfirmModal) {
        // Category detail is not valid.
        if (categoryDetail == null)
            return;
        // Update category information into box.
        this._selectCategoryDetail = categoryDetail;
        // Open delete category confirmation box.
        deleteCategoryConfirmModal.show();
    };
    // This callback is called when user confirms to delete the selected category.
    CategoryManagementComponent.prototype.clickConfirmDeleteCategory = function (deleteCategoryConfirmModal) {
        var _this = this;
        // Find category by using specific conditions.
        var findCategoriesConditions = new FindCategoriesViewModel_1.FindCategoriesViewModel();
        findCategoriesConditions.id = this._selectCategoryDetail.id;
        // No category detail is selected.
        if (this._selectCategoryDetail != null) {
            // Call category service to delete the selected category.
            this._clientCategoryService.deleteCategories(findCategoriesConditions)
                .then(function (response) {
                // Reload the search records list.
                _this.clickSearch();
            })
                .catch(function (response) {
                console.log(response);
            });
        }
        // Close the modal first.
        deleteCategoryConfirmModal.hide();
    };
    // Callback which is fired when change category detail button is clicked.
    CategoryManagementComponent.prototype.clickChangeCategoryDetail = function (categoryDetail, changeCategoryDetailModal) {
        // Category detail is invalid.
        if (categoryDetail == null)
            return;
        // Copy the category detail to selected category detail.
        this._selectCategoryDetail = categoryDetail;
        // Display the change category detail box.
        changeCategoryDetailModal.show();
    };
    // Callback which is fired when change category detail action is confirmed.
    CategoryManagementComponent.prototype.clickConfirmChangeCategoryDetail = function (changeCategoryInfoModal) {
        var _this = this;
        // Selected category detail is invalid.
        if (this._selectCategoryDetail == null)
            return;
        // Initiate category information.
        var category = new Category_1.Category();
        category.id = this._selectCategoryDetail.id;
        category.name = this._selectCategoryDetail.name;
        // Close the change category info modal.
        changeCategoryInfoModal.hide();
        // Call service to update category information.
        this._clientCategoryService.changeCategoryDetails(category.id, category)
            .then(function (response) {
            console.log(response);
            // Reload the categories list.
            _this.clickSearch();
        })
            .catch(function (response) {
            console.log(response);
        });
    };
    // Callback which is fired when search button of category search box is clicked.
    CategoryManagementComponent.prototype.clickSearch = function () {
        var _this = this;
        // Freeze the find box.
        this._isLoading = true;
        // Reset the selected category detail.
        this._selectCategoryDetail = null;
        // Find categories by using specific conditions.
        this._clientCategoryService.findCategories(this._findCategoriesViewModel)
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
        this._findCategoriesViewModel = new FindCategoriesViewModel_1.FindCategoriesViewModel();
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
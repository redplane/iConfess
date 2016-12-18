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
var CategoryService_1 = require("../services/CategoryService");
var TimeService_1 = require("../services/TimeService");
var CategorySearchViewModel_1 = require("../viewmodels/category/CategorySearchViewModel");
var HyperlinkService_1 = require("../services/HyperlinkService");
var ResponseAnalyzeService_1 = require("../services/ResponseAnalyzeService");
var ConfigurationService_1 = require("../services/ConfigurationService");
var CategoryManagementComponent = (function () {
    // Initiate component with dependency injections.
    function CategoryManagementComponent(categoryService, timeService, responseAnalyzeService, configurationService) {
        this._categoryService = categoryService;
        this._timeService = timeService;
        this._responseAnalyzeService = responseAnalyzeService;
        // Find configuration service in IoC.
        this._configurationService = configurationService;
        // Initiate categories search result.
        this._categorySearchResult = new CategorySearchDetailViewModel_1.CategorySearchDetailViewModel();
        // Initiate category search conditions.
        this._categorySearchConditions = new CategorySearchViewModel_1.CategorySearchViewModel();
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
    CategoryManagementComponent.prototype.clickSearch = function (categoriesSearchConditions) {
        var _this = this;
        // Update search conditions.
        this._categorySearchConditions = categoriesSearchConditions;
        // Freeze the find box.
        this._isLoading = true;
        // Find categories by using specific conditions.
        this._categoryService.findCategories(categoriesSearchConditions)
            .then(function (response) {
            // Update categories list.
            _this._categorySearchResult = response.json();
            // Unfreeze the category find box.
            _this._isLoading = false;
            console.log(response);
        })
            .catch(function (response) {
            _this._isLoading = false;
        });
    };
    return CategoryManagementComponent;
}());
CategoryManagementComponent = __decorate([
    core_1.Component({
        selector: 'category-management',
        templateUrl: './app/html/pages/category-management.component.html',
        providers: [
            CategoryService_1.CategoryService,
            TimeService_1.TimeService,
            HyperlinkService_1.HyperlinkService,
            ResponseAnalyzeService_1.ResponseAnalyzeService,
            ConfigurationService_1.ConfigurationService
        ],
    }),
    __metadata("design:paramtypes", [CategoryService_1.CategoryService, TimeService_1.TimeService,
        ResponseAnalyzeService_1.ResponseAnalyzeService, ConfigurationService_1.ConfigurationService])
], CategoryManagementComponent);
exports.CategoryManagementComponent = CategoryManagementComponent;
//# sourceMappingURL=category-management.component.js.map
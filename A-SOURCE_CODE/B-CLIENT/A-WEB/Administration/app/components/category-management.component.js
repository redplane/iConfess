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
var core_1 = require('@angular/core');
var CategoryService_1 = require("../services/CategoryService");
var TimeService_1 = require("../services/TimeService");
var CategoryManagementComponent = (function () {
    function CategoryManagementComponent(categoryService, timeService) {
        this._categoryService = categoryService;
        this._timeService = timeService;
        this._categorySearchResult = this._categoryService.findCategories();
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
    CategoryManagementComponent = __decorate([
        core_1.Component({
            selector: 'category-management',
            templateUrl: './app/html/pages/category-management.component.html',
            providers: [
                CategoryService_1.CategoryService,
                TimeService_1.TimeService
            ],
        }), 
        __metadata('design:paramtypes', [CategoryService_1.CategoryService, TimeService_1.TimeService])
    ], CategoryManagementComponent);
    return CategoryManagementComponent;
}());
exports.CategoryManagementComponent = CategoryManagementComponent;
//# sourceMappingURL=category-management.component.js.map
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
var CategorySearchDetailViewModel_1 = require("../viewmodels/category/CategorySearchDetailViewModel");
var CategoryDetailViewModel_1 = require("../viewmodels/category/CategoryDetailViewModel");
var Account_1 = require("../models/Account");
var AccountStatuses_1 = require("../enumerations/AccountStatuses");
var core_1 = require('@angular/core');
/*
* Service which handles category business.
* */
var CategoryService = (function () {
    // Initiate instance of category service.
    function CategoryService() {
        // Initiate account information.
        this.creator = new Account_1.Account();
        this.creator.id = 1;
        this.creator.email = "linhndse03150@fpt.edu.vn";
        this.creator.nickname = "Linh Nguyen";
        this.creator.status = AccountStatuses_1.AccountStatuses.Active;
        this.creator.joined = 0;
        this.creator.lastModified = 0;
        // Initiate list of categories.
        this.categories = new Array();
        for (var i = 0; i < 10; i++) {
            var category = new CategoryDetailViewModel_1.CategoryDetailViewModel();
            category.id = i;
            category.creator = this.creator;
            category.name = "category[" + i + "]";
            category.created = i;
            category.lastModified = i;
            this.categories.push(category);
        }
    }
    // Find categories by using specific conditions.
    CategoryService.prototype.findCategories = function () {
        // Initiate category search result.
        var categoriesSearchResult = new CategorySearchDetailViewModel_1.CategorySearchDetailViewModel();
        categoriesSearchResult.categories = this.categories;
        categoriesSearchResult.total = this.categories.length;
        return categoriesSearchResult;
    };
    CategoryService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [])
    ], CategoryService);
    return CategoryService;
}());
exports.CategoryService = CategoryService;
//# sourceMappingURL=CategoryService.js.map
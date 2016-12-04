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
var CategoryDetailViewModel_1 = require("../../../viewmodels/category/CategoryDetailViewModel");
var Account_1 = require("../../../models/Account");
var CategoryEditBoxComponent = (function () {
    // Initiate category edit box component.
    function CategoryEditBoxComponent() {
        // Category hasn't been initialized.
        if (this._category == null)
            this._category = new CategoryDetailViewModel_1.CategoryDetailViewModel();
        // Creator hasn't been initialized.
        if (this._category.creator == null)
            this._category.creator = new Account_1.Account();
        this._category.name = 'Category 01';
    }
    // Update category which should be shown on screen.
    CategoryEditBoxComponent.prototype.setCategory = function (category) {
        this._category = category;
    };
    // Display change category information modal.
    CategoryEditBoxComponent.prototype.open = function () {
        $(this.changeCategoryBox.nativeElement).modal('show');
    };
    // Close change category information modal.
    CategoryEditBoxComponent.prototype.close = function () {
        $(this.changeCategoryBox.nativeElement).modal('close');
    };
    // Toggle change category information modal.
    CategoryEditBoxComponent.prototype.toggle = function () {
        $(this.changeCategoryBox.nativeElement).modal('toggle');
    };
    __decorate([
        core_1.ViewChild('changeCategoryBox'), 
        __metadata('design:type', core_1.ElementRef)
    ], CategoryEditBoxComponent.prototype, "changeCategoryBox", void 0);
    CategoryEditBoxComponent = __decorate([
        core_1.Component({
            selector: 'category-edit-box',
            templateUrl: './app/html/content/category/category-edit-box.component.html',
            providers: [
                CategoryDetailViewModel_1.CategoryDetailViewModel,
                Account_1.Account
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], CategoryEditBoxComponent);
    return CategoryEditBoxComponent;
}());
exports.CategoryEditBoxComponent = CategoryEditBoxComponent;
//# sourceMappingURL=category-edit-box.component.js.map
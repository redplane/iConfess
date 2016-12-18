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
var CategoryDetailViewModel_1 = require("../../../viewmodels/category/CategoryDetailViewModel");
var CategoryDeleteBoxComponent = (function () {
    // Initiate category edit box component.
    function CategoryDeleteBoxComponent() {
        // Category hasn't been initialized.
        if (this._category == null)
            this._category = new CategoryDetailViewModel_1.CategoryDetailViewModel();
    }
    // Update category which should be shown on screen.
    CategoryDeleteBoxComponent.prototype.setCategory = function (category) {
        this._category = category;
    };
    // Display change category information modal.
    CategoryDeleteBoxComponent.prototype.open = function () {
        $(this.deleteCategoryBox.nativeElement).modal('show');
    };
    // Close change category information modal.
    CategoryDeleteBoxComponent.prototype.close = function () {
        $(this.deleteCategoryBox.nativeElement).modal('close');
    };
    // Toggle change category information modal.
    CategoryDeleteBoxComponent.prototype.toggle = function () {
        $(this.deleteCategoryBox.nativeElement).modal('toggle');
    };
    return CategoryDeleteBoxComponent;
}());
__decorate([
    core_1.ViewChild('deleteCategoryBox'),
    __metadata("design:type", core_1.ElementRef)
], CategoryDeleteBoxComponent.prototype, "deleteCategoryBox", void 0);
CategoryDeleteBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-delete-box',
        templateUrl: './app/html/content/category/category-delete-box.component.html',
        providers: [
            CategoryDetailViewModel_1.CategoryDetailViewModel
        ]
    }),
    __metadata("design:paramtypes", [])
], CategoryDeleteBoxComponent);
exports.CategoryDeleteBoxComponent = CategoryDeleteBoxComponent;
//# sourceMappingURL=category-delete-box.component.js.map
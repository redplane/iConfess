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
var CategorySearchViewModel_1 = require("../../../viewmodels/category/CategorySearchViewModel");
var CategoryFindBoxComponent = (function () {
    // Initiate component with default dependency injection.
    function CategoryFindBoxComponent() {
        // Initiate category search view model.
        this._categorySearch = new CategorySearchViewModel_1.CategorySearchViewModel();
        // Initiate event emitters.
        this.search = new core_1.EventEmitter();
    }
    // Callback which is fired when search button is clicked.
    CategoryFindBoxComponent.prototype.clickSearch = function () {
        var _this = this;
        this._isLoading = true;
        setTimeout(function () {
            _this._isLoading = false;
            _this.search.emit(_this._categorySearch);
        }, 10000);
    };
    CategoryFindBoxComponent = __decorate([
        core_1.Component({
            selector: 'category-find-box',
            templateUrl: './app/html/content/category/category-find-box.component.html',
            inputs: ['categorySearch'],
            outputs: ['search'],
            providers: [
                CategorySearchViewModel_1.CategorySearchViewModel
            ]
        }), 
        __metadata('design:paramtypes', [])
    ], CategoryFindBoxComponent);
    return CategoryFindBoxComponent;
}());
exports.CategoryFindBoxComponent = CategoryFindBoxComponent;
//# sourceMappingURL=category-find-box.component.js.map
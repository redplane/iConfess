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
var CategoryDetailsViewModel_1 = require("../../viewmodels/category/CategoryDetailsViewModel");
var _ = require("lodash");
var CategoryDeleteBoxComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with injectors.
    function CategoryDeleteBoxComponent() {
        this.details = new CategoryDetailsViewModel_1.CategoryDetailsViewModel();
    }
    //#endregion
    //#region Methods
    // Set details and attach to delete box.
    CategoryDeleteBoxComponent.prototype.setDetails = function (details) {
        this.details = _.cloneDeep(details);
    };
    // Get category details.
    CategoryDeleteBoxComponent.prototype.getDetails = function () {
        return this.details;
    };
    return CategoryDeleteBoxComponent;
}());
CategoryDeleteBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-delete-box',
        templateUrl: 'category-delete-box.component.html',
        exportAs: 'category-delete-box'
    }),
    __metadata("design:paramtypes", [])
], CategoryDeleteBoxComponent);
exports.CategoryDeleteBoxComponent = CategoryDeleteBoxComponent;
//# sourceMappingURL=category-delete-box.component.js.map
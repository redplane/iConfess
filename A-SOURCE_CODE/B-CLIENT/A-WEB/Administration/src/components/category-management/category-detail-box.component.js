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
var ClientTimeService_1 = require("../../services/ClientTimeService");
var CategoryDetailBoxComponent = (function () {
    // Initiate category detail box with dependency injections.
    function CategoryDetailBoxComponent(timeService) {
        this._timeService = timeService;
        this.clickRemoveCategory = new core_1.EventEmitter();
        this.clickChangeCategoryInfo = new core_1.EventEmitter();
    }
    // Fired when a category is clicked to be removed.
    CategoryDetailBoxComponent.prototype.deleteCategory = function (category) {
        this.clickRemoveCategory.emit(category);
    };
    // Fired when a category is clicked to be changed.
    CategoryDetailBoxComponent.prototype.changeCategoryInfo = function (category) {
        this.clickChangeCategoryInfo.emit(category);
    };
    return CategoryDetailBoxComponent;
}());
CategoryDetailBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-detail',
        inputs: ['category'],
        outputs: ['clickRemoveCategory', 'clickChangeCategoryInfo'],
        templateUrl: 'category-detail-box.component.html',
        providers: [
            ClientTimeService_1.ClientTimeService
        ]
    }),
    __metadata("design:paramtypes", [ClientTimeService_1.ClientTimeService])
], CategoryDetailBoxComponent);
exports.CategoryDetailBoxComponent = CategoryDetailBoxComponent;
//# sourceMappingURL=category-detail-box.component.js.map
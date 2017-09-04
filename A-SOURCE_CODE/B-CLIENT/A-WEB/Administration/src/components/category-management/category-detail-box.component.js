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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var CategoryDetailsViewModel_1 = require("../../viewmodels/category/CategoryDetailsViewModel");
var _ = require("lodash");
var CategoryDetailBoxComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate category detail box with dependency injections.
    function CategoryDetailBoxComponent(clientTimeService) {
        this.clientTimeService = clientTimeService;
        this.details = new CategoryDetailsViewModel_1.CategoryDetailsViewModel();
    }
    //#endregion
    //#region Methods
    // Set detail information and attach to component.
    CategoryDetailBoxComponent.prototype.setDetails = function (details) {
        this.details = _.cloneDeep(details);
    };
    // Get detail information attached to component.
    CategoryDetailBoxComponent.prototype.getDetails = function () {
        return this.details;
    };
    return CategoryDetailBoxComponent;
}());
CategoryDetailBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-detail-box',
        templateUrl: 'category-detail-box.component.html',
        exportAs: 'category-detail-box'
    }),
    __param(0, core_1.Inject("IClientTimeService")),
    __metadata("design:paramtypes", [Object])
], CategoryDetailBoxComponent);
exports.CategoryDetailBoxComponent = CategoryDetailBoxComponent;
//# sourceMappingURL=category-detail-box.component.js.map
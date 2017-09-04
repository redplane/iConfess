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
var InitiateCategoryViewModel_1 = require("../../viewmodels/category/InitiateCategoryViewModel");
var forms_1 = require("@angular/forms");
var CategoryInitiateBoxComponent = (function () {
    //#region Constructor
    // Initiate component with default dependency injection.
    function CategoryInitiateBoxComponent() {
        // Initiate initiator.
        this.initiator = new InitiateCategoryViewModel_1.InitiateCategoryViewModel();
    }
    //#endregion
    //#region Methods
    // Get initiator information.
    CategoryInitiateBoxComponent.prototype.getInitiator = function () {
        return this.initiator;
    };
    CategoryInitiateBoxComponent.prototype.getForm = function () {
        return this.categoryInitiatorComponent;
    };
    return CategoryInitiateBoxComponent;
}());
__decorate([
    core_1.ViewChild('categoryInitiatorComponent'),
    __metadata("design:type", forms_1.NgForm)
], CategoryInitiateBoxComponent.prototype, "categoryInitiatorComponent", void 0);
CategoryInitiateBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-initiate-box',
        templateUrl: 'category-initiate-box.component.html',
        exportAs: 'category-initiate-box'
    }),
    __metadata("design:paramtypes", [])
], CategoryInitiateBoxComponent);
exports.CategoryInitiateBoxComponent = CategoryInitiateBoxComponent;
//# sourceMappingURL=category-initiate-box.component.js.map
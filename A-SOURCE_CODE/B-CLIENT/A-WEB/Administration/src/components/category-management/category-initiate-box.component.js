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
var forms_1 = require("@angular/forms");
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var ClientAccountService_1 = require("../../services/clients/ClientAccountService");
var ClientDataConstraintService_1 = require("../../services/ClientDataConstraintService");
var CategoryInitiateBoxComponent = (function () {
    // Initiate component with default dependency injection.
    function CategoryInitiateBoxComponent(formBuilder, clientDataConstraintService) {
        this.formBuilder = formBuilder;
        this.clientDataConstraintService = clientDataConstraintService;
        // Form control of find category box.
        this.initiateCategoryBox = this.formBuilder.group({
            name: [null]
        });
        // Initiate event emitters.
        this.initiateCategory = new core_1.EventEmitter();
        this.isLoading = false;
    }
    // Callback which is fired when confirm button is clicked.
    CategoryInitiateBoxComponent.prototype.clickInitiateCategory = function (category) {
        this.initiateCategory.emit(category);
    };
    return CategoryInitiateBoxComponent;
}());
CategoryInitiateBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-initiate-box',
        templateUrl: 'category-initiate-box.component.html',
        inputs: ['isLoading'],
        outputs: ['initiateCategory'],
        providers: [
            forms_1.FormBuilder,
            ClientDataConstraintService_1.ClientDataConstraintService,
            ClientConfigurationService_1.ClientConfigurationService,
            ClientAccountService_1.ClientAccountService
        ]
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        ClientDataConstraintService_1.ClientDataConstraintService])
], CategoryInitiateBoxComponent);
exports.CategoryInitiateBoxComponent = CategoryInitiateBoxComponent;
//# sourceMappingURL=category-initiate-box.component.js.map
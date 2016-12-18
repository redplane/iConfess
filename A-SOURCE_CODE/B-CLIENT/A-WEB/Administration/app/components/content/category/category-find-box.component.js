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
var CategorySearchViewModel_1 = require("../../../viewmodels/category/CategorySearchViewModel");
var forms_1 = require("@angular/forms");
var ConfigurationService_1 = require("../../../services/ConfigurationService");
var Pagination_1 = require("../../../viewmodels/Pagination");
var CategoryFindBoxComponent = (function () {
    // Initiate component with default dependency injection.
    function CategoryFindBoxComponent(formBuilder, configurationService) {
        this.formBuilder = formBuilder;
        // Find configuration service from IoC.
        this._configurationService = configurationService;
        // Form control of find category box.
        this.findCategoryBox = this.formBuilder.group({
            name: [null],
            creatorIndex: [null],
            created: this.formBuilder.group({
                from: [null, forms_1.Validators.nullValidator],
                to: [null, forms_1.Validators.nullValidator]
            }),
            lastModified: this.formBuilder.group({
                from: [null, forms_1.Validators.nullValidator],
                to: [null, forms_1.Validators.nullValidator]
            }),
            pagination: this.formBuilder.group({
                records: [null, forms_1.Validators.nullValidator]
            }),
            sort: [null],
            direction: [null]
        });
        // Initiate conditions.
        this._categoriesSearchConditions = new CategorySearchViewModel_1.CategorySearchViewModel();
        this._categoriesSearchConditions.pagination = new Pagination_1.Pagination();
        this._categoriesSearchConditions.pagination.records = this._configurationService.pageRecords[0];
        this._categoriesSearchConditions.direction = this._configurationService.sortDirections.values()[0];
        // Initiate event emitters.
        this.search = new core_1.EventEmitter();
    }
    // Callback which is fired when search button is clicked.
    CategoryFindBoxComponent.prototype.clickSearch = function (event) {
        console.log(event);
        //this.search.emit(this.findCategoryBox.value);
    };
    return CategoryFindBoxComponent;
}());
CategoryFindBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-find-box',
        templateUrl: './app/html/content/category/category-find-box.component.html',
        inputs: ['categorySearch', 'isLoading'],
        outputs: ['search'],
        providers: [
            CategorySearchViewModel_1.CategorySearchViewModel,
            forms_1.FormBuilder,
            ConfigurationService_1.ConfigurationService
        ]
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder, ConfigurationService_1.ConfigurationService])
], CategoryFindBoxComponent);
exports.CategoryFindBoxComponent = CategoryFindBoxComponent;
//# sourceMappingURL=category-find-box.component.js.map
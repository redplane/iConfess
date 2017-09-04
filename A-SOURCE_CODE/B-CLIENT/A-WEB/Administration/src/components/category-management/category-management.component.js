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
var SearchCategoriesViewModel_1 = require("../../viewmodels/category/SearchCategoriesViewModel");
var Category_1 = require("../../models/entities/Category");
var Pagination_1 = require("../../viewmodels/Pagination");
var ng2_bootstrap_1 = require("ng2-bootstrap");
var SearchResult_1 = require("../../models/SearchResult");
var SortDirection_1 = require("../../enumerations/SortDirection");
var CategorySortProperty_1 = require("../../enumerations/order/CategorySortProperty");
var Sorting_1 = require("../../models/Sorting");
var category_initiate_box_component_1 = require("./category-initiate-box.component");
var category_detail_box_component_1 = require("./category-detail-box.component");
var category_delete_box_component_1 = require("./category-delete-box.component");
var CategoryManagementComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with dependency injections.
    function CategoryManagementComponent(clientCategoryService, clientApiService, clientToastrService, clientTimeService, clientCommonService) {
        this.clientCategoryService = clientCategoryService;
        this.clientApiService = clientApiService;
        this.clientToastrService = clientToastrService;
        this.clientTimeService = clientTimeService;
        this.clientCommonService = clientCommonService;
        // Initiate search condition.
        var conditions = new SearchCategoriesViewModel_1.SearchCategoriesViewModel();
        var sorting = new Sorting_1.Sorting();
        sorting.direction = SortDirection_1.SortDirection.Ascending;
        sorting.property = CategorySortProperty_1.CategorySortProperty.index;
        conditions.sorting = sorting;
        var pagination = new Pagination_1.Pagination();
        pagination.page = 1;
        pagination.records = this.clientCommonService.getMaxPageRecords();
        conditions.pagination = pagination;
        // Update initial conditions.
        this.conditions = conditions;
        // Initiate categories search result.
        this.searchResult = new SearchResult_1.SearchResult();
    }
    //#endregion
    //#region Methods
    // Callback is fired when a category is created to be removed.
    CategoryManagementComponent.prototype.clickRemoveCategory = function (categoryDetail) {
        // Category detail is not valid.
        if (categoryDetail == null)
            return;
        console.log(categoryDetail);
        // Set the detail first.
        this.categoryDeleteBox.setDetails(categoryDetail);
        // Show the modal.
        this.deleteCategoryModal.show();
    };
    // This callback is called when user confirms to delete the selected category.
    CategoryManagementComponent.prototype.clickConfirmDeleteCategory = function () {
        var _this = this;
        // Find category by using specific conditions.
        var details = this.categoryDeleteBox.getDetails();
        if (details == null) {
            return;
        }
        // Make the loading start.
        this.isBusy = true;
        // Initiate search category view model.
        var conditions = new SearchCategoriesViewModel_1.SearchCategoriesViewModel();
        conditions.id = details.id;
        // Call category service to delete the selected category.
        this.clientCategoryService.deleteCategories(conditions)
            .then(function (x) {
            // Cancel loading state.
            _this.isBusy = false;
            // Close the modal.
            _this.deleteCategoryModal.hide();
            // Reload the categories list.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel loading.
            _this.isBusy = false;
            // Proceed common invalid response.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when change category detail button is clicked.
    CategoryManagementComponent.prototype.clickChangeCategoryDetail = function (categoryDetail) {
        // Category detail is invalid.
        if (categoryDetail == null)
            return;
        // Set details to editor.
        this.categoryDetailBox.setDetails(categoryDetail);
        // Display the change category detail box.
        this.categoryDetailModal.show();
    };
    // Callback which is fired when change category detail action is confirmed.
    CategoryManagementComponent.prototype.clickConfirmChangeCategoryDetail = function () {
        var _this = this;
        // Selected category detail is invalid.
        var details = this.categoryDetailBox.getDetails();
        // Category details is not valid.
        if (details == null) {
            return;
        }
        // Initiate category information.
        var category = new Category_1.Category();
        category.id = details.id;
        category.name = details.name;
        // Start loading.
        this.isBusy = true;
        // Call service to update category information.
        this.clientCategoryService.editCategoryDetails(category.id, category)
            .then(function (x) {
            // Close the modal.
            _this.categoryDetailModal.hide();
            // Clear busy state.
            _this.isBusy = false;
            // Reload the categories list.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel loading.
            _this.isBusy = false;
        });
    };
    // Callback which is fired when a category should be created into system.
    CategoryManagementComponent.prototype.clickInitiateCategory = function () {
        // Display category initial modal.
        this.initiateCategoryInfoModal.show();
    };
    // This callback is fired when confirm button is clicked which tells client to send request to service to create a new category.
    CategoryManagementComponent.prototype.clickConfirmInitiateCategory = function () {
        var _this = this;
        // Find category defined in initiator dialog.
        var category = this.categoryInitiatorContent.getInitiator();
        // Invalid category detected.
        if (category == null) {
            return;
        }
        // Call service to initiate category.
        this.clientCategoryService.initiateCategory(category)
            .then(function (x) {
            // Cancel content loading.
            _this.isBusy = false;
            // Parse information of response.
            var category = x.json();
            // Display notification to client screen.
            _this.clientToastrService.success(category.name + " has been created successfully", 'System', null);
            // Close the modal.
            _this.initiateCategoryInfoModal.hide();
            // Reload search results.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel content loading.
            _this.isBusy = false;
            // Proceed common function to handle invalid process.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when search button of category search box is clicked.
    CategoryManagementComponent.prototype.clickSearch = function () {
        var _this = this;
        // Freeze the find box.
        this.isBusy = true;
        // Find categories by using specific conditions.
        this.clientCategoryService.getCategories(this.conditions)
            .then(function (x) {
            // Update categories list.
            _this.searchResult = x.json();
            // Unfreeze the category find box.
            _this.isBusy = false;
        })
            .catch(function (response) {
            // Unlock screen components.
            _this.isBusy = false;
            // Call common function to handle error response.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when page selection is changed.
    CategoryManagementComponent.prototype.selectPage = function (page) {
        // Update pagination page.
        this.conditions.pagination.page = page;
        // Call search function.
        this.clickSearch();
    };
    // This callback is fired when category management component is initiated.
    CategoryManagementComponent.prototype.ngOnInit = function () {
        // Search for categories using specific conditions.
        this.clickSearch();
    };
    return CategoryManagementComponent;
}());
__decorate([
    core_1.ViewChild('initiateCategoryInfoModal'),
    __metadata("design:type", ng2_bootstrap_1.ModalDirective)
], CategoryManagementComponent.prototype, "initiateCategoryInfoModal", void 0);
__decorate([
    core_1.ViewChild('categoryInitiatorContent'),
    __metadata("design:type", category_initiate_box_component_1.CategoryInitiateBoxComponent)
], CategoryManagementComponent.prototype, "categoryInitiatorContent", void 0);
__decorate([
    core_1.ViewChild('categoryDetailBox'),
    __metadata("design:type", category_detail_box_component_1.CategoryDetailBoxComponent)
], CategoryManagementComponent.prototype, "categoryDetailBox", void 0);
__decorate([
    core_1.ViewChild('categoryDetailModal'),
    __metadata("design:type", ng2_bootstrap_1.ModalDirective)
], CategoryManagementComponent.prototype, "categoryDetailModal", void 0);
__decorate([
    core_1.ViewChild('categoryDeleteBox'),
    __metadata("design:type", category_delete_box_component_1.CategoryDeleteBoxComponent)
], CategoryManagementComponent.prototype, "categoryDeleteBox", void 0);
__decorate([
    core_1.ViewChild('deleteCategoryModal'),
    __metadata("design:type", ng2_bootstrap_1.ModalDirective)
], CategoryManagementComponent.prototype, "deleteCategoryModal", void 0);
CategoryManagementComponent = __decorate([
    core_1.Component({
        selector: 'category-management',
        templateUrl: 'category-management.component.html'
    }),
    __param(0, core_1.Inject("IClientCategoryService")),
    __param(1, core_1.Inject("IClientApiService")),
    __param(2, core_1.Inject("IClientToastrService")),
    __param(3, core_1.Inject("IClientTimeService")),
    __param(4, core_1.Inject('IClientCommonService')),
    __metadata("design:paramtypes", [Object, Object, Object, Object, Object])
], CategoryManagementComponent);
exports.CategoryManagementComponent = CategoryManagementComponent;
//# sourceMappingURL=category-management.component.js.map
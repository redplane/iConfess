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
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var SearchCategoriesViewModel_1 = require("../../viewmodels/category/SearchCategoriesViewModel");
var Category_1 = require("../../models/entities/Category");
var Pagination_1 = require("../../viewmodels/Pagination");
var SearchResult_1 = require("../../models/SearchResult");
var CategoryManagementComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with dependency injections.
    function CategoryManagementComponent(clientCategoryService, clientConfigurationService, clientApiService, clientToastrService, clientTimeService) {
        this.clientCategoryService = clientCategoryService;
        this.clientConfigurationService = clientConfigurationService;
        this.clientApiService = clientApiService;
        this.clientToastrService = clientToastrService;
        this.clientTimeService = clientTimeService;
        // Initiate categories search result.
        this.categorySearchResult = new SearchResult_1.SearchResult();
    }
    //#endregion
    //#region Methods
    // Callback is fired when a category is created to be removed.
    CategoryManagementComponent.prototype.clickRemoveCategory = function (categoryDetail, deleteCategoryConfirmModal) {
        // Category detail is not valid.
        if (categoryDetail == null)
            return;
        // Update category information into box.
        this.selectCategoryDetail = categoryDetail;
        // Open delete category confirmation box.
        deleteCategoryConfirmModal.show();
    };
    // This callback is called when user confirms to delete the selected category.
    CategoryManagementComponent.prototype.clickConfirmDeleteCategory = function (deleteCategoryConfirmModal) {
        var _this = this;
        // Find category by using specific conditions.
        var findCategoriesConditions = new SearchCategoriesViewModel_1.SearchCategoriesViewModel();
        findCategoriesConditions.id = this.selectCategoryDetail.id;
        // No category detail is selected.
        if (this.selectCategoryDetail != null) {
            // Make the loading start.
            this.isLoading = true;
            // Call category service to delete the selected category.
            this.clientCategoryService.deleteCategories(findCategoriesConditions)
                .then(function (response) {
                // Cancel loading state.
                _this.isLoading = false;
                // Reload the categories list.
                _this.clickSearch();
            })
                .catch(function (response) {
                // Cancel loading.
                _this.isLoading = false;
                // Proceed common invalid response.
                _this.clientApiService.handleInvalidResponse(response);
            });
        }
        // Close the modal first.
        deleteCategoryConfirmModal.hide();
    };
    // Callback which is fired when change category detail button is clicked.
    CategoryManagementComponent.prototype.clickChangeCategoryDetail = function (categoryDetail, changeCategoryDetailModal) {
        // Category detail is invalid.
        if (categoryDetail == null)
            return;
        // Copy the category detail to selected category detail.
        this.selectCategoryDetail = categoryDetail;
        // Display the change category detail box.
        changeCategoryDetailModal.show();
    };
    // Callback which is fired when change category detail action is confirmed.
    CategoryManagementComponent.prototype.clickConfirmChangeCategoryDetail = function (changeCategoryInfoModal) {
        var _this = this;
        // Selected category detail is invalid.
        if (this.selectCategoryDetail == null)
            return;
        // Initiate category information.
        var category = new Category_1.Category();
        category.id = this.selectCategoryDetail.id;
        category.name = this.selectCategoryDetail.name;
        // Close the change category info modal.
        changeCategoryInfoModal.hide();
        // Start loading.
        this.isLoading = true;
        // Call service to update category information.
        this.clientCategoryService.editCategoryDetails(category.id, category)
            .then(function (response) {
            // Reload the categories list.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel loading.
            _this.isLoading = false;
        });
    };
    // Callback which is fired when a category should be created into system.
    CategoryManagementComponent.prototype.clickInitiateCategory = function (category, initiateCategoryModal) {
        var _this = this;
        // Make content be loaded.
        this.isLoading = true;
        // Call service to initiate category.
        this.clientCategoryService.initiateCategory(category)
            .then(function (response) {
            // Cancel content loading.
            _this.isLoading = false;
            // Parse information of response.
            var information = response.json();
            // Display notification to client screen.
            _this.clientToastrService.success(information['name'] + " has been created successfully", 'System', null);
            // Close the modal.
            initiateCategoryModal.hide();
            // Reload search results.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel content loading.
            _this.isLoading = false;
            // Proceed common function to handle invalid process.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when search button of category search box is clicked.
    CategoryManagementComponent.prototype.clickSearch = function () {
        var _this = this;
        // Freeze the find box.
        this.isLoading = true;
        // Reset the selected category detail.
        this.selectCategoryDetail = null;
        // Find categories by using specific conditions.
        this.clientCategoryService.getCategories(this.findCategoriesViewModel)
            .then(function (response) {
            // Update categories list.
            _this.categorySearchResult = response.json();
            // Unfreeze the category find box.
            _this.isLoading = false;
        })
            .catch(function (response) {
            // Unlock screen components.
            _this.isLoading = false;
            // Call common function to handle error response.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when page selection is changed.
    CategoryManagementComponent.prototype.clickPageChange = function (pagination) {
        // Update pagination page.
        this.findCategoriesViewModel.pagination.page = pagination.page;
        // Call search function.
        this.clickSearch();
    };
    // This callback is fired when category management component is initiated.
    CategoryManagementComponent.prototype.ngOnInit = function () {
        // Initiate category search conditions.
        this.findCategoriesViewModel = new SearchCategoriesViewModel_1.SearchCategoriesViewModel();
        // Refactoring pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.page = 1;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();
        this.findCategoriesViewModel.pagination = pagination;
    };
    return CategoryManagementComponent;
}());
CategoryManagementComponent = __decorate([
    core_1.Component({
        selector: 'category-management',
        templateUrl: 'category-management.component.html',
        providers: [
            ClientConfigurationService_1.ClientConfigurationService,
        ],
    }),
    __param(0, core_1.Inject("IClientCategoryService")),
    __param(2, core_1.Inject("IClientApiService")),
    __param(3, core_1.Inject("IClientToastrService")),
    __param(4, core_1.Inject("IClientTimeService")),
    __metadata("design:paramtypes", [Object, ClientConfigurationService_1.ClientConfigurationService, Object, Object, Object])
], CategoryManagementComponent);
exports.CategoryManagementComponent = CategoryManagementComponent;
//# sourceMappingURL=category-management.component.js.map
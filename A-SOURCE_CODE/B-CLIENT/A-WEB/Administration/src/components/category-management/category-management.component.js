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
var ClientCategoryService_1 = require("../../services/clients/ClientCategoryService");
var ClientTimeService_1 = require("../../services/ClientTimeService");
var ClientApiService_1 = require("../../services/ClientApiService");
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var ClientNotificationService_1 = require("../../services/ClientNotificationService");
var SearchCategoriesResultViewModel_1 = require("../../viewmodels/category/SearchCategoriesResultViewModel");
var SearchCategoriesViewModel_1 = require("../../viewmodels/category/SearchCategoriesViewModel");
var Category_1 = require("../../models/Category");
var Pagination_1 = require("../../viewmodels/Pagination");
var CategoryManagementComponent = (function () {
    // Initiate component with dependency injections.
    function CategoryManagementComponent(clientCategoryService, clientConfigurationService, clientApiService, clientNotificationService, clientTimeService) {
        this.clientCategoryService = clientCategoryService;
        this.clientConfigurationService = clientConfigurationService;
        this.clientApiService = clientApiService;
        this.clientNotificationService = clientNotificationService;
        this.clientTimeService = clientTimeService;
        // Initiate categories search result.
        this.categorySearchResult = new SearchCategoriesResultViewModel_1.SearchCategoriesResultViewModel();
    }
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
                _this.clientApiService.proceedHttpNonSolidResponse(response);
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
        this.clientCategoryService.changeCategoryDetails(category.id, category)
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
            _this.clientNotificationService.success(information['name'] + " has been created successfully", 'System');
            // Close the modal.
            initiateCategoryModal.hide();
            // Reload search results.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel content loading.
            _this.isLoading = false;
            // Proceed common function to handle invalid process.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
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
        this.clientCategoryService.findCategories(this.findCategoriesViewModel)
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
            _this.clientApiService.proceedHttpNonSolidResponse(response);
        });
    };
    // Callback which is fired when page selection is changed.
    CategoryManagementComponent.prototype.clickPageChange = function (pagination) {
        // Update pagination index.
        this.findCategoriesViewModel.pagination.index = pagination.page;
        // Call search function.
        this.clickSearch();
    };
    // This callback is fired when category management component is initiated.
    CategoryManagementComponent.prototype.ngOnInit = function () {
        // Initiate category search conditions.
        this.findCategoriesViewModel = new SearchCategoriesViewModel_1.SearchCategoriesViewModel();
        // Refactoring pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        this.findCategoriesViewModel.pagination = pagination;
    };
    return CategoryManagementComponent;
}());
CategoryManagementComponent = __decorate([
    core_1.Component({
        selector: 'category-management',
        templateUrl: 'category-management.component.html',
        providers: [
            ClientCategoryService_1.ClientCategoryService,
            ClientTimeService_1.ClientTimeService,
            ClientApiService_1.ClientApiService,
            ClientConfigurationService_1.ClientConfigurationService,
            ClientAuthenticationService_1.ClientAuthenticationService,
            ClientNotificationService_1.ClientNotificationService
        ],
    }),
    __metadata("design:paramtypes", [ClientCategoryService_1.ClientCategoryService,
        ClientConfigurationService_1.ClientConfigurationService,
        ClientApiService_1.ClientApiService,
        ClientNotificationService_1.ClientNotificationService,
        ClientTimeService_1.ClientTimeService])
], CategoryManagementComponent);
exports.CategoryManagementComponent = CategoryManagementComponent;
//# sourceMappingURL=category-management.component.js.map
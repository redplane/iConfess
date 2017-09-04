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
var ng2_bootstrap_1 = require("ng2-bootstrap");
var SearchAccountsViewModel_1 = require("../../viewmodels/accounts/SearchAccountsViewModel");
var Pagination_1 = require("../../viewmodels/Pagination");
var SearchResult_1 = require("../../models/SearchResult");
var Sorting_1 = require("../../models/Sorting");
var AccountSortProperty_1 = require("../../enumerations/order/AccountSortProperty");
var SortDirection_1 = require("../../enumerations/SortDirection");
var account_profile_box_component_1 = require("./account-profile-box.component");
var AccountStatuses_1 = require("../../enumerations/AccountStatuses");
var AccountManagementComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with injections.
    function AccountManagementComponent(clientAccountService, clientCommonService, clientApiService, clientTimeService) {
        this.clientAccountService = clientAccountService;
        this.clientCommonService = clientCommonService;
        this.clientApiService = clientApiService;
        this.clientTimeService = clientTimeService;
        // Point to enumeration to take values out.
        this.AccountStatuses = AccountStatuses_1.AccountStatuses;
        // Initiate search conditions.
        var sorting = new Sorting_1.Sorting();
        sorting.direction = SortDirection_1.SortDirection.Ascending;
        sorting.property = AccountSortProperty_1.AccountSortProperty.index;
        var pagination = new Pagination_1.Pagination();
        pagination.page = 1;
        pagination.records = clientCommonService.getMaxPageRecords();
        this.conditions = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        this.conditions.sorting = sorting;
        this.conditions.pagination = pagination;
        // Initiate search result.
        this.searchResult = new SearchResult_1.SearchResult();
    }
    //#endregion
    //#region Methods
    // Callback which is fired when search button of category search box is clicked.
    AccountManagementComponent.prototype.clickSearch = function () {
        var _this = this;
        // Make component be busy.
        this.isBusy = true;
        this.clientAccountService.getAccounts(this.conditions)
            .then(function (x) {
            // Find list of accounts which has been found from service.
            _this.searchResult = x.json();
            // Cancel loading.
            _this.isBusy = false;
        })
            .catch(function (x) {
            // Cancel loading.
            _this.isBusy = false;
            // Proceed non-solid response handling.
            _this.clientApiService.handleInvalidResponse(x);
        });
    };
    // Callback which is fired when change account information button is clicked.
    AccountManagementComponent.prototype.clickChangeAccountInfo = function (account) {
        // Update account information into profile box.
        this.profileBox.setProfile(account);
        // Display modal.
        this.profileBoxContainer.show();
    };
    // Callback which is fired when change account information ok button is clicked.
    AccountManagementComponent.prototype.clickConfirmAccountInfo = function () {
        var _this = this;
        // Find account from profile box.
        var account = this.profileBox.getProfile();
        // Account is invalid.
        if (account == null) {
            return;
        }
        // Set components to loading state.
        this.isBusy = true;
        // Send request to service to change account information.
        this.clientAccountService.editUserProfile(account.id, account)
            .then(function (response) {
            // Cancel loading.
            _this.isBusy = false;
            // Close the dialog.
            _this.profileBoxContainer.hide();
            // Reload the page.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel loading process.
            _this.isBusy = false;
            // Close the dialog.
            _this.profileBoxContainer.hide();
            // Handle common error response.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Called when component has been successfully rendered.
    AccountManagementComponent.prototype.ngOnInit = function () {
        // Components are not busy loading.
        this.isBusy = false;
        // Load all accounts from service.
        this.clickSearch();
    };
    // Callback which is called when page is clicked on.
    AccountManagementComponent.prototype.selectPage = function (page) {
        var pagination = this.conditions.pagination;
        pagination.page = page;
        this.conditions.pagination = pagination;
        // Update page.
        this.conditions.pagination.page = page;
        // Base on specific condition to load accounts list from database.
        this.clickSearch();
    };
    return AccountManagementComponent;
}());
__decorate([
    core_1.ViewChild('profileBox'),
    __metadata("design:type", account_profile_box_component_1.AccountProfileBoxComponent)
], AccountManagementComponent.prototype, "profileBox", void 0);
__decorate([
    core_1.ViewChild('profileBoxContainer'),
    __metadata("design:type", ng2_bootstrap_1.ModalDirective)
], AccountManagementComponent.prototype, "profileBoxContainer", void 0);
AccountManagementComponent = __decorate([
    core_1.Component({
        selector: 'account-management',
        templateUrl: 'account-management.component.html'
    }),
    __param(0, core_1.Inject("IClientAccountService")),
    __param(1, core_1.Inject('IClientCommonService')),
    __param(2, core_1.Inject("IClientApiService")),
    __param(3, core_1.Inject("IClientTimeService")),
    __metadata("design:paramtypes", [Object, Object, Object, Object])
], AccountManagementComponent);
exports.AccountManagementComponent = AccountManagementComponent;
//# sourceMappingURL=account-management.component.js.map
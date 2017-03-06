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
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var ClientNotificationService_1 = require("../../services/ClientNotificationService");
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var ClientAccountService_1 = require("../../services/clients/ClientAccountService");
var ClientApiService_1 = require("../../services/ClientApiService");
var ClientCommonService_1 = require("../../services/ClientCommonService");
var SearchAccountsResultViewModel_1 = require("../../viewmodels/accounts/SearchAccountsResultViewModel");
var SearchAccountsViewModel_1 = require("../../viewmodels/accounts/SearchAccountsViewModel");
var AccountStatuses_1 = require("../../enumerations/AccountStatuses");
var Pagination_1 = require("../../viewmodels/Pagination");
var AccountManagementComponent = (function () {
    // Initiate component with injections.
    function AccountManagementComponent(clientConfigurationService, clientAccountService, clientCommonService, clientApiService) {
        this.clientConfigurationService = clientConfigurationService;
        this.clientAccountService = clientAccountService;
        this.clientCommonService = clientCommonService;
        this.clientApiService = clientApiService;
        // Account status enumeration.
        this.accountStatuses = AccountStatuses_1.AccountStatuses;
        // Initiate search conditions.
        this.conditions = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        // Initiate find accounts result.
        this.findAccountsResult = new SearchAccountsResultViewModel_1.SearchAccountsResultViewModel();
    }
    // Callback which is fired when search button of category search box is clicked.
    AccountManagementComponent.prototype.clickSearch = function () {
        var _this = this;
        // Freeze the find box.
        this.isLoading = true;
        this.clientAccountService.findAccounts(this.conditions)
            .then(function (response) {
            // Find list of accounts which has been found from service.
            var findAccountsResult = response.json();
            _this.findAccountsResult = findAccountsResult;
            // Cancel loading.
            _this.isLoading = false;
        })
            .catch(function (response) {
            // Cancel loading.
            _this.isLoading = false;
            // Proceed non-solid response handling.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
        });
    };
    // Callback which is fired when change account information button is clicked.
    AccountManagementComponent.prototype.clickChangeAccountInfo = function (account, changeAccountInfoModal) {
        this.selectedAccount = account;
        changeAccountInfoModal.show();
    };
    // Callback which is fired when change account information ok button is clicked.
    AccountManagementComponent.prototype.clickConfirmChangeAccountDetail = function (changeAccountModal) {
        var _this = this;
        // No account has been selected for edit.
        if (this.selectedAccount == null) {
            // Close the dialog.
            changeAccountModal.hide();
            return;
        }
        // Set components to loading state.
        this.isLoading = true;
        // Send request to service to change account information.
        this.clientAccountService.changeAccountInformation(this.selectedAccount.id, this.selectedAccount)
            .then(function (response) {
            // Cancel loading.
            _this.isLoading = false;
            // Close the dialog.
            changeAccountModal.hide();
            // Reload the page.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel loading process.
            _this.isLoading = false;
            // Close the dialog.
            changeAccountModal.hide();
            // Handle common error response.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
        });
    };
    // Callback which is fired when paging item is clicked.
    AccountManagementComponent.prototype.clickPageChange = function (parameters) {
        // Update page index.
        this.conditions.pagination.index = parameters['page'];
        // Search.
        this.clickSearch();
    };
    // Check whether account search result is available or not.
    AccountManagementComponent.prototype.isAccountSearchResultAvailable = function () {
        // Check search result.
        var result = this.findAccountsResult;
        if (result == null)
            return false;
        // No account has been found,
        if (result.accounts == null || result.accounts.length < 1)
            return false;
        return true;
    };
    // Called when component has been successfully rendered.
    AccountManagementComponent.prototype.ngOnInit = function () {
        // Components are not busy loading.
        this.isLoading = false;
        // Initiate category search conditions.
        this.conditions = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        this.conditions.pagination = pagination;
        // Initiate account statuses.
        var accountStatuses = new Array();
        for (var index = 0; index < this.clientConfigurationService.accountStatusSelections.keys().length; index++) {
            // Find the key.
            var key = this.clientConfigurationService.accountStatusSelections.keys()[index];
            accountStatuses.push(this.clientConfigurationService.accountStatusSelections.item(key));
        }
        this.conditions.statuses = accountStatuses;
    };
    return AccountManagementComponent;
}());
AccountManagementComponent = __decorate([
    core_1.Component({
        selector: 'account-management',
        templateUrl: 'account-management.component.html',
        providers: [
            ClientConfigurationService_1.ClientConfigurationService,
            ClientNotificationService_1.ClientNotificationService,
            ClientAuthenticationService_1.ClientAuthenticationService,
            ClientAccountService_1.ClientAccountService,
            ClientApiService_1.ClientApiService,
            ClientCommonService_1.ClientCommonService
        ]
    }),
    __metadata("design:paramtypes", [ClientConfigurationService_1.ClientConfigurationService,
        ClientAccountService_1.ClientAccountService,
        ClientCommonService_1.ClientCommonService,
        ClientApiService_1.ClientApiService])
], AccountManagementComponent);
exports.AccountManagementComponent = AccountManagementComponent;
//# sourceMappingURL=account-management.component.js.map
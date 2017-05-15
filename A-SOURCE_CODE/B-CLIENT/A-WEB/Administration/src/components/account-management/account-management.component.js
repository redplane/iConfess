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
var Account_1 = require("../../models/entities/Account");
var SearchAccountsViewModel_1 = require("../../viewmodels/accounts/SearchAccountsViewModel");
var AccountStatuses_1 = require("../../enumerations/AccountStatuses");
var Pagination_1 = require("../../viewmodels/Pagination");
var SearchResult_1 = require("../../models/SearchResult");
var AccountManagementComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with injections.
    function AccountManagementComponent(clientAccountService, clientCommonService, clientApiService, clientTimeService) {
        this.clientAccountService = clientAccountService;
        this.clientCommonService = clientCommonService;
        this.clientApiService = clientApiService;
        this.clientTimeService = clientTimeService;
        // Account status enumeration.
        this.accountStatuses = AccountStatuses_1.AccountStatuses;
        // Initiate search conditions.
        this.conditions = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        // Initiate search result.
        this.searchResult = new SearchResult_1.SearchResult();
        // Initiate account statuses summary.
        this.summaries = new Array();
        // Initiate account instance to prevent it from being null.
        this.account = new Account_1.Account();
    }
    //#endregion
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
        // Update account information which should be edited.
        this.account = account;
        // Display modal.
        this.changeAccountInfoModal.show();
    };
    // Callback which is fired when change account information ok button is clicked.
    AccountManagementComponent.prototype.clickConfirmAccountInfo = function () {
        var _this = this;
        // No account has been selected for edit.
        if (this.account == null) {
            // Close the dialog.
            this.changeAccountInfoModal.hide();
            return;
        }
        // Set components to loading state.
        this.isBusy = true;
        // Send request to service to change account information.
        this.clientAccountService.editUserProfile(this.account.id, this.account)
            .then(function (response) {
            // Cancel loading.
            _this.isBusy = false;
            // Close the dialog.
            _this.changeAccountInfoModal.hide();
            // Reload the page.
            _this.clickSearch();
        })
            .catch(function (response) {
            // Cancel loading process.
            _this.isBusy = false;
            // Close the dialog.
            _this.changeAccountInfoModal.hide();
            // Handle common error response.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when paging item is clicked.
    AccountManagementComponent.prototype.clickPageChange = function (parameters) {
        if (parameters == null || parameters['page'] == null)
            return;
        // Update page index.
        this.conditions.pagination.page = parameters['page'];
        // Search.
        this.clickSearch();
    };
    // Check whether account search result is available or not.
    AccountManagementComponent.prototype.isResultAvailable = function () {
        // Check search result.
        var result = this.searchResult;
        if (result == null)
            return false;
        // No account has been found,
        return !(result.records == null || result.records.length < 1);
    };
    // Called when component has been successfully rendered.
    AccountManagementComponent.prototype.ngOnInit = function () {
        // Components are not busy loading.
        this.isBusy = false;
        // Initiate category search conditions.
        this.conditions = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.page = 1;
        pagination.records = this.clientCommonService.getMaxPageRecords();
        this.conditions.pagination = pagination;
        // Initiate account statuses.
        // let accountStatuses = new Array<AccountStatuses>();
        // for (let index = 0; index < this.clientConfigurationService.accountStatusSelections.keys().length; index++) {
        //     // Find the key.
        //     let key = this.clientConfigurationService.accountStatusSelections.keys()[index];
        //     accountStatuses.push(this.clientConfigurationService.accountStatusSelections.item(key));
        // }
        // this.conditions.statuses = accountStatuses;
        // Load all accounts from service.
        this.clickSearch();
    };
    return AccountManagementComponent;
}());
__decorate([
    core_1.ViewChild("changeAccountInfoModal"),
    __metadata("design:type", ng2_bootstrap_1.ModalDirective)
], AccountManagementComponent.prototype, "changeAccountInfoModal", void 0);
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
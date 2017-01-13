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
var AccountSearchDetailViewModel_1 = require("../viewmodels/accounts/AccountSearchDetailViewModel");
var Account_1 = require("../models/Account");
var AccountStatuses_1 = require("../enumerations/AccountStatuses");
var FindAccountsViewModel_1 = require("../viewmodels/accounts/FindAccountsViewModel");
var Pagination_1 = require("../viewmodels/Pagination");
var ClientConfigurationService_1 = require("../services/ClientConfigurationService");
var AccountManagementComponent = (function () {
    // Initiate component with injections.
    function AccountManagementComponent(clientConfigurationService) {
        this._findAccountConditions = new FindAccountsViewModel_1.FindAccountsViewModel();
        // Services injection.
        this._clientConfigurationService = clientConfigurationService;
    }
    // Called when component has been successfully rendered.
    AccountManagementComponent.prototype.ngOnInit = function () {
        // Initiate forgery results.
        this._findAccountsResult = new AccountSearchDetailViewModel_1.AccountSearchDetailViewModel();
        this._findAccountsResult.accounts = new Array();
        for (var index = 0; index < 10; index++) {
            var account = new Account_1.Account();
            account.id = index;
            account.email = "Email(" + index + ")@yahoo.com.vn";
            account.status = AccountStatuses_1.AccountStatuses.Active;
            account.joined = 0;
            account.lastModified = 0;
            this._findAccountsResult.accounts.push(account);
        }
        this._findAccountsResult.total = 100;
        // Components are not busy loading.
        this._isLoading = false;
        // Initiate category search conditions.
        this._findAccountConditions = new FindAccountsViewModel_1.FindAccountsViewModel();
        var pagination = new Pagination_1.Pagination();
        pagination.index = 1;
        pagination.records = this._clientConfigurationService.findMaxPageRecords();
        this._findAccountConditions.pagination = pagination;
    };
    return AccountManagementComponent;
}());
AccountManagementComponent = __decorate([
    core_1.Component({
        selector: 'account-management',
        templateUrl: './app/views/pages/account-management.component.html',
        providers: [
            ClientConfigurationService_1.ClientConfigurationService
        ]
    }),
    __metadata("design:paramtypes", [ClientConfigurationService_1.ClientConfigurationService])
], AccountManagementComponent);
exports.AccountManagementComponent = AccountManagementComponent;
//# sourceMappingURL=account-management.component.js.map
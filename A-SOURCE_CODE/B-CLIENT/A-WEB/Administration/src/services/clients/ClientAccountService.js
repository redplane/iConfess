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
var ClientApiService_1 = require("../ClientApiService");
require("rxjs/add/operator/toPromise");
var ClientAuthenticationService_1 = require("./ClientAuthenticationService");
/*
 * Service which handles category business.
 * */
var ClientAccountService = (function () {
    // Initiate instance of category service.
    function ClientAccountService(clientApiService, clientAuthenticationService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
    }
    // Find categories by using specific conditions.
    ClientAccountService.prototype.findAccounts = function (findAccountsViewModel) {
        // Page index should be decrease by one.
        var conditions = Object.assign({}, findAccountsViewModel);
        conditions['pagination'] = Object.assign({}, findAccountsViewModel.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiFindAccount, null, conditions).toPromise();
    };
    // Sign an account into system.
    ClientAccountService.prototype.login = function (loginViewModel) {
        return this.clientApiService.post(null, this.clientApiService.apiLogin, null, loginViewModel)
            .toPromise();
    };
    // Change account information in service.
    ClientAccountService.prototype.changeAccountInformation = function (index, information) {
        // Build a complete url of account information change.
        var urlParameters = {
            id: index
        };
        return this.clientApiService.put(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiChangeAccountInfo, urlParameters, information)
            .toPromise();
    };
    // Request service to send an email which is for changing account password.
    ClientAccountService.prototype.requestPasswordChange = function (email) {
        // Parameter construction.
        var urlParameters = {
            email: email
        };
        return this.clientApiService.get(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiRequestChangePassword, urlParameters)
            .toPromise();
    };
    // Request service to change password by using specific token.
    ClientAccountService.prototype.submitPasswordRequest = function (submitPasswordViewModel) {
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiRequestSubmitPassword, null, submitPasswordViewModel)
            .toPromise();
    };
    // Request service to summarize by using specific conditions.
    ClientAccountService.prototype.summarizeAccountStatus = function () {
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiSummaryAccountStatus, null, null)
            .toPromise();
    };
    return ClientAccountService;
}());
ClientAccountService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [ClientApiService_1.ClientApiService,
        ClientAuthenticationService_1.ClientAuthenticationService])
], ClientAccountService);
exports.ClientAccountService = ClientAccountService;
//# sourceMappingURL=ClientAccountService.js.map
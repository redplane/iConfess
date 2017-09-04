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
require("rxjs/add/operator/toPromise");
/*
 * Service which handles category business.
 * */
var ClientAccountService = (function () {
    //#endregion
    //#region Constructor
    // Initiate instance of category service.
    function ClientAccountService(clientApiService, clientAuthenticationService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
        //#region Properties
        // Url which is used for signing user into system.
        this.urlLogin = "api/account/login";
        // Url which is used for searching accounts in the system.
        this.urlSearchAccount = "api/account/find";
        // Url which is used for changing account information.
        this.urlChangeAccountInfo = "api/account";
        // Url which is used for requesting password change.
        this.urlRequestChangePassword = "api/account/forgot-password";
        // Url which is used for submitting password change.
        this.urlSubmitPasswordReset = "api/account/forgot-password";
        // Url which is for getting self profile.
        this.urlGetProfile = "api/account/profile";
    }
    //#endregion
    //#region Methods
    // Find categories by using specific conditions.
    ClientAccountService.prototype.getAccounts = function (conditions) {
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlSearchAccount, null, conditions);
    };
    // Sign an account into system.
    ClientAccountService.prototype.login = function (loginViewModel) {
        return this.clientApiService.post(null, this.clientApiService.getBaseUrl() + "/" + this.urlLogin, null, loginViewModel);
    };
    // Change account information in service.
    ClientAccountService.prototype.editUserProfile = function (index, information) {
        // Build a complete url of account information change.
        var urlParameters = {
            id: index
        };
        return this.clientApiService.put(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlChangeAccountInfo, urlParameters, information);
    };
    // Request service to send an email which is for changing account password.
    ClientAccountService.prototype.sendPasswordChangeRequest = function (email) {
        // Parameter construction.
        var urlParameters = {
            email: email
        };
        return this.clientApiService.get(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlRequestChangePassword, urlParameters);
    };
    // Request service to change password by using specific token.
    ClientAccountService.prototype.submitPasswordReset = function (submitPasswordViewModel) {
        return this.clientApiService
            .post(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlSubmitPasswordReset, null, submitPasswordViewModel);
    };
    // Request service to return account profile.
    ClientAccountService.prototype.getClientProfile = function () {
        return this.clientApiService
            .post(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlGetProfile, null, null);
    };
    return ClientAccountService;
}());
ClientAccountService = __decorate([
    core_1.Injectable(),
    __param(0, core_1.Inject("IClientApiService")),
    __param(1, core_1.Inject("IClientAuthenticationService")),
    __metadata("design:paramtypes", [Object, Object])
], ClientAccountService);
exports.ClientAccountService = ClientAccountService;
//# sourceMappingURL=ClientAccountService.js.map
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
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var ClientApiService_1 = require("../../services/ClientApiService");
var ClientNotificationService_1 = require("../../services/ClientNotificationService");
var ClientAccountService_1 = require("../../services/clients/ClientAccountService");
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var LoginViewModel_1 = require("../../viewmodels/accounts/LoginViewModel");
var AccountLoginComponent = (function () {
    // Initiate component with default settings.
    function AccountLoginComponent(clientApiService, clientAuthenticationService, clientNotificationService, clientAccountService, clientRoutingService, formBuilder) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
        this.clientNotificationService = clientNotificationService;
        this.clientAccountService = clientAccountService;
        this.clientRoutingService = clientRoutingService;
        this.formBuilder = formBuilder;
        this.loginBox = this.formBuilder.group({
            email: [''],
            password: ['']
        });
        this.loginViewModel = new LoginViewModel_1.LoginViewModel();
    }
    // Callback is fired when login button is clicked.
    AccountLoginComponent.prototype.clickLogin = function () {
        var _this = this;
        // Call service api to authenticate do authentication.
        this.clientAccountService.login(this.loginViewModel)
            .then(function (response) {
            // Convert response from service to ClientAuthenticationToken data type.
            var clientAuthenticationDetail = response.json();
            // Save the client authentication information.
            _this.clientAuthenticationService.initiateLocalAuthenticationToken(clientAuthenticationDetail);
            // Redirect user to account management page.
            _this.clientRoutingService.navigate(['/account-management']);
            // Cancel loading process.
            _this.isLoading = false;
        })
            .catch(function (response) {
            // Unfreeze the UI.
            _this.isLoading = false;
            // Proceed the common logic handling.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
        });
    };
    return AccountLoginComponent;
}());
AccountLoginComponent = __decorate([
    core_1.Component({
        selector: 'account-login',
        templateUrl: 'account-login.component.html',
        providers: [
            ClientApiService_1.ClientApiService,
            ClientNotificationService_1.ClientNotificationService,
            ClientAccountService_1.ClientAccountService,
            ClientAuthenticationService_1.ClientAuthenticationService
        ]
    }),
    __metadata("design:paramtypes", [ClientApiService_1.ClientApiService,
        ClientAuthenticationService_1.ClientAuthenticationService,
        ClientNotificationService_1.ClientNotificationService,
        ClientAccountService_1.ClientAccountService,
        router_1.Router,
        forms_1.FormBuilder])
], AccountLoginComponent);
exports.AccountLoginComponent = AccountLoginComponent;
//# sourceMappingURL=account-login.component.js.map
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
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var LoginViewModel_1 = require("../../viewmodels/accounts/LoginViewModel");
var AccountLoginComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with default settings.
    function AccountLoginComponent(clientApiService, clientAuthenticationService, clientAccountService, clientRoutingService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
        this.clientAccountService = clientAccountService;
        this.clientRoutingService = clientRoutingService;
        this.loginViewModel = new LoginViewModel_1.LoginViewModel();
    }
    //#endregion
    //#region Methods
    // Callback is fired when login button is clicked.
    AccountLoginComponent.prototype.clickLogin = function (event) {
        var _this = this;
        // Prevent default behaviour.
        event.preventDefault();
        // Make component be loaded.
        this.isBusy = true;
        // Call service api to authenticate do authentication.
        this.clientAccountService.login(this.loginViewModel)
            .then(function (x) {
            // Convert response from service to AuthenticationToken data type.
            var clientAuthenticationDetail = x.json();
            // Save the client authentication information.
            _this.clientAuthenticationService.setToken(clientAuthenticationDetail);
            // Redirect user to account management page.
            _this.clientRoutingService.navigate(['/account-management']);
            // Cancel loading process.
            _this.isBusy = false;
        })
            .catch(function (response) {
            // Unfreeze the UI.
            _this.isBusy = false;
            // Proceed the common logic handling.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    return AccountLoginComponent;
}());
__decorate([
    core_1.ViewChild("loginPanel"),
    __metadata("design:type", forms_1.NgForm)
], AccountLoginComponent.prototype, "loginPanel", void 0);
AccountLoginComponent = __decorate([
    core_1.Component({
        selector: 'account-login',
        templateUrl: 'account-login.component.html'
    }),
    __param(0, core_1.Inject("IClientApiService")),
    __param(1, core_1.Inject("IClientAuthenticationService")),
    __param(2, core_1.Inject("IClientAccountService")),
    __metadata("design:paramtypes", [Object, Object, Object, router_1.Router])
], AccountLoginComponent);
exports.AccountLoginComponent = AccountLoginComponent;
//# sourceMappingURL=account-login.component.js.map
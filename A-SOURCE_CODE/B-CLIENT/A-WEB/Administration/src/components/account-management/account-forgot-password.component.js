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
var ClientDataConstraintService_1 = require("../../services/ClientDataConstraintService");
var forms_1 = require("@angular/forms");
var Account_1 = require("../../models/Account");
var ClientAccountService_1 = require("../../services/clients/ClientAccountService");
var ClientApiService_1 = require("../../services/ClientApiService");
var ClientNotificationService_1 = require("../../services/ClientNotificationService");
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var router_1 = require("@angular/router");
var AccountForgotPasswordComponent = (function () {
    // Initiate component with default settings.
    function AccountForgotPasswordComponent(formBuilder, clientDataConstraintService, clientAccountService, clientNotificationService, clientApiService, clientRoutingService) {
        this.formBuilder = formBuilder;
        this.clientDataConstraintService = clientDataConstraintService;
        this.clientAccountService = clientAccountService;
        this.clientNotificationService = clientNotificationService;
        this.clientApiService = clientApiService;
        this.clientRoutingService = clientRoutingService;
        // Initiate form.
        this.accountPasswordChangeBox = this.formBuilder.group({
            email: [''],
            token: [''],
            password: [''],
            passwordConfirmation: ['']
        });
        // Initiate account instance.
        this.account = new Account_1.Account();
        this.account.email = 'redplane_dt@yahoo.com.vn';
        // Set loading to be false.
        this.isLoading = false;
    }
    // Callback which is fired when seach button is clicked for requesting a password reset.
    AccountForgotPasswordComponent.prototype.clickRequestPassword = function () {
        var _this = this;
        // Set component to loading state.
        this.isLoading = true;
        // Call api to request password change.
        this.clientAccountService.requestPasswordChange(this.account.email)
            .then(function (response) {
            // Tell client that password request has been submitted.
            _this.clientNotificationService.success('CHANGE_PASSWORD_REQUEST_SUBMITTED');
            // Redirect user to submit password page.
            _this.clientRoutingService.navigate(['/submit-password']);
            // Cancel component loading state.
            _this.isLoading = false;
        })
            .catch(function (response) {
            // Proceed common response analyzation.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
            // Cancel component loading state.
            _this.isLoading = false;
        });
        ;
    };
    return AccountForgotPasswordComponent;
}());
AccountForgotPasswordComponent = __decorate([
    core_1.Component({
        selector: 'account-forgot-password-box',
        templateUrl: 'account-forgot-password.component.html',
        inputs: ['isLoading'],
        providers: [
            ClientDataConstraintService_1.ClientDataConstraintService,
            ClientAccountService_1.ClientAccountService,
            ClientApiService_1.ClientApiService,
            ClientNotificationService_1.ClientNotificationService,
            ClientAuthenticationService_1.ClientAuthenticationService
        ]
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        ClientDataConstraintService_1.ClientDataConstraintService,
        ClientAccountService_1.ClientAccountService,
        ClientNotificationService_1.ClientNotificationService,
        ClientApiService_1.ClientApiService,
        router_1.Router])
], AccountForgotPasswordComponent);
exports.AccountForgotPasswordComponent = AccountForgotPasswordComponent;
//# sourceMappingURL=account-forgot-password.component.js.map
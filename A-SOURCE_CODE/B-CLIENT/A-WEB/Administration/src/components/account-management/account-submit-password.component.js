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
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var ClientAccountService_1 = require("../../services/clients/ClientAccountService");
var ClientApiService_1 = require("../../services/ClientApiService");
var ClientNotificationService_1 = require("../../services/ClientNotificationService");
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var ClientDataConstraintService_1 = require("../../services/ClientDataConstraintService");
var SubmitPasswordViewModel_1 = require("../../viewmodels/accounts/SubmitPasswordViewModel");
var AccountSubmitPasswordComponent = (function () {
    function AccountSubmitPasswordComponent(clientAccountService, clientNotificationService, clientDataConstraintService, clientApiService, clientRoutingService, formBuilder) {
        this.clientAccountService = clientAccountService;
        this.clientNotificationService = clientNotificationService;
        this.clientDataConstraintService = clientDataConstraintService;
        this.clientApiService = clientApiService;
        this.clientRoutingService = clientRoutingService;
        this.formBuilder = formBuilder;
        // Initiate account password submission box.
        this.accountPasswordSubmitBox = this.formBuilder.group({
            email: [''],
            token: [''],
            password: [''],
            passwordConfirmation: ['']
        });
        this.accountPasswordSubmitModel = new SubmitPasswordViewModel_1.SubmitPasswordViewModel();
    }
    // This callback is fired when submit button is clicked.
    AccountSubmitPasswordComponent.prototype.clickSubmitPassword = function () {
        var _this = this;
        // Call service to change password.
        this.clientAccountService.submitPasswordRequest(this.accountPasswordSubmitModel)
            .then(function (response) {
            // Tell user password has been changed successfully.
            _this.clientNotificationService.success('SUBMIT_PASSWORD_SUCCESSFULLY');
            // Redirect user to login page.
            _this.clientRoutingService.navigate(['/']);
        })
            .catch(function (response) {
            // Proceed common response.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
        });
    };
    return AccountSubmitPasswordComponent;
}());
AccountSubmitPasswordComponent = __decorate([
    core_1.Component({
        selector: 'account-submit-password',
        templateUrl: 'account-submit-password.component.html',
        providers: [
            ClientAccountService_1.ClientAccountService,
            ClientApiService_1.ClientApiService,
            ClientNotificationService_1.ClientNotificationService,
            ClientAuthenticationService_1.ClientAuthenticationService,
            ClientDataConstraintService_1.ClientDataConstraintService
        ]
    }),
    __metadata("design:paramtypes", [ClientAccountService_1.ClientAccountService,
        ClientNotificationService_1.ClientNotificationService,
        ClientDataConstraintService_1.ClientDataConstraintService,
        ClientApiService_1.ClientApiService,
        router_1.Router,
        forms_1.FormBuilder])
], AccountSubmitPasswordComponent);
exports.AccountSubmitPasswordComponent = AccountSubmitPasswordComponent;
//# sourceMappingURL=account-submit-password.component.js.map
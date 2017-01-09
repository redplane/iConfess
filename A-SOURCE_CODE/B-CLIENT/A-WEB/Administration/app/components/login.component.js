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
var forms_1 = require("@angular/forms");
var ClientAuthenticationService_1 = require("../services/clients/ClientAuthenticationService");
var LoginViewModel_1 = require("../viewmodels/accounts/LoginViewModel");
var ClientApiService_1 = require("../services/ClientApiService");
var ClientAccountService_1 = require("../services/clients/ClientAccountService");
var http_1 = require("@angular/http");
var ClientValidationService_1 = require("../services/ClientValidationService");
var LoginComponent = (function () {
    // Initiate login box component with IoC.
    function LoginComponent(formBuilder, clientApiService, clientValidationService, clientAuthenticationService, clientAccountService) {
        // Initiate login view model.
        this._loginViewModel = new LoginViewModel_1.LoginViewModel();
        // Initiate login box and its components.
        this.loginBox = formBuilder.group({
            email: ['', forms_1.Validators.compose([forms_1.Validators.required])],
            password: ['', forms_1.Validators.compose([forms_1.Validators.required])]
        });
        // Client api service injection.
        this._clientApiService = clientApiService;
        // Client validation service injection.
        this._clientValidationService = clientValidationService;
        // Client authentication service injection.
        this._clientAuthenticationService = clientAuthenticationService;
        // Client account service injection.
        this._clientAccountService = clientAccountService;
    }
    // This callback is fired when login button is clicked.
    LoginComponent.prototype.login = function (event) {
        var _this = this;
        // Pass the login view model to service.
        var result = this._clientAccountService.login(this._loginViewModel)
            .then(function (response) {
        })
            .catch(function (response) {
            if (!(response instanceof http_1.Response))
                return;
            // Find the response object.
            var information = response.json();
            switch (response.status) {
                case 400:
                    var model = {};
                    _this._clientValidationService.findFrontendValidationModel(_this._clientValidationService.validationDictionary, model, information);
                    console.log(model);
                    break;
            }
        });
        console.log(result);
    };
    return LoginComponent;
}());
LoginComponent = __decorate([
    core_1.Component({
        selector: 'login',
        templateUrl: './app/views/pages/login.component.html',
        providers: [
            ClientValidationService_1.ClientValidationService,
            ClientApiService_1.ClientApiService,
            ClientAccountService_1.ClientAccountService,
            ClientAuthenticationService_1.ClientAuthenticationService
        ]
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        ClientApiService_1.ClientApiService,
        ClientValidationService_1.ClientValidationService,
        ClientAuthenticationService_1.ClientAuthenticationService, ClientAccountService_1.ClientAccountService])
], LoginComponent);
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map
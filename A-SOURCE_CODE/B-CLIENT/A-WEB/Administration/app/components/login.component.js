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
var router_1 = require("@angular/router");
var LoginComponent = (function () {
    // Initiate login box component with IoC.
    function LoginComponent(formBuilder, clientApiService, clientValidationService, clientAuthenticationService, clientAccountService, clientRoutingService) {
        // Initiate login view model.
        this._loginViewModel = new LoginViewModel_1.LoginViewModel();
        // Initiate login box and its components.
        this.loginBox = formBuilder.group({
            email: [''],
            password: ['']
        });
        // Client api service injection.
        this._clientApiService = clientApiService;
        // Client validation service injection.
        this._clientValidationService = clientValidationService;
        // Client authentication service injection.
        this._clientAuthenticationService = clientAuthenticationService;
        // Client account service injection.
        this._clientAccountService = clientAccountService;
        // Service which is for routing.
        this._clientRoutingService = clientRoutingService;
    }
    // This callback is fired when login button is clicked.
    LoginComponent.prototype.login = function (event) {
        var _this = this;
        // Make the component show the loading process.
        this._isLoading = true;
        // Pass the login view model to service.
        this._clientAccountService.login(this._loginViewModel)
            .then(function (response) {
            // Convert response from service to ClientAuthenticationToken data type.
            var clientAuthenticationDetail = response.json();
            // Save the client authentication information.
            _this._clientAuthenticationService.saveAuthenticationToken(clientAuthenticationDetail);
            // Redirect user to account management page.
            _this._clientRoutingService.navigate(['/account-management']);
            // Cancel loading process.
            _this._isLoading = false;
        })
            .catch(function (response) {
            if (!(response instanceof http_1.Response))
                return;
            // Find the response object.
            var information = response.json();
            switch (response.status) {
                // Bad request, usually submited parameters are invalid.
                case 400:
                    // Refined the information.
                    information = _this._clientValidationService.findPropertiesValidationMessages(information);
                    // Parse the response and update to controls of form.
                    _this._clientValidationService.findFrontendValidationModel(_this._clientValidationService.validationDictionary, _this.loginBox, information);
                    break;
                case 404:
                    console.log(response);
                    // TODO: Display message.
                    break;
                case 500:
                    console.log(response);
                    // TODO: Display message.
                    break;
            }
            // Cancel loading process.
            _this._isLoading = false;
        });
    };
    // Called when component has been rendered successfully.
    LoginComponent.prototype.ngOnInit = function () {
        // By default, component loads nothing.
        this._isLoading = false;
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
        ClientAuthenticationService_1.ClientAuthenticationService,
        ClientAccountService_1.ClientAccountService,
        router_1.Router])
], LoginComponent);
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map
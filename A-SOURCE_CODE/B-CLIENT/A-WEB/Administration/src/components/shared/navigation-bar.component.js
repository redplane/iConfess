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
var NavigationBarComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate instance with IoC.
    function NavigationBarComponent(clientAuthenticationService, clientAccountService, clientRoutingService) {
        this.clientAuthenticationService = clientAuthenticationService;
        this.clientAccountService = clientAccountService;
        this.clientRoutingService = clientRoutingService;
    }
    //#endregion
    //#region Methods
    // Sign the user out.
    NavigationBarComponent.prototype.clickSignOut = function () {
        // Clear the authentication service.
        this.clientAuthenticationService.clearToken();
        // Re-direct to login page.
        this.clientRoutingService.navigate(['/']);
    };
    // This callback is fired when this component has been initialized.
    NavigationBarComponent.prototype.ngOnInit = function () {
        // Find account information.
        this.clientAccountService.getClientProfile()
            .then(function (x) {
            console.log(x);
        })
            .catch(function (x) {
            console.log(x);
        });
    };
    return NavigationBarComponent;
}());
NavigationBarComponent = __decorate([
    core_1.Component({
        selector: 'navigation-bar',
        templateUrl: 'navigation-bar.component.html'
    }),
    __param(0, core_1.Inject("IClientAuthenticationService")),
    __param(1, core_1.Inject("IClientAccountService")),
    __metadata("design:paramtypes", [Object, Object, router_1.Router])
], NavigationBarComponent);
exports.NavigationBarComponent = NavigationBarComponent;
//# sourceMappingURL=navigation-bar.component.js.map
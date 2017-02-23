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
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var NavigationBarComponent = (function () {
    // Initiate instance with IoC.
    function NavigationBarComponent(clientAuthenticationService, clientRoutingService) {
        this.clientAuthenticationService = clientAuthenticationService;
        this.clientRoutingService = clientRoutingService;
    }
    // Sign the user out.
    NavigationBarComponent.prototype.clickSignOut = function () {
        // Clear the authentication service.
        this.clientAuthenticationService.clearAuthenticationToken();
        // Re-direct to login page.
        this.clientRoutingService.navigate(['/']);
    };
    return NavigationBarComponent;
}());
NavigationBarComponent = __decorate([
    core_1.Component({
        selector: 'navigation-bar',
        templateUrl: 'navigation-bar.component.html',
        providers: [
            ClientAuthenticationService_1.ClientAuthenticationService
        ]
    }),
    __metadata("design:paramtypes", [ClientAuthenticationService_1.ClientAuthenticationService,
        router_1.Router])
], NavigationBarComponent);
exports.NavigationBarComponent = NavigationBarComponent;
//# sourceMappingURL=navigation-bar.component.js.map
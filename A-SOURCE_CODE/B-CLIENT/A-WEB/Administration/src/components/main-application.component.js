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
var Dictionary_1 = require("../viewmodels/Dictionary");
var MainApplicationComponent = (function () {
    // Initiate component with IoC.
    function MainApplicationComponent(clientRoutingService) {
        this.clientRoutingService = clientRoutingService;
        this.unauthenticatedUrls = new Dictionary_1.Dictionary();
    }
    // Find location of the current page.
    MainApplicationComponent.prototype.getLocation = function () {
        return this.clientRoutingService.url;
    };
    // Check whether navigation bar should be displayed or not.
    MainApplicationComponent.prototype.shouldNavigationBarsBeAvailable = function () {
        // Find the current page location.
        var location = this.getLocation();
        // If location is the prohibited list, navigation bars should not be displayed.
        if (this.unauthenticatedUrls.containsKey(location))
            return false;
        return true;
    };
    // Callback which is fired when component has been loaded successfully.
    MainApplicationComponent.prototype.ngOnInit = function () {
        this.unauthenticatedUrls.add("/", true);
        this.unauthenticatedUrls.add("/forgot-password", true);
        this.unauthenticatedUrls.add("/submit-password", true);
    };
    return MainApplicationComponent;
}());
MainApplicationComponent = __decorate([
    core_1.Component({
        selector: 'main-application',
        templateUrl: 'main-application.html'
    }),
    __metadata("design:paramtypes", [router_1.Router])
], MainApplicationComponent);
exports.MainApplicationComponent = MainApplicationComponent;
//# sourceMappingURL=main-application.component.js.map
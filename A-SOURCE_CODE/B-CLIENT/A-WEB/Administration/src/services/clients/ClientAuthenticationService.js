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
/*
* Implement authentication business handler
* */
var ClientAuthenticationService = (function () {
    // Initiate service with IoC.
    function ClientAuthenticationService() {
        this._authenticationKey = "iConfessAuthenticationToken";
    }
    // The the name of key which is used for sotring authentication information.
    ClientAuthenticationService.prototype.findAuthenticationStorageKey = function () {
        return this._authenticationKey;
    };
    // When should the token be expired.
    ClientAuthenticationService.prototype.findAuthenticationExpire = function () {
        return this._authenticationExpire;
    };
    // Find client authentication token from local storage.
    ClientAuthenticationService.prototype.findClientAuthenticationToken = function () {
        // Find information from local storage with given key.
        var clientAuthenticationToken = localStorage.getItem(this._authenticationKey);
        // Parse the information into authentication class.
        return clientAuthenticationToken;
    };
    // Save authentication information into local storage.
    ClientAuthenticationService.prototype.initiateLocalAuthenticationToken = function (clientAuthenticationToken) {
        // Save the authentication information into local storage
        localStorage.setItem(this._authenticationKey, clientAuthenticationToken.token);
    };
    // Clear authentication token from local storage.
    ClientAuthenticationService.prototype.clearAuthenticationToken = function () {
        localStorage.removeItem(this._authenticationKey);
    };
    return ClientAuthenticationService;
}());
ClientAuthenticationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ClientAuthenticationService);
exports.ClientAuthenticationService = ClientAuthenticationService;
//# sourceMappingURL=ClientAuthenticationService.js.map
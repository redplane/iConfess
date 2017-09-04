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
    //#endregion
    //#region Constructor
    // Initiate service with IoC.
    function ClientAuthenticationService() {
        this.tokenStorageKey = "iConfess.Administration";
    }
    //#endregion
    //#region Methods
    // The the name of key which is used for sotring authentication information.
    ClientAuthenticationService.prototype.getTokenKey = function () {
        return this.tokenStorageKey;
    };
    // Find client authentication token from local storage.
    ClientAuthenticationService.prototype.getTokenCode = function () {
        // Find information from local storage with given key.
        var authenticationToken = this.getToken();
        // Token is not valid.
        if (authenticationToken == null) {
            // Clear token from local storage.
            this.clearToken();
            return "";
        }
        // Parse the information into authentication class.
        return authenticationToken.token;
    };
    // Save authentication information into local storage.
    ClientAuthenticationService.prototype.setToken = function (authenticationToken) {
        // Save the authentication information into local storage
        localStorage.setItem(this.tokenStorageKey, JSON.stringify(authenticationToken));
    };
    // Clear authentication token from local storage.
    ClientAuthenticationService.prototype.clearToken = function () {
        localStorage.removeItem(this.tokenStorageKey);
    };
    // Get token which is stored inside local storage.
    ClientAuthenticationService.prototype.getToken = function () {
        // Get token which is stored inside local storage.
        var item = localStorage.getItem(this.tokenStorageKey);
        // Item is not valid.
        if (item == null || item.length < 1)
            return null;
        // Cast item to authentication token.
        var authToken = JSON.parse(item);
        // Authentication is not valid.
        if (authToken == null || (authToken.expireAt > Date.now()))
            return null;
        // Token is empty.
        var code = authToken.token;
        if (code == null || code.length < 1)
            return null;
        return authToken;
    };
    return ClientAuthenticationService;
}());
ClientAuthenticationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ClientAuthenticationService);
exports.ClientAuthenticationService = ClientAuthenticationService;
//# sourceMappingURL=ClientAuthenticationService.js.map
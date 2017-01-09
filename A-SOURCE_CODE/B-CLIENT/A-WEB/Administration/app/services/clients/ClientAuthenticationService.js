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
var ClientAuthenticationToken_1 = require("../../models/ClientAuthenticationToken");
var core_1 = require("@angular/core");
var ClientApiService_1 = require("../ClientApiService");
var http_1 = require("@angular/http");
/*
* Implement authentication business handler
* */
var ClientAuthenticationService = (function () {
    // Initiate service with IoC.
    function ClientAuthenticationService(clientApiService, httpClient) {
        this._authenticationKey = "authentication-iConfess";
        // client api service injection.
        this._clientApiService = clientApiService;
    }
    // The the name of key which is used for sotring authentication information.
    ClientAuthenticationService.prototype.findAuthenticationStorageKey = function () {
        return this._authenticationKey;
    };
    // Find client authentication token from local storage.
    ClientAuthenticationService.prototype.findClientAuthenticationToken = function () {
        // Find information from local storage with given key.
        var clientAuthenticationInfo = localStorage.getItem(this._authenticationKey);
        // No information is stored in localStorage.
        if (clientAuthenticationInfo == null || clientAuthenticationInfo.length < 1)
            return null;
        // Parse the information into authentication class.
        var clientAuthenticationToken = new ClientAuthenticationToken_1.ClientAuthenticationToken();
        clientAuthenticationToken = JSON.parse(clientAuthenticationInfo);
        return clientAuthenticationToken;
    };
    // Check whether client authentication information is valid to login or not.
    ClientAuthenticationService.prototype.isAuthenticationSolid = function (clientAuthenticationToken) {
        // Token is empty.
        if (clientAuthenticationToken.token == null || clientAuthenticationToken.token.length < 1)
            return false;
        // Token type is invalid.
        if (clientAuthenticationToken.type == null || clientAuthenticationToken.type.length < 1)
            return false;
        // Token expiration is invalid.
        if (clientAuthenticationToken.expire == null)
            return false;
        // Token has been expired.
        if (clientAuthenticationToken.expire < Date.now())
            return false;
        return true;
    };
    // Save authentication information into local storage.
    ClientAuthenticationService.prototype.saveAuthenticationToken = function (clientAuthenticationToken) {
        // Serialize token into string.
        var authenticationInfo = JSON.stringify(clientAuthenticationToken);
        // Save the authentication information into local storage
        localStorage.setItem(this._authenticationKey, authenticationInfo);
    };
    return ClientAuthenticationService;
}());
ClientAuthenticationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [ClientApiService_1.ClientApiService, http_1.Http])
], ClientAuthenticationService);
exports.ClientAuthenticationService = ClientAuthenticationService;
//# sourceMappingURL=ClientAuthenticationService.js.map
"use strict";
var ClientAuthenticationToken_1 = require("../../models/ClientAuthenticationToken");
// Implement authentication business handler.
var ClientAuthenticationService = (function () {
    // Initiate service with IoC.
    function ClientAuthenticationService() {
        this._authenticationKey = "authentication-iConfess";
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
exports.ClientAuthenticationService = ClientAuthenticationService;
//# sourceMappingURL=ClientAuthenticationService.js.map
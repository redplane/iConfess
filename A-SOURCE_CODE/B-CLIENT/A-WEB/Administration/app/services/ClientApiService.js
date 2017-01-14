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
var http_1 = require("@angular/http");
/*
* Service which handles hyperlink of api.
* */
var ClientApiService = (function () {
    // Initiate service with settings.
    function ClientApiService(clientRequestService) {
        // Api which web application will consume the service.
        this.apiUrl = "http://confession.azurewebsites.net";
        // Find category api url.
        this.apiFindCategory = this.apiUrl + "/api/category/find";
        this.apiDeleteCategory = this.apiUrl + "/api/category";
        this.apiChangeCategoryDetail = this.apiUrl + "/api/category";
        this.apiInitiateCategory = this.apiUrl + "/api/category";
        // Find category account api url.
        this.apiFindAccount = this.apiUrl + "/api/account/find";
        // Initiate api which is used for logging into system.
        this.apiLogin = this.apiUrl + "/api/account/login";
        // Key of local storage in which access token should be stored.
        this.accessTokenStorage = 'iConfess';
        // Services injection.
        this._clientRequestService = clientRequestService;
    }
    // Send 'GET' to service.
    ClientApiService.prototype.get = function (clientAuthenticationToken, url, parameters) {
        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = url + "?" + this.encryptUrlParameters(parameters);
        // Initiate headers.
        var clientRequestHeaders = new http_1.Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        clientRequestHeaders.append('Authorization', "Bearer " + clientAuthenticationToken.token);
        // Initiate request options.
        var clientRequestOptions = new http_1.RequestOptions({
            headers: clientRequestHeaders
        });
        // Request to api to obtain list of available categories in system.
        return this._clientRequestService.get(url, clientRequestOptions);
    };
    // Send 'POST' to service.
    ClientApiService.prototype.post = function (clientAuthenticationToken, url, parameters, body) {
        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = url + "?" + this.encryptUrlParameters(parameters);
        // Initiate headers.
        var clientRequestHeaders = new http_1.Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        clientRequestHeaders.append('Authorization', "Bearer " + clientAuthenticationToken.token);
        // Initiate request options.
        var clientRequestOptions = new http_1.RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });
        // Request to api to obtain list of available categories in system.
        return this._clientRequestService.post(url, null, clientRequestOptions);
    };
    // Send 'PUT' to service.
    ClientApiService.prototype.put = function (clientAuthenticationToken, url, parameters, body) {
        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = url + "?" + this.encryptUrlParameters(parameters);
        // Initiate headers.
        var clientRequestHeaders = new http_1.Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        clientRequestHeaders.append('Authorization', "Bearer " + clientAuthenticationToken.token);
        // Initiate request options.
        var clientRequestOptions = new http_1.RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });
        // Request to api to obtain list of available categories in system.
        return this._clientRequestService.put(url, null, clientRequestOptions);
    };
    // Send 'PUT' to service.
    ClientApiService.prototype.delete = function (clientAuthenticationToken, url, parameters, body) {
        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = url + "?" + this.encryptUrlParameters(parameters);
        // Initiate headers.
        var clientRequestHeaders = new http_1.Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        clientRequestHeaders.append('Authorization', "Bearer " + clientAuthenticationToken.token);
        // Initiate request options.
        var clientRequestOptions = new http_1.RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });
        // Request to api to obtain list of available categories in system.
        return this._clientRequestService.delete(url, clientRequestOptions);
    };
    // Encrypt url parameters to prevent dangerous parameters are passed to service.
    ClientApiService.prototype.encryptUrlParameters = function (parameters) {
        return Object.keys(parameters).map(function (key) {
            return [key, parameters[key]].map(encodeURIComponent).join("=");
        }).join("&");
    };
    return ClientApiService;
}());
ClientApiService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], ClientApiService);
exports.ClientApiService = ClientApiService;
//# sourceMappingURL=ClientApiService.js.map
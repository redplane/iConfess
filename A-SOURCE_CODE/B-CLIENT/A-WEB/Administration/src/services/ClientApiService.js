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
var http_1 = require("@angular/http");
var router_1 = require("@angular/router");
/*
* Service which handles hyperlink of api.
* */
var ClientApiService = (function () {
    //#endregion
    //#region Constructor
    // Initiate service with settings.
    function ClientApiService(clientRequestService, clientToastrService, clientAuthenticationService, clientRoutingService) {
        this.clientRequestService = clientRequestService;
        this.clientToastrService = clientToastrService;
        this.clientAuthenticationService = clientAuthenticationService;
        this.clientRoutingService = clientRoutingService;
        //#region Properties
        // Api which web application will consume the service.
        // private apiUrl = "http://confession.azurewebsites.net";
        this.apiUrl = "http://localhost:2101";
        // Key of local storage in which access token should be stored.
        this.accessTokenStorage = 'iConfess';
    }
    //#endregion
    //#region Methods
    // Get base url.
    ClientApiService.prototype.getBaseUrl = function () {
        return this.apiUrl;
    };
    // Send 'GET' to service.
    ClientApiService.prototype.get = function (clientAuthenticationToken, url, parameters) {
        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = url + "?" + this.encryptUrlParameters(parameters);
        // Initiate headers.
        var clientRequestHeaders = new http_1.Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        if (clientAuthenticationToken != null && clientAuthenticationToken.length > 0)
            clientRequestHeaders.append('Authorization', "Bearer " + clientAuthenticationToken);
        // Initiate request options.
        var clientRequestOptions = new http_1.RequestOptions({
            headers: clientRequestHeaders
        });
        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.get(url, clientRequestOptions)
            .toPromise();
    };
    // Send 'POST' to service.
    ClientApiService.prototype.post = function (clientAuthenticationToken, url, parameters, body) {
        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = url + "?" + this.encryptUrlParameters(parameters);
        // Initiate headers.
        var clientRequestHeaders = new http_1.Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        if (clientAuthenticationToken != null && clientAuthenticationToken.length > 0)
            clientRequestHeaders.append('Authorization', "Bearer " + clientAuthenticationToken);
        // Initiate request options.
        var clientRequestOptions = new http_1.RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });
        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.post(url, null, clientRequestOptions)
            .toPromise();
    };
    // Send 'PUT' to service.
    ClientApiService.prototype.put = function (clientAuthenticationToken, url, parameters, body) {
        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = url + "?" + this.encryptUrlParameters(parameters);
        // Initiate headers.
        var clientRequestHeaders = new http_1.Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        if (clientAuthenticationToken != null && clientAuthenticationToken.length > 0)
            clientRequestHeaders.append('Authorization', "Bearer " + clientAuthenticationToken);
        // Initiate request options.
        var clientRequestOptions = new http_1.RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });
        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.put(url, null, clientRequestOptions)
            .toPromise();
    };
    // Send 'PUT' to service.
    ClientApiService.prototype.delete = function (clientAuthenticationToken, url, parameters, body) {
        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = url + "?" + this.encryptUrlParameters(parameters);
        // Initiate headers.
        var clientRequestHeaders = new http_1.Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        if (clientAuthenticationToken != null)
            clientRequestHeaders.append('Authorization', "Bearer " + clientAuthenticationToken);
        // Initiate request options.
        var clientRequestOptions = new http_1.RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });
        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.delete(url, clientRequestOptions)
            .toPromise();
    };
    // Common function to proceed invalid response.
    ClientApiService.prototype.handleInvalidResponse = function (response) {
        // Find response information of request.
        var information = response.json();
        // Base on the status code to determine what action should be taken.
        switch (response.status) {
            // This status is about invalid parameters have been submitted to service.
            case 400:
                // TODO: Form control should be passed here to update screen display.
                break;
            // This status is about invalid authentication information has been passed to service.
            case 401:
                // Clear the local storage.
                this.clientAuthenticationService.clearToken();
                // Display the error message.
                this.clientToastrService.error(information['message'], 'System', null);
                // Redirect user back to login page.
                this.clientRoutingService.navigate(['/']);
                break;
            // This status is about request doesn't have enough permission to access service function.
            case 403:
                // Display the error message.
                this.clientToastrService.error(information['message'], 'System', null);
                break;
            // Something went wrong with the service.
            case 500:
                // Display the error message.
                this.clientToastrService.error('Service malfunctioned. Please try again', 'System', null);
                break;
            // For default error. Just display messages sent back from service.
            default:
                this.clientToastrService.error(information['message'], 'System', null);
                break;
        }
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
    __param(1, core_1.Inject("IClientToastrService")),
    __param(2, core_1.Inject("IClientAuthenticationService")),
    __metadata("design:paramtypes", [http_1.Http, Object, Object, router_1.Router])
], ClientApiService);
exports.ClientApiService = ClientApiService;
//# sourceMappingURL=ClientApiService.js.map
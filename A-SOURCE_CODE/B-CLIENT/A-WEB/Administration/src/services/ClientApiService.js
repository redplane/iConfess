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
var http_1 = require("@angular/http");
var ClientNotificationService_1 = require("./ClientNotificationService");
var ClientAuthenticationService_1 = require("./clients/ClientAuthenticationService");
var router_1 = require("@angular/router");
/*
* Service which handles hyperlink of api.
* */
var ClientApiService = (function () {
    // Initiate service with settings.
    function ClientApiService(clientRequestService, clientNotificationService, clientAuthenticationService, clientRoutingService) {
        this.clientRequestService = clientRequestService;
        this.clientNotificationService = clientNotificationService;
        this.clientAuthenticationService = clientAuthenticationService;
        this.clientRoutingService = clientRoutingService;
        // Api which web application will consume the service.
        this.apiUrl = "http://confession.azurewebsites.net";
        // Find category api url.
        this.apiFindCategory = this.apiUrl + "/api/category/find";
        this.apiDeleteCategory = this.apiUrl + "/api/category";
        this.apiChangeCategoryDetail = this.apiUrl + "/api/category";
        this.apiInitiateCategory = this.apiUrl + "/api/category";
        // Account api.
        this.apiFindAccount = this.apiUrl + "/api/account/find";
        this.apiLogin = this.apiUrl + "/api/account/login";
        this.apiChangeAccountInfo = this.apiUrl + "/api/account";
        this.apiRequestChangePassword = this.apiUrl + "/api/account/lost_password";
        this.apiRequestSubmitPassword = this.apiUrl + "/api/account/lost_password";
        // Post api.
        this.apiFindPost = this.apiUrl + "/api/post/find";
        this.apiFindPostDetails = this.apiUrl + "/api/post/details";
        // Comment api.
        this.apiSearchComment = this.apiUrl + "/api/comment/find";
        this.apiSearchCommentDetails = this.apiUrl + "/api/comment/details";
        // Post report api.
        this.apiFindPostReport = this.apiUrl + "/api/report/post/find";
        this.apiDeletePostReport = this.apiUrl + "/api/report/post";
        // Key of local storage in which access token should be stored.
        this.accessTokenStorage = 'iConfess';
    }
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
        return this.clientRequestService.get(url, clientRequestOptions);
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
        return this.clientRequestService.post(url, null, clientRequestOptions);
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
        return this.clientRequestService.put(url, null, clientRequestOptions);
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
        return this.clientRequestService.delete(url, clientRequestOptions);
    };
    // Common function to proceed invalid response.
    ClientApiService.prototype.proceedHttpNonSolidResponse = function (response) {
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
                this.clientAuthenticationService.clearAuthenticationToken();
                // Display the error message.
                this.clientNotificationService.error(information['message'], 'System');
                // Redirect user back to login page.
                this.clientRoutingService.navigate(['/']);
                break;
            // This status is about request doesn't have enough permission to access service function.
            case 403:
                // Display the error message.
                this.clientNotificationService.error(information['message'], 'System');
                break;
            // Something went wrong with the service.
            case 500:
                // Display the error message.
                this.clientNotificationService.error('Service malfunctioned. Please try again');
                break;
            // For default error. Just display messages sent back from service.
            default:
                this.clientNotificationService.error(information['message'], 'System');
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
    __metadata("design:paramtypes", [http_1.Http,
        ClientNotificationService_1.ClientNotificationService,
        ClientAuthenticationService_1.ClientAuthenticationService,
        router_1.Router])
], ClientApiService);
exports.ClientApiService = ClientApiService;
//# sourceMappingURL=ClientApiService.js.map
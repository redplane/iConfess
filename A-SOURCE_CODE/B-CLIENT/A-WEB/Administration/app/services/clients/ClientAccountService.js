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
var ClientApiService_1 = require("../ClientApiService");
var http_1 = require("@angular/http");
require("rxjs/add/operator/toPromise");
/*
 * Service which handles category business.
 * */
var ClientAccountService = (function () {
    // Initiate instance of category service.
    function ClientAccountService(clientApiService, httpClient) {
        this._clientApiService = clientApiService;
        this._httpClient = httpClient;
    }
    // Find categories by using specific conditions.
    ClientAccountService.prototype.findAccounts = function (findAccountsViewModel) {
        // Page index should be decrease by one.
        var conditions = Object.assign({}, findAccountsViewModel);
        conditions['pagination'] = Object.assign({}, findAccountsViewModel.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;
        var requestOptions = new http_1.RequestOptions({
            headers: new http_1.Headers({
                'Content-Type': 'application/json'
            })
        });
        // Request to api to obtain list of available categories in system.
        return this._httpClient.post(this._clientApiService.apiFindAccount, conditions, requestOptions)
            .toPromise();
    };
    // Sign an account into system.
    ClientAccountService.prototype.login = function (loginViewModel) {
        return this._httpClient.post(this._clientApiService.apiLogin, loginViewModel).toPromise();
    };
    return ClientAccountService;
}());
ClientAccountService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [ClientApiService_1.ClientApiService, http_1.Http])
], ClientAccountService);
exports.ClientAccountService = ClientAccountService;
//# sourceMappingURL=ClientAccountService.js.map
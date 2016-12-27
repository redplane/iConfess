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
/*
* Service which handles hyperlink of api.
* */
var ClientApiService = (function () {
    // Initiate service with settings.
    function ClientApiService() {
        // Api which web application will consume the service.
        this.apiUrl = "http://confession.azurewebsites.net";
        // Find category api url.
        this.apiFindCategory = this.apiUrl + "/api/category/find";
        this.apiDeleteCategory = this.apiUrl + "/api/category";
        this.apiChangeCategoryDetail = this.apiUrl + "/api/category";
        this.apiInitiateCategory = this.apiUrl + "/api/category";
        // Find category account api url.
        this.apiFindAccount = this.apiUrl + "/api/account/find";
    }
    return ClientApiService;
}());
ClientApiService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ClientApiService);
exports.ClientApiService = ClientApiService;
//# sourceMappingURL=ClientApiService.js.map
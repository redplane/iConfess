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
var HyperlinkService_1 = require("./HyperlinkService");
var http_1 = require("@angular/http");
require("rxjs/add/operator/toPromise");
/*
* Service which handles category business.
* */
var CategoryService = (function () {
    // Initiate instance of category service.
    function CategoryService(hyperlinkService, httpClient) {
        this._hyperlinkService = hyperlinkService;
        this._httpClient = httpClient;
    }
    // Find categories by using specific conditions.
    CategoryService.prototype.findCategories = function (categorySearch) {
        var requestOptions = new http_1.RequestOptions({
            headers: new http_1.Headers({
                'Content-Type': 'application/json'
            })
        });
        var requestBody = {
            pagination: {
                index: 0,
                records: 20
            }
        };
        // Request to api to obtain list of available categories in system.
        return this._httpClient.post(this._hyperlinkService.apiFindCategory, requestBody, requestOptions)
            .toPromise();
    };
    return CategoryService;
}());
CategoryService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [HyperlinkService_1.HyperlinkService, http_1.Http])
], CategoryService);
exports.CategoryService = CategoryService;
//# sourceMappingURL=CategoryService.js.map
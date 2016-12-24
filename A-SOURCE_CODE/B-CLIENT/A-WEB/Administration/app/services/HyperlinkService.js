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
var HyperlinkService = (function () {
    function HyperlinkService() {
        // Hyperlink which is used for searching for categories.
        this.apiFindCategory = "http://confession.azurewebsites.net/api/category/find";
        // Hyperlink which is used for searching for categories for deleting 'em.
        this.apiDeleteCategory = "http://confession.azurewebsites.net/api/category";
        // Hyperlink which is used for changing category information.
        this.apiChangeCategoryDetail = "http://confession.azurewebsites.net/api/category";
        // Hyperlink which is used for searching for accounts.
        this.apiFindAccount = "http://confession.azurewebsites.net/api/account/find";
    }
    return HyperlinkService;
}());
HyperlinkService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], HyperlinkService);
exports.HyperlinkService = HyperlinkService;
//# sourceMappingURL=HyperlinkService.js.map
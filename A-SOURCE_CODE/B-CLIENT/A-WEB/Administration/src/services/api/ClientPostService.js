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
var ClientPostService = (function () {
    //#endregion
    //#region Constructor
    // Initiate instance of category service.
    function ClientPostService(clientApiService, clientAuthenticationService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
        //#region Properties
        // Search posts.
        this.urlSearchPosts = "api/post/find";
        // Search post details.
        this.urlSearchPostDetails = "api/post/details";
    }
    //#endregion
    //#region Methods
    // Find categories by using specific conditions.
    ClientPostService.prototype.getPosts = function (conditions) {
        // Page page should be decrease by one.
        var localConditions = Object.assign({}, conditions);
        localConditions['pagination'] = Object.assign({}, localConditions.pagination);
        localConditions.pagination.page -= 1;
        if (localConditions.pagination.page < 0)
            localConditions.pagination.page = 0;
        // Initiate url.
        var url = this.clientApiService.getBaseUrl() + "/" + this.urlSearchPosts;
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.getTokenCode(), url, null, localConditions);
    };
    // Find post details.
    ClientPostService.prototype.getPostDetails = function (index) {
        // Build full url.
        var url = this.clientApiService.getBaseUrl() + "/" + this.urlSearchPostDetails + "?index=" + index;
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.get(this.clientAuthenticationService.getTokenCode(), url, null);
    };
    return ClientPostService;
}());
ClientPostService = __decorate([
    core_1.Injectable(),
    __param(0, core_1.Inject("IClientApiService")),
    __param(1, core_1.Inject("IClientAuthenticationService")),
    __metadata("design:paramtypes", [Object, Object])
], ClientPostService);
exports.ClientPostService = ClientPostService;
//# sourceMappingURL=ClientPostService.js.map
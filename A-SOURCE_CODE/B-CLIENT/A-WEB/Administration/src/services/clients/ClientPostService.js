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
var ClientApiService_1 = require("../ClientApiService");
var ClientAuthenticationService_1 = require("./ClientAuthenticationService");
var ClientPostService = (function () {
    // Initiate instance of category service.
    function ClientPostService(clientApiService, clientAuthenticationService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
    }
    // Find categories by using specific conditions.
    ClientPostService.prototype.findPosts = function (findPostsCondition) {
        // Page index should be decrease by one.
        var conditions = Object.assign({}, findPostsCondition);
        conditions['pagination'] = Object.assign({}, findPostsCondition.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiFindPost, null, conditions).toPromise();
    };
    return ClientPostService;
}());
ClientPostService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [ClientApiService_1.ClientApiService,
        ClientAuthenticationService_1.ClientAuthenticationService])
], ClientPostService);
exports.ClientPostService = ClientPostService;
//# sourceMappingURL=ClientPostService.js.map
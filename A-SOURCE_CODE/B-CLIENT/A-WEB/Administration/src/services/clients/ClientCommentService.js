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
var ClientCommentService = (function () {
    // Initiate service with IoC.
    function ClientCommentService(clientApiService, clientAuthenticationService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
    }
    // Search for comments by using specific conditions.
    ClientCommentService.prototype.searchComments = function (conditions) {
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiSearchComment, null, conditions)
            .toPromise();
    };
    // Search for a specific comment's detail.
    ClientCommentService.prototype.searchCommentDetails = function (index) {
        return this.clientApiService.get(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiSearchCommentDetails, {
            index: index
        }).toPromise();
    };
    return ClientCommentService;
}());
ClientCommentService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [ClientApiService_1.ClientApiService,
        ClientAuthenticationService_1.ClientAuthenticationService])
], ClientCommentService);
exports.ClientCommentService = ClientCommentService;
//# sourceMappingURL=ClientCommentService.js.map
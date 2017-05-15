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
var ClientCommentService = (function () {
    //#endregion
    //#region Constructor
    // Initiate service with injectors.
    function ClientCommentService(clientApiService, clientAuthenticationService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
        //#region Properties
        // Url which is for searching comments.
        this.urlSearchComment = "api/comment/find";
        // Url which is for searching for comment details.
        this.urlSearchCommentDetails = "api/comments/details";
    }
    //#endregion
    //#region Methods
    // Search for comments by using specific conditions.
    ClientCommentService.prototype.getComments = function (conditions) {
        return this.clientApiService.post(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlSearchComment, null, conditions);
    };
    // Search for a specific comment's detail.
    ClientCommentService.prototype.getCommentDetails = function (conditions) {
        return this.clientApiService.post(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlSearchCommentDetails, null, conditions);
    };
    return ClientCommentService;
}());
ClientCommentService = __decorate([
    core_1.Injectable(),
    __param(0, core_1.Inject("IClientApiService")),
    __param(1, core_1.Inject("IClientAuthenticationService")),
    __metadata("design:paramtypes", [Object, Object])
], ClientCommentService);
exports.ClientCommentService = ClientCommentService;
//# sourceMappingURL=ClientCommentService.js.map
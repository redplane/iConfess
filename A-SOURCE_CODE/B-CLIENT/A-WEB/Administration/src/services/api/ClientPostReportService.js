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
var ClientPostReportService = (function () {
    //#endregion
    //#region Constructor
    // Initiate service with injectors.
    function ClientPostReportService(clientApiService, clientAuthenticationService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
        //#region Properties
        // Url which is for searching for post reports.
        this.urlSearchPostReport = "api/report/post/find";
        // Url which is for deleting for post reports.
        this.urlDeletePostReport = "api/report/post";
    }
    //#endregion
    //#region Methods
    // Find post reports by using specific conditions.
    ClientPostReportService.prototype.getPostReports = function (conditions) {
        return this.clientApiService.post(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlSearchPostReport, null, conditions);
    };
    // Delete post reports by using specific conditions.
    ClientPostReportService.prototype.deletePostReports = function (conditions) {
        return this.clientApiService.delete(this.clientAuthenticationService.getTokenCode(), this.clientApiService.getBaseUrl() + "/" + this.urlDeletePostReport, null, conditions);
    };
    return ClientPostReportService;
}());
ClientPostReportService = __decorate([
    core_1.Injectable(),
    __param(0, core_1.Inject("IClientApiService")),
    __param(1, core_1.Inject("IClientAuthenticationService")),
    __metadata("design:paramtypes", [Object, Object])
], ClientPostReportService);
exports.ClientPostReportService = ClientPostReportService;
//# sourceMappingURL=ClientPostReportService.js.map
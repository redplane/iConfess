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
var ClientPostReportService = (function () {
    // Initiate service with IoC.
    function ClientPostReportService(clientApiService, clientAuthenticationService) {
        this.clientApiService = clientApiService;
        this.clientAuthenticationService = clientAuthenticationService;
    }
    // Find post reports by using specific conditions.
    ClientPostReportService.prototype.findPostReports = function (conditions) {
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiFindPostReport, null, conditions)
            .toPromise();
    };
    // Delete post reports by using specific conditions.
    ClientPostReportService.prototype.deletePostReports = function (conditions) {
        return this.clientApiService.delete(this.clientAuthenticationService.findClientAuthenticationToken(), this.clientApiService.apiDeletePostReport, null, conditions)
            .toPromise();
    };
    return ClientPostReportService;
}());
ClientPostReportService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [ClientApiService_1.ClientApiService,
        ClientAuthenticationService_1.ClientAuthenticationService])
], ClientPostReportService);
exports.ClientPostReportService = ClientPostReportService;
//# sourceMappingURL=ClientPostReportService.js.map
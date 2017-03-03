"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ClientNotificationService_1 = require("../../services/ClientNotificationService");
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var CommentReportManagementComponent = (function () {
    function CommentReportManagementComponent() {
    }
    return CommentReportManagementComponent;
}());
CommentReportManagementComponent = __decorate([
    core_1.Component({
        selector: 'comment-report-management',
        templateUrl: 'comment-report-management.component.html',
        providers: [
            ClientNotificationService_1.ClientNotificationService,
            ClientAuthenticationService_1.ClientAuthenticationService
        ]
    })
], CommentReportManagementComponent);
exports.CommentReportManagementComponent = CommentReportManagementComponent;
//# sourceMappingURL=comment-report-management.component.js.map
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
var ClientTimeService_1 = require("../../services/ClientTimeService");
var AccountStatuses_1 = require("../../enumerations/AccountStatuses");
var AccountProfileBoxComponent = (function () {
    // Initiate component with injections.
    function AccountProfileBoxComponent(clientTimeService) {
        this.clientTimeService = clientTimeService;
        // Find list of account statuses.
        this.accountStatuses = AccountStatuses_1.AccountStatuses;
        // Initialize event emitters.
        this.clickChangeAccountStatus = new core_1.EventEmitter();
    }
    // Callback which is fired when change account button is clicked.
    AccountProfileBoxComponent.prototype.changeAccountStatus = function (account) {
        this.clickChangeAccountStatus.emit(account);
    };
    return AccountProfileBoxComponent;
}());
AccountProfileBoxComponent = __decorate([
    core_1.Component({
        selector: 'account-profile-box',
        templateUrl: 'account-profile-box.component.html',
        inputs: ['account'],
        outputs: ['clickChangeAccountStatus'],
        providers: [
            ClientTimeService_1.ClientTimeService
        ]
    }),
    __metadata("design:paramtypes", [ClientTimeService_1.ClientTimeService])
], AccountProfileBoxComponent);
exports.AccountProfileBoxComponent = AccountProfileBoxComponent;
//# sourceMappingURL=account-profile-box.component.js.map
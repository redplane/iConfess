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
var TimeService_1 = require("../../../services/TimeService");
var AccountDetailBoxComponent = (function () {
    // Initiate category detail box with dependency injections.
    function AccountDetailBoxComponent(timeService) {
        this._timeService = timeService;
        // Event handler initialization.
        this.clickRemoveAccount = new core_1.EventEmitter();
        this.clickChangeAccountInfo = new core_1.EventEmitter();
    }
    // Fired when a account is clicked to be removed.
    AccountDetailBoxComponent.prototype.deleteAccount = function (account) {
        this.clickRemoveAccount.emit(account);
    };
    // Fired when a account is clicked to be changed.
    AccountDetailBoxComponent.prototype.changeAccountInfo = function (category) {
        this.clickChangeAccountInfo.emit(category);
    };
    return AccountDetailBoxComponent;
}());
AccountDetailBoxComponent = __decorate([
    core_1.Component({
        selector: 'account-detail-box',
        inputs: ['account'],
        outputs: ['clickRemoveAccount', 'clickChangeAccountInfo'],
        templateUrl: './app/views/contents/account/account-detail-box.component.html',
        providers: [
            TimeService_1.TimeService
        ]
    }),
    __metadata("design:paramtypes", [TimeService_1.TimeService])
], AccountDetailBoxComponent);
exports.AccountDetailBoxComponent = AccountDetailBoxComponent;
//# sourceMappingURL=account-detail-box.component.js.map
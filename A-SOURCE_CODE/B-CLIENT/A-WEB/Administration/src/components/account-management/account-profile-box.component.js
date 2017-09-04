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
var AccountStatuses_1 = require("../../enumerations/AccountStatuses");
var Account_1 = require("../../models/entities/Account");
var _ = require("lodash");
var AccountProfileBoxComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with injections.
    function AccountProfileBoxComponent(clientTimeService, clientCommonService) {
        this.clientTimeService = clientTimeService;
        this.clientCommonService = clientCommonService;
        //#region Properties
        // Find list of account statuses.
        this.AccountStatuses = AccountStatuses_1.AccountStatuses;
        // Initialize account profile information.
        this.account = new Account_1.Account();
        // Initialize event emitters.
        this.clickChangeAccountStatus = new core_1.EventEmitter();
    }
    //#endregion
    //#region Methods
    // Callback which is fired when change account button is clicked.
    AccountProfileBoxComponent.prototype.changeAccountStatus = function (account) {
        this.clickChangeAccountStatus.emit(account);
    };
    // Attach a profile to this component.
    AccountProfileBoxComponent.prototype.setProfile = function (account) {
        this.account = _.cloneDeep(account);
    };
    // Get profile from component
    AccountProfileBoxComponent.prototype.getProfile = function () {
        return this.account;
    };
    return AccountProfileBoxComponent;
}());
__decorate([
    core_1.Input('account'),
    __metadata("design:type", Account_1.Account)
], AccountProfileBoxComponent.prototype, "account", void 0);
AccountProfileBoxComponent = __decorate([
    core_1.Component({
        selector: 'account-profile-box',
        templateUrl: 'account-profile-box.component.html',
        exportAs: 'account-profile-box'
    }),
    __param(0, core_1.Inject("IClientTimeService")),
    __param(1, core_1.Inject('IClientCommonService')),
    __metadata("design:paramtypes", [Object, Object])
], AccountProfileBoxComponent);
exports.AccountProfileBoxComponent = AccountProfileBoxComponent;
//# sourceMappingURL=account-profile-box.component.js.map
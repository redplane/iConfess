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
var SearchAccountsViewModel_1 = require("../../viewmodels/accounts/SearchAccountsViewModel");
var AccountSortProperty_1 = require("../../enumerations/order/AccountSortProperty");
var Dictionary_1 = require("../../models/Dictionary");
var AccountSearchBoxComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with default dependency injection.
    function AccountSearchBoxComponent(clientAccountService, clientCommonService) {
        this.clientAccountService = clientAccountService;
        this.clientCommonService = clientCommonService;
        // Initiate event emitters.
        this.search = new core_1.EventEmitter();
        this.accounts = new Array();
        // Initiate list of properties which can be used for sorting.
        var sortProperties = new Dictionary_1.Dictionary();
        sortProperties.add('Index', AccountSortProperty_1.AccountSortProperty.index);
        sortProperties.add('Email', AccountSortProperty_1.AccountSortProperty.email);
        sortProperties.add('Nickname', AccountSortProperty_1.AccountSortProperty.nickname);
        sortProperties.add('Status', AccountSortProperty_1.AccountSortProperty.status);
        sortProperties.add('Joined', AccountSortProperty_1.AccountSortProperty.joined);
        sortProperties.add('Last modified', AccountSortProperty_1.AccountSortProperty.lastModified);
        this.sortProperties = sortProperties;
    }
    //#endregion
    //#region Methods
    // Callback which is fired when status button is toggled.
    AccountSearchBoxComponent.prototype.toggleStatuses = function (status) {
        // Statuses list hasn't been initialized.
        if (this.conditions.statuses == null) {
            this.conditions.statuses = new Array();
            this.conditions.statuses.push(status);
            return;
        }
        // Find status in the list.
        var index = this.conditions.statuses.indexOf(status);
        if (index == -1) {
            this.conditions.statuses.push(status);
            return;
        }
        this.conditions.statuses.splice(index, 1);
    };
    // Callback which is fired when search button is clicked.
    AccountSearchBoxComponent.prototype.clickSearch = function () {
        this.search.emit();
    };
    // Callback which is fired when statuses selection is updated.
    AccountSearchBoxComponent.prototype.updateStatuses = function (statuses) {
        this.conditions.statuses = statuses.map(function (x) { return x.value; });
    };
    /*
    * Callback which is fired when component has been loaded successfully.
    * */
    AccountSearchBoxComponent.prototype.ngOnInit = function () {
    };
    return AccountSearchBoxComponent;
}());
__decorate([
    core_1.Input('is-loading'),
    __metadata("design:type", Boolean)
], AccountSearchBoxComponent.prototype, "isLoading", void 0);
__decorate([
    core_1.Output('search'),
    __metadata("design:type", core_1.EventEmitter)
], AccountSearchBoxComponent.prototype, "search", void 0);
__decorate([
    core_1.Input('conditions'),
    __metadata("design:type", SearchAccountsViewModel_1.SearchAccountsViewModel)
], AccountSearchBoxComponent.prototype, "conditions", void 0);
AccountSearchBoxComponent = __decorate([
    core_1.Component({
        selector: 'account-search-box',
        templateUrl: 'account-search-box.component.html'
    }),
    __param(0, core_1.Inject("IClientAccountService")),
    __param(1, core_1.Inject('IClientCommonService')),
    __metadata("design:paramtypes", [Object, Object])
], AccountSearchBoxComponent);
exports.AccountSearchBoxComponent = AccountSearchBoxComponent;
//# sourceMappingURL=account-search-box.component.js.map
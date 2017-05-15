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
var forms_1 = require("@angular/forms");
var SearchAccountsViewModel_1 = require("../../viewmodels/accounts/SearchAccountsViewModel");
var AccountSearchBoxComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with default dependency injection.
    function AccountSearchBoxComponent(formBuilder, clientAccountService) {
        this.formBuilder = formBuilder;
        this.clientAccountService = clientAccountService;
        // Form control of find category box.
        this.findAccountBox = formBuilder.group({
            email: [''],
            nickname: [''],
            joined: formBuilder.group({
                from: [''],
                to: ['']
            }),
            lastModified: formBuilder.group({
                from: [''],
                to: ['']
            }),
            pagination: formBuilder.group({
                index: [0],
                records: [10]
            }),
            sort: [null],
            direction: [null]
        });
        // Initiate event emitters.
        this.search = new core_1.EventEmitter();
        this.accounts = new Array();
        // Initiate search conditions.
        this.conditions = new SearchAccountsViewModel_1.SearchAccountsViewModel();
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
    __param(1, core_1.Inject("IClientAccountService")),
    __metadata("design:paramtypes", [forms_1.FormBuilder, Object])
], AccountSearchBoxComponent);
exports.AccountSearchBoxComponent = AccountSearchBoxComponent;
//# sourceMappingURL=account-search-box.component.js.map
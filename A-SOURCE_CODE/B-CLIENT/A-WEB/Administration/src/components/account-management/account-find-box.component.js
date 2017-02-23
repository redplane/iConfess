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
var forms_1 = require("@angular/forms");
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var ClientAccountService_1 = require("../../services/clients/ClientAccountService");
var FindAccountsViewModel_1 = require("../../viewmodels/accounts/FindAccountsViewModel");
var ClientApiService_1 = require("../../services/ClientApiService");
var AccountFindBoxComponent = (function () {
    // Initiate component with default dependency injection.
    function AccountFindBoxComponent(formBuilder, clientConfigurationService, clientAccountService) {
        this.formBuilder = formBuilder;
        this.clientConfigurationService = clientConfigurationService;
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
        // Initiate search conditions.
        this.conditions = new FindAccountsViewModel_1.FindAccountsViewModel();
    }
    // Callback which is fired when status button is toggled.
    AccountFindBoxComponent.prototype.toggleStatuses = function (status) {
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
    AccountFindBoxComponent.prototype.clickSearch = function () {
        this.search.emit();
    };
    // Callback which is fired when control is starting to load data of accounts from service.
    AccountFindBoxComponent.prototype.loadAccounts = function () {
        // // Initiate find account conditions.
        // let findAccountsViewModel = new FindAccountsViewModel();
        //
        // // Update account which should be searched for.
        // if (findAccountsViewModel.email == null)
        //     findAccountsViewModel.email = new TextSearch();
        // findAccountsViewModel.email.value = this.findCategoryBox.controls['categoryCreatorEmail'].value;
        //
        // // Initiate pagination.
        // let pagination = new Pagination();
        // pagination.index = 0;
        // pagination.records = this._clientConfigurationService.findMaxPageRecords();
        //
        // // Pagination update.
        // findAccountsViewModel.pagination = pagination;
        //
        // this._clientAccountService.findAccounts(findAccountsViewModel)
        //     .then((response: Response | any) => {
        //
        //         // Analyze find account response view model.
        //         let findAccountResult = response.json();
        //
        //         // Find list of accounts which has been responded from service.
        //         this._accounts = findAccountResult.accounts;
        //     })
        //     .catch((response: any) => {
        //
        //     });
    };
    /*
    * Callback which is fired when component has been loaded successfully.
    * */
    AccountFindBoxComponent.prototype.ngOnInit = function () {
    };
    return AccountFindBoxComponent;
}());
AccountFindBoxComponent = __decorate([
    core_1.Component({
        selector: 'account-find-box',
        templateUrl: 'account-find-box.component.html',
        inputs: ['conditions', 'isLoading'],
        outputs: ['search'],
        providers: [
            forms_1.FormBuilder,
            ClientConfigurationService_1.ClientConfigurationService,
            ClientAccountService_1.ClientAccountService,
            ClientApiService_1.ClientApiService
        ]
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        ClientConfigurationService_1.ClientConfigurationService,
        ClientAccountService_1.ClientAccountService])
], AccountFindBoxComponent);
exports.AccountFindBoxComponent = AccountFindBoxComponent;
//# sourceMappingURL=account-find-box.component.js.map
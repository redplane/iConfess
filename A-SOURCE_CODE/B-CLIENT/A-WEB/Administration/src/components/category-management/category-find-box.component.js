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
var FindCategoriesViewModel_1 = require("../../viewmodels/category/FindCategoriesViewModel");
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var ClientAccountService_1 = require("../../services/clients/ClientAccountService");
var ClientDataConstraintService_1 = require("../../services/ClientDataConstraintService");
var Pagination_1 = require("../../viewmodels/Pagination");
var TextSearch_1 = require("../../viewmodels/TextSearch");
var FindAccountsViewModel_1 = require("../../viewmodels/accounts/FindAccountsViewModel");
var CategoryFindBoxComponent = (function () {
    // Initiate component with default dependency injection.
    function CategoryFindBoxComponent(formBuilder, clientConfigurationService, clientAccountService, clientDataConstraintService) {
        this.formBuilder = formBuilder;
        this.clientConfigurationService = clientConfigurationService;
        this.clientAccountService = clientAccountService;
        this.clientDataConstraintService = clientDataConstraintService;
        // Initiate account typeahead data.
        this._accounts = new Array();
        // Form control of find category box.
        this.findCategoryBox = this.formBuilder.group({
            name: [null],
            categoryCreatorEmail: [null],
            created: this.formBuilder.group({
                from: [null, forms_1.Validators.nullValidator],
                to: [null, forms_1.Validators.nullValidator]
            }),
            lastModified: this.formBuilder.group({
                from: [null, forms_1.Validators.nullValidator],
                to: [null, forms_1.Validators.nullValidator]
            }),
            pagination: this.formBuilder.group({
                records: [null, forms_1.Validators.nullValidator]
            }),
            sort: [null],
            direction: [null]
        });
        // Initiate event emitters.
        this.search = new core_1.EventEmitter();
    }
    // Callback which is fired when search button is clicked.
    CategoryFindBoxComponent.prototype.clickSearch = function () {
        this.search.emit();
    };
    // Find accounts list and bind it to typeahead control.
    // This callback is fired when account is typed in control.
    CategoryFindBoxComponent.prototype.selectCategoryCreator = function (event) {
        // Find account which has been selected.
        var account = event.item;
        // Account is invalid.
        if (account == null)
            return;
        // Account doesn't have id column.
        if (account['id'] == null)
            return;
        this.conditions.creatorIndex = account.id;
    };
    // Callback which is fired when control is starting to load data of accounts from service.
    CategoryFindBoxComponent.prototype.loadAccounts = function () {
        var _this = this;
        // Initiate find account conditions.
        var findAccountsViewModel = new FindAccountsViewModel_1.FindAccountsViewModel();
        // Update account which should be searched for.
        if (findAccountsViewModel.email == null)
            findAccountsViewModel.email = new TextSearch_1.TextSearch();
        findAccountsViewModel.email.value = this.findCategoryBox.controls['categoryCreatorEmail'].value;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        // Pagination update.
        findAccountsViewModel.pagination = pagination;
        // Find accounts with specific conditions.
        this.clientAccountService.findAccounts(findAccountsViewModel)
            .then(function (response) {
            // Analyze find account response view model.
            var findAccountResult = response.json();
            // Find list of accounts which has been responded from service.
            _this._accounts = findAccountResult.accounts;
        })
            .catch(function (response) {
            // TODO:
        });
    };
    return CategoryFindBoxComponent;
}());
CategoryFindBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-find-box',
        templateUrl: 'category-find-box.component.html',
        inputs: ['conditions', 'isLoading'],
        outputs: ['search'],
        providers: [
            forms_1.FormBuilder,
            FindCategoriesViewModel_1.FindCategoriesViewModel,
            ClientConfigurationService_1.ClientConfigurationService,
            ClientAccountService_1.ClientAccountService,
            ClientDataConstraintService_1.ClientDataConstraintService
        ]
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        ClientConfigurationService_1.ClientConfigurationService,
        ClientAccountService_1.ClientAccountService,
        ClientDataConstraintService_1.ClientDataConstraintService])
], CategoryFindBoxComponent);
exports.CategoryFindBoxComponent = CategoryFindBoxComponent;
//# sourceMappingURL=category-find-box.component.js.map
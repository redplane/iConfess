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
var SearchCategoriesViewModel_1 = require("../../viewmodels/category/SearchCategoriesViewModel");
var Pagination_1 = require("../../viewmodels/Pagination");
var TextSearch_1 = require("../../viewmodels/TextSearch");
var SearchAccountsViewModel_1 = require("../../viewmodels/accounts/SearchAccountsViewModel");
var CategorySortProperty_1 = require("../../enumerations/order/CategorySortProperty");
var Dictionary_1 = require("../../models/Dictionary");
var TextSearchMode_1 = require("../../enumerations/TextSearchMode");
var CategorySearchBoxComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate component with default dependency injection.
    function CategorySearchBoxComponent(clientAccountService, clientApiService, clientCommonService) {
        this.clientAccountService = clientAccountService;
        this.clientApiService = clientApiService;
        this.clientCommonService = clientCommonService;
        // Initiate account typeahead data.
        this.accounts = new Array();
        // Initiate category sorting properties.
        this.categorySortProperties = new Dictionary_1.Dictionary();
        this.categorySortProperties.add('Index', CategorySortProperty_1.CategorySortProperty.index);
        this.categorySortProperties.add('Creator', CategorySortProperty_1.CategorySortProperty.creatorIndex);
        this.categorySortProperties.add('Category name', CategorySortProperty_1.CategorySortProperty.name);
        this.categorySortProperties.add('Created', CategorySortProperty_1.CategorySortProperty.created);
        this.categorySortProperties.add('Last modified', CategorySortProperty_1.CategorySortProperty.lastModified);
    }
    //#endregion
    //#region Methods
    // Callback which is fired when control is starting to load data of accounts from service.
    CategorySearchBoxComponent.prototype.loadAccounts = function (keyword) {
        var _this = this;
        // Initiate find account conditions.
        var conditions = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        // Update account which should be searched for.
        if (conditions.email == null)
            conditions.email = new TextSearch_1.TextSearch();
        conditions.email.value = keyword;
        conditions.email.mode = TextSearchMode_1.TextSearchMode.contains;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.page = 1;
        pagination.records = this.clientCommonService.getMaxPageRecords();
        // Pagination update.
        conditions.pagination = pagination;
        // Find accounts with specific conditions.
        this.clientAccountService.getAccounts(conditions)
            .then(function (x) {
            // Analyze find account response view model.
            var result = x.json();
            // Find list of accounts which has been responded from service.
            _this.accounts = result.records;
        })
            .catch(function (x) {
            _this.clientApiService.handleInvalidResponse(x);
        });
    };
    // Update accounts list.
    CategorySearchBoxComponent.prototype.updateAccounts = function (accounts) {
        var ids = accounts.map(function (x) { return x.id; });
        if (ids != null && ids.length > 0) {
            this.conditions.creatorIndex = ids[0];
        }
        else {
            this.conditions.creatorIndex = null;
        }
    };
    // Callback which is fired when component has been loaded.
    CategorySearchBoxComponent.prototype.ngOnInit = function () {
        this.loadAccounts('');
    };
    return CategorySearchBoxComponent;
}());
__decorate([
    core_1.Input('conditions'),
    __metadata("design:type", SearchCategoriesViewModel_1.SearchCategoriesViewModel)
], CategorySearchBoxComponent.prototype, "conditions", void 0);
CategorySearchBoxComponent = __decorate([
    core_1.Component({
        selector: 'category-search-box',
        templateUrl: 'category-search-box.component.html'
    }),
    __param(0, core_1.Inject('IClientAccountService')),
    __param(1, core_1.Inject('IClientApiService')),
    __param(2, core_1.Inject('IClientCommonService')),
    __metadata("design:paramtypes", [Object, Object, Object])
], CategorySearchBoxComponent);
exports.CategorySearchBoxComponent = CategorySearchBoxComponent;
//# sourceMappingURL=category-search-box.component.js.map
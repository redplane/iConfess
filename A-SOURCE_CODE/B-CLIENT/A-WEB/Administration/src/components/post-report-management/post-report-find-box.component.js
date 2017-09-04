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
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var SearchPostReportsViewModel_1 = require("../../viewmodels/post-report/SearchPostReportsViewModel");
var TextSearch_1 = require("../../viewmodels/TextSearch");
var TextSearchMode_1 = require("../../enumerations/TextSearchMode");
var SearchAccountsViewModel_1 = require("../../viewmodels/accounts/SearchAccountsViewModel");
var Pagination_1 = require("../../viewmodels/Pagination");
var SearchPostsViewModel_1 = require("../../viewmodels/post/SearchPostsViewModel");
var PostReportFindBoxComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate post report component.
    function PostReportFindBoxComponent(clientConfigurationService, clientAccountService, clientPostService, clientApiService, formBuilder) {
        this.clientConfigurationService = clientConfigurationService;
        this.clientAccountService = clientAccountService;
        this.clientPostService = clientPostService;
        this.clientApiService = clientApiService;
        this.formBuilder = formBuilder;
        // Find post report control group.
        this.findPostReportBox = this.formBuilder.group({
            postIndex: [],
            postOwnerIndex: [],
            postReporterIndex: [],
            created: this.formBuilder.group({
                from: [],
                to: []
            }),
            pagination: this.formBuilder.group({
                index: [],
                records: []
            }),
            sort: [],
            direction: []
        });
        this.posts = new Array();
        this.owners = new Array();
        this.reporters = new Array();
        // Initiate emitter.
        this.search = new core_1.EventEmitter();
    }
    //#endregion
    //#region Methods
    // Callback which is fired when control is starting to load data of accounts from service.
    PostReportFindBoxComponent.prototype.loadPostReporters = function () {
        var _this = this;
        var email = new TextSearch_1.TextSearch();
        email.mode = TextSearchMode_1.TextSearchMode.contains;
        email.value = this.findPostReportBox.controls['postReporterIndex'].value;
        // Initiate find account conditions.
        var findAccountsViewModel = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        // Update account which should be searched for.
        findAccountsViewModel.email = email;
        // All statuses can be found.
        findAccountsViewModel.statuses = null;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.page = 0;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();
        // Pagination update.
        findAccountsViewModel.pagination = pagination;
        // Find reporters by using specific conditions.
        this.clientAccountService.getAccounts(findAccountsViewModel)
            .then(function (response) {
            // Analyze find account response view model.
            var findAccountResult = response.json();
            // Find list of accounts which has been responded from service.
            _this.reporters = findAccountResult.accounts;
        })
            .catch(function (response) {
            // Handle failed response.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when control is starting to load data of accounts from service.
    PostReportFindBoxComponent.prototype.loadPostOwners = function () {
        var _this = this;
        var email = new TextSearch_1.TextSearch();
        email.mode = TextSearchMode_1.TextSearchMode.contains;
        email.value = this.findPostReportBox.controls['postOwnerIndex'].value;
        // Initiate find account conditions.
        var findAccountsViewModel = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        // Update account which should be searched for.
        findAccountsViewModel.email = email;
        // All statuses can be found.
        findAccountsViewModel.statuses = null;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.page = 0;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();
        // Pagination update.
        findAccountsViewModel.pagination = pagination;
        // Find reporters by using specific conditions.
        this.clientAccountService.getAccounts(findAccountsViewModel)
            .then(function (response) {
            // Analyze find account response view model.
            var findAccountResult = response.json();
            // Find list of accounts which has been responded from service.
            _this.owners = findAccountResult.accounts;
        })
            .catch(function (response) {
            // Handle failed response.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when post title input field is inputed.
    PostReportFindBoxComponent.prototype.loadPostTitles = function () {
        var _this = this;
        // Initiate find account conditions.
        var findPostsViewModel = new SearchPostsViewModel_1.SearchPostsViewModel();
        // Update title search.
        var title = new TextSearch_1.TextSearch();
        title.mode = TextSearchMode_1.TextSearchMode.contains;
        title.value = this.findPostReportBox.controls['postIndex'].value;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.page = 0;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();
        // Title update.
        findPostsViewModel.title = title;
        // Pagination update.
        findPostsViewModel.pagination = pagination;
        // Find reporters by using specific conditions.
        this.clientPostService.getPosts(findPostsViewModel)
            .then(function (response) {
            // Analyze find account response view model.
            var findPostsResult = response.json();
            // Find list of accounts which has been responded from service.
            _this.posts = findPostsResult.posts;
        })
            .catch(function (response) {
            // Handle failed response.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Find accounts list and bind it to typeahead control.
    // This callback is fired when account is typed in control.
    PostReportFindBoxComponent.prototype.selectPostReporter = function (event) {
        // Find account which has been selected.
        var account = event.item;
        // Account is invalid.
        if (account == null)
            return;
        // Account doesn't have id column.
        if (account['id'] == null)
            return;
        this.conditions.postReporterIndex = account.id;
    };
    // Find accounts list and bind it to typeahead control.
    // This callback is fired when account is typed in control.
    PostReportFindBoxComponent.prototype.selectPostOwner = function (event) {
        // Find account which has been selected.
        var account = event.item;
        // Account is invalid.
        if (account == null)
            return;
        // Account doesn't have id column.
        if (account['id'] == null)
            return;
        this.conditions.postOwnerIndex = account.id;
    };
    // This callback is fired when search button is clicked.
    PostReportFindBoxComponent.prototype.clickSearch = function () {
        // Copy the object.
        var condition = JSON.parse(JSON.stringify(this.conditions));
        var created = condition['created'];
        if (created != null) {
            var date = void 0;
            if (created['from'] != null) {
                date = new Date(created['from']);
                created['from'] = date.getTime();
            }
            if (created['to'] != null) {
                date = new Date(created['to']);
                created['to'] = date.getTime();
            }
        }
        if (this.search != null) {
            this.search.emit(condition);
        }
        return;
    };
    return PostReportFindBoxComponent;
}());
__decorate([
    core_1.Input('conditions'),
    __metadata("design:type", SearchPostReportsViewModel_1.SearchPostReportsViewModel)
], PostReportFindBoxComponent.prototype, "conditions", void 0);
__decorate([
    core_1.Input('is-busy'),
    __metadata("design:type", Boolean)
], PostReportFindBoxComponent.prototype, "isBusy", void 0);
PostReportFindBoxComponent = __decorate([
    core_1.Component({
        selector: 'post-report-find-box',
        templateUrl: 'post-report-find-box.component.html',
        outputs: ['search'],
        providers: [
            ClientConfigurationService_1.ClientConfigurationService
        ]
    }),
    __param(1, core_1.Inject("IClientAccountService")),
    __param(2, core_1.Inject("IClientPostService")),
    __param(3, core_1.Inject("IClientApiService")),
    __metadata("design:paramtypes", [ClientConfigurationService_1.ClientConfigurationService, Object, Object, Object, forms_1.FormBuilder])
], PostReportFindBoxComponent);
exports.PostReportFindBoxComponent = PostReportFindBoxComponent;
//# sourceMappingURL=post-report-find-box.component.js.map
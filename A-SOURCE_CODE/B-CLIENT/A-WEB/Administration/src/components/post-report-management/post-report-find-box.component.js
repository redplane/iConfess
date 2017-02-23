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
var ClientApiService_1 = require("../../services/ClientApiService");
var ClientPostService_1 = require("../../services/clients/ClientPostService");
var ClientNotificationService_1 = require("../../services/ClientNotificationService");
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var TextSearch_1 = require("../../viewmodels/TextSearch");
var TextSearchMode_1 = require("../../enumerations/TextSearchMode");
var FindAccountsViewModel_1 = require("../../viewmodels/accounts/FindAccountsViewModel");
var Pagination_1 = require("../../viewmodels/Pagination");
var FindPostViewModel_1 = require("../../viewmodels/post/FindPostViewModel");
var PostReportFindBoxComponent = (function () {
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
        // Initiate emitter.
        this.search = new core_1.EventEmitter();
    }
    // Callback which is fired when control is starting to load data of accounts from service.
    PostReportFindBoxComponent.prototype.loadPostReporters = function () {
        var _this = this;
        var email = new TextSearch_1.TextSearch();
        email.mode = TextSearchMode_1.TextSearchMode.contains;
        email.value = this.findPostReportBox.controls['postReporterIndex'].value;
        // Initiate find account conditions.
        var findAccountsViewModel = new FindAccountsViewModel_1.FindAccountsViewModel();
        // Update account which should be searched for.
        findAccountsViewModel.email = email;
        // All statuses can be found.
        findAccountsViewModel.statuses = null;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        // Pagination update.
        findAccountsViewModel.pagination = pagination;
        // Find reporters by using specific conditions.
        this.clientAccountService.findAccounts(findAccountsViewModel)
            .then(function (response) {
            // Analyze find account response view model.
            var findAccountResult = response.json();
            // Find list of accounts which has been responded from service.
            _this.reporters = findAccountResult.accounts;
        })
            .catch(function (response) {
            // Handle failed response.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
        });
    };
    // Callback which is fired when control is starting to load data of accounts from service.
    PostReportFindBoxComponent.prototype.loadPostOwners = function () {
        var _this = this;
        var email = new TextSearch_1.TextSearch();
        email.mode = TextSearchMode_1.TextSearchMode.contains;
        email.value = this.findPostReportBox.controls['postOwnerIndex'].value;
        // Initiate find account conditions.
        var findAccountsViewModel = new FindAccountsViewModel_1.FindAccountsViewModel();
        // Update account which should be searched for.
        findAccountsViewModel.email = email;
        // All statuses can be found.
        findAccountsViewModel.statuses = null;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        // Pagination update.
        findAccountsViewModel.pagination = pagination;
        // Find reporters by using specific conditions.
        this.clientAccountService.findAccounts(findAccountsViewModel)
            .then(function (response) {
            // Analyze find account response view model.
            var findAccountResult = response.json();
            // Find list of accounts which has been responded from service.
            _this.owners = findAccountResult.accounts;
        })
            .catch(function (response) {
            // Handle failed response.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
        });
    };
    // Callback which is fired when post title input field is inputed.
    PostReportFindBoxComponent.prototype.loadPostTitles = function () {
        var _this = this;
        // Initiate find account conditions.
        var findPostsViewModel = new FindPostViewModel_1.FindPostViewModel();
        // Update title search.
        var title = new TextSearch_1.TextSearch();
        title.mode = TextSearchMode_1.TextSearchMode.contains;
        title.value = this.findPostReportBox.controls['postIndex'].value;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        // Title update.
        findPostsViewModel.title = title;
        // Pagination update.
        findPostsViewModel.pagination = pagination;
        // Find reporters by using specific conditions.
        this.clientPostService.findPosts(findPostsViewModel)
            .then(function (response) {
            // Analyze find account response view model.
            var findPostsResult = response.json();
            // Find list of accounts which has been responded from service.
            _this.posts = findPostsResult.posts;
        })
            .catch(function (response) {
            // Handle failed response.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
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
PostReportFindBoxComponent = __decorate([
    core_1.Component({
        selector: 'post-report-find-box',
        templateUrl: 'post-report-find-box.component.html',
        inputs: ['conditions', 'isLoading'],
        outputs: ['search'],
        providers: [
            ClientConfigurationService_1.ClientConfigurationService,
            ClientAccountService_1.ClientAccountService,
            ClientApiService_1.ClientApiService,
            ClientPostService_1.ClientPostService,
            ClientNotificationService_1.ClientNotificationService,
            ClientAuthenticationService_1.ClientAuthenticationService
        ]
    }),
    __metadata("design:paramtypes", [ClientConfigurationService_1.ClientConfigurationService,
        ClientAccountService_1.ClientAccountService,
        ClientPostService_1.ClientPostService,
        ClientApiService_1.ClientApiService,
        forms_1.FormBuilder])
], PostReportFindBoxComponent);
exports.PostReportFindBoxComponent = PostReportFindBoxComponent;
//# sourceMappingURL=post-report-find-box.component.js.map
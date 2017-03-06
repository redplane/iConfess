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
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var forms_1 = require("@angular/forms");
var SearchCommentReportsViewModel_1 = require("../../viewmodels/comment-report/SearchCommentReportsViewModel");
var SearchAccountsViewModel_1 = require("../../viewmodels/accounts/SearchAccountsViewModel");
var TextSearch_1 = require("../../viewmodels/TextSearch");
var ClientAccountService_1 = require("../../services/clients/ClientAccountService");
var Pagination_1 = require("../../viewmodels/Pagination");
var TextSearchMode_1 = require("../../enumerations/TextSearchMode");
var ClientApiService_1 = require("../../services/ClientApiService");
var CommentReportFindBoxComponent = (function () {
    function CommentReportFindBoxComponent(clientConfigurationService, clientAccountService, formBuilder) {
        this.clientConfigurationService = clientConfigurationService;
        this.clientAccountService = clientAccountService;
        this.formBuilder = formBuilder;
        // Initiate comment report find box components container.
        this.commentReportFindBox = this.formBuilder.group({
            commentOwner: [''],
            commentReporter: [''],
            body: this.formBuilder.group({
                mode: [''],
                value: ['']
            }),
            reason: this.formBuilder.group({
                mode: [''],
                value: []
            }),
            created: this.formBuilder.group({
                from: [''],
                to: ['']
            }),
            pagination: this.formBuilder.group({
                index: [''],
                records: ['']
            }),
            sort: [''],
            direction: ['']
        });
        this.conditions = new SearchCommentReportsViewModel_1.SearchCommentReportsViewModel();
    }
    // Callback which is fired when component has been rendered successfully.
    CommentReportFindBoxComponent.prototype.ngOnInit = function () {
        this.conditions.pagination.records = this.clientConfigurationService.findMaxPageRecords();
    };
    // Callback which is fired when control is starting to load data of accounts from service.
    CommentReportFindBoxComponent.prototype.loadAccounts = function (email) {
        // Initiate find account conditions.
        var findAccountsViewModel = new SearchAccountsViewModel_1.SearchAccountsViewModel();
        // Update account which should be searched for.
        if (findAccountsViewModel.email == null)
            findAccountsViewModel.email = new TextSearch_1.TextSearch();
        findAccountsViewModel.email.value = email;
        findAccountsViewModel.email.mode = TextSearchMode_1.TextSearchMode.contains;
        // Initiate pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        // Pagination update.
        findAccountsViewModel.pagination = pagination;
        return this.clientAccountService.findAccounts(findAccountsViewModel);
        //
        // .then((response: Response | any) => {
        //
        //     // Analyze find account response view model.
        //     let findAccountResult = response.json();
        //
        //     // Find list of accounts which has been responded from service.
        //     this._accounts = findAccountResult.accounts;
        // })
        // .catch((response: any) => {
        //
        // });
    };
    return CommentReportFindBoxComponent;
}());
CommentReportFindBoxComponent = __decorate([
    core_1.Component({
        selector: 'comment-report-find-box',
        templateUrl: 'comment-report-find-box.component.html',
        inputs: ['conditions'],
        providers: [
            ClientConfigurationService_1.ClientConfigurationService,
            ClientAccountService_1.ClientAccountService,
            ClientApiService_1.ClientApiService
        ]
    }),
    __metadata("design:paramtypes", [ClientConfigurationService_1.ClientConfigurationService,
        ClientAccountService_1.ClientAccountService,
        forms_1.FormBuilder])
], CommentReportFindBoxComponent);
exports.CommentReportFindBoxComponent = CommentReportFindBoxComponent;
//# sourceMappingURL=comment-report-find-box.component.js.map
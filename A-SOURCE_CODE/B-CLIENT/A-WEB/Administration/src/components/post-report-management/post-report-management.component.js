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
var ng2_bootstrap_1 = require("ng2-bootstrap");
var ClientConfigurationService_1 = require("../../services/ClientConfigurationService");
var ClientCommonService_1 = require("../../services/ClientCommonService");
var SearchPostReportsViewModel_1 = require("../../viewmodels/post-report/SearchPostReportsViewModel");
var PostReportSortProperty_1 = require("../../enumerations/order/PostReportSortProperty");
var Pagination_1 = require("../../viewmodels/Pagination");
var SortDirection_1 = require("../../enumerations/SortDirection");
var UnixDateRange_1 = require("../../viewmodels/UnixDateRange");
var SearchCommentsDetailsViewModel_1 = require("../../viewmodels/comment/SearchCommentsDetailsViewModel");
var AccountStatuses_1 = require("../../enumerations/AccountStatuses");
var SearchResult_1 = require("../../models/SearchResult");
var PostReportManagementComponent = (function () {
    //#endregion
    //#region Constructor
    // Initiate instance of component.
    function PostReportManagementComponent(clientConfigurationService, clientCommonService, clientApiService, clientTimeService, clientPostReportService, clientPostService, clientCommentService, clientAccountService) {
        this.clientConfigurationService = clientConfigurationService;
        this.clientCommonService = clientCommonService;
        this.clientApiService = clientApiService;
        this.clientTimeService = clientTimeService;
        this.clientPostReportService = clientPostReportService;
        this.clientPostService = clientPostService;
        this.clientCommentService = clientCommentService;
        this.clientAccountService = clientAccountService;
        // Initiate post reports search result.
        this.postReportsSearchResult = new SearchResult_1.SearchResult();
    }
    //#endregion
    //#region Methods
    // Callback which is fired when component has been initiated successfully.
    PostReportManagementComponent.prototype.ngOnInit = function () {
        this.findPostReportConditions = new SearchPostReportsViewModel_1.SearchPostReportsViewModel();
        // Update sort.
        this.findPostReportConditions.direction = SortDirection_1.SortDirection.Ascending;
        // Update pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.page = 0;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();
        this.findPostReportConditions.pagination = pagination;
        // Update the sort property.
        this.findPostReportConditions.sort = PostReportSortProperty_1.PostReportSortProperty.id;
        // Update unix created.
        var unixCreated = new UnixDateRange_1.UnixDateRange();
        this.findPostReportConditions.created = unixCreated;
    };
    // Callback is fired when search button is clicked.
    PostReportManagementComponent.prototype.clickSearch = function (condition) {
        var _this = this;
        // Make the component be loading.
        this.isLoading = true;
        this.clientPostReportService.getPostReports(condition)
            .then(function (x) {
            // Response is invalid.
            if (x == null)
                return;
            // Find post reports search result.
            _this.postReportsSearchResult = x.json();
            // Cancel loading state.
            _this.isLoading = false;
        })
            .catch(function (x) {
            // Response is invalid.
            if (x == null)
                return;
            // Cancel loading state.
            _this.isLoading = false;
            // Handle common business.
            _this.clientApiService.handleInvalidResponse(x);
        });
    };
    // Delete post report by searching for specific index.
    PostReportManagementComponent.prototype.clickDeletePostReport = function (postReport, deletePostReportConfirmModal) {
        // Invalid index.
        if (postReport == null)
            return;
        // Update the selected post report.
        this.selectPostReport = postReport;
        // Display confirmation dialog first.
        deletePostReportConfirmModal.show();
    };
    // This callback is fired when post report is confirmed to be deleted.
    PostReportManagementComponent.prototype.clickConfirmDeletePostReport = function (deletePostReportConfirmModal) {
        var _this = this;
        // Post report is invalid.
        if (this.selectPostReport == null)
            return;
        var conditions = new SearchPostReportsViewModel_1.SearchPostReportsViewModel();
        conditions.id = this.selectPostReport.id;
        // Make components be loaded.
        this.isLoading = true;
        // Close the modal first.
        if (deletePostReportConfirmModal != null)
            deletePostReportConfirmModal.hide();
        // Reset the selected item to null.
        this.selectPostReport = null;
        this.clientPostReportService.deletePostReports(conditions)
            .then(function (response) {
            // Reload the list.
            _this.clickSearch(_this.findPostReportConditions);
        })
            .catch(function (response) {
            // Proceed common business handling.
            _this.clientApiService.handleInvalidResponse(response);
            // Cancel the loading state.
            _this.isLoading = false;
        });
    };
    // Pick an account and monitor its profile.
    PostReportManagementComponent.prototype.monitorAccountProfile = function (account, accountProfileModal) {
        // Pick the account.
        this.monitoringAccountProfile = account;
        // Display profile modal.
        accountProfileModal.show();
    };
    // Find post with its detail.
    PostReportManagementComponent.prototype.clickOpenPostDetailBox = function (index, postDetailBox) {
        var _this = this;
        // Make application understand that a post is being searched.
        this.isSearchingPost = true;
        // Reset the search comments result.
        this.getCommentDetailsResult = new SearchResult_1.SearchResult();
        // Find details of the specific post.
        this.clientPostService.getPostDetails(index)
            .then(function (x) {
            // Cancel the search progress.
            _this.isSearchingPost = false;
            // Get the details.
            var details = x.json();
            _this.monitoringPostDetail = details;
            // Display post detail box.
            postDetailBox.show();
        })
            .catch(function (response) {
            // Cancel the search progress.
            _this.isSearchingPost = false;
            // Handle the common errors.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // This callback is fired when a comment button is searched.
    PostReportManagementComponent.prototype.clickSearchComment = function (page) {
        var _this = this;
        // Page is not correct.
        if (page == null)
            return;
        // Post is incorrect.
        if (this.monitoringPostDetail == null)
            return;
        var pagination = new Pagination_1.Pagination();
        pagination.page = page;
        pagination.records = this.clientConfigurationService.getMinPageRecords();
        var searchCommentsDetailsCondition = new SearchCommentsDetailsViewModel_1.SearchCommentsDetailsViewModel();
        searchCommentsDetailsCondition.postIndex = this.monitoringPostDetail.id;
        searchCommentsDetailsCondition.pagination = pagination;
        // Make component understand comments are loading.
        this.isSearchingComments = true;
        // Search for comments.
        this.clientCommentService.getCommentDetails(searchCommentsDetailsCondition)
            .then(function (x) {
            // Cancel comment loading status.
            _this.isSearchingComments = false;
            // Get the comments search result.
            var getCommentDetailsResult = x.json();
            _this.getCommentDetailsResult = getCommentDetailsResult;
        })
            .catch(function (response) {
            // Cancel comment loading status.
            _this.isSearchingComments = false;
            // Proceed common handling process.
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when change account status button is clicked inside change account information box.
    PostReportManagementComponent.prototype.clickChangeAccountStatus = function (account) {
        var _this = this;
        // Account is invalid.
        if (account == null)
            return;
        switch (account.status) {
            case AccountStatuses_1.AccountStatuses.Disabled:
            case AccountStatuses_1.AccountStatuses.Pending:
                account.status = AccountStatuses_1.AccountStatuses.Active;
                break;
            default:
                account.status = AccountStatuses_1.AccountStatuses.Disabled;
                break;
        }
        //Prevent UI interaction while data is being proceeded.
        this.isLoading = true;
        this.clientAccountService.editUserProfile(account.id, account)
            .then(function (response) {
            // Cancel loading status.
            _this.isLoading = false;
            // Hide the profile box.
            _this.profileBox.hide();
        })
            .catch(function (response) {
            // Cancel loading status.
            _this.isLoading = false;
            // Hide the profile box.
            _this.profileBox.hide();
            _this.clientApiService.handleInvalidResponse(response);
        });
    };
    // Callback which is fired when post details box is hidden.
    PostReportManagementComponent.prototype.onPostDetailsBoxHidden = function () {
        this.monitoringPostDetail = null;
        this.isSearchingComments = false;
        this.isSearchingPost = false;
    };
    return PostReportManagementComponent;
}());
__decorate([
    core_1.ViewChild('accountProfileBox'),
    __metadata("design:type", ng2_bootstrap_1.ModalDirective)
], PostReportManagementComponent.prototype, "profileBox", void 0);
PostReportManagementComponent = __decorate([
    core_1.Component({
        selector: 'post-report-management',
        templateUrl: 'post-report-management.component.html',
        providers: [
            ClientConfigurationService_1.ClientConfigurationService,
            ClientCommonService_1.ClientCommonService
        ]
    }),
    __param(2, core_1.Inject("IClientApiService")),
    __param(3, core_1.Inject("IClientTimeService")),
    __param(4, core_1.Inject("IClientPostReportService")),
    __param(5, core_1.Inject("IClientPostService")),
    __param(6, core_1.Inject("IClientCommentService")),
    __param(7, core_1.Inject("IClientAccountService")),
    __metadata("design:paramtypes", [ClientConfigurationService_1.ClientConfigurationService,
        ClientCommonService_1.ClientCommonService, Object, Object, Object, Object, Object, Object])
], PostReportManagementComponent);
exports.PostReportManagementComponent = PostReportManagementComponent;
//# sourceMappingURL=post-report-management.component.js.map
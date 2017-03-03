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
var ClientCommonService_1 = require("../../services/ClientCommonService");
var ClientTimeService_1 = require("../../services/ClientTimeService");
var ClientApiService_1 = require("../../services/ClientApiService");
var ClientNotificationService_1 = require("../../services/ClientNotificationService");
var ClientAuthenticationService_1 = require("../../services/clients/ClientAuthenticationService");
var ClientPostReportService_1 = require("../../services/clients/ClientPostReportService");
var FindPostReportViewModel_1 = require("../../viewmodels/post-report/FindPostReportViewModel");
var FindPostReportSearchResultViewModel_1 = require("../../viewmodels/post-report/FindPostReportSearchResultViewModel");
var PostReportSortProperty_1 = require("../../enumerations/order/PostReportSortProperty");
var Pagination_1 = require("../../viewmodels/Pagination");
var SortDirection_1 = require("../../enumerations/SortDirection");
var UnixDateRange_1 = require("../../viewmodels/UnixDateRange");
var account_profile_box_component_1 = require("../account-management/account-profile-box.component");
var ClientPostService_1 = require("../../services/clients/ClientPostService");
var ClientCommentService_1 = require("../../services/clients/ClientCommentService");
var SearchCommentsDetailsViewModel_1 = require("../../viewmodels/comment/SearchCommentsDetailsViewModel");
var SearchCommentsDetailsResultViewModel_1 = require("../../viewmodels/comment/SearchCommentsDetailsResultViewModel");
var PostReportManagementComponent = (function () {
    // Initiate instance of component.
    function PostReportManagementComponent(clientConfigurationService, clientCommonService, clientApiService, clientTimeService, clientPostReportService, clientPostService, clientCommentService) {
        this.clientConfigurationService = clientConfigurationService;
        this.clientCommonService = clientCommonService;
        this.clientApiService = clientApiService;
        this.clientTimeService = clientTimeService;
        this.clientPostReportService = clientPostReportService;
        this.clientPostService = clientPostService;
        this.clientCommentService = clientCommentService;
        // Initiate post reports search result.
        this.postReportsSearchResult = new FindPostReportSearchResultViewModel_1.FindPostReportSearchResultViewModel();
    }
    // Callback which is fired when component has been initiated successfully.
    PostReportManagementComponent.prototype.ngOnInit = function () {
        this.findPostReportConditions = new FindPostReportViewModel_1.FindPostReportViewModel();
        // Update sort.
        this.findPostReportConditions.direction = SortDirection_1.SortDirection.Ascending;
        // Update pagination.
        var pagination = new Pagination_1.Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
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
        this.clientPostReportService.findPostReports(condition)
            .then(function (response) {
            // Response is invalid.
            if (response == null)
                return;
            var result = response.json();
            _this.postReportsSearchResult.postReports = result['postReports'];
            _this.postReportsSearchResult.total = result['total'];
            // Cancel loading state.
            _this.isLoading = false;
        })
            .catch(function (response) {
            // Response is invalid.
            if (response == null)
                return;
            // Cancel loading state.
            _this.isLoading = false;
            // Handle common business.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
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
        var conditions = new FindPostReportViewModel_1.FindPostReportViewModel();
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
            _this.clientApiService.proceedHttpNonSolidResponse(response);
            // Cancel the loading state.
            _this.isLoading = false;
        });
    };
    // Pick an account and monitor its profile.
    PostReportManagementComponent.prototype.monitorAccountProfile = function (account, accountProfileModal) {
        // Pick the account.
        this.monitoringAccountProfile = account;
        console.log(this.monitoringAccountProfile);
        console.log(accountProfileModal);
        // Display profile modal.
        accountProfileModal.show();
    };
    // Find post with its detail.
    PostReportManagementComponent.prototype.clickOpenPostDetailBox = function (index, postDetailBox) {
        var _this = this;
        // Make application understand that a post is being searched.
        this.isSearchingPost = true;
        // Reset the search comments result.
        this.searchCommentsDetailsResult = new SearchCommentsDetailsResultViewModel_1.SearchCommentsDetailsResultViewModel();
        // Find details of the specific post.
        this.clientPostService.findPostDetails(index)
            .then(function (response) {
            // Cancel the search progress.
            _this.isSearchingPost = false;
            // Get the details.
            var details = response.json();
            _this.monitoringPostDetail = details;
            // Display post detail box.
            postDetailBox.show();
        })
            .catch(function (response) {
            // Cancel the search progress.
            _this.isSearchingPost = false;
            // Handle the common errors.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
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
        pagination.index = page;
        pagination.records = this.clientConfigurationService.getMinPageRecords();
        var searchCommentsDetailsCondition = new SearchCommentsDetailsViewModel_1.SearchCommentsDetailsViewModel();
        searchCommentsDetailsCondition.postIndex = this.monitoringPostDetail.id;
        searchCommentsDetailsCondition.pagination = pagination;
        // Make component understand comments are loading.
        this.isSearchingComments = true;
        // Search for comments.
        this.clientCommentService.searchCommentDetails(searchCommentsDetailsCondition)
            .then(function (response) {
            // Cancel comment loading status.
            _this.isSearchingComments = false;
            // Get the comments search result.
            var commentsDetailsSearchResult = response.json();
            _this.searchCommentsDetailsResult = commentsDetailsSearchResult;
        })
            .catch(function (response) {
            // Cancel comment loading status.
            _this.isSearchingComments = false;
            // Proceed common handling process.
            _this.clientApiService.proceedHttpNonSolidResponse(response);
        });
    };
    return PostReportManagementComponent;
}());
PostReportManagementComponent = __decorate([
    core_1.Component({
        selector: 'post-report-management',
        templateUrl: 'post-report-management.component.html',
        providers: [
            ClientConfigurationService_1.ClientConfigurationService,
            ClientCommonService_1.ClientCommonService,
            ClientTimeService_1.ClientTimeService,
            ClientApiService_1.ClientApiService,
            ClientNotificationService_1.ClientNotificationService,
            ClientAuthenticationService_1.ClientAuthenticationService,
            ClientPostReportService_1.ClientPostReportService,
            ClientPostService_1.ClientPostService,
            ClientCommentService_1.ClientCommentService,
            account_profile_box_component_1.AccountProfileBoxComponent
        ]
    }),
    __metadata("design:paramtypes", [ClientConfigurationService_1.ClientConfigurationService,
        ClientCommonService_1.ClientCommonService,
        ClientApiService_1.ClientApiService,
        ClientTimeService_1.ClientTimeService,
        ClientPostReportService_1.ClientPostReportService,
        ClientPostService_1.ClientPostService,
        ClientCommentService_1.ClientCommentService])
], PostReportManagementComponent);
exports.PostReportManagementComponent = PostReportManagementComponent;
//# sourceMappingURL=post-report-management.component.js.map
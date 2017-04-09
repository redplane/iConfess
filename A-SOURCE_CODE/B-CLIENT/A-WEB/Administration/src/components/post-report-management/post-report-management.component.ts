import {Component, Inject, OnInit, ViewChild} from "@angular/core";
import {ModalDirective} from "ng2-bootstrap";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {ClientCommonService} from "../../services/ClientCommonService";
import {ClientApiService} from "../../services/ClientApiService";
import {SearchPostReportsViewModel} from "../../viewmodels/post-report/SearchPostReportsViewModel";
import {SearchPostReportsResultViewModel} from "../../viewmodels/post-report/SearchPostReportsResultViewModel";
import {PostReport} from "../../models/PostReport";
import {PostReportSortProperty} from "../../enumerations/order/PostReportSortProperty";
import {Pagination} from "../../viewmodels/Pagination";
import {SortDirection} from "../../enumerations/SortDirection";
import {UnixDateRange} from "../../viewmodels/UnixDateRange";
import {Response} from "@angular/http";
import {AccountProfileBoxComponent} from "../account-management/account-profile-box.component";
import {Account} from "../../models/Account";
import {Post} from "../../models/Post";
import {SearchCommentsDetailsViewModel} from "../../viewmodels/comment/SearchCommentsDetailsViewModel";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {SearchResult} from "../../models/SearchResult";
import {CommentDetailsViewModel} from "../../viewmodels/comment/CommentDetailsViewModel";
import {IClientTimeService} from "../../interfaces/services/IClientTimeService";
import {IClientCommentService} from "../../interfaces/services/api/IClientCommentService";
import {IClientPostReportService} from "../../interfaces/services/api/IClientPostReportService";
import {IClientPostService} from "../../interfaces/services/api/IClientPostService";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";

@Component({
    selector: 'post-report-management',
    templateUrl: 'post-report-management.component.html',
    providers: [
        ClientConfigurationService,
        ClientCommonService,
        AccountProfileBoxComponent
    ]
})

export class PostReportManagementComponent implements OnInit {

    //#region Properties

    // Profile box element on management component.
    @ViewChild('accountProfileBox') profileBox : ModalDirective;

    // Conditions which are used for finding post reports.
    public findPostReportConditions: SearchPostReportsViewModel;

    // Find post reports search result
    public postReportsSearchResult: SearchPostReportsResultViewModel;

    // Result of finding comments of a specific post.
    public getCommentDetailsResult: SearchResult<CommentDetailsViewModel>;

    // Post report which is selected to be deleted.
    public selectPostReport: PostReport;

    // Whether component is being loaded or not.
    public isLoading: boolean;

    // Whether application is searching for comments related to a specific post.
    public isSearchingComments: boolean;

    // Whether application is searching for a specific post's information.
    public isSearchingPost: boolean;

    // Which account profile is being monitored.
    public monitoringAccountProfile: Account;

    // Which post is being monitored.
    public monitoringPostDetail: Post;

    //#endregion

    //#region Constructor

    // Initiate instance of component.
    public constructor(public clientConfigurationService: ClientConfigurationService,
                       public clientCommonService: ClientCommonService,
                       public clientApiService: ClientApiService,
                       @Inject("IClientTimeService") public clientTimeService: IClientTimeService,
                       @Inject("IClientPostReportService") public clientPostReportService: IClientPostReportService,
                       @Inject("IClientPostService") public clientPostService: IClientPostService,
                       @Inject("IClientCommentService") public clientCommentService: IClientCommentService,
                       @Inject("IClientAccountService") public clientAccountService: IClientAccountService) {
        // Initiate post reports search result.
        this.postReportsSearchResult = new SearchPostReportsResultViewModel();
    }

    //#endregion

    //#region Methods

    // Callback which is fired when component has been initiated successfully.
    public ngOnInit(): void {
        this.findPostReportConditions = new SearchPostReportsViewModel();

        // Update sort.
        this.findPostReportConditions.direction = SortDirection.Ascending;

        // Update pagination.
        let pagination = new Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        this.findPostReportConditions.pagination = pagination;

        // Update the sort property.
        this.findPostReportConditions.sort = PostReportSortProperty.id;

        // Update unix created.
        let unixCreated = new UnixDateRange();
        this.findPostReportConditions.created = unixCreated;

    }

    // Callback is fired when search button is clicked.
    public clickSearch(condition: SearchPostReportsViewModel): void {

        // Make the component be loading.
        this.isLoading = true;

        this.clientPostReportService.getPostReports(condition)
            .then((response: Response) => {
                // Response is invalid.
                if (response == null)
                    return;

                let result = response.json();
                this.postReportsSearchResult.postReports = result['postReports'];
                this.postReportsSearchResult.total = result['total'];

                // Cancel loading state.
                this.isLoading = false;
            })
            .catch((response: Response) => {
                // Response is invalid.
                if (response == null)
                    return;

                // Cancel loading state.
                this.isLoading = false;

                // Handle common business.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    // Delete post report by searching for specific index.
    public clickDeletePostReport(postReport: PostReport, deletePostReportConfirmModal: ModalDirective) {

        // Invalid index.
        if (postReport == null)
            return;

        // Update the selected post report.
        this.selectPostReport = postReport;

        // Display confirmation dialog first.
        deletePostReportConfirmModal.show();

    }

    // This callback is fired when post report is confirmed to be deleted.
    public clickConfirmDeletePostReport(deletePostReportConfirmModal: ModalDirective) {

        // Post report is invalid.
        if (this.selectPostReport == null)
            return;


        let conditions = new SearchPostReportsViewModel();
        conditions.id = this.selectPostReport.id;

        // Make components be loaded.
        this.isLoading = true;

        // Close the modal first.
        if (deletePostReportConfirmModal != null)
            deletePostReportConfirmModal.hide();

        // Reset the selected item to null.
        this.selectPostReport = null;

        this.clientPostReportService.deletePostReports(conditions)
            .then((response: Response) => {



                // Reload the list.
                this.clickSearch(this.findPostReportConditions);
            })
            .catch((response: Response) => {

                // Proceed common business handling.
                this.clientApiService.proceedHttpNonSolidResponse(response);

                // Cancel the loading state.
                this.isLoading = false;
            });
    }

    // Pick an account and monitor its profile.
    public monitorAccountProfile(account: Account, accountProfileModal: ModalDirective): void {
        // Pick the account.
        this.monitoringAccountProfile = account;

        // Display profile modal.
        accountProfileModal.show();
    }

    // Find post with its detail.
    public clickOpenPostDetailBox(index: number, postDetailBox: ModalDirective): void {

        // Make application understand that a post is being searched.
        this.isSearchingPost = true;

        // Reset the search comments result.
        this.getCommentDetailsResult = new SearchResult<CommentDetailsViewModel>();

        // Find details of the specific post.
        this.clientPostService.getPostDetails(index)
            .then((x: Response) => {

                // Cancel the search progress.
                this.isSearchingPost = false;

                // Get the details.
                let details = x.json();
                this.monitoringPostDetail = details;

                // Display post detail box.
                postDetailBox.show();
            })
            .catch((response: Response) => {
                // Cancel the search progress.
                this.isSearchingPost = false;

                // Handle the common errors.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    // This callback is fired when a comment button is searched.
    public clickSearchComment(page: number): void {
        // Page is not correct.
        if (page == null)
            return;

        // Post is incorrect.
        if (this.monitoringPostDetail == null)
            return;

        let pagination = new Pagination();
        pagination.index = page;
        pagination.records = this.clientConfigurationService.getMinPageRecords();

        let searchCommentsDetailsCondition = new SearchCommentsDetailsViewModel();
        searchCommentsDetailsCondition.postIndex = this.monitoringPostDetail.id;
        searchCommentsDetailsCondition.pagination = pagination;

        // Make component understand comments are loading.
        this.isSearchingComments = true;

        // Search for comments.
        this.clientCommentService.getCommentDetails(searchCommentsDetailsCondition)
            .then((x: Response) => {

                // Cancel comment loading status.
                this.isSearchingComments = false;

                // Get the comments search result.
                let getCommentDetailsResult = <SearchResult<CommentDetailsViewModel>> x.json();
                this.getCommentDetailsResult = getCommentDetailsResult;
            })
            .catch((response: Response) => {
                // Cancel comment loading status.
                this.isSearchingComments = false;

                // Proceed common handling process.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            })
    }

    // Callback which is fired when change account status button is clicked inside change account information box.
    public clickChangeAccountStatus(account: Account): void {

        // Account is invalid.
        if (account == null)
            return;

        switch (account.status) {
            case AccountStatuses.Disabled:
            case AccountStatuses.Pending:
                account.status = AccountStatuses.Active;
                break;
            default:
                account.status = AccountStatuses.Disabled;
                break;
        }


        //Prevent UI interaction while data is being proceeded.
        this.isLoading = true;

        this.clientAccountService.editUserProfile(account.id, account)
            .then((response: Response) => {

                // Cancel loading status.
                this.isLoading = false;

                // Hide the profile box.
                this.profileBox.hide();
            })
            .catch((response: Response) => {

                // Cancel loading status.
                this.isLoading = false;

                // Hide the profile box.
                this.profileBox.hide();

                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    // Callback which is fired when post details box is hidden.
    public onPostDetailsBoxHidden(): void {
        this.monitoringPostDetail = null;
        this.isSearchingComments = false;
        this.isSearchingPost = false;
    }

    //#endregion
}
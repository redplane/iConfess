import {Component, OnInit} from "@angular/core";
import {ModalDirective} from "ng2-bootstrap";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {ClientCommonService} from "../../services/ClientCommonService";
import {ClientTimeService} from "../../services/ClientTimeService";
import {ClientApiService} from "../../services/ClientApiService";
import {ClientNotificationService} from "../../services/ClientNotificationService";
import {ClientAuthenticationService} from "../../services/clients/ClientAuthenticationService";
import {ClientPostReportService} from "../../services/clients/ClientPostReportService";
import {FindPostReportViewModel} from "../../viewmodels/post-report/FindPostReportViewModel";
import {FindPostReportSearchResultViewModel} from "../../viewmodels/post-report/FindPostReportSearchResultViewModel";
import {PostReport} from "../../models/PostReport";
import {PostReportSortProperty} from "../../enumerations/order/PostReportSortProperty";
import {Pagination} from "../../viewmodels/Pagination";
import {SortDirection} from "../../enumerations/SortDirection";
import {UnixDateRange} from "../../viewmodels/UnixDateRange";
import {Response} from "@angular/http";
import {AccountProfileBoxComponent} from "../account-management/account-profile-box.component";
import {Account} from "../../models/Account";
import {Post} from "../../models/Post";
import {ClientPostService} from "../../services/clients/ClientPostService";
import {FindCommentResultViewModel} from "../../viewmodels/comment/FindCommentResultViewModel";
import {CommentSearchViewModel} from "../../viewmodels/comment/CommentSearchViewModel";
import {ClientCommentService} from "../../services/clients/ClientCommentService";

@Component({
    selector: 'post-report-management',
    templateUrl: 'post-report-management.component.html',
    providers:[
        ClientConfigurationService,
        ClientCommonService,
        ClientTimeService,
        ClientApiService,
        ClientNotificationService,
        ClientAuthenticationService,
        ClientPostReportService,
        ClientPostService,
        ClientCommentService,

        AccountProfileBoxComponent
    ]
})

export class PostReportManagementComponent implements OnInit{

    // Conditions which are used for finding post reports.
    public findPostReportConditions: FindPostReportViewModel;

    // Find post reports search result
    public postReportsSearchResult: FindPostReportSearchResultViewModel;

    // Result of finding comments of a specific post.
    public searchCommentsResult: FindCommentResultViewModel;

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

    // Initiate instance of component.
    public constructor(public clientConfigurationService: ClientConfigurationService,
                       public clientCommonService: ClientCommonService,
                       public clientApiService: ClientApiService,
                       public clientTimeService: ClientTimeService,
                       public clientPostReportService: ClientPostReportService,
                       public clientPostService: ClientPostService,
                       public clientCommentService: ClientCommentService){
        // Initiate post reports search result.
        this.postReportsSearchResult = new FindPostReportSearchResultViewModel();
    }

    // Callback which is fired when component has been initiated successfully.
    public ngOnInit(): void {
        this.findPostReportConditions = new FindPostReportViewModel();

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
    public clickSearch(condition: FindPostReportViewModel): void{

        // Make the component be loading.
        this.isLoading = true;

        this.clientPostReportService.findPostReports(condition)
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
    public clickDeletePostReport(postReport: PostReport, deletePostReportConfirmModal: ModalDirective){

        // Invalid index.
        if (postReport == null)
            return;

        // Update the selected post report.
        this.selectPostReport = postReport;

        // Display confirmation dialog first.
        deletePostReportConfirmModal.show();

    }

    // This callback is fired when post report is confirmed to be deleted.
    public clickConfirmDeletePostReport(deletePostReportConfirmModal: ModalDirective){

        // Post report is invalid.
        if (this.selectPostReport == null)
            return;



        let conditions = new FindPostReportViewModel();
        conditions.id = this.selectPostReport.id;

        // Make components be loaded.
        this.isLoading = true;

        // Close the modal first.
        if (deletePostReportConfirmModal != null)
            deletePostReportConfirmModal.hide();

        // Reset the selected item to null.
        this.selectPostReport = null;

        this.clientPostReportService.deletePostReports(conditions)
            .then((response: Response) =>{



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
    public monitorAccountProfile(account: Account, accountProfileModal: ModalDirective): void{
        // Pick the account.
        this.monitoringAccountProfile = account;
        console.log(this.monitoringAccountProfile);

        console.log(accountProfileModal);

        // Display profile modal.
        accountProfileModal.show();
    }

    // Find post with its detail.
    public clickOpenPostDetailBox(index: number, postDetailBox: ModalDirective): void{

        // Make application understand that a post is being searched.
        this.isSearchingPost = true;

        // Reset the search comments result.
        this.searchCommentsResult = new FindCommentResultViewModel();

        // Find details of the specific post.
        this.clientPostService.findPostDetails(index)
            .then((response: Response) =>{

                // Cancel the search progress.
                this.isSearchingPost = false;

                // Get the details.
                let details = response.json();
                this.monitoringPostDetail = details;

                // Display post detail box.
                postDetailBox.show();
            })
            .catch((response: Response) =>{
                // Cancel the search progress.
                this.isSearchingPost = false;

                // Handle the common errors.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    // This callback is fired when a comment button is searched.
    public clickSearchComment(page: number): void{

        // Page is not correct.
        if (page == null)
            return;

        // Post is incorrect.
        if (this.monitoringPostDetail == null)
            return;

        let pagination = new Pagination();
        pagination.index = page;
        pagination.records = this.clientConfigurationService.getMinPageRecords();

        let commentsSearchCondition = new CommentSearchViewModel();
        commentsSearchCondition.postIndex = this.monitoringPostDetail.id;
        commentsSearchCondition.pagination = pagination;

        // Make component understand comments are loading.
        this.isSearchingComments = true;

        // Search for comments.
        this.clientCommentService.searchComments(commentsSearchCondition)
            .then((response: Response) => {

                // Cancel comment loading status.
                this.isSearchingComments = false;

                // Get the comments search result.
                let commentSearchResult = response.json();
                console.log(commentSearchResult);
                this.searchCommentsResult = commentSearchResult;
            })
            .catch((response: Response) => {
                // Cancel comment loading status.
                this.isSearchingComments = false;

                // Proceed common handling process.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            })
    }
}
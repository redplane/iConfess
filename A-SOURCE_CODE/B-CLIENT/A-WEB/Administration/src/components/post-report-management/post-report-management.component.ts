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
        ClientPostReportService
    ]
})

export class PostReportManagementComponent implements OnInit{

    // Conditions which are used for finding post reports.
    public findPostReportConditions: FindPostReportViewModel;

    // Find post reports search result
    public postReportsSearchResult: FindPostReportSearchResultViewModel;

    // Post report which is selected to be deleted.
    public selectPostReport: PostReport;

    // Whether component is being loaded or not.
    public isLoading: boolean;

    // Initiate instance of component.
    public constructor(public clientConfigurationService: ClientConfigurationService,
                       public clientCommonService: ClientCommonService,
                       public clientApiService: ClientApiService,
                       public clientTimeService: ClientTimeService,
                       public clientPostReportService: ClientPostReportService ){
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
}
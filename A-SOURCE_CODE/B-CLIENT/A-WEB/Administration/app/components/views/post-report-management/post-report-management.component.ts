import {Component, OnInit} from "@angular/core";
import {FindPostReportViewModel} from "../../../viewmodels/post-report/FindPostReportViewModel";
import {Pagination} from "../../../viewmodels/Pagination";
import {ClientConfigurationService} from "../../../services/ClientConfigurationService";
import {UnixDateRange} from "../../../viewmodels/UnixDateRange";
import {SortDirection} from "../../../enumerations/SortDirection";
import {PostReportSortProperty} from "../../../enumerations/order/PostReportSortProperty";
import {FindPostReportSearchResultViewModel} from "../../../viewmodels/post-report/FindPostReportSearchResultViewModel";
import {ClientCommonService} from "../../../services/ClientCommonService";
import {ClientTimeService} from '../../../services/ClientTimeService';
import {ClientPostReportService} from "../../../services/clients/ClientPostReportService";
import {Response} from "@angular/http";
import {ClientApiService} from "../../../services/ClientApiService";
import {ClientNotificationService} from "../../../services/ClientNotificationService";
import {ClientAuthenticationService} from "../../../services/clients/ClientAuthenticationService";

@Component({
    selector: 'post-report-management',
    templateUrl: './app/views/views/post-report-management/post-report-management.component.html',
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
}
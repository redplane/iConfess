import {Component, EventEmitter, Inject} from "@angular/core";
import {FormGroup, FormBuilder} from "@angular/forms";
import {Response} from "@angular/http";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {SearchPostReportsViewModel} from "../../viewmodels/post-report/SearchPostReportsViewModel";
import {Account} from "../../models/entities/Account";
import {Post} from "../../models/entities/Post";
import {TextSearch} from "../../viewmodels/TextSearch";
import {TextSearchMode} from "../../enumerations/TextSearchMode";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {Pagination} from "../../viewmodels/Pagination";
import {SearchPostsViewModel} from "../../viewmodels/post/SearchPostsViewModel";
import {IClientPostService} from "../../interfaces/services/api/IClientPostService";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";

@Component({
    selector: 'post-report-find-box',
    templateUrl: 'post-report-find-box.component.html',
    inputs: ['conditions', 'isLoading'],
    outputs:['search'],
    providers: [
        ClientConfigurationService
    ]
})

export class PostReportFindBoxComponent {

    //#region Properties

    // Conditions which are used for finding post report.
    public conditions: SearchPostReportsViewModel;

    // Find post report control group.
    public findPostReportBox: FormGroup;

    // List of reporters in database.
    public reporters: Array<Account>;

    // List of post owners in database.
    public owners: Array<Account>;

    // List of posts in database.
    private posts: Array<Post>;

    // Find post report emitter
    private search: EventEmitter<SearchPostReportsViewModel>;

    //#endregion

    //#region Constructor

    // Initiate post report component.
    public constructor(public clientConfigurationService: ClientConfigurationService,
                       @Inject("IClientAccountService") public clientAccountService: IClientAccountService,
                       @Inject("IClientPostService") public clientPostService: IClientPostService,
                       @Inject("IClientApiService") public clientApiService: IClientApiService,
                       public formBuilder: FormBuilder) {

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

        this.posts = new Array<Post>();
        this.owners = new Array<Account>();
        this.reporters = new Array<Account>();

        // Initiate emitter.
        this.search = new EventEmitter<SearchPostReportsViewModel>();
    }

    //#endregion

    //#region Methods

    // Callback which is fired when control is starting to load data of accounts from service.
    public loadPostReporters(): void {
        let email = new TextSearch();
        email.mode = TextSearchMode.contains;
        email.value = this.findPostReportBox.controls['postReporterIndex'].value;

        // Initiate find account conditions.
        let findAccountsViewModel = new SearchAccountsViewModel();

        // Update account which should be searched for.
        findAccountsViewModel.email = email;

        // All statuses can be found.
        findAccountsViewModel.statuses = null;

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();

        // Pagination update.
        findAccountsViewModel.pagination = pagination;

        // Find reporters by using specific conditions.
        this.clientAccountService.getAccounts(findAccountsViewModel)
            .then((response: Response | any) => {

                // Analyze find account response view model.
                let findAccountResult = response.json();

                // Find list of accounts which has been responded from service.
                this.reporters = findAccountResult.accounts;
            })
            .catch((response: any) => {
                // Handle failed response.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Callback which is fired when control is starting to load data of accounts from service.
    public loadPostOwners(): void {
        let email = new TextSearch();
        email.mode = TextSearchMode.contains;
        email.value = this.findPostReportBox.controls['postOwnerIndex'].value;

        // Initiate find account conditions.
        let findAccountsViewModel = new SearchAccountsViewModel();

        // Update account which should be searched for.
        findAccountsViewModel.email = email;

        // All statuses can be found.
        findAccountsViewModel.statuses = null;

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();

        // Pagination update.
        findAccountsViewModel.pagination = pagination;

        // Find reporters by using specific conditions.
        this.clientAccountService.getAccounts(findAccountsViewModel)
            .then((response: Response | any) => {

                // Analyze find account response view model.
                let findAccountResult = response.json();

                // Find list of accounts which has been responded from service.
                this.owners = findAccountResult.accounts;
            })
            .catch((response: any) => {
                // Handle failed response.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Callback which is fired when post title input field is inputed.
    public loadPostTitles(): void {

        // Initiate find account conditions.
        let findPostsViewModel = new SearchPostsViewModel();

        // Update title search.
        let title = new TextSearch();
        title.mode = TextSearchMode.contains;
        title.value = this.findPostReportBox.controls['postIndex'].value;

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();

        // Title update.
        findPostsViewModel.title = title;

        // Pagination update.
        findPostsViewModel.pagination = pagination;

        // Find reporters by using specific conditions.
        this.clientPostService.getPosts(findPostsViewModel)
            .then((response: Response | any) => {

                // Analyze find account response view model.
                let findPostsResult = response.json();

                // Find list of accounts which has been responded from service.
                this.posts = findPostsResult.posts;
            })
            .catch((response: any) => {
                // Handle failed response.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Find accounts list and bind it to typeahead control.
    // This callback is fired when account is typed in control.
    public selectPostReporter(event: any): void {

        // Find account which has been selected.
        let account = event.item;

        // Account is invalid.
        if (account == null)
            return;

        // Account doesn't have id column.
        if (account['id'] == null)
            return;

        this.conditions.postReporterIndex = account.id;
    }

    // Find accounts list and bind it to typeahead control.
    // This callback is fired when account is typed in control.
    public selectPostOwner(event: any): void {

        // Find account which has been selected.
        let account = event.item;

        // Account is invalid.
        if (account == null)
            return;

        // Account doesn't have id column.
        if (account['id'] == null)
            return;

        this.conditions.postOwnerIndex = account.id;
    }

    // This callback is fired when search button is clicked.
    public clickSearch(): void{

        // Copy the object.
        let condition = JSON.parse(JSON.stringify(this.conditions));
        let created = condition['created'];
        if (created != null){

            let date: Date;

            if (created['from'] != null){
                date = new Date(created['from']);
                created['from'] = date.getTime();
            }

            if (created['to'] != null){
                date = new Date(created['to']);
                created['to'] = date.getTime();
            }
        }

        if (this.search != null) {
            this.search.emit(condition);
        }
        return;
    }

    //#endregion
}
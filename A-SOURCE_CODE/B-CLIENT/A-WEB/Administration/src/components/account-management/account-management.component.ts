import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {Response} from "@angular/http";
import {ModalDirective} from "ng2-bootstrap";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {ClientApiService} from "../../services/ClientApiService";
import {ClientCommonService} from "../../services/ClientCommonService";
import {Account} from "../../models/entities/Account";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {Pagination} from "../../viewmodels/Pagination";
import {AccountSummaryStatusViewModel} from "../../viewmodels/accounts/AccountSummaryStatusViewModel";
import {SearchResult} from "../../models/SearchResult";
import {IClientTimeService} from "../../interfaces/services/IClientTimeService";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";

@Component({
    selector: 'account-management',
    templateUrl: 'account-management.component.html',
    providers: [
        ClientConfigurationService,
        ClientCommonService
    ]
})

export class AccountManagementComponent implements OnInit {

    //#region Properties

    // Inject change account modal from view.
    @ViewChild("changeAccountInfoModal")
    public changeAccountInfoModal: ModalDirective;

    // List of accounts.
    public searchResult: SearchResult<Account>;

    // Account which is being selected for editing / being deleted.
    private account: Account;

    // List of conditions to search for accounts.
    private conditions: SearchAccountsViewModel;

    // Whether components are busy or not.
    private isBusy: boolean;

    // Account status enumeration.
    private accountStatuses = AccountStatuses;

    // Account statuses summary.
    private summaries: Array<AccountSummaryStatusViewModel>;

    //#endregion

    //#region Constructor

    // Initiate component with injections.
    public constructor(private clientConfigurationService: ClientConfigurationService,
                       @Inject("IClientAccountService") private clientAccountService: IClientAccountService,
                       private clientCommonService: ClientCommonService,
                       @Inject("IClientApiService") private clientApiService: IClientApiService,
                       @Inject("IClientTimeService") private clientTimeService: IClientTimeService) {

        // Initiate search conditions.
        this.conditions = new SearchAccountsViewModel();

        // Initiate search result.
        this.searchResult = new SearchResult<Account>();

        // Initiate account statuses summary.
        this.summaries = new Array<AccountSummaryStatusViewModel>();

        // Initiate account instance to prevent it from being null.
        this.account = new Account();
    }

    //#endregion

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {

        // Make component be busy.
        this.isBusy = true;

        this.clientAccountService.getAccounts(this.conditions)
            .then((x: Response) => {

                // Find list of accounts which has been found from service.
                this.searchResult = <SearchResult<Account>> x.json();

                // Cancel loading.
                this.isBusy = false;
            })
            .catch((x: Response) => {

                // Cancel loading.
                this.isBusy = false;

                // Proceed non-solid response handling.
                this.clientApiService.handleInvalidResponse(x);

            });
    }

    // Callback which is fired when change account information button is clicked.
    public clickChangeAccountInfo(account: Account): void {
        // Update account information which should be edited.
        this.account = account;

        // Display modal.
        this.changeAccountInfoModal.show();
    }

    // Callback which is fired when change account information ok button is clicked.
    public clickConfirmAccountInfo(): void {

        // No account has been selected for edit.
        if (this.account == null) {
            // Close the dialog.
            this.changeAccountInfoModal.hide();
            return;
        }

        // Set components to loading state.
        this.isBusy = true;

        // Send request to service to change account information.
        this.clientAccountService.editUserProfile(this.account.id, this.account)
            .then((response: Response) => {

                // Cancel loading.
                this.isBusy = false;

                // Close the dialog.
                this.changeAccountInfoModal.hide();

                // Reload the page.
                this.clickSearch();
            })
            .catch((response: Response) => {

                // Cancel loading process.
                this.isBusy = false;

                // Close the dialog.
                this.changeAccountInfoModal.hide();

                // Handle common error response.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Callback which is fired when paging item is clicked.
    public clickPageChange(parameters: any): void {

        if (parameters == null || parameters['page'] == null)
            return;

        // Update page index.
        this.conditions.pagination.index = parameters['page'];

        // Search.
        this.clickSearch();
    }

    // Check whether account search result is available or not.
    public isResultAvailable(): boolean {

        // Check search result.
        let result = this.searchResult;
        if (result == null)
            return false;

        // No account has been found,
        return !(result.records == null || result.records.length < 1);
    }

    // Called when component has been successfully rendered.
    public ngOnInit(): void {
        // Components are not busy loading.
        this.isBusy = false;

        // Initiate category search conditions.
        this.conditions = new SearchAccountsViewModel();

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();
        this.conditions.pagination = pagination;

        // Initiate account statuses.
        let accountStatuses = new Array<AccountStatuses>();
        for (let index = 0; index < this.clientConfigurationService.accountStatusSelections.keys().length; index++) {
            // Find the key.
            let key = this.clientConfigurationService.accountStatusSelections.keys()[index];
            accountStatuses.push(this.clientConfigurationService.accountStatusSelections.item(key));
        }
        this.conditions.statuses = accountStatuses;

        // Load all accounts from service.
        this.clickSearch();
    }
}

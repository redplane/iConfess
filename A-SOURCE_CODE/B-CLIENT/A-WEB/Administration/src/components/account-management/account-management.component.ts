import {Component, OnInit} from '@angular/core';
import {Response} from "@angular/http";
import {ModalDirective} from "ng2-bootstrap";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {ClientNotificationService} from "../../services/ClientNotificationService";
import {ClientAuthenticationService} from "../../services/clients/ClientAuthenticationService";
import {ClientAccountService} from "../../services/clients/ClientAccountService";
import {ClientApiService} from "../../services/ClientApiService";
import {ClientCommonService} from "../../services/ClientCommonService";
import {SearchAccountsResultViewModel} from "../../viewmodels/accounts/SearchAccountsResultViewModel";
import {Account} from "../../models/Account";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {Pagination} from "../../viewmodels/Pagination";
import {ClientTimeService} from "../../services/ClientTimeService";
import {AccountSummaryStatusViewModel} from "../../viewmodels/accounts/AccountSummaryStatusViewModel";

@Component({
    selector: 'account-management',
    templateUrl: 'account-management.component.html',
    providers: [
        ClientConfigurationService,
        ClientNotificationService,
        ClientAuthenticationService,
        ClientAccountService,
        ClientApiService,
        ClientCommonService,
        ClientTimeService
    ]
})

export class AccountManagementComponent implements OnInit {

    // List of accounts.
    private findAccountsResult: SearchAccountsResultViewModel;

    // Account which is being selected for editing.
    private selectedAccount: Account;

    // List of conditions to search for accounts.
    private conditions: SearchAccountsViewModel;

    // Whether components are busy or not.
    private isLoading: boolean;

    // Account status enumeration.
    private accountStatuses = AccountStatuses;

    // Account statuses summary.
    private summaries: Array<AccountSummaryStatusViewModel>;

    // Initiate component with injections.
    public constructor(private clientConfigurationService: ClientConfigurationService,
                       private clientAccountService: ClientAccountService,
                       private clientCommonService: ClientCommonService,
                       private clientApiService: ClientApiService,
                       private clientTimeService: ClientTimeService) {

        // Initiate search conditions.
        this.conditions = new SearchAccountsViewModel();

        // Initiate find accounts result.
        this.findAccountsResult = new SearchAccountsResultViewModel();

        // Initiate account statuses summary.
        this.summaries = new Array<AccountSummaryStatusViewModel>();
    }

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {
        // Freeze the find box.
        this.isLoading = true;

        this.clientAccountService.findAccounts(this.conditions)
            .then((response: Response | any) => {

                // Find list of accounts which has been found from service.
                let findAccountsResult = response.json();
                this.findAccountsResult = <SearchAccountsResultViewModel> findAccountsResult;

                // Cancel loading.
                this.isLoading = false;
            })
            .catch((response: Response | any) => {

                // Cancel loading.
                this.isLoading = false;

                // Proceed non-solid response handling.
                this.clientApiService.proceedHttpNonSolidResponse(response);

            });
    }

    // Callback which is fired when change account information button is clicked.
    public clickChangeAccountInfo(account: Account, changeAccountInfoModal: ModalDirective) {
        this.selectedAccount = account;
        changeAccountInfoModal.show();
    }

    // Callback which is fired when change account information ok button is clicked.
    public clickConfirmChangeAccountDetail(changeAccountModal: ModalDirective): void {

        // No account has been selected for edit.
        if (this.selectedAccount == null) {
            // Close the dialog.
            changeAccountModal.hide();
            return;
        }

        // Set components to loading state.
        this.isLoading = true;

        // Send request to service to change account information.
        this.clientAccountService.changeAccountInformation(this.selectedAccount.id, this.selectedAccount)
            .then((response: Response) => {

                // Cancel loading.
                this.isLoading = false;

                // Close the dialog.
                changeAccountModal.hide();

                // Reload the page.
                this.clickSearch();
            })
            .catch((response: Response) => {

                // Cancel loading process.
                this.isLoading = false;

                // Close the dialog.
                changeAccountModal.hide();

                // Handle common error response.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    // Callback which is fired when paging item is clicked.
    public clickPageChange(parameters: any): void {

        // Update page index.
        this.conditions.pagination.index = parameters['page'];

        // Search.
        this.clickSearch();
    }

    // Check whether account search result is available or not.
    public isAccountSearchResultAvailable(): boolean {

        // Check search result.
        let result = this.findAccountsResult;
        if (result == null)
            return false;

        // No account has been found,
        if (result.accounts == null || result.accounts.length < 1)
            return false;

        return true;
    }

    // Called when component has been successfully rendered.
    public ngOnInit(): void {
        // Components are not busy loading.
        this.isLoading = false;

        // Initiate category search conditions.
        this.conditions = new SearchAccountsViewModel();

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        this.conditions.pagination = pagination;

        // Initiate account statuses.
        let accountStatuses = new Array<AccountStatuses>();
        for (let index = 0; index < this.clientConfigurationService.accountStatusSelections.keys().length; index++) {
            // Find the key.
            let key = this.clientConfigurationService.accountStatusSelections.keys()[index];
            accountStatuses.push(this.clientConfigurationService.accountStatusSelections.item(key));
        }
        this.conditions.statuses = accountStatuses;

        // Summarize accounts by their statuses.
        this.clientAccountService.summarizeAccountStatus()
            .then((response: Response) => {
                this.summaries = <Array<AccountSummaryStatusViewModel>>response.json();
            })
            .catch((response: Response) => {
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }
}

import {Component, OnInit} from '@angular/core';
import {FindAccountsResultViewModel} from "../viewmodels/accounts/FindAccountsResultViewModel";
import {FindAccountsViewModel} from "../viewmodels/accounts/FindAccountsViewModel";
import {Pagination} from "../viewmodels/Pagination";
import {ClientConfigurationService} from "../services/ClientConfigurationService";
import {ClientNotificationService} from "../services/ClientNotificationService";
import {ClientAuthenticationService} from "../services/clients/ClientAuthenticationService";
import {ClientAccountService} from "../services/clients/ClientAccountService";
import {Response} from "@angular/http";
import {ClientApiService} from "../services/ClientApiService";
import {AccountStatuses} from "../enumerations/AccountStatuses";
import {Account} from "../models/Account";
import {ModalDirective} from "ng2-bootstrap";

@Component({
    selector: 'account-management',
    templateUrl: './app/views/pages/account-management.component.html',
    providers: [
        ClientConfigurationService,
        ClientNotificationService,
        ClientAuthenticationService,
        ClientAccountService,
        ClientApiService
    ]
})

export class AccountManagementComponent implements OnInit{

    // List of accounts.
    private findAccountsResult : FindAccountsResultViewModel;

    // Account which is being selected for editing.
    private selectedAccount: Account;

    // List of conditions to search for accounts.
    private conditions: FindAccountsViewModel;

    // Whether components are busy or not.
    private isLoading: boolean;

    // Initiate component with injections.
    public constructor(private clientConfigurationService: ClientConfigurationService,
                       private clientAccountService: ClientAccountService,
                       private clientApiService: ClientApiService){

        // Initiate search conditions.
        this.conditions = new FindAccountsViewModel();

        // Initiate find accounts result.
        this.findAccountsResult = new FindAccountsResultViewModel();
    }

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {
        // Freeze the find box.
        this.isLoading = true;

        this.clientAccountService.findAccounts(this.conditions)
            .then((response: Response | any) =>{

                // Find list of accounts which has been found from service.
                let findAccountsResult = response.json();
                this.findAccountsResult = <FindAccountsResultViewModel> findAccountsResult;

                // Cancel loading.
                this.isLoading = false;
            })
            .catch((response: Response | any) =>{

                // Cancel loading.
                this.isLoading = false;

                // Proceed non-solid response handling.
                this.clientApiService.proceedHttpNonSolidResponse(response);

            });
    }

    // Callback which is fired when change account information button is clicked.
    public clickChangeAccountInfo(account: Account, changeAccountInfoModal: ModalDirective){
        this.selectedAccount = account;
        changeAccountInfoModal.show();
    }

    // Callback which is fired when change account information ok button is clicked.
    public clickConfirmChangeAccountDetail(changeAccountModal: ModalDirective): void{

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

    // Check whether account search result is available or not.
    public isAccountSearchResultAvailable(): boolean{

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
        this.conditions = new FindAccountsViewModel();

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        this.conditions.pagination = pagination;

        // Initiate account statuses.
        let accountStatuses = new Array<AccountStatuses>();
        for (let index = 0; index < this.clientConfigurationService.accountStatusSelections.keys().length; index++){
            // Find the key.
            let key = this.clientConfigurationService.accountStatusSelections.keys()[index];
            accountStatuses.push(this.clientConfigurationService.accountStatusSelections.item(key));
        }
        this.conditions.statuses = accountStatuses;
    }
}

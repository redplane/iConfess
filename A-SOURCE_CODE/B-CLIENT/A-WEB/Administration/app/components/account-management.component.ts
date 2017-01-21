import {Component, OnInit} from '@angular/core';
import {AccountSearchDetailViewModel} from "../viewmodels/accounts/AccountSearchDetailViewModel";
import {Account} from "../models/Account";
import {AccountStatuses} from "../enumerations/AccountStatuses";
import {FindAccountsViewModel} from "../viewmodels/accounts/FindAccountsViewModel";
import {Pagination} from "../viewmodels/Pagination";
import {ClientConfigurationService} from "../services/ClientConfigurationService";
import {ClientNotificationService} from "../services/ClientNotificationService";
import {ClientAuthenticationService} from "../services/clients/ClientAuthenticationService";

@Component({
    selector: 'account-management',
    templateUrl: './app/views/pages/account-management.component.html',
    providers: [
        ClientConfigurationService,
        ClientNotificationService,
        ClientAuthenticationService
    ]
})

export class AccountManagementComponent implements OnInit{

    // List of accounts.
    private _findAccountsResult : AccountSearchDetailViewModel;

    // List of conditions to search for accounts.
    private _findAccountConditions: FindAccountsViewModel;

    // Whether components are busy or not.
    private _isLoading: boolean;

    // Initiate component with injections.
    public constructor(private clientConfigurationService: ClientConfigurationService){
        this._findAccountConditions = new FindAccountsViewModel();
    }

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(): void {
        // Freeze the find box.
        this._isLoading = true;
    }

    // Called when component has been successfully rendered.
    ngOnInit(): void {

        // Initiate forgery results.
        this._findAccountsResult = new AccountSearchDetailViewModel();
        this._findAccountsResult.accounts = new Array<Account>();

        for (let index = 0; index < 10; index++){
            let account = new Account();
            account.id = index;
            account.email = `Email(${index})@yahoo.com.vn`;
            account.status = AccountStatuses.Active;
            account.joined = 0;
            account.lastModified = 0;
            this._findAccountsResult.accounts.push(account);
        }

        this._findAccountsResult.total = 10;

        // Components are not busy loading.
        this._isLoading = false;

        // Initiate category search conditions.
        this._findAccountConditions = new FindAccountsViewModel();

        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();
        this._findAccountConditions.pagination = pagination;
    }
}

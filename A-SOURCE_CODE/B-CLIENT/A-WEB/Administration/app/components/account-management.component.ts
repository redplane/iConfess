import {Component, OnInit} from '@angular/core';
import {AccountSearchDetailViewModel} from "../viewmodels/accounts/AccountSearchDetailViewModel";
import {Account} from "../models/Account";
import {AccountStatuses} from "../enumerations/AccountStatuses";
import {FindAccountsViewModel} from "../viewmodels/accounts/FindAccountsViewModel";
import {Pagination} from "../viewmodels/Pagination";
import {ClientConfigurationService} from "../services/ClientConfigurationService";

@Component({
    selector: 'account-management',
    templateUrl: './app/views/pages/account-management.component.html',
    providers: [
        ClientConfigurationService
    ]
})

export class AccountManagementComponent implements OnInit{

    // List of accounts.
    private _findAccountsResult : AccountSearchDetailViewModel;

    // List of conditions to search for accounts.
    private _findAccountConditions: FindAccountsViewModel;

    // Whether components are busy or not.
    private _isLoading: boolean;

    // Service which handles configuration.
    private _clientConfigurationService: ClientConfigurationService;

    // Initiate component with injections.
    public constructor(clientConfigurationService: ClientConfigurationService){
        this._findAccountConditions = new FindAccountsViewModel();

        // Services injection.
        this._clientConfigurationService = clientConfigurationService;
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

        this._findAccountsResult.total = 100;

        // Components are not busy loading.
        this._isLoading = false;

        // Initiate category search conditions.
        this._findAccountConditions = new FindAccountsViewModel();

        let pagination = new Pagination();
        pagination.index = 1;
        pagination.records = this._clientConfigurationService.findMaxPageRecords();
        this._findAccountConditions.pagination = pagination;
    }

}

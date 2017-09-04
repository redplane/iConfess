import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {Response} from "@angular/http";
import {ModalDirective} from "ng2-bootstrap";
import {Account} from "../../models/entities/Account";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {Pagination} from "../../viewmodels/Pagination";
import {SearchResult} from "../../models/SearchResult";
import {IClientTimeService} from "../../interfaces/services/IClientTimeService";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";
import {IClientCommonService} from "../../interfaces/services/IClientCommonService";
import {Sorting} from "../../models/Sorting";
import {AccountSortProperty} from "../../enumerations/order/AccountSortProperty";
import {SortDirection} from "../../enumerations/SortDirection";
import {AccountProfileBoxComponent} from "./account-profile-box.component";
import {AccountStatuses} from "../../enumerations/AccountStatuses";

@Component({
    selector: 'account-management',
    templateUrl: 'account-management.component.html'
})

export class AccountManagementComponent implements OnInit {

    //#region Properties

    // Inject account profile box component into management component.
    @ViewChild('profileBox')
    private profileBox: AccountProfileBoxComponent;

    // Modal which contains account profile box component.
    @ViewChild('profileBoxContainer')
    private profileBoxContainer: ModalDirective;

    // List of accounts.
    public searchResult: SearchResult<Account>;

    // List of conditions to search for accounts.
    private conditions: SearchAccountsViewModel;

    // Whether components are busy or not.
    private isBusy: boolean;

    // Point to enumeration to take values out.
    private AccountStatuses = AccountStatuses;

    //#endregion

    //#region Constructor

    // Initiate component with injections.
    public constructor(@Inject("IClientAccountService") public clientAccountService: IClientAccountService,
                       @Inject('IClientCommonService') public clientCommonService: IClientCommonService,
                       @Inject("IClientApiService") public clientApiService: IClientApiService,
                       @Inject("IClientTimeService") public clientTimeService: IClientTimeService) {

        // Initiate search conditions.
        let sorting = new Sorting<AccountSortProperty>();
        sorting.direction = SortDirection.Ascending;
        sorting.property = AccountSortProperty.index;

        let pagination = new Pagination();
        pagination.page = 1;
        pagination.records = clientCommonService.getMaxPageRecords();

        this.conditions = new SearchAccountsViewModel();
        this.conditions.sorting = sorting;
        this.conditions.pagination = pagination;

        // Initiate search result.
        this.searchResult = new SearchResult<Account>();
    }

    //#endregion

    //#region Methods

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

        // Update account information into profile box.
        this.profileBox.setProfile(account);

        // Display modal.
        this.profileBoxContainer.show();
    }

    // Callback which is fired when change account information ok button is clicked.
    public clickConfirmAccountInfo(): void {

        // Find account from profile box.
        let account = this.profileBox.getProfile();

        // Account is invalid.
        if (account == null){
            return;
        }

        // Set components to loading state.
        this.isBusy = true;

        // Send request to service to change account information.
        this.clientAccountService.editUserProfile(account.id, account)
            .then((response: Response) => {

                // Cancel loading.
                this.isBusy = false;

                // Close the dialog.
                this.profileBoxContainer.hide();

                // Reload the page.
                this.clickSearch();
            })
            .catch((response: Response) => {

                // Cancel loading process.
                this.isBusy = false;

                // Close the dialog.
                this.profileBoxContainer.hide();

                // Handle common error response.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    // Called when component has been successfully rendered.
    public ngOnInit(): void {

        // Components are not busy loading.
        this.isBusy = false;

        // Load all accounts from service.
        this.clickSearch();
    }

    // Callback which is called when page is clicked on.
    public selectPage(page: number): void{

        let pagination = this.conditions.pagination;
        pagination.page = page;
        this.conditions.pagination = pagination;

        // Update page.
        this.conditions.pagination.page = page;
        // Base on specific condition to load accounts list from database.
        this.clickSearch();
    }

    //#endregion
}

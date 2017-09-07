import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {Response} from "@angular/http";
import {AccountProfileBoxComponent} from "./account-profile-box.component";
import {SearchResult} from "../../../models/search-result";
import {SearchAccountsViewModel} from "../../../viewmodels/accounts/search-accounts.view-model";
import {AccountStatus} from "../../../enumerations/account-status";
import {AccountSortProperty} from "../../../enumerations/order/account-sort-property";
import {SortDirection} from "../../../enumerations/sort-direction";
import {Sorting} from "../../../models/sorting";
import {Pagination} from "../../../models/pagination";
import {IClientCommonService} from "../../../interfaces/services/client-common-service.interface";
import {IClientTimeService} from "../../../interfaces/services/client-time-service.interface";
import {Account} from "../../../models/entities/account";
import {ModalDirective} from "ngx-bootstrap";
import {IAccountService} from "../../../interfaces/services/api/account-service.interface";

@Component({
  selector: 'account-management',
  templateUrl: 'account-management.component.html'
})

export class AccountManagementComponent implements OnInit {

  //#region Properties

  /*
  * Inject account profile box component into management component.
  * */
  @ViewChild('profileBox')
  private profileBox: AccountProfileBoxComponent;

  /*
  * Modal which contains account profile box component.
  * */
  @ViewChild('profileBoxContainer')
  private profileBoxContainer: ModalDirective;

  /*
  * List of accounts.
  * */
  public searchResult: SearchResult<Account>;

  /*
  * List of conditions to search for accounts.
  * */
  private conditions: SearchAccountsViewModel;

  /*
  * Whether components are busy or not.
  * */
  private isBusy: boolean;

  /*
  * Point to enumeration to take values out.
  * */
  private AccountStatus = AccountStatus;

  //#endregion

  //#region Constructor

  /*
  * Initiate component with injections.
  * */
  public constructor(@Inject("IAccountService") public clientAccountService: IAccountService,
                     @Inject('IClientCommonService') public clientCommonService: IClientCommonService,
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

  /*
  * Callback which is fired when search button of category search box is clicked.
  * */
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
      });
  }

  /*
  * Callback which is fired when change account information button is clicked.
  * */
  public clickChangeAccountInfo(account: Account): void {

    // Update account information into profile box.
    this.profileBox.setProfile(account);

    // Display modal.
    this.profileBoxContainer.show();
  }

  /*
  * Callback which is fired when change account information ok button is clicked.
  * */
  public clickConfirmAccountInfo(): void {

    // Find account from profile box.
    let account = this.profileBox.getProfile();

    // Account is invalid.
    if (account == null) {
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
      });
  }

  /*
  * Called when component has been successfully rendered.
  * */
  public ngOnInit(): void {

    // Components are not busy loading.
    this.isBusy = false;

    // Load all accounts from service.
    this.clickSearch();
  }

  /*
  * Callback which is called when page is clicked on.
  * */
  public selectPage(page: number): void {

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

/*
 * Contains common business calculation.
 * */
import {Pagination} from "../models/pagination";
import {Injectable} from "@angular/core";
import {IDictionary} from "../interfaces/dictionary.interface";
import {KeyValuePair} from "../models/key-value-pair";
import {Dictionary} from "../models/dictionary";
import {SortDirection} from "../enumerations/sort-direction";
import {AccountStatus} from "../enumerations/account-status";
import {NgxOrdinaryPagerOption} from "ngx-numeric-paginator/ngx-ordinary-pager/ngx-ordinary-pager-option";
import {IApplicationSettingService} from "../interfaces/services/application-setting-service.interface";

@Injectable()
export class ApplicationSettingService implements IApplicationSettingService {

  //#region Properties


  /*
  * Ordinary pager settings.
  * */
  private pagerOptions: NgxOrdinaryPagerOption;

  // List of account statuses.
  private accountStatuses: IDictionary<AccountStatus>;

  // List of sorting modes.
  private sortDirections: IDictionary<SortDirection>;

  //#endregion

  //#region Constructor

  // Initiate service with settings.
  public constructor() {
  }

  //#endregion

  //#region Methods

  // Find account display from enumeration.
  public getAccountStatusDisplay(accountStatus: AccountStatus): string {
    switch (accountStatus) {
      case AccountStatus.Active:
        return 'Active';
      case AccountStatus.Disabled:
        return 'Disabled';
      case AccountStatus.Pending:
        return 'Pending';
      default:
        return 'N/A';
    }
  }

  // Get maximum number of records which can be displayed on the page.
  public getMaxPageRecords(): number {
    return 20;
  }

  /*
  * Get ordinary pager settings.
  * */
  public getOrdinaryPagerOptions(): NgxOrdinaryPagerOption {
    if (this.pagerOptions != null) {
      return this.pagerOptions;
    }

    this.pagerOptions = new NgxOrdinaryPagerOption();
    this.pagerOptions.class = 'pagination pagination-sm';
    this.pagerOptions.bAutoHide = true;
    this.pagerOptions.itemCount = this.getMaxPageRecords();
    this.pagerOptions.back = 2;
    this.pagerOptions.front = 2;
    this.pagerOptions.bLastButton = true;
    this.pagerOptions.bPreviousButton = true;
    this.pagerOptions.bNextButton = true;
    this.pagerOptions.bLastButton = true;

    return this.pagerOptions;
  }

  // Search account statuses by using keyword.
  public getAccountStatuses(keyword: string): Array<KeyValuePair<AccountStatus>> {
    if (this.accountStatuses == null) {
      this.accountStatuses = new Dictionary<AccountStatus>();
      this.accountStatuses.add('Disabled', AccountStatus.Disabled);
      this.accountStatuses.add('Pending', AccountStatus.Pending);
      this.accountStatuses.add('Active', AccountStatus.Active);
    }

    // Initiate result.
    let result = this.accountStatuses
      .getKeyValuePairs();

    // Keyword is specified. Filter statuses,
    if (keyword != null && keyword.length > 1) {
      result = result.filter((x: KeyValuePair<AccountStatus>) => {
        return x.key.indexOf(keyword) != -1
      })
    }

    return result;
  }

  // Get sort directions.
  public getSortDirections(keyword: string): Array<KeyValuePair<SortDirection>> {
    if (this.sortDirections == null) {
      this.sortDirections = new Dictionary<SortDirection>();
      this.sortDirections.add('Ascending', SortDirection.Ascending);
      this.sortDirections.add('Descending', SortDirection.Descending);
    }

    return this.sortDirections.getKeyValuePairs();
  }

  //#endregion
}

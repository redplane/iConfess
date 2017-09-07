import {TextSearch} from "../../models/text-search";
import {AccountSortProperty} from "../../enumerations/order/account-sort-property";
import {Pagination} from "../../models/pagination";
import {Sorting} from "../../models/sorting";
import {AccountStatus} from "../../enumerations/account-status";
import {Range} from "../../models/range";

export class SearchAccountsViewModel {

  //#region Properties

  /*
  * Email which is used for registering account.
  * */
  public email: TextSearch;

  /*
  * Nickname which is use for display.
  * */
  public nickname: TextSearch;

  /*
  * Account statuses which can be displayed.
  * */
  public statuses: Array<AccountStatus>;

  /*
  * When account joined into system.
  * */
  public joined: Range<number>;

  /*
  * When account was lastly modified.
  * */
  public lastModified: Range<number>;

  /*
  * Results pagination.
  * */
  public pagination: Pagination;

  /*
  * Results sorting.
  * */
  public sorting: Sorting<AccountSortProperty>;

  //#endregion

  //#region Constructor

  /*
  * Initiate find accounts view model with default settings.
  * */
  public constructor() {
    this.email = new TextSearch();
    this.nickname = new TextSearch();
    this.statuses = [];
    this.joined = new Range<number>();
    this.lastModified = new Range<number>();
    this.pagination = new Pagination();
    this.sorting = new Sorting<AccountSortProperty>();
  }

  //#endregion
}

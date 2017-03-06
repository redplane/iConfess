import {TextSearch} from "../TextSearch";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {UnixDateRange} from "../UnixDateRange";
import {AccountSortProperty} from "../../enumerations/order/AccountSortProperty";
import {Pagination} from "../Pagination";
import {SortDirection} from "../../enumerations/SortDirection";

export class SearchAccountsViewModel{

    // Email which is used for registering account.
    public email: TextSearch;

    // Nickname which is use for display.
    public nickname: TextSearch;

    // Account statuses which can be displayed.
    public statuses: Array<AccountStatuses>;

    // When account joined into system.
    public joined: UnixDateRange;

    // When account was lastly modified.
    public lastModified: UnixDateRange;

    // Results pagination.
    public pagination: Pagination;

    // Results sorting.
    public direction: SortDirection;

    // Property which is used for sorting.
    public sort: AccountSortProperty;

    // Initiate find accounts view model with default settings.
    public constructor(){
        this.email = new TextSearch();
        this.nickname = new TextSearch();
        this.statuses = [];
        this.joined = new UnixDateRange();
        this.lastModified = new UnixDateRange();
        this.pagination = new Pagination();
        this.direction = SortDirection.Ascending;
        this.sort = AccountSortProperty.index;
    }
}
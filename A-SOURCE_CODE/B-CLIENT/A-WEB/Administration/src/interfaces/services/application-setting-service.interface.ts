import {AccountStatus} from "../../enumerations/account-status";
import {KeyValuePair} from "../../models/key-value-pair";
import {SortDirection} from "../../enumerations/sort-direction";
import {NgxOrdinaryPagerOption} from "ngx-numeric-paginator/ngx-ordinary-pager/ngx-ordinary-pager-option";

export interface IApplicationSettingService{

    // Find account display from enumeration.
    getAccountStatusDisplay(accountStatus: AccountStatus): string

    // Find maximum number of records which can be displayed on page.
    getMaxPageRecords(): number;

    // Find general settings of numeric pagination.
    getOrdinaryPagerOptions(): NgxOrdinaryPagerOption;

    // Search account statuses by using keyword.
    getAccountStatuses(keyword: string): Array<KeyValuePair<AccountStatus>>;

    // Get sort directions.
    getSortDirections(keyword: string): Array<KeyValuePair<SortDirection>>;
}

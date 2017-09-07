import {Pagination} from "../../models/pagination";
import {AccountStatus} from "../../enumerations/account-status";
import {NgxPaginatorOption} from "ngx-numeric-paginator/ngx-paginator-option";
import {KeyValuePair} from "../../models/key-value-pair";
import {SortDirection} from "../../enumerations/sort-direction";

export interface IClientCommonService{

    findRecordStartIndex(pageIndex: number, pageRecords: number, subtractIndex: boolean);

    // Find the start index of record by calculating pagination instance.
    findRecordStartIndexFromPagination(pagination: Pagination, subtractIndex: boolean);

    // Find account display from enumeration.
    getAccountStatusDisplay(accountStatus: AccountStatus): string

    // Find maximum number of records which can be displayed on page.
    getMaxPageRecords(): number;

    // Find general settings of numeric pagination.
    getPaginationOptions(): NgxPaginatorOption;

    // Search account statuses by using keyword.
    getAccountStatuses(keyword: string): Array<KeyValuePair<AccountStatus>>;

    // Get sort directions.
    getSortDirections(keyword: string): Array<KeyValuePair<SortDirection>>;
}

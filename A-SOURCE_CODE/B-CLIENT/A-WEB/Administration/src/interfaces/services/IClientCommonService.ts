import {Pagination} from "../../viewmodels/Pagination";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {NgxPaginatorOption} from "ngx-numeric-paginator/ngx-paginator-option";
import {KeyValuePair} from "../../models/KeyValuePair";

export interface IClientCommonService{

    findRecordStartIndex(pageIndex: number, pageRecords: number, subtractIndex: boolean);

    // Find the start index of record by calculating pagination instance.
    findRecordStartIndexFromPagination(pagination: Pagination, subtractIndex: boolean);

    // Find account display from enumeration.
    getAccountStatusDisplay(accountStatus: AccountStatuses): string

    // Find maximum number of records which can be displayed on page.
    getMaxPageRecords(): number;

    // Find general settings of numeric pagination.
    getPaginationOptions(): NgxPaginatorOption;

    // Search account statuses by using keyword.
    getAccountStatuses(keyword: string): Array<KeyValuePair<AccountStatuses>>;
}
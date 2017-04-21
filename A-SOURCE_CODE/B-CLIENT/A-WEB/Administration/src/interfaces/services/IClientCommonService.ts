import {Pagination} from "../../viewmodels/Pagination";
import {AccountStatuses} from "../../enumerations/AccountStatuses";

export interface IClientCommonService{

    findRecordStartIndex(pageIndex: number, pageRecords: number, subtractIndex: boolean);

    // Find the start index of record by calculating pagination instance.
    findRecordStartIndexFromPagination(pagination: Pagination, subtractIndex: boolean);

    // Find account display from enumeration.
    findAccountStatusDisplay(accountStatus: AccountStatuses): string
}
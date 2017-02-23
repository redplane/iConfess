/*
* Contains common business calculation.
* */
import {AccountStatuses} from "../enumerations/AccountStatuses";
import {Pagination} from "../viewmodels/Pagination";
export class ClientCommonService{

    // Find the start record index by calculating page index and page records.
    public findRecordStartIndex(pageIndex: number, pageRecords: number, subtractIndex: boolean){

        if (subtractIndex)
            pageIndex -= 1;

        return (pageIndex * pageRecords);
    }

    // Find the start index of record by calculating pagination instance.
    public findRecordStartIndexFromPagination(pagination: Pagination, subtractIndex: boolean){

        // Start from 0.
        if (pagination == null)
            return 0;

        // Page index calculation.
        let pageIndex = pagination.index;
        if (subtractIndex)
            pageIndex--;

        // Start index calculation.
        let startIndex = pageIndex * pagination.records;
        return startIndex;
    }
    // Find account display from enumeration.
    public findAccountStatusDisplay(accountStatus: AccountStatuses): string{
        switch (accountStatus){
            case AccountStatuses.Active:
                return 'Active';
            case AccountStatuses.Disabled:
                return 'Disabled';
            case AccountStatuses.Pending:
                return 'Pending';
            default:
                return 'N/A';
        }
    }
}
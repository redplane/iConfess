/*
 * Contains common business calculation.
 * */
import {Pagination} from "../models/pagination";
import {Injectable} from "@angular/core";
import {IClientCommonService} from "../interfaces/services/client-common-service.interface";
import {NgxPaginatorOption} from "ngx-numeric-paginator/ngx-paginator-option";
import {IDictionary} from "../interfaces/dictionary.interface";
import {KeyValuePair} from "../models/key-value-pair";
import {Dictionary} from "../models/dictionary";
import {SortDirection} from "../enumerations/sort-direction";
import {AccountStatus} from "../enumerations/account-status";

@Injectable()
export class ClientCommonService implements IClientCommonService {

    //#region Properties

    // Paginator settings.
    private paginatorOption: NgxPaginatorOption;

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

    // Find the start record page by calculating page page and page records.
    public findRecordStartIndex(pageIndex: number, pageRecords: number, subtractIndex: boolean) {

        if (subtractIndex)
            pageIndex -= 1;

        return (pageIndex * pageRecords);
    }

    // Find the start page of record by calculating pagination instance.
    public findRecordStartIndexFromPagination(pagination: Pagination, subtractIndex: boolean) {

        // Start from 0.
        if (pagination == null)
            return 0;

        // Page page calculation.
        let pageIndex = pagination.page;
        if (subtractIndex)
            pageIndex--;

        // Start page calculation.
        let startIndex = pageIndex * pagination.records;
        return startIndex;
    }

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

    // Get paginator settings.
    public getPaginationOptions(): NgxPaginatorOption {
        if (this.paginatorOption != null) {
            return this.paginatorOption;
        }

        this.paginatorOption = new NgxPaginatorOption();
        this.paginatorOption.class = 'pagination pagination-sm';
        this.paginatorOption.bAutoHide = true;
        this.paginatorOption.itemCount = this.getMaxPageRecords();
        this.paginatorOption.back = 2;
        this.paginatorOption.front = 2;
        this.paginatorOption.bLastButton = true;
        this.paginatorOption.bPreviousButton = true;
        this.paginatorOption.bNextButton = true;
        this.paginatorOption.bLastButton = true;

        return this.paginatorOption;
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
    public getSortDirections(keyword: string): Array<KeyValuePair<SortDirection>>{
        if (this.sortDirections == null){
            this.sortDirections = new Dictionary<SortDirection>();
            this.sortDirections.add('Ascending', SortDirection.Ascending);
            this.sortDirections.add('Descending', SortDirection.Descending);
        }

        return this.sortDirections.getKeyValuePairs();
    }

    //#endregion
}

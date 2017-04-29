/*
 * Contains common business calculation.
 * */
import {AccountStatuses} from "../enumerations/AccountStatuses";
import {Pagination} from "../viewmodels/Pagination";
import {Injectable} from "@angular/core";
import {IClientCommonService} from "../interfaces/services/IClientCommonService";
import {NgxPaginatorOption} from "ngx-numeric-paginator/ngx-paginator-option";
import {IDictionary} from "../interfaces/IDictionary";
import {Dictionary} from "../viewmodels/Dictionary";
import {KeyValuePair} from "../models/KeyValuePair";

@Injectable()
export class ClientCommonService implements IClientCommonService {

    //#region Properties

    // Paginator settings.
    private paginatorOption: NgxPaginatorOption;

    // List of account statuses.
    private accountStatuses: IDictionary<AccountStatuses>;

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
    public getAccountStatusDisplay(accountStatus: AccountStatuses): string {
        switch (accountStatus) {
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
    public getAccountStatuses(keyword: string): Array<KeyValuePair<AccountStatuses>> {
        if (this.accountStatuses == null) {
            this.accountStatuses = new Dictionary<AccountStatuses>();
            this.accountStatuses.add('Disabled', AccountStatuses.Disabled);
            this.accountStatuses.add('Pending', AccountStatuses.Pending);
            this.accountStatuses.add('Active', AccountStatuses.Active);
        }

        // Initiate result.
        let result = this.accountStatuses
            .getKeyValuePairs();

        // Keyword is specified. Filter statuses,
        if (keyword != null && keyword.length > 1) {
            result = result.filter((x: KeyValuePair<AccountStatuses>) => {
                return x.key.indexOf(keyword) != -1
            })
        }

        return result;
    }

    //#endregion
}
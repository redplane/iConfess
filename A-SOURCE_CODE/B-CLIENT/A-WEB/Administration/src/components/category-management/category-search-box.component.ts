import {Component, Inject, Input, OnInit} from '@angular/core';
import {Response} from "@angular/http";
import {SearchCategoriesViewModel} from "../../viewmodels/category/SearchCategoriesViewModel";
import {Account} from "../../models/entities/Account";
import {Pagination} from "../../viewmodels/Pagination";
import {TextSearch} from "../../viewmodels/TextSearch";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";
import {IClientCommonService} from "../../interfaces/services/IClientCommonService";
import {SearchResult} from "../../models/SearchResult";
import {CategorySortProperty} from "../../enumerations/order/CategorySortProperty";
import {IDictionary} from "../../interfaces/IDictionary";
import {Dictionary} from "../../models/Dictionary";
import {TextSearchMode} from "../../enumerations/TextSearchMode";

@Component({
    selector: 'category-search-box',
    templateUrl: 'category-search-box.component.html'
})

export class CategorySearchBoxComponent implements OnInit{

    //#region Properties

    // Collection of conditions which are used for searching categories.
    @Input('conditions')
    private conditions: SearchCategoriesViewModel;

    // List of accounts which are used for typeahead binding.
    private accounts: Array<Account>;

    // List of properties which can be used for sorting categories.
    private categorySortProperties: IDictionary<CategorySortProperty>;

    //#endregion

    //#region Constructor

    // Initiate component with default dependency injection.
    public constructor(@Inject('IClientAccountService') public clientAccountService: IClientAccountService,
                       @Inject('IClientApiService') public clientApiService: IClientApiService,
                       @Inject('IClientCommonService') public clientCommonService: IClientCommonService) {
        // Initiate account typeahead data.
        this.accounts = new Array<Account>();

        // Initiate category sorting properties.
        this.categorySortProperties = new Dictionary<CategorySortProperty>();
        this.categorySortProperties.add('Index', CategorySortProperty.index);
        this.categorySortProperties.add('Creator', CategorySortProperty.creatorIndex);
        this.categorySortProperties.add('Category name', CategorySortProperty.name);
        this.categorySortProperties.add('Created', CategorySortProperty.created);
        this.categorySortProperties.add('Last modified', CategorySortProperty.lastModified);
    }

    //#endregion

    //#region Methods

    // Callback which is fired when control is starting to load data of accounts from service.
    public loadAccounts(keyword: string): void {

        // Initiate find account conditions.
        let conditions = new SearchAccountsViewModel();

        // Update account which should be searched for.
        if (conditions.email == null)
            conditions.email = new TextSearch();

        conditions.email.value = keyword;
        conditions.email.mode = TextSearchMode.contains;

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.page = 1;
        pagination.records = this.clientCommonService.getMaxPageRecords();

        // Pagination update.
        conditions.pagination = pagination;

        // Find accounts with specific conditions.
        this.clientAccountService.getAccounts(conditions)
            .then((x: Response | any) => {
                // Analyze find account response view model.
                let result = <SearchResult<Account>> x.json();

                // Find list of accounts which has been responded from service.
                this.accounts = result.records;
            })
            .catch((x: Response) => {
                this.clientApiService.handleInvalidResponse(x);
            });
    }

    // Update accounts list.
    public updateAccounts(accounts: Array<Account>): void{
        let ids = accounts.map((x: Account) => {return x.id});
        if (ids != null && ids.length > 0){
            this.conditions.creatorIndex = ids[0];
        } else {
            this.conditions.creatorIndex = null;
        }
    }

    // Callback which is fired when component has been loaded.
    public ngOnInit(): void {
        this.loadAccounts('');
    }

    //#endregion
}
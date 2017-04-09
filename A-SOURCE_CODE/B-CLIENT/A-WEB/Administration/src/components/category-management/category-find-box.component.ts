import {Component, EventEmitter, Inject} from '@angular/core';
import {Response} from "@angular/http";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {SearchCategoriesViewModel} from "../../viewmodels/category/SearchCategoriesViewModel";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {ClientDataConstraintService} from "../../services/ClientDataConstraintService";
import {Account} from "../../models/Account";
import {Pagination} from "../../viewmodels/Pagination";
import {TextSearch} from "../../viewmodels/TextSearch";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";

@Component({
    selector: 'category-find-box',
    templateUrl: 'category-find-box.component.html',
    inputs: ['conditions', 'isLoading'],
    outputs: ['search'],
    providers: [
        FormBuilder,
        SearchCategoriesViewModel,

        ClientConfigurationService,
        ClientDataConstraintService
    ]
})

export class CategoryFindBoxComponent {

    //#region Properties

    // Whether records are being loaded from server or not.
    public isLoading: boolean;

    // Event which is emitted when search button is clicked.
    private search: EventEmitter<any>;

    // Category find box which contains category.
    private findCategoryBox: FormGroup;

    // Collection of conditions which are used for searching categories.
    private conditions: SearchCategoriesViewModel;

    // List of accounts which are used for typeahead binding.
    private _accounts: Array<Account>;

    //#endregion

    //#region Constructor

    // Initiate component with default dependency injection.
    public constructor(private formBuilder: FormBuilder,
                       public clientConfigurationService: ClientConfigurationService,
                       @Inject("IClientAccountService") public clientAccountService: IClientAccountService,
                       public clientDataConstraintService: ClientDataConstraintService) {

        // Initiate account typeahead data.
        this._accounts = new Array<Account>();

        // Form control of find category box.
        this.findCategoryBox = this.formBuilder.group({
            name: [null],
            categoryCreatorEmail: [null],
            created: this.formBuilder.group({
                from: [null, Validators.nullValidator],
                to: [null, Validators.nullValidator]
            }),
            lastModified: this.formBuilder.group({
                from: [null, Validators.nullValidator],
                to: [null, Validators.nullValidator]
            }),
            pagination: this.formBuilder.group({
                records: [null, Validators.nullValidator]
            }),
            sort: [null],
            direction: [null]
        });

        // Initiate event emitters.
        this.search = new EventEmitter();
    }

    //#endregion

    //#region Methods

    // Callback which is fired when search button is clicked.
    public clickSearch(): void {
        this.search.emit();
    }

    // Find accounts list and bind it to typeahead control.
    // This callback is fired when account is typed in control.
    public selectCategoryCreator(event: any): void {

        // Find account which has been selected.
        let account = event.item;

        // Account is invalid.
        if (account == null)
            return;

        // Account doesn't have id column.
        if (account['id'] == null)
            return;

        this.conditions.creatorIndex = account.id;
    }

    // Callback which is fired when control is starting to load data of accounts from service.
    public loadAccounts(): void {

        // Initiate find account conditions.
        let findAccountsViewModel = new SearchAccountsViewModel();

        // Update account which should be searched for.
        if (findAccountsViewModel.email == null)
            findAccountsViewModel.email = new TextSearch();
        findAccountsViewModel.email.value = this.findCategoryBox.controls['categoryCreatorEmail'].value;

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();

        // Pagination update.
        findAccountsViewModel.pagination = pagination;

        // Find accounts with specific conditions.
        this.clientAccountService.getAccounts(findAccountsViewModel)
            .then((response: Response | any) => {

                // Analyze find account response view model.
                let findAccountResult = response.json();

                // Find list of accounts which has been responded from service.
                this._accounts = findAccountResult.accounts;
            })
            .catch((response: any) => {
            // TODO:
            });
    }

    //#endregion
}
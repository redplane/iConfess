import {Component, EventEmitter} from '@angular/core';
import {FindCategoriesViewModel} from "../../../viewmodels/category/FindCategoriesViewModel";
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ConfigurationService} from "../../../services/ClientConfigurationService";
import {Account} from "../../../models/Account";
import {IClientAccountService} from "../../../interfaces/services/IClientAccountService";
import {ClientAccountService} from "../../../services/clients/ClientAccountService";
import {Pagination} from "../../../viewmodels/Pagination";
import {FindAccountsViewModel} from "../../../viewmodels/accounts/FindAccountsViewModel";
import {Response} from "@angular/http";
import {TextSearch} from "../../../viewmodels/TextSearch";

@Component({
    selector: 'category-find-box',
    templateUrl: './app/views/contents/category/category-find-box.component.html',
    inputs: ['conditions', 'isLoading'],
    outputs: ['search'],
    providers: [
        FormBuilder,
        FindCategoriesViewModel,

        ConfigurationService,
        ClientAccountService
    ]
})

export class CategoryFindBoxComponent {

    // Whether records are being loaded from server or not.
    public isLoading: boolean;

    // Event which is emitted when search button is clicked.
    private search: EventEmitter<any>;

    // Category find box which contains category.
    private findCategoryBox: FormGroup;

    // Service which provides function to access application configuration.
    private _clientConfigurationService: ConfigurationService;

    // Collection of conditions which are used for searching categories.
    private conditions: FindCategoriesViewModel;

    // List of accounts which are used for typeahead binding.
    private _accounts: Array<Account>;

    // Service which handles client accounts api to service.
    private _clientAccountService: IClientAccountService;

    // Initiate component with default dependency injection.
    public constructor(private formBuilder: FormBuilder, clientConfigurationService: ConfigurationService,
                       clientAccountService: ClientAccountService) {

        // Initiate account typeahead data.
        this._accounts = new Array<Account>();

        // Find configuration service from IoC.
        this._clientConfigurationService = clientConfigurationService;
        this._clientAccountService = clientAccountService;

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

    // Callback which is fired when search button is clicked.
    public clickSearch(): void {
        this.search.emit();
    }

    // Find accounts list and bind it to typeahead control.
    // This callback is fired when account is typed in control.
    public selectCategoryCreator(event): void {

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
        let findAccountsViewModel = new FindAccountsViewModel();

        // Update account which should be searched for.
        if (findAccountsViewModel.email == null)
            findAccountsViewModel.email = new TextSearch();
        findAccountsViewModel.email.value = this.findCategoryBox.controls['categoryCreatorEmail'].value;

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 0;
        pagination.records = this._clientConfigurationService.findMaxPageRecords();

        // Pagination update.
        findAccountsViewModel.pagination = pagination;

        this._clientAccountService.findAccounts(findAccountsViewModel)
            .then((response: Response | any) => {

                // Analyze find account response view model.
                let findAccountResult = response.json();

                // Find list of accounts which has been responded from service.
                this._accounts = findAccountResult.accounts;
            })
            .catch((response: any) => {

            });
    }
}
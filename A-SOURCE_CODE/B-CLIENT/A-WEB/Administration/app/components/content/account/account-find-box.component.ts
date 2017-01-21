import {Component, EventEmitter, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ClientConfigurationService} from "../../../services/ClientConfigurationService";
import {ClientAccountService} from "../../../services/clients/ClientAccountService";
import {FindAccountsViewModel} from "../../../viewmodels/accounts/FindAccountsViewModel";
import {ClientApiService} from "../../../services/ClientApiService";
import {AccountStatuses} from "../../../enumerations/AccountStatuses";

@Component({
    selector: 'account-find-box',
    templateUrl: './app/views/contents/account/account-find-box.component.html',
    inputs: ['conditions', 'isLoading'],
    outputs: ['search'],
    providers: [
        FormBuilder,
        ClientConfigurationService,
        ClientAccountService,
        ClientApiService
    ]
})

export class AccountFindBoxComponent implements OnInit {

    // Whether records are being loaded from server or not.
    public isLoading: boolean;

    // Event which is emitted when search button is clicked.
    private search: EventEmitter<any>;

    // Form contains controls which are for searching accounts.
    private findAccountBox: FormGroup;

    // Collection of conditions which are used for searching categories.
    private conditions: FindAccountsViewModel;

    private selected: any;

    // Initiate component with default dependency injection.
    public constructor(private formBuilder: FormBuilder,
                       private clientConfigurationService: ClientConfigurationService,
                       private clientAccountService: ClientAccountService) {

        // Form control of find category box.
        this.findAccountBox = formBuilder.group({
            email: [''],
            nickname: [''],
            joined: formBuilder.group({
                from: [''],
                to: ['']
            }),
            lastModified: formBuilder.group({
                from: [''],
                to: ['']
            }),
            pagination: formBuilder.group({
                index: [0],
                records: [10]
            }),
            sort: [null],
            direction: [null]
        });

        // Initiate event emitters.
        this.search = new EventEmitter();

        // Initiate search conditions.
        this.conditions = new FindAccountsViewModel();
    }

    // Callback which is fired when search button is clicked.
    public clickSearch(): void {
        this.search.emit();
    }

    // Callback which is fired when control is starting to load data of accounts from service.
    public loadAccounts(): void {

        // // Initiate find account conditions.
        // let findAccountsViewModel = new FindAccountsViewModel();
        //
        // // Update account which should be searched for.
        // if (findAccountsViewModel.email == null)
        //     findAccountsViewModel.email = new TextSearch();
        // findAccountsViewModel.email.value = this.findCategoryBox.controls['categoryCreatorEmail'].value;
        //
        // // Initiate pagination.
        // let pagination = new Pagination();
        // pagination.index = 0;
        // pagination.records = this._clientConfigurationService.findMaxPageRecords();
        //
        // // Pagination update.
        // findAccountsViewModel.pagination = pagination;
        //
        // this._clientAccountService.findAccounts(findAccountsViewModel)
        //     .then((response: Response | any) => {
        //
        //         // Analyze find account response view model.
        //         let findAccountResult = response.json();
        //
        //         // Find list of accounts which has been responded from service.
        //         this._accounts = findAccountResult.accounts;
        //     })
        //     .catch((response: any) => {
        //
        //     });
    }

    /*
    * Callback which is fired when component has been loaded successfully.
    * */
    public ngOnInit(): void {
    }
}
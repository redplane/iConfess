import {Component, EventEmitter, Input, OnInit, Inject, Output} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";

@Component({
    selector: 'account-search-box',
    templateUrl: 'account-search-box.component.html'
})

export class AccountSearchBoxComponent implements OnInit {

    //#region Properties

    // Whether records are being loaded from server or not.
    @Input('is-loading')
    public isLoading: boolean;

    // Event which is emitted when search button is clicked.
    @Output('search')
    private search: EventEmitter<any>;

    // Form contains controls which are for searching accounts.
    private findAccountBox: FormGroup;

    // Collection of conditions which are used for searching categories.
    @Input('conditions')
    private conditions: SearchAccountsViewModel;

    // List of accounts which can be selected.
    public accounts: Array<Account>;
    //#endregion

    //#region Constructor

    // Initiate component with default dependency injection.
    public constructor(private formBuilder: FormBuilder,
                       @Inject("IClientAccountService") private clientAccountService: IClientAccountService) {

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
        this.accounts = new Array<Account>();

        // Initiate search conditions.
        this.conditions = new SearchAccountsViewModel();
    }

    //#endregion

    //#region Methods

    // Callback which is fired when status button is toggled.
    public toggleStatuses(status: AccountStatuses): void{

        // Statuses list hasn't been initialized.
        if (this.conditions.statuses == null) {
            this.conditions.statuses = new Array<AccountStatuses>();
            this.conditions.statuses.push(status);
            return;
        }

        // Find status in the list.
        let index = this.conditions.statuses.indexOf(status);
        if (index == -1){
            this.conditions.statuses.push(status);
            return;
        }

        this.conditions.statuses.splice(index, 1);
    }

    // Callback which is fired when search button is clicked.
    public clickSearch(): void {
        this.search.emit();
    }

    /*
    * Callback which is fired when component has been loaded successfully.
    * */
    public ngOnInit(): void {
    }

    //#endregion
}
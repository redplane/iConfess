import {Component, EventEmitter, Input, OnInit, Inject, Output} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {IClientCommonService} from "../../interfaces/services/IClientCommonService";
import {AccountSortProperty} from "../../enumerations/order/AccountSortProperty";
import {SortDirection} from "../../enumerations/SortDirection";
import {Dictionary} from "../../models/Dictionary";
import {IDictionary} from "../../interfaces/IDictionary";
import {KeyValuePair} from "../../models/KeyValuePair";

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

    // Collection of conditions which are used for searching categories.
    @Input('conditions')
    private conditions: SearchAccountsViewModel;

    // List of accounts which can be selected.
    public accounts: Array<Account>;

    // List of properties which can be used for sorting.
    private sortProperties: IDictionary<AccountSortProperty>;

    //#endregion

    //#region Constructor

    // Initiate component with default dependency injection.
    public constructor(@Inject("IClientAccountService") public clientAccountService: IClientAccountService,
                       @Inject('IClientCommonService') public clientCommonService: IClientCommonService) {

        // Initiate event emitters.
        this.search = new EventEmitter();
        this.accounts = new Array<Account>();

        // Initiate list of properties which can be used for sorting.
        let sortProperties = new Dictionary<AccountSortProperty>();
        sortProperties.add('Index', AccountSortProperty.index);
        sortProperties.add('Email', AccountSortProperty.email);
        sortProperties.add('Nickname', AccountSortProperty.nickname);
        sortProperties.add('Status', AccountSortProperty.status);
        sortProperties.add('Joined', AccountSortProperty.joined);
        sortProperties.add('Last modified', AccountSortProperty.lastModified);

        this.sortProperties = sortProperties;

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

    // Callback which is fired when statuses selection is updated.
    public updateStatuses(statuses: Array<KeyValuePair<AccountStatuses>>): void{
        this.conditions.statuses = statuses.map((x: KeyValuePair<AccountStatuses>) => {return x.value});
    }

    /*
    * Callback which is fired when component has been loaded successfully.
    * */
    public ngOnInit(): void {
    }

    //#endregion
}
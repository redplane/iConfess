import {Component, EventEmitter, Inject, Input} from "@angular/core";
import * as _ from "lodash";
import {Account} from "../../../models/entities/account";
import {AccountStatus} from "../../../enumerations/account-status";
import {ITimeService} from "../../../interfaces/services/time-service.interface";

@Component({
    selector: 'account-profile-box',
    templateUrl: 'account-profile-box.component.html',
    exportAs: 'account-profile-box'
})

export class AccountProfileBoxComponent{

    //#region Properties

    // Find list of account statuses.
    private AccountStatuses = AccountStatus;

    // Emitter which is fired when change account status button is clicked.
    private clickChangeAccountStatus: EventEmitter<Account>;

    // Account profile.
    @Input('account')
    private account: Account;

    //#endregion

    //#region Constructor

    // Initiate component with injections.
    public constructor(
        @Inject("ITimeService") public timeService: ITimeService){

        // Initialize account profile information.
        this.account = new Account();

        // Initialize event emitters.
        this.clickChangeAccountStatus = new EventEmitter<Account>();
    }

    //#endregion

    //#region Methods

    // Callback which is fired when change account button is clicked.
    public changeAccountStatus(account: Account): void{
        this.clickChangeAccountStatus.emit(account);
    }

    // Attach a profile to this component.
    public setProfile(account: Account): void{
        this.account = _.cloneDeep(account) ;
    }

    // Get profile from component
    public getProfile(): Account{
        return this.account;
    }

    //#endregion
}

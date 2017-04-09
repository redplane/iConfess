import {Component, EventEmitter, Inject} from "@angular/core";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {Account} from "../../models/Account";
import {IClientTimeService} from "../../interfaces/services/IClientTimeService";

@Component({
    selector: 'account-profile-box',
    templateUrl: 'account-profile-box.component.html',
    inputs:['account'],
    outputs:['clickChangeAccountStatus']
})

export class AccountProfileBoxComponent{

    // Find list of account statuses.
    private accountStatuses = AccountStatuses;

    // Emitter which is fired when change account status button is clicked.
    private clickChangeAccountStatus: EventEmitter<Account>;

    // Initiate component with injections.
    public constructor(@Inject("IClientTimeService") public clientTimeService: IClientTimeService){

        // Initialize event emitters.
        this.clickChangeAccountStatus = new EventEmitter<Account>();
    }

    // Callback which is fired when change account button is clicked.
    public changeAccountStatus(account: Account): void{
        this.clickChangeAccountStatus.emit(account);
    }
}
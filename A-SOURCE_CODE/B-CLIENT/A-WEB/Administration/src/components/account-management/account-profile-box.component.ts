import {Component, EventEmitter } from "@angular/core";
import {ClientTimeService} from "../../services/ClientTimeService";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {Account} from "../../models/Account";

@Component({
    selector: 'account-profile-box',
    templateUrl: 'account-profile-box.component.html',
    inputs:['account'],
    outputs:['clickChangeAccountStatus'],
    providers:[
        ClientTimeService
    ]
})

export class AccountProfileBoxComponent{

    // Find list of account statuses.
    private accountStatuses = AccountStatuses;

    // Emitter which is fired when change account status button is clicked.
    private clickChangeAccountStatus: EventEmitter<Account>;

    // Initiate component with injections.
    public constructor(public clientTimeService: ClientTimeService){

        // Initialize event emitters.
        this.clickChangeAccountStatus = new EventEmitter<Account>();
    }

    // Callback which is fired when change account button is clicked.
    public changeAccountStatus(account: Account): void{
        this.clickChangeAccountStatus.emit(account);
    }
}
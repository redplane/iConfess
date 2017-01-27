import {Component, EventEmitter} from '@angular/core';
import {TimeService} from "../../../services/TimeService";
import {ITimeService} from "../../../interfaces/services/ITimeService";
import {CategoryDetailViewModel} from "../../../viewmodels/category/CategoryDetailViewModel";
import {Account} from "../../../models/Account";
import {AccountStatuses} from "../../../enumerations/AccountStatuses";

@Component({
    selector: 'account-detail-box',
    inputs: ['account'],
    outputs: ['clickRemoveAccount', 'clickChangeAccountInfo'],
    templateUrl: './app/views/contents/account/account-detail-box.component.html',
    providers:[
        TimeService
    ]
})

export class AccountDetailBoxComponent{

    // Event emitter which is fired when a category is clicked to be removed.
    private clickRemoveAccount: EventEmitter<any>;

    // Event emitter which is fired when a category is clicked to be changed.
    private clickChangeAccountInfo: EventEmitter<any>;

    // List of statuses which client can have.
    private clientAccountStatuses = AccountStatuses;

    // Initiate category detail box with dependency injections.
    public constructor(public clientTimeService: TimeService){

        // Event handler initialization.
        this.clickRemoveAccount = new EventEmitter();
        this.clickChangeAccountInfo = new EventEmitter();
    }

    // Fired when a account is clicked to be removed.
    public deleteAccount(account: Account): void{
        this.clickRemoveAccount.emit(account);
    }

    // Fired when a account is clicked to be changed.
    public changeAccountInfo(account: Account): void{
        this.clickChangeAccountInfo.emit(account);
    }
}
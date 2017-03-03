import {Component} from "@angular/core";
import {ClientTimeService} from "../../services/ClientTimeService";
import {AccountStatuses} from "../../enumerations/AccountStatuses";

@Component({
    selector: 'account-profile-box',
    templateUrl: 'account-profile-box.component.html',
    inputs:['account'],
    providers:[
        ClientTimeService
    ]
})

export class AccountProfileBoxComponent{

    // Find list of account statuses.
    private accountStatuses = AccountStatuses;

    // Initiate component with injections.
    public constructor(public clientTimeService: ClientTimeService){
    }
}
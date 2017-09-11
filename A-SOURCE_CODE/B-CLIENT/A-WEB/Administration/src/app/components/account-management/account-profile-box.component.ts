import {Component, EventEmitter, Inject, Input, OnInit} from "@angular/core";
import * as _ from "lodash";
import {Account} from "../../../models/entities/account";
import {AccountStatus} from "../../../enumerations/account-status";
import {ITimeService} from "../../../interfaces/services/time-service.interface";
import {KeyValuePair} from "../../../models/key-value-pair";
import {IAccountService} from "../../../interfaces/services/api/account-service.interface";
import {Response} from '@angular/http';
import {IConfigurationService} from "../../../interfaces/services/configuration-service.interface";

@Component({
  selector: 'account-profile-box',
  templateUrl: 'account-profile-box.component.html',
  exportAs: 'account-profile-box'
})

export class AccountProfileBoxComponent implements OnInit {

  //#region Properties

  // Find list of account statuses.
  private AccountStatuses = AccountStatus;

  // Emitter which is fired when change account status button is clicked.
  private clickChangeAccountStatus: EventEmitter<Account>;

  // Account profile.
  @Input('account')
  private account: Account;

  /*
  * Account status (name - value)
  * */
  private accountStatuses: Array<KeyValuePair<AccountStatus>>;

  //#endregion

  //#region Constructor

  // Initiate component with injections.
  public constructor(@Inject("ITimeService") public timeService: ITimeService,
                     @Inject('IAccountService') public accountService: IAccountService,
                     @Inject('IConfigurationService') public configurationService: IConfigurationService) {

    // Initialize account profile information.
    this.account = new Account();

    // Initialize event emitters.
    this.clickChangeAccountStatus = new EventEmitter<Account>();
  }

  //#endregion

  //#region Methods

  /*
  * Callback which is fired when component has been initiated successfully.
  * */
  public ngOnInit(): void {

    // Load account statuses.
    this.configurationService.getAccountStatuses()
      .then((x: Response) => {
        this.accountStatuses = <Array<KeyValuePair<AccountStatus>>> x.json();
      });
  }

  // Callback which is fired when change account button is clicked.
  public changeAccountStatus(account: Account): void {
    this.clickChangeAccountStatus.emit(account);
  }

  // Attach a profile to this component.
  public setProfile(account: Account): void {
    this.account = _.cloneDeep(account);
  }

  // Get profile from component
  public getProfile(): Account {
    return this.account;
  }

  //#endregion
}

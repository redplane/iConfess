import {Component, Inject, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {Response} from "@angular/http";
import {IAccountService} from "../../../interfaces/services/api/account-service.interface";
import {IAuthenticationService} from "../../../interfaces/services/authentication-service.interface";

@Component({
  selector: 'navigation-bar',
  templateUrl: 'navigation-bar.component.html'
})

export class NavigationBarComponent implements OnInit {

  //#region Properties

  // Account property.
  private account: Account;

  //#endregion

  //#region Constructor

  // Initiate instance with IoC.
  public constructor(@Inject("IAuthenticationService") public authenticationService: IAuthenticationService,
                     @Inject("IAccountService") public clientAccountService: IAccountService,
                     public clientRoutingService: Router) {
  }

  //#endregion

  //#region Methods

  // Sign the user out.
  public clickSignOut(): void {
    // Clear the authentication service.
    this.authenticationService.clearIdentity();

    // Re-direct to login page.
    this.clientRoutingService.navigate(['/']);
  }

  // This callback is fired when this component has been initialized.
  public ngOnInit(): void {
    // Find account information.
    this.clientAccountService.getClientProfile()
      .then((x: Response) => {
        console.log(x);
      })
      .catch((x: Response) => {
      });
  }

  //#endregion
}

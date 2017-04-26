import {Component, Inject, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {Response} from "@angular/http";

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
    public constructor(@Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService,
                       @Inject("IClientAccountService") public clientAccountService: IClientAccountService,
                       public clientRoutingService: Router) {
    }

    //#endregion

    //#region Methods

    // Sign the user out.
    public clickSignOut(): void {
        // Clear the authentication service.
        this.clientAuthenticationService.clearToken();

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
                console.log(x);
            });
    }

    //#endregion
}

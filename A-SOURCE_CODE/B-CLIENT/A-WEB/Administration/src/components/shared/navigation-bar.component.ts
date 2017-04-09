import {Component, Inject} from '@angular/core';
import {Router} from "@angular/router";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";

@Component({
    selector: 'navigation-bar',
    templateUrl: 'navigation-bar.component.html'
})

export class NavigationBarComponent {

    //#region Constructor

    // Initiate instance with IoC.
    public constructor(@Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService,
                       public clientRoutingService: Router) {
    }

    //#endregion

    //#region Methods

    // Sign the user out.
    public clickSignOut(): void {
        // Clear the authentication service.
        this.clientAuthenticationService.clearAuthenticationToken();

        // Re-direct to login page.
        this.clientRoutingService.navigate(['/']);
    }

    //#endregion
}

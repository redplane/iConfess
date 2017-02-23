import {Component} from '@angular/core';
import {Router} from "@angular/router";
import {ClientAuthenticationService} from "../../services/clients/ClientAuthenticationService";

@Component({
    selector: 'navigation-bar',
    templateUrl: 'navigation-bar.component.html',
    providers: [
        ClientAuthenticationService
    ]
})

export class NavigationBarComponent {

    // Initiate instance with IoC.
    public constructor(public clientAuthenticationService: ClientAuthenticationService,
                       public clientRoutingService: Router) {
    }

    // Sign the user out.
    public clickSignOut(): void {
        // Clear the authentication service.
        this.clientAuthenticationService.clearAuthenticationToken();

        // Re-direct to login page.
        this.clientRoutingService.navigate(['/']);
    }
}

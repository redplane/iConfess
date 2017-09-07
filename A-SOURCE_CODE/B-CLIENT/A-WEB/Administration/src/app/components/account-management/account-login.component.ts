import {Component, Inject, ViewChild} from "@angular/core";
import {Response} from "@angular/http";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, NgForm} from "@angular/forms";
import {LoginViewModel} from "../../../viewmodels/accounts/login.view-model";
import {IAccountService} from "../../../interfaces/services/api/account-service.interface";
import {IAuthenticationService} from "../../../interfaces/services/authentication-service.interface";
import {AuthorizationToken} from "../../../models/authorization-token";

@Component({
    selector: 'account-login',
    templateUrl: 'account-login.component.html'
})

export class AccountLoginComponent{

    //#region Properties

    // Whether login function is being executed or not.
    private isBusy: boolean;

    // Login view model.
    private loginViewModel: LoginViewModel;

    // Login form group.
    @ViewChild("loginPanel")
    public loginPanel: NgForm;

    //#endregion

    //#region Constructor

    // Initiate component with default settings.
    public constructor(@Inject("IAuthenticationService") public authenticationService: IAuthenticationService,
                       @Inject("IAccountService") public clientAccountService: IAccountService,
                       public clientRoutingService: Router){

        this.loginViewModel = new LoginViewModel();
    }

    //#endregion

    //#region Methods

    // Callback is fired when login button is clicked.
    public clickLogin(event: Event){

        // Prevent default behaviour.
        event.preventDefault();

        // Make component be loaded.
        this.isBusy = true;

        // Call service api to authenticate do authentication.
        this.clientAccountService.login(this.loginViewModel)
            .then((x: Response) => {
                // Convert response from service to AuthenticationToken data type.
                let clientAuthenticationDetail = <AuthorizationToken> x.json();

                // Save the client authentication information.
                this.authenticationService.setAuthorization(clientAuthenticationDetail);

                // Redirect user to account management page.
                this.clientRoutingService.navigate(['/account-management']);

                // Cancel loading process.
                this.isBusy = false;
            })
            .catch((response: Response) =>{
                // Unfreeze the UI.
                this.isBusy = false;
            });
    }

    //#endregion
}

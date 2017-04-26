import {Component, Inject, ViewChild} from "@angular/core";
import {Response} from "@angular/http";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, NgForm} from "@angular/forms";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";
import {AuthenticationToken} from "../../models/AuthenticationToken";

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
    public constructor(@Inject("IClientApiService") public clientApiService: IClientApiService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService,
                       @Inject("IClientAccountService") public clientAccountService: IClientAccountService,
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
                let clientAuthenticationDetail = <AuthenticationToken> x.json();

                // Save the client authentication information.
                this.clientAuthenticationService.setToken(clientAuthenticationDetail);

                // Redirect user to account management page.
                this.clientRoutingService.navigate(['/account-management']);

                // Cancel loading process.
                this.isBusy = false;
            })
            .catch((response: Response) =>{
                // Unfreeze the UI.
                this.isBusy = false;

                // Proceed the common logic handling.
                this.clientApiService.handleInvalidResponse(response);
            });
    }

    //#endregion
}
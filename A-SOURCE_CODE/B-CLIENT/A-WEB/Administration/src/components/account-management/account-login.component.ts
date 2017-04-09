import {Component, Inject} from "@angular/core";
import {Response} from "@angular/http";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup} from "@angular/forms";
import {ClientApiService} from "../../services/ClientApiService";
import {ClientToastrService} from "../../services/ClientToastrService";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {ClientAuthenticationToken} from "../../models/ClientAuthenticationToken";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";

@Component({
    selector: 'account-login',
    templateUrl: 'account-login.component.html'
})

export class AccountLoginComponent{

    //#region Properties

    // Whether login function is being executed or not.
    private isLoading: boolean;

    // Login view model.
    private loginViewModel: LoginViewModel;

    // Login form group.
    public loginBox: FormGroup;

    //#endregion

    //#region Constructor

    // Initiate component with default settings.
    public constructor(public clientApiService: ClientApiService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService,
                       public clientNotificationService: ClientToastrService,
                       @Inject("IClientAccountService") public clientAccountService: IClientAccountService,
                       public clientRoutingService: Router,
                       public formBuilder: FormBuilder){

        this.loginBox = this.formBuilder.group({
            email: [''],
            password: ['']
        });

        this.loginViewModel = new LoginViewModel();
    }

    //#endregion

    //#region Methods

    // Callback is fired when login button is clicked.
    public clickLogin(){

        // Make component be loaded.
        this.isLoading = true;

        // Call service api to authenticate do authentication.
        this.clientAccountService.login(this.loginViewModel)
            .then((response: Response) => {
                // Convert response from service to ClientAuthenticationToken data type.
                let clientAuthenticationDetail = <ClientAuthenticationToken> response.json();

                // Save the client authentication information.
                this.clientAuthenticationService.initiateLocalAuthenticationToken(clientAuthenticationDetail);

                // Redirect user to account management page.
                this.clientRoutingService.navigate(['/account-management']);

                // Cancel loading process.
                this.isLoading = false;
            })
            .catch((response: Response) =>{
                // Unfreeze the UI.
                this.isLoading = false;

                // Proceed the common logic handling.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }

    //#endregion
}
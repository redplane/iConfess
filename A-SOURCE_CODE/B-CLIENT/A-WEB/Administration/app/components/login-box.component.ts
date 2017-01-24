import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ClientAuthenticationService} from "../services/clients/ClientAuthenticationService";
import {IClientAuthenticationService} from "../interfaces/services/IClientAuthenticationService";
import {LoginViewModel} from "../viewmodels/accounts/LoginViewModel";
import {ClientApiService} from "../services/ClientApiService";
import {ClientAccountService} from "../services/clients/ClientAccountService";
import {IClientAccountService} from "../interfaces/services/IClientAccountService";
import {Response} from "@angular/http";
import {ClientValidationService} from "../services/ClientValidationService";
import {Router} from '@angular/router';
import {ClientAuthenticationToken} from "../models/ClientAuthenticationToken";
import {ClientNotificationService} from "../services/ClientNotificationService";

@Component({
    selector: 'login-box',
    templateUrl: './app/views/pages/login-box.component.html',
    providers: [
        ClientValidationService,
        ClientApiService,
        ClientAccountService,
        ClientAuthenticationService,
        ClientNotificationService
    ]
})
export class LoginComponent implements OnInit {


    // Box which contains information for login purpose.
    public loginBox: FormGroup;

    // Model which stores
    private loginViewModel: LoginViewModel;

    // Whether component is being loaded or not.
    private isLoading: boolean;

    // Initiate login box component with IoC.
    public constructor(private formBuilder: FormBuilder,
                       private clientApiService: ClientApiService,
                       private clientValidationService: ClientValidationService,
                       private clientAuthenticationService: ClientAuthenticationService,
                       private clientAccountService: ClientAccountService,
                       private clientRoutingService: Router) {

        // Initiate login view model.
        this.loginViewModel = new LoginViewModel();

        // Initiate login box and its components.
        this.loginBox = this.formBuilder.group({
            email: [''],
            password: ['']
        });

    }

    // This callback is fired when login button is clicked.
    public login(): void {

        // Make the component show the loading process.
        this.isLoading = true;

        // Pass the login view model to service.
        this.clientAccountService.login(this.loginViewModel)
            .then((response: Response | any) => {

                // Convert response from service to ClientAuthenticationToken data type.
                let clientAuthenticationDetail = <ClientAuthenticationToken> response.json();

                // Save the client authentication information.
                this.clientAuthenticationService.initiateLocalAuthenticationToken(clientAuthenticationDetail);

                // Redirect user to account management page.
                this.clientRoutingService.navigate(['/account-management']);

                // Cancel loading process.
                this.isLoading = false;
            })
            .catch((response: any) => {
                // Proceed non-solid response.
                this.clientApiService.proceedHttpNonSolidResponse(response);

                // Cancel loading process.
                this.isLoading = false;
            });
    }

    // Called when component has been rendered successfully.
    public ngOnInit(): void {
        // By default, component loads nothing.
        this.isLoading = false;
    }
}

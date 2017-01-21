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
    selector: 'login',
    templateUrl: './app/views/pages/login.component.html',
    providers:[
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

    // Service which handles links to access apis.
    private _clientApiService: ClientApiService;

    // Service which handles client authentication.
    private _clientAuthenticationService: IClientAuthenticationService;

    // Service which handles stuffs related to account.
    private _clientAccountService : IClientAccountService;

    // Service which handles client validation.
    private _clientValidationService: ClientValidationService;

    // Service which is for routing in client application.
    private _clientRoutingService: Router;

    // Model which stores
    private _loginViewModel: LoginViewModel;

    // Whether component is being loaded or not.
    private _isLoading: boolean;

    // Initiate login box component with IoC.
    public constructor(formBuilder: FormBuilder,
                       clientApiService: ClientApiService,
                       clientValidationService: ClientValidationService,
                       clientAuthenticationService: ClientAuthenticationService,
                       clientAccountService: ClientAccountService,
                       clientRoutingService: Router){

        // Initiate login view model.
        this._loginViewModel = new LoginViewModel();

        // Initiate login box and its components.
        this.loginBox = formBuilder.group({
            email: [''],
            password: ['']
        });

        // Client api service injection.
        this._clientApiService = clientApiService;

        // Client validation service injection.
        this._clientValidationService = clientValidationService;

        // Client authentication service injection.
        this._clientAuthenticationService = clientAuthenticationService;

        // Client account service injection.
        this._clientAccountService = clientAccountService;

        // Service which is for routing.
        this._clientRoutingService = clientRoutingService;

    }

    // This callback is fired when login button is clicked.
    public login(event:any): void{

        // Make the component show the loading process.
        this._isLoading = true;

        // Pass the login view model to service.
        this._clientAccountService.login(this._loginViewModel)
            .then((response: Response | any) => {

                // Convert response from service to ClientAuthenticationToken data type.
                let clientAuthenticationDetail = <ClientAuthenticationToken> response.json();

                // Save the client authentication information.
                this._clientAuthenticationService.saveAuthenticationToken(clientAuthenticationDetail);

                // Redirect user to account management page.
                this._clientRoutingService.navigate(['/account-management']);

                // Cancel loading process.
                this._isLoading = false;
            })
            .catch((response : any) => {
                // Proceed non-solid response.
                this._clientApiService.proceedHttpNonSolidResponse(response);

                // Cancel loading process.
                this._isLoading = false;
            });
    }

    // Called when component has been rendered successfully.
    public ngOnInit(): void {
        // By default, component loads nothing.
        this._isLoading = false;
    }
}

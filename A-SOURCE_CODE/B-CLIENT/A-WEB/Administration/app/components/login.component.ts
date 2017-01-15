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

@Component({
    selector: 'login',
    templateUrl: './app/views/pages/login.component.html',
    providers:[
        ClientValidationService,
        ClientApiService,
        ClientAccountService,
        ClientAuthenticationService
    ]
})
export class LoginComponent implements OnInit {


    // Error message which should be displayed on the login box component.
    private _loginResponseMessage: string;

    // Box which contains information for login purpose.
    public loginBox: FormGroup;

    // Model which stores
    private _loginViewModel: LoginViewModel;

    // Whether component is being loaded or not.
    private _isLoading: boolean;

    // Initiate login box component with IoC.
    public constructor(private formBuilder: FormBuilder,
                       private clientApiService: ClientApiService,
                       private clientValidationService: ClientValidationService,
                       private clientAuthenticationService: ClientAuthenticationService,
                       private clientAccountService: ClientAccountService,
                       private clientRoutingService: Router){

        // Initiate login view model.
        this._loginViewModel = new LoginViewModel();

        // Initiate login box and its components.
        this.loginBox = this.formBuilder.group({
            email: [''],
            password: ['']
        });

    }

    // This callback is fired when login button is clicked.
    public login(event:any): void{

        // Make the component show the loading process.
        this._isLoading = true;

        // Clear the previous message.
        this._loginResponseMessage = "";

        // Pass the login view model to service.
        this.clientAccountService.login(this._loginViewModel)
            .then((response: Response | any) => {

                // Convert response from service to ClientAuthenticationToken data type.
                let clientAuthenticationDetail = <ClientAuthenticationToken> response.json();

                // Save the client authentication information.
                this.clientAuthenticationService.saveAuthenticationToken(clientAuthenticationDetail);

                // Redirect user to account management page.
                this.clientRoutingService.navigate(['/account-management']);

                // Cancel loading process.
                this._isLoading = false;
            })
            .catch((response : any) => {
                if (!(response instanceof Response))
                    return;

                // Find the response object.
                let information = response.json();

                switch (response.status){

                    // Bad request, usually submited parameters are invalid.
                    case 400:

                        // Refined the information.
                        information = this.clientValidationService.findPropertiesValidationMessages(information);

                        // Parse the response and update to controls of form.
                        this.clientValidationService.findFrontendValidationModel(this.clientValidationService.validationDictionary, this.loginBox, information);
                        break;
                    case 404:
                        this._loginResponseMessage = information['message'];
                        console.log(response);
                        // TODO: Display message.
                        break;
                    case 500:
                        console.log(response);
                        // TODO: Display message.
                        break;
                }

                // Cancel loading process.
                this._isLoading = false;
            });
    }

    // Called when component has been rendered successfully.
    public ngOnInit(): void {

        // No error should be shown on startup.
        this._loginResponseMessage = "";

        // By default, component loads nothing.
        this._isLoading = false;
    }
}

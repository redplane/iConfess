import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ClientAuthenticationService} from "../services/clients/ClientAuthenticationService";
import {IClientAuthenticationService} from "../interfaces/services/IClientAuthenticationService";
import {LoginViewModel} from "../viewmodels/accounts/LoginViewModel";
import {ClientApiService} from "../services/ClientApiService";
import {ClientAccountService} from "../services/clients/ClientAccountService";
import {IClientAccountService} from "../interfaces/services/IClientAccountService";
import {Response} from "@angular/http";
import {ClientValidationService} from "../services/ClientValidationService";

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
export class LoginComponent {

    // Box which contains information for login purpose.
    private loginBox: FormGroup;

    // Service which handles links to access apis.
    private _clientApiService: ClientApiService;

    // Service which handles client authentication.
    private _clientAuthenticationService: IClientAuthenticationService;

    // Service which handles stuffs related to account.
    private _clientAccountService : IClientAccountService;

    // Service which handles client validation.
    private _clientValidationService: ClientValidationService;

    // Model which stores
    private _loginViewModel: LoginViewModel;

    // Initiate login box component with IoC.
    public constructor(formBuilder: FormBuilder,
                       clientApiService: ClientApiService,
                       clientValidationService: ClientValidationService,
                       clientAuthenticationService: ClientAuthenticationService, clientAccountService: ClientAccountService){

        // Initiate login view model.
        this._loginViewModel = new LoginViewModel();

        // Initiate login box and its components.
        this.loginBox = formBuilder.group({
            email: ['', Validators.compose([Validators.required])],
            password: ['', Validators.compose([Validators.required])]
        });

        // Client api service injection.
        this._clientApiService = clientApiService;

        // Client validation service injection.
        this._clientValidationService = clientValidationService;

        // Client authentication service injection.
        this._clientAuthenticationService = clientAuthenticationService;

        // Client account service injection.
        this._clientAccountService = clientAccountService;
    }

    // This callback is fired when login button is clicked.
    public login(event:any): void{

        // Pass the login view model to service.
        let result = this._clientAccountService.login(this._loginViewModel)
            .then((response: Response | any) => {

            })
            .catch((response : any) => {
                if (!(response instanceof Response))
                    return;

                // Find the response object.
                let information = response.json();

                switch (response.status){
                    case 400: // Bad request

                        let model = {};
                        this._clientValidationService.findFrontendValidationModel(this._clientValidationService.validationDictionary, model, information);
                        console.log(model);
                        break;
                }


            });
        console.log(result);
    }
}

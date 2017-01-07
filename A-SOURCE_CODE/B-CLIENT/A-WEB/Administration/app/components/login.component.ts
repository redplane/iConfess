import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ClientAuthenticationService} from "../services/clients/ClientAuthenticationService";
import {IClientAuthenticationService} from "../interfaces/services/IClientAuthenticationService";
import {LoginViewModel} from "../viewmodels/accounts/LoginViewModel";

@Component({
    selector: 'login',
    templateUrl: './app/views/pages/login.component.html',
    providers:[
        ClientAuthenticationService
    ]
})
export class LoginComponent {

    // Box which contains information for login purpose.
    private loginBox: FormGroup;

    // Service which handles client authentication.
    private _clientAuthenticationService: IClientAuthenticationService;

    // Model which stores
    private _loginViewModel: LoginViewModel;

    // Initiate login box component with IoC.
    public constructor(formBuilder: FormBuilder, clientAuthenticationService: ClientAuthenticationService){

        // Initiate login view model.
        this._loginViewModel = new LoginViewModel();

        // Initiate login box and its components.
        this.loginBox = formBuilder.group({
            email: ['', Validators.compose([Validators.required])],
            password: ['', Validators.compose([Validators.required])]
        });

        // Client authentication service injection.
        this._clientAuthenticationService = clientAuthenticationService;
    }

    // This callback is fired when login button is clicked.
    public login(event:any): void{
    }
}

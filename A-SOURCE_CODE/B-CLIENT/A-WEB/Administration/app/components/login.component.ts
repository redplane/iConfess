import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
    selector: 'login',
    templateUrl: './app/views/pages/login.component.html'
})
export class LoginComponent {

    // Box which contains information for login purpose.
    private loginBox: FormGroup;

    // Initiate login box component with IoC.
    public constructor(formBuilder: FormBuilder){

        // Initiate login box and its components.
        this.loginBox = formBuilder.group({
            email: ['', Validators.compose([Validators.required])],
            password: ['', Validators.compose([Validators.required])]
        })
    }
}

import {Component, EventEmitter} from "@angular/core";
import {ClientDataConstraintService} from "../../services/ClientDataConstraintService";
import {FormBuilder, FormGroup} from "@angular/forms";
import {Account} from "../../models/Account";
import {ClientAccountService} from "../../services/clients/ClientAccountService";
import {Response} from "@angular/http";
import {ClientApiService} from "../../services/ClientApiService";
import {ClientNotificationService} from "../../services/ClientNotificationService";
import {ClientAuthenticationService} from "../../services/clients/ClientAuthenticationService";
import {Router} from "@angular/router";

@Component({
    selector: 'account-forgot-password-box',
    templateUrl: 'account-forgot-password.component.html',
    inputs: ['isLoading'],
    providers:[
        ClientDataConstraintService,
        ClientAccountService,
        ClientApiService,
        ClientNotificationService,
        ClientAuthenticationService
    ]
})

export class AccountForgotPasswordComponent {

    // Form of request password change.
    private accountPasswordChangeBox: FormGroup;

    // Account information.
    private account: Account;

    // Whether component is busy or not.
    private isLoading: boolean;

    // Initiate component with default settings.
    public constructor(private formBuilder: FormBuilder,
                       private clientDataConstraintService: ClientDataConstraintService,
                       private clientAccountService: ClientAccountService,
                       private clientNotificationService: ClientNotificationService,
                       private clientApiService: ClientApiService,
                       private clientRoutingService: Router){

        // Initiate form.
        this.accountPasswordChangeBox = this.formBuilder.group({
            email: [''],
            token: [''],
            password: [''],
            passwordConfirmation: ['']
        });

        // Initiate account instance.
        this.account = new Account();
        this.account.email = 'redplane_dt@yahoo.com.vn';

        // Set loading to be false.
        this.isLoading = false;
    }

    // Callback which is fired when seach button is clicked for requesting a password reset.
    public clickRequestPassword(): void{

        // Set component to loading state.
        this.isLoading = true;

        // Call api to request password change.
        this.clientAccountService.requestPasswordChange(this.account.email)
            .then((response: Response) => {

            // Tell client that password request has been submitted.
                this.clientNotificationService.success('CHANGE_PASSWORD_REQUEST_SUBMITTED');

                // Redirect user to submit password page.
                this.clientRoutingService.navigate(['/submit-password']);

                // Cancel component loading state.
                this.isLoading = false;
            })
            .catch((response: Response) => {
                // Proceed common response analyzation.
                this.clientApiService.proceedHttpNonSolidResponse(response);

                // Cancel component loading state.
                this.isLoading = false;
            });
        ;
    }
}
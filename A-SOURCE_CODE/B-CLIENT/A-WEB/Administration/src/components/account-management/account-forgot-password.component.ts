import {Inject, Component, EventEmitter} from "@angular/core";
import {ClientDataConstraintService} from "../../services/ClientDataConstraintService";
import {FormBuilder, FormGroup} from "@angular/forms";
import {Account} from "../../models/entities/Account";
import {Response} from "@angular/http";
import {ClientApiService} from "../../services/ClientApiService";
import {ClientToastrService} from "../../services/ClientToastrService";
import {Router} from "@angular/router";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";

@Component({
    selector: 'account-forgot-password-box',
    templateUrl: 'account-forgot-password.component.html',
    inputs: ['isBusy'],
    providers:[
        ClientDataConstraintService
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
                       @Inject("IClientAccountService") private clientAccountService: IClientAccountService,
                       private clientNotificationService: ClientToastrService,
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
        this.clientAccountService.sendPasswordChangeRequest(this.account.email)
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
                this.clientApiService.handleInvalidResponse(response);

                // Cancel component loading state.
                this.isLoading = false;
            });
        ;
    }
}
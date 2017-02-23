import {Component} from "@angular/core";
import {FormGroup, FormBuilder} from "@angular/forms";
import {Router} from "@angular/router";
import {Response} from "@angular/http";
import {ClientAccountService} from "../../services/clients/ClientAccountService";
import {ClientApiService} from "../../services/ClientApiService";
import {ClientNotificationService} from "../../services/ClientNotificationService";
import {ClientAuthenticationService} from "../../services/clients/ClientAuthenticationService";
import {ClientDataConstraintService} from "../../services/ClientDataConstraintService";
import {SubmitPasswordViewModel} from "../../viewmodels/accounts/SubmitPasswordViewModel";

@Component({
    selector: 'account-submit-password',
    templateUrl: 'account-submit-password.component.html',
    providers: [
        ClientAccountService,
        ClientApiService,
        ClientNotificationService,
        ClientAuthenticationService,
        ClientDataConstraintService
    ]
})

export class AccountSubmitPasswordComponent {

    // Form contains controls of account password submission form.
    private accountPasswordSubmitBox: FormGroup;

    // Initiate change account password submit model.
    private accountPasswordSubmitModel: SubmitPasswordViewModel;

    public constructor(private clientAccountService: ClientAccountService,
                       private clientNotificationService: ClientNotificationService,
                       private clientDataConstraintService: ClientDataConstraintService,
                       private clientApiService: ClientApiService,
                       private clientRoutingService: Router,
                       private formBuilder: FormBuilder) {

        // Initiate account password submission box.
        this.accountPasswordSubmitBox = this.formBuilder.group({
            email: [''],
            token: [''],
            password: [''],
            passwordConfirmation: ['']
        });

        this.accountPasswordSubmitModel = new SubmitPasswordViewModel();
    }

    // This callback is fired when submit button is clicked.
    public clickSubmitPassword(): void {

        // Call service to change password.
        this.clientAccountService.submitPasswordRequest(this.accountPasswordSubmitModel)
            .then((response: Response) => {
                // Tell user password has been changed successfully.
                this.clientNotificationService.success('SUBMIT_PASSWORD_SUCCESSFULLY');

                // Redirect user to login page.
                this.clientRoutingService.navigate(['/']);
            })
            .catch((response: Response) => {
                // Proceed common response.
                this.clientApiService.proceedHttpNonSolidResponse(response);
            });
    }
}
import {Component, Inject} from "@angular/core";
import {FormGroup, FormBuilder} from "@angular/forms";
import {Router} from "@angular/router";
import {Response} from "@angular/http";
import {SubmitPasswordViewModel} from "../../../viewmodels/accounts/submit-password.view-model";
import {ClientDataConstraintService} from "../../../services/client-data-constraint.service";
import {IAccountService} from "../../../interfaces/services/api/account-service.interface";
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'account-submit-password',
    templateUrl: 'account-submit-password.component.html'
})

export class AccountSubmitPasswordComponent {

    // Form contains controls of account password submission form.
    private accountPasswordSubmitBox: FormGroup;

    // Initiate change account password submit model.
    private accountPasswordSubmitModel: SubmitPasswordViewModel;

    public constructor(@Inject("IAccountService") private accountService: IAccountService,
                       private toastr: ToastrService,
                       private clientDataConstraintService: ClientDataConstraintService,
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
        this.accountService.submitPasswordReset(this.accountPasswordSubmitModel)
            .then((response: Response) => {
                // Tell user password has been changed successfully.
                this.toastr.success('SUBMIT_PASSWORD_SUCCESSFULLY');

                // Redirect user to login page.
                this.clientRoutingService.navigate(['/']);
            })
            .catch((response: Response) => {
                // Proceed common response.
            });
    }
}

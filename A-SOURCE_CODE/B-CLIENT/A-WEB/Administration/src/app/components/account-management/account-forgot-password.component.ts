import {Inject, Component} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Response} from '@angular/http';
import {Router} from '@angular/router';
import {ClientDataConstraintService} from '../../../services/client-data-constraint.service';
import {ClientApiService} from '../../../services/client-api.service';
import {ClientToastrService} from '../../../services/client-toastr.service';
import {Account} from '../../../models/entities/account';
import {IClientAccountService} from "../../../interfaces/services/api/account-service.interface";

@Component({
  selector: 'account-forgot-password-box',
  templateUrl: 'account-forgot-password.component.html',
  inputs: ['isBusy'],
})

export class AccountForgotPasswordComponent {

  //#region Properties

  // Form of request password change.
  private accountPasswordChangeBox: FormGroup;

  // Account information.
  private account: Account;

  // Whether component is busy or not.
  private isLoading: boolean;

  //#endregion

  //#region Constructor

  // Initiate component with default settings.
  public constructor(private formBuilder: FormBuilder,
                     private clientDataConstraintService: ClientDataConstraintService,
                     @Inject('IClientAccountService') private clientAccountService: IClientAccountService,
                     private clientNotificationService: ClientToastrService,
                     private clientApiService: ClientApiService,
                     private clientRoutingService: Router) {

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

  //#endregion

  // Callback which is fired when seach button is clicked for requesting a password reset.
  public clickRequestPassword(): void {

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
  }
}

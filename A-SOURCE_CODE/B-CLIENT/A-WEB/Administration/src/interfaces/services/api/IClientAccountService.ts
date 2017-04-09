import {SearchAccountsViewModel} from "../../../viewmodels/accounts/SearchAccountsViewModel";
import {LoginViewModel} from "../../../viewmodels/accounts/LoginViewModel";
import {Account} from "../../../models/Account";
import {SubmitPasswordViewModel} from "../../../viewmodels/accounts/SubmitPasswordViewModel";
import {Response} from "@angular/http";

/*
 * Provides function to deal with accounts api.
 * */
export interface IClientAccountService {

    //#region Methods

    // Find accounts by using specific conditions.
    getAccounts(conditions: SearchAccountsViewModel): Promise<Response>;

    // Call login api to obtain client authentication token instance.
    login(loginViewModel: LoginViewModel): Promise<Response>;

    // Change account information.
    editUserProfile(index: number, information: Account): Promise<Response>;

    // Send request to service to request an instruction email to change password.
    sendPasswordChangeRequest(email: string): Promise<Response>;

    // Submit new password to service.
    submitPasswordReset(submitPasswordViewModel: SubmitPasswordViewModel): Promise<Response>

    //#endregion
}
import {FindAccountsViewModel} from "../../viewmodels/accounts/FindAccountsViewModel";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {AccountStatuses} from "../../enumerations/AccountStatuses";

/*
 * Provides function to deal with accounts api.
 * */
export interface IClientAccountService {

    // Find accounts by using specific conditions.
    findAccounts(conditions: FindAccountsViewModel): any;

    // Call login api to obtain client authentication token instance.
    login(loginViewModel: LoginViewModel): any;

    // Send request to service to obtain token to change password
    initiatePasswordChangeRequest(email: string);

    // From the token which has been sent to mail to change password of account.
    initiatePasswordChange(email: string, password: string, token: string);

    // Change account status in the system
    changeAccountInformation(id: number, status: AccountStatuses);
}
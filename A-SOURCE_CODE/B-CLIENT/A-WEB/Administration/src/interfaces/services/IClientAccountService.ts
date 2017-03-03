import {FindAccountsViewModel} from "../../viewmodels/accounts/FindAccountsViewModel";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {AccountStatuses} from "../../enumerations/AccountStatuses";
import {Account} from "../../models/Account";

/*
 * Provides function to deal with accounts api.
 * */
export interface IClientAccountService {

    // Find accounts by using specific conditions.
    findAccounts(conditions: FindAccountsViewModel): any;

    // Call login api to obtain client authentication token instance.
    login(loginViewModel: LoginViewModel): any;

    // Change account information.
    changeAccountInformation(index: number, information: Account) : any;
}
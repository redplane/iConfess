import {FindAccountsViewModel} from "../../viewmodels/accounts/FindAccountsViewModel";

/*
* Provides function to deal with accounts api.
* */
export interface IClientAccountService{

    // Find accounts by using specific conditions.
    findAccounts(conditions: FindAccountsViewModel): any;
}
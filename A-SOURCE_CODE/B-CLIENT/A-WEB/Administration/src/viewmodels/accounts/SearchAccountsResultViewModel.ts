import {Account} from "../../models/Account";

/*
 * Result instance of category search.
 * */
export class SearchAccountsResultViewModel{

    // List of detailed accounts responded from service.
    public accounts: Account[];

    // Total records which match with search conditions.
    public total: number;
}
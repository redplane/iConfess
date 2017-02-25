import {Injectable} from '@angular/core';
import {ClientApiService} from "../ClientApiService";
import 'rxjs/add/operator/toPromise';
import {IClientAccountService} from "../../interfaces/services/IClientAccountService";
import {FindAccountsViewModel} from "../../viewmodels/accounts/FindAccountsViewModel";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {ClientAuthenticationService} from "./ClientAuthenticationService";
import {Account} from "../../models/Account";
import {AccountSubmitPasswordComponent} from "../../components/views/account-management/account-submit-password.component";
import {SubmitPasswordViewModel} from "../../viewmodels/accounts/SubmitPasswordViewModel";

/*
 * Service which handles category business.
 * */
@Injectable()
export class ClientAccountService implements IClientAccountService {

    // Initiate instance of category service.
    public constructor(private clientApiService: ClientApiService,
                       public clientAuthenticationService: ClientAuthenticationService){
    }

    // Find categories by using specific conditions.
    public findAccounts(findAccountsViewModel: FindAccountsViewModel) {
        // Page index should be decrease by one.
        let conditions = Object.assign({}, findAccountsViewModel);
        conditions['pagination'] = Object.assign({}, findAccountsViewModel.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;

        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiFindAccount,
            null,
            conditions).toPromise();
    }

    // Sign an account into system.
    public login(loginViewModel: LoginViewModel): any {
        return this.clientApiService.post(null, this.clientApiService.apiLogin, null, loginViewModel)
            .toPromise();
    }

    // Change account information in service.
    public changeAccountInformation(index: number, information: Account) : any{

        // Build a complete url of account information change.
        let urlParameters = {
            id: index
        };

        return this.clientApiService.put(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiChangeAccountInfo, urlParameters, information)
            .toPromise();
    }

    // Request service to send an email which is for changing account password.
    public requestPasswordChange(email: string): any{
        // Parameter construction.
        let urlParameters = {
            email: email
        };

        return this.clientApiService.get(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiRequestChangePassword, urlParameters)
            .toPromise();
    }

    // Request service to change password by using specific token.
    public submitPasswordRequest(submitPasswordViewModel: SubmitPasswordViewModel){
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(),
        this.clientApiService.apiRequestSubmitPassword, null, submitPasswordViewModel)
            .toPromise();
    }
}
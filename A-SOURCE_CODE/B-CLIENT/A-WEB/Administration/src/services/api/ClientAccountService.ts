import {Injectable, Inject} from '@angular/core';
import {ClientApiService} from "../ClientApiService";
import 'rxjs/add/operator/toPromise';
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {Account} from "../../models/Account";
import {SubmitPasswordViewModel} from "../../viewmodels/accounts/SubmitPasswordViewModel";
import {Response} from "@angular/http";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";

/*
 * Service which handles category business.
 * */
@Injectable()
export class ClientAccountService implements IClientAccountService {

    //#region Constructor

    // Initiate instance of category service.
    public constructor(private clientApiService: ClientApiService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService){
    }

    //#endregion

    //#region Methods

    // Find categories by using specific conditions.
    public getAccounts(findAccountsViewModel: SearchAccountsViewModel): Promise<Response> {
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
    public login(loginViewModel: LoginViewModel): Promise<Response> {
        return this.clientApiService.post(null, this.clientApiService.apiLogin, null, loginViewModel)
            .toPromise();
    }

    // Change account information in service.
    public editUserProfile(index: number, information: Account): Promise<Response>{

        // Build a complete url of account information change.
        let urlParameters = {
            id: index
        };

        return this.clientApiService.put(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiChangeAccountInfo, urlParameters, information)
            .toPromise();
    }

    // Request service to send an email which is for changing account password.
    public sendPasswordChangeRequest(email: string): Promise<Response>{
        // Parameter construction.
        let urlParameters = {
            email: email
        };

        return this.clientApiService.get(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiRequestChangePassword, urlParameters)
            .toPromise();
    }

    // Request service to change password by using specific token.
    public submitPasswordReset(submitPasswordViewModel: SubmitPasswordViewModel): Promise<Response>{
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(),
        this.clientApiService.apiRequestSubmitPassword, null, submitPasswordViewModel)
            .toPromise();
    }

    //#endregion
}
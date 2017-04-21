import {Injectable, Inject} from '@angular/core';
import 'rxjs/add/operator/toPromise';
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {Account} from "../../models/entities/Account";
import {SubmitPasswordViewModel} from "../../viewmodels/accounts/SubmitPasswordViewModel";
import {Response} from "@angular/http";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";

/*
 * Service which handles category business.
 * */
@Injectable()
export class ClientAccountService implements IClientAccountService {

    //#region Properties

    // Url which is used for signing user into system.
    public urlLogin: string = "api/account/login";

    // Url which is used for searching accounts in the system.
    public urlSearchAccount: string = "api/account/find";

    // Url which is used for changing account information.
    public urlChangeAccountInfo: string = "api/account";

    // Url which is used for requesting password change.
    public urlRequestChangePassword = "api/account/forgot-password";

    // Url which is used for submitting password change.
    public urlSubmitPasswordReset = "api/account/forgot-password";

    // Url which is for getting self profile.
    public urlGetProfile = "api/account/profile";

    //#endregion

    //#region Constructor

    // Initiate instance of category service.
    public constructor(@Inject("IClientApiService") public clientApiService: IClientApiService,
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
        return this.clientApiService.post(this.clientAuthenticationService.getTokenCode(),
            `${this.clientApiService.getBaseUrl()}/${this.urlSearchAccount}`,
            null,
            conditions);
    }

    // Sign an account into system.
    public login(loginViewModel: LoginViewModel): Promise<Response> {
        return this.clientApiService.post(null,
            `${this.clientApiService.getBaseUrl()}/${this.urlLogin}`, null, loginViewModel);
    }

    // Change account information in service.
    public editUserProfile(index: number, information: Account): Promise<Response>{

        // Build a complete url of account information change.
        let urlParameters = {
            id: index
        };

        return this.clientApiService.put(
            this.clientAuthenticationService.getTokenCode(),
            `${this.clientApiService.getBaseUrl()}/${this.urlChangeAccountInfo}`,
            urlParameters, information);
    }

    // Request service to send an email which is for changing account password.
    public sendPasswordChangeRequest(email: string): Promise<Response>{
        // Parameter construction.
        let urlParameters = {
            email: email
        };

        return this.clientApiService.get(
            this.clientAuthenticationService.getTokenCode(),
            `${this.clientApiService.getBaseUrl()}/${this.urlRequestChangePassword}`,
            urlParameters);
    }

    // Request service to change password by using specific token.
    public submitPasswordReset(submitPasswordViewModel: SubmitPasswordViewModel): Promise<Response>{
        return this.clientApiService
            .post(
                this.clientAuthenticationService.getTokenCode(),
                `${this.clientApiService.getBaseUrl()}/${this.urlSubmitPasswordReset}`,
                null,
                submitPasswordViewModel);
    }

    // Request service to return account profile.
    public getClientProfile(): Promise<Response>{
        return this.clientApiService
            .post(
                this.clientAuthenticationService.getTokenCode(),
                `${this.clientApiService.getBaseUrl()}/${this.urlGetProfile}`,
                null,
                null);
    }

    //#endregion
}
import {Injectable, Inject} from '@angular/core';
import 'rxjs/add/operator/toPromise';
import {SearchAccountsViewModel} from "../../viewmodels/accounts/search-accounts.view-model";
import {LoginViewModel} from "../../viewmodels/accounts/login.view-model";
import {Account} from "../../models/entities/account";
import {SubmitPasswordViewModel} from "../../viewmodels/accounts/submit-password.view-model";
import {Response} from "@angular/http";
import {IAccountService} from "../../interfaces/services/api/account-service.interface";
import {IApiService} from "../../interfaces/services/api/api-service.interface";
import {environment} from "../../environments/environment";
import {ApiUrl} from "../../constants/api-url";

/*
 * Service which handles category business.
 * */
@Injectable()
export class AccountService implements IAccountService {

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
    public constructor(@Inject("IApiService") public clientApiService: IApiService){
    }

    //#endregion

    //#region Methods

    // Find categories by using specific conditions.
    public getAccounts(conditions: SearchAccountsViewModel): Promise<Response> {
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(environment.baseUrl, ApiUrl.getAccounts,
            null,
            conditions);
    }

    /*
    * Sign an account into system.
    * */
    public login(loginViewModel: LoginViewModel): Promise<Response> {
        return this.clientApiService.post(environment.baseUrl, ApiUrl.login , null, loginViewModel);
    }

    /*
    * Change account information in service.
    * */
    public editUserProfile(index: number, information: Account): Promise<Response>{

        // Build a complete url of account information change.
        let urlParameters = {
            id: index
        };

        return this.clientApiService.put(environment.baseUrl, ApiUrl.editAccount, urlParameters, information);
    }

    // Request service to send an email which is for changing account password.
    public sendPasswordChangeRequest(email: string): Promise<Response>{
        // Parameter construction.
        let urlParameters = {
            email: email
        };

        return this.clientApiService.get(environment.baseUrl, ApiUrl.submitAccountPassword, urlParameters);
    }

    /*
    * Request service to change password by using specific token.
    * */
    public submitPasswordReset(submitPasswordViewModel: SubmitPasswordViewModel): Promise<Response>{
        return this.clientApiService
            .post(environment.baseUrl, ApiUrl.requestPasswordReset,
                null,
                submitPasswordViewModel);
    }

    /*
    * Request service to return account profile.
    * */
    public getClientProfile(): Promise<Response>{
        return this.clientApiService
            .post(
                environment.baseUrl, ApiUrl.getAccountProfile,
                null,
                null);
    }

    //#endregion
}

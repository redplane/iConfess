import {Injectable} from '@angular/core';
import {ClientApiService} from "../ClientApiService";
import {Http, Headers, RequestOptions, Response} from "@angular/http";
import 'rxjs/add/operator/toPromise';
import {IClientAccountService} from "../../interfaces/services/IClientAccountService";
import {FindAccountsViewModel} from "../../viewmodels/accounts/FindAccountsViewModel";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {AccountStatuses} from "../../enumerations/AccountStatuses";

/*
 * Service which handles category business.
 * */
@Injectable()
export class ClientAccountService implements IClientAccountService {

    // Service which handles hyperlink.
    private _clientApiService: ClientApiService;

    // HttpClient which is used for handling request to web api service.
    private _httpClient: Http;

    // Initiate instance of category service.
    public constructor(clientApiService: ClientApiService, httpClient: Http){

        this._clientApiService = clientApiService;
        this._httpClient = httpClient;
    }

    // Find categories by using specific conditions.
    public findAccounts(findAccountsViewModel: FindAccountsViewModel) {

        // Page index should be decrease by one.
        let conditions = Object.assign({}, findAccountsViewModel);
        conditions['pagination'] = Object.assign({}, findAccountsViewModel.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;

        let requestOptions = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        // Request to api to obtain list of available categories in system.
        return this._httpClient.post(this._clientApiService.apiFindAccount, conditions, requestOptions)
            .toPromise();
    }

    // Sign an account into system.
    public login(loginViewModel: LoginViewModel): any {
        return this._httpClient.post(this._clientApiService.apiLogin, loginViewModel).toPromise();
    }

    // Send request to service to obtain token to change password
    public initiatePasswordChangeRequest(email: string): any{
        throw 'Not implemented exception';
    }

    // From the token which has been sent to mail to change password of account.
    public initiatePasswordChange(email: string, password: string, token: string): any {
        throw 'Not implemented exception';
    }

    // Up to account information to forbid account access to system.
    public forbidAccountAccess(id: number): any{
        throw 'Not implemented exception';
    }


    public changeAccountInformation(id: number, status: AccountStatuses) : any {
        throw 'Not implemented exception';
    }
}
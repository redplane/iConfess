import {IClientAuthenticationService} from "../../interfaces/services/IClientAuthenticationService";
import {ClientAuthenticationToken} from "../../models/ClientAuthenticationToken";
import {Injectable} from "@angular/core";
import {ClientApiService} from "../ClientApiService";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";
import {Http} from "@angular/http";

/*
* Implement authentication business handler
* */
@Injectable()
export class ClientAuthenticationService implements IClientAuthenticationService{

    // Key in local storage where authentication token should be stored at.
    private _authenticationKey : string;

    // The the name of key which is used for sotring authentication information.
    public findAuthenticationStorageKey(): string{
        return this._authenticationKey;
    }

    // Find client authentication token from local storage.
    public findClientAuthenticationToken(): ClientAuthenticationToken {

        // Find information from local storage with given key.
        let clientAuthenticationInfo = localStorage.getItem(this._authenticationKey);

        // No information is stored in localStorage.
        if (clientAuthenticationInfo == null || clientAuthenticationInfo.length < 1)
            return null;

        // Parse the information into authentication class.
        let clientAuthenticationToken = new ClientAuthenticationToken();
        clientAuthenticationToken = JSON.parse(clientAuthenticationInfo);
        return clientAuthenticationToken;
    }

    // Check whether client authentication information is valid to login or not.
    public isAuthenticationSolid(clientAuthenticationToken: ClientAuthenticationToken): boolean {

        // Token is empty.
        if (clientAuthenticationToken.token == null || clientAuthenticationToken.token.length < 1)
            return false;

        // Token type is invalid.
        if (clientAuthenticationToken.type == null || clientAuthenticationToken.type.length < 1)
            return false;

        // Token expiration is invalid.
        if (clientAuthenticationToken.expire == null)
            return false;

        // Token has been expired.
        if (clientAuthenticationToken.expire < Date.now())
            return false;

        return true;
    }

    // Save authentication information into local storage.
    public saveAuthenticationToken(clientAuthenticationToken: ClientAuthenticationToken): void {

        // Serialize token into string.
        let authenticationInfo = JSON.stringify(clientAuthenticationToken);

        // Save the authentication information into local storage
        localStorage.setItem(this._authenticationKey, authenticationInfo);
    }

    // Initiate service with IoC.
    public constructor(clientApiService: ClientApiService){
        this._authenticationKey = "authentication-iConfess";
    }
}
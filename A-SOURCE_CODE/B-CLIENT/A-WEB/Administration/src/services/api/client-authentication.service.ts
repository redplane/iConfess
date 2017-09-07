import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {AuthenticationToken} from "../../models/authentication-token";
import {Injectable} from "@angular/core";

/*
* Implement authentication business handler
* */
@Injectable()
export class ClientAuthenticationService implements IClientAuthenticationService{

    //#region Properties

    // Key in local storage where authentication token should be stored at.
    private tokenStorageKey : string;

    //#endregion

    //#region Methods

    // The the name of key which is used for sotring authentication information.
    public getTokenKey(): string{
        return this.tokenStorageKey;
    }

    // Find client authentication token from local storage.
    public getTokenCode(): string {

        // Find information from local storage with given key.
        let authenticationToken = this.getToken();

        // Token is not valid.
        if (authenticationToken == null){
            // Clear token from local storage.
            this.clearToken();
            return "";
        }

        // Parse the information into authentication class.
        return authenticationToken.token;
    }

    // Save authentication information into local storage.
    public setToken(authenticationToken: AuthenticationToken): void {

        // Save the authentication information into local storage
        localStorage.setItem(this.tokenStorageKey, JSON.stringify(authenticationToken));
    }

    // Clear authentication token from local storage.
    public clearToken(): void{
        localStorage.removeItem(this.tokenStorageKey);
    }

    // Get token which is stored inside local storage.
    public getToken(): AuthenticationToken{

        // Get token which is stored inside local storage.
        let item = localStorage.getItem(this.tokenStorageKey);

        // Item is not valid.
        if (item == null || item.length < 1)
            return null;

        // Cast item to authentication token.
        let authToken = <AuthenticationToken> JSON.parse(item);

        // Authentication is not valid.
        if (authToken == null || (authToken.expireAt > Date.now()))
            return null;

        // Token is empty.
        let code = authToken.token;
        if (code == null || code.length < 1)
            return null;

        return authToken;
    }
    //#endregion

    //#region Constructor

    // Initiate service with IoC.
    public constructor(){
        this.tokenStorageKey = "iConfess.Administration";
    }

    //#endregion
}

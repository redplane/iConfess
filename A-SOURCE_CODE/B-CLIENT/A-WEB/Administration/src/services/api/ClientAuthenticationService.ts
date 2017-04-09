import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {ClientAuthenticationToken} from "../../models/ClientAuthenticationToken";
import {Injectable} from "@angular/core";

/*
* Implement authentication business handler
* */
@Injectable()
export class ClientAuthenticationService implements IClientAuthenticationService{

    // Key in local storage where authentication token should be stored at.
    private _authenticationKey : string;

    // Key in local storage indicates when the authentication token should be expired.
    private _authenticationExpire: number;

    // The the name of key which is used for sotring authentication information.
    public findAuthenticationStorageKey(): string{
        return this._authenticationKey;
    }

    // When should the token be expired.
    public findAuthenticationExpire(): number{
        return this._authenticationExpire;
    }

    // Find client authentication token from local storage.
    public findClientAuthenticationToken(): string {

        // Find information from local storage with given key.
        let clientAuthenticationToken = localStorage.getItem(this._authenticationKey);

        // Parse the information into authentication class.
        return clientAuthenticationToken;
    }

    // Save authentication information into local storage.
    public initiateLocalAuthenticationToken(clientAuthenticationToken: ClientAuthenticationToken): void {
        // Save the authentication information into local storage
        localStorage.setItem(this._authenticationKey, clientAuthenticationToken.token);
    }

    // Clear authentication token from local storage.
    public clearAuthenticationToken(): void{
        localStorage.removeItem(this._authenticationKey);
    }

    // Initiate service with IoC.
    public constructor(){
        this._authenticationKey = "iConfessAuthenticationToken";
    }
}
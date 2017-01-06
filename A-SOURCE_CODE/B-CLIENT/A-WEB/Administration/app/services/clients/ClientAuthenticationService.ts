import {IClientAuthenticationService} from "../../interfaces/services/IClientAuthenticationService";
import {ClientAuthenticationToken} from "../../models/ClientAuthenticationToken";

// Implement authentication business handler.
export class ClientAuthenticationService implements IClientAuthenticationService{

    // Key in local storage where authentication token should be stored at.
    private _authenticationKey : string;

    findAuthenticationStorageKey: string;

    // Find client authentication token from local storage.
    findClientAuthenticationToken(): ClientAuthenticationToken {

    }

    isAuthenticationSolid(clientAuthenticationToken: ClientAuthenticationToken): boolean {
        return undefined;
    }

    saveAuthenticationToken(clientAuthenticationToken: ClientAuthenticationToken): void {
    }

    // Initiate service with IoC.
    public constructor(){
        this._authenticationKey = "authentication-iConfess";
    }
}
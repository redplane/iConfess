import {ClientAuthenticationToken} from "../../models/ClientAuthenticationToken";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";

export interface IClientAuthenticationService{

    // Find key in local storage where authentication information is stored.
    findAuthenticationStorageKey(): string;

    // Find client authentication token information from local storage.
    findClientAuthenticationToken(): ClientAuthenticationToken;

    // Check whether authentication information is valid or not.
    isAuthenticationSolid(clientAuthenticationToken: ClientAuthenticationToken): boolean;

    // Save authentication token information into local storage for future use.
    saveAuthenticationToken(clientAuthenticationToken: ClientAuthenticationToken): void;
}
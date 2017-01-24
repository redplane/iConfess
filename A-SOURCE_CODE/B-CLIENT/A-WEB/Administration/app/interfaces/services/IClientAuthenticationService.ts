import {ClientAuthenticationToken} from "../../models/ClientAuthenticationToken";
import {LoginViewModel} from "../../viewmodels/accounts/LoginViewModel";

export interface IClientAuthenticationService{

    // Find key in local storage where authentication information is stored.
    findAuthenticationStorageKey(): string;

    // Find client authentication token information from local storage.
    findClientAuthenticationToken(): string;

    // Save authentication token information into local storage for future use.
    initiateLocalAuthenticationToken(clientAuthenticationToken: ClientAuthenticationToken): void;

    // Clear authentication token from local storage.
    clearAuthenticationToken(): void;
}
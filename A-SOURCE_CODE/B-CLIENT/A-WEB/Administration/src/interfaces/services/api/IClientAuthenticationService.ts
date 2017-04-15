import {ClientAuthenticationToken} from "../../../models/ClientAuthenticationToken";

export interface IClientAuthenticationService{

    //#region Methods

    // Find key in local storage where authentication information is stored.
    findAuthenticationStorageKey(): string;

    // Find client authentication token information from local storage.
    findClientAuthenticationToken(): string;

    // Save authentication token information into local storage for future use.
    initiateLocalAuthenticationToken(clientAuthenticationToken: ClientAuthenticationToken): void;

    // Clear authentication token from local storage.
    clearAuthenticationToken(): void;

    //#endregion
}
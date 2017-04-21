import {AuthenticationToken} from "../../../models/AuthenticationToken";

export interface IClientAuthenticationService{

    //#region Methods

    // Find key in local storage where authentication information is stored.
    getTokenKey(): string;

    // Find client authentication token information from local storage.
    getTokenCode(): string;

    // Get authentication token from local storage.
    getToken(): AuthenticationToken;

    // Save authentication token information into local storage for future use.
    setToken(authenticationToken: AuthenticationToken): void;

    // Clear authentication token from local storage.
    clearToken(): void;

    //#endregion
}
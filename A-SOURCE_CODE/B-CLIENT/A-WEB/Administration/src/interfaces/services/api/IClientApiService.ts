import {Response} from "@angular/http";

export interface IClientApiService{

    //#region Methods

    // Get API base url.
    getBaseUrl(): string;

    // Send 'GET' to service.
    get(clientAuthenticationToken: string, url: string, parameters: any) : Promise<Response>;

    // Send 'POST' to service.
    post(clientAuthenticationToken: string, url: string, parameters: any, body: any) : Promise<Response>;

    // Send 'PUT' to service.
    put(clientAuthenticationToken: string, url: string, parameters: any, body: any) : Promise<Response>;

    // Send 'PUT' to service.
    delete(clientAuthenticationToken: string, url: string, parameters: any, body: any) : Promise<Response>;

    // Common function to proceed invalid response.
    handleInvalidResponse(x : Response): void;

    //#endregion
}
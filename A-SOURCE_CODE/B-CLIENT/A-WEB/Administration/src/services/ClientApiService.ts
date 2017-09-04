import {Inject, Injectable} from "@angular/core";
import {Headers, Http, RequestOptions, Response} from "@angular/http";
import {Router} from "@angular/router";
import {IClientAuthenticationService} from "../interfaces/services/api/IClientAuthenticationService";
import {IClientToastrService} from "../interfaces/services/IClientToastrService";
import {IClientApiService} from "../interfaces/services/api/IClientApiService";

/*
* Service which handles hyperlink of api.
* */
@Injectable()
export class ClientApiService implements IClientApiService{

    //#region Properties

    // Api which web application will consume the service.
    // private apiUrl = "http://confession.azurewebsites.net";
    // private apiUrl = "http://localhost:2101";
    private apiUrl = 'http://192.168.1.102:45455';
    // Key in local storage which access token should be stored.
    public accessTokenStorage: string;

    //#endregion

    //#region Constructor

    // Initiate service with settings.
    public constructor(public clientRequestService: Http,
                       @Inject("IClientToastrService") public clientToastrService: IClientToastrService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService,
                       public clientRoutingService: Router){

        // Key of local storage in which access token should be stored.
        this.accessTokenStorage = 'iConfess';
    }

    //#endregion

    //#region Methods

    // Get base url.
    public getBaseUrl(): string{
        return this.apiUrl;
    }

    // Send 'GET' to service.
    public get(clientAuthenticationToken: string, url: string, parameters: any) : Promise<Response> {

        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = `${url}?${this.encryptUrlParameters(parameters)}`;

        // Initiate headers.
        let clientRequestHeaders = new Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');

        if (clientAuthenticationToken != null && clientAuthenticationToken.length > 0)
            clientRequestHeaders.append('Authorization', `Bearer ${clientAuthenticationToken}`);

        // Initiate request options.
        let clientRequestOptions = new RequestOptions({
            headers: clientRequestHeaders
        });

        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.get(url, clientRequestOptions)
            .toPromise();
    }

    // Send 'POST' to service.
    public post(clientAuthenticationToken: string, url: string, parameters: any, body: any) : Promise<Response> {

        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = `${url}?${this.encryptUrlParameters(parameters)}`;

        // Initiate headers.
        let clientRequestHeaders = new Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');

        if (clientAuthenticationToken != null && clientAuthenticationToken.length > 0)
            clientRequestHeaders.append('Authorization', `Bearer ${clientAuthenticationToken}`);

        // Initiate request options.
        let clientRequestOptions = new RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });

        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.post(url, null, clientRequestOptions)
            .toPromise();
    }

    // Send 'PUT' to service.
    public put(clientAuthenticationToken: string, url: string, parameters: any, body: any) : Promise<Response> {

        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = `${url}?${this.encryptUrlParameters(parameters)}`;

        // Initiate headers.
        let clientRequestHeaders = new Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        if (clientAuthenticationToken != null && clientAuthenticationToken.length > 0)
            clientRequestHeaders.append('Authorization', `Bearer ${clientAuthenticationToken}`);

        // Initiate request options.
        let clientRequestOptions = new RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });

        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.put(url, null, clientRequestOptions)
            .toPromise();
    }

    // Send 'PUT' to service.
    public delete(clientAuthenticationToken: string, url: string, parameters: any, body: any) : Promise<Response> {

        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = `${url}?${this.encryptUrlParameters(parameters)}`;

        // Initiate headers.
        let clientRequestHeaders = new Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        if (clientAuthenticationToken != null)
            clientRequestHeaders.append('Authorization', `Bearer ${clientAuthenticationToken}`);

        // Initiate request options.
        let clientRequestOptions = new RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });

        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.delete(url, clientRequestOptions)
            .toPromise();
    }

    // Common function to proceed invalid response.
    public handleInvalidResponse(response : Response): void{

        // Find response information of request.
        let information = response.json();

        // Base on the status code to determine what action should be taken.
        switch (response.status){

            // This status is about invalid parameters have been submitted to service.
            case 400:
                // TODO: Form control should be passed here to update screen display.
                break;

            // This status is about invalid authentication information has been passed to service.
            case 401:
                // Clear the local storage.
                this.clientAuthenticationService.clearToken();

                // Display the error message.
                this.clientToastrService.error(information['message'], 'System', null);

                // Redirect user back to login page.
                this.clientRoutingService.navigate(['/']);

                break;

            // This status is about request doesn't have enough permission to access service function.
            case 403:
                // Display the error message.
                this.clientToastrService.error(information['message'], 'System', null);
                break;

            // Something went wrong with the service.
            case 500:
                // Display the error message.
                this.clientToastrService.error('Service malfunctioned. Please try again', 'System', null);
                break;

            // For default error. Just display messages sent back from service.
            default:
                this.clientToastrService.error(information['message'], 'System', null);
                break;
        }
    }

    // Encrypt url parameters to prevent dangerous parameters are passed to service.
    private encryptUrlParameters(parameters: any) : string{
        return Object.keys(parameters).map(key => {
            return [key, parameters[key]].map(encodeURIComponent).join("=");
        }).join("&");
    }

    //#endregion
}
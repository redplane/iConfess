import {Injectable} from "@angular/core";
import {TokenViewModel} from "../viewmodels/accounts/TokenViewModel";
import {Headers, Http, RequestOptions, Response} from "@angular/http";
import {ClientNotificationService} from "./ClientNotificationService";
import {ClientAuthenticationService} from "./clients/ClientAuthenticationService";
import {Router} from "@angular/router";

/*
* Service which handles hyperlink of api.
* */
@Injectable()
export class ClientApiService{

    // Api which web application will consume the service.
    private apiUrl = "http://confession.azurewebsites.net";
    //private apiUrl = "http://localhost:2101";

    // Hyperlink which is used for searching for categories.
    public apiFindCategory : string;

    // Hyperlink which is used for searching for categories for deleting 'em.
    public apiDeleteCategory : string;

    // Hyperlink which is used for changing category information.
    public apiChangeCategoryDetail: string;

    // Hyperlink which is used for initiating category information.
    public apiInitiateCategory: string;

    // Hyperlink which is used for searching for accounts.
    public apiFindAccount : string;

    // Hyperlink which is used for logging an user into system.
    public apiLogin : string;

    // Key in local storage which access token should be stored.
    public accessTokenStorage: string;

    // Initiate service with settings.
    public constructor(public clientRequestService: Http,
                       public clientNotificationService: ClientNotificationService,
                       public clientAuthenticationService: ClientAuthenticationService,
                       public clientRoutingService: Router){

        // Find category api url.
        this.apiFindCategory = `${this.apiUrl}/api/category/find`;
        this.apiDeleteCategory = `${this.apiUrl}/api/category`;
        this.apiChangeCategoryDetail = `${this.apiUrl}/api/category`;
        this.apiInitiateCategory = `${this.apiUrl}/api/category`;

        // Find category account api url.
        this.apiFindAccount = `${this.apiUrl}/api/account/find`;

        // Initiate api which is used for logging into system.
        this.apiLogin = `${this.apiUrl}/api/account/login`;

        // Key of local storage in which access token should be stored.
        this.accessTokenStorage = 'iConfess';
    }

    // Send 'GET' to service.
    public get(clientAuthenticationToken: TokenViewModel, url: string, parameters: any) : any {

        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = `${url}?${this.encryptUrlParameters(parameters)}`;

        // Initiate headers.
        let clientRequestHeaders = new Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        clientRequestHeaders.append('Authorization', `Bearer ${clientAuthenticationToken.token}`);

        // Initiate request options.
        let clientRequestOptions = new RequestOptions({
            headers: clientRequestHeaders
        });

        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.get(url, clientRequestOptions);
    }

    // Send 'POST' to service.
    public post(clientAuthenticationToken: TokenViewModel, url: string, parameters: any, body: any) : any {

        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = `${url}?${this.encryptUrlParameters(parameters)}`;

        // Initiate headers.
        let clientRequestHeaders = new Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        clientRequestHeaders.append('Authorization', `Bearer ${clientAuthenticationToken.token}`);

        // Initiate request options.
        let clientRequestOptions = new RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });

        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.post(url, null, clientRequestOptions);
    }

    // Send 'PUT' to service.
    public put(clientAuthenticationToken: TokenViewModel, url: string, parameters: any, body: any) : any {

        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = `${url}?${this.encryptUrlParameters(parameters)}`;

        // Initiate headers.
        let clientRequestHeaders = new Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        clientRequestHeaders.append('Authorization', `Bearer ${clientAuthenticationToken.token}`);

        // Initiate request options.
        let clientRequestOptions = new RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });

        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.put(url, null, clientRequestOptions);
    }

    // Send 'PUT' to service.
    public delete(clientAuthenticationToken: TokenViewModel, url: string, parameters: any, body: any) : any {

        // Parameters are specified. Reconstruct 'em.
        if (parameters != null)
            url = `${url}?${this.encryptUrlParameters(parameters)}`;

        // Initiate headers.
        let clientRequestHeaders = new Headers();
        clientRequestHeaders.append('Content-Type', 'application/json');
        clientRequestHeaders.append('Authorization', `Bearer ${clientAuthenticationToken.token}`);

        // Initiate request options.
        let clientRequestOptions = new RequestOptions({
            headers: clientRequestHeaders,
            body: body
        });

        // Request to api to obtain list of available categories in system.
        return this.clientRequestService.delete(url, clientRequestOptions);
    }

    // Common function to proceed invalid response.
    public proceedHttpNonSolidResponse(response : Response){

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
                this.clientAuthenticationService.clearAuthenticationToken();

                // Display the error message.
                this.clientNotificationService.error(information['message'], 'System');

                // Redirect user back to login page.
                this.clientRoutingService.navigate(['/']);
                break;

            // This status is about request doesn't have enough permission to access service function.
            case 403:
                // Display the error message.
                this.clientNotificationService.error(information['message'], 'System');
                break;

            // Something went wrong with the service.
            case 500:
                // Display the error message.
                this.clientNotificationService.error('Service malfunctioned. Please try again');
                break;

            // For default error. Just display messages sent back from service.
            default:
                this.clientNotificationService.error(information['message'], 'System');
                break;
        }
    }

    // Encrypt url parameters to prevent dangerous parameters are passed to service.
    private encryptUrlParameters(parameters: any) : string{
        return Object.keys(parameters).map(key => {
            return [key, parameters[key]].map(encodeURIComponent).join("=");
        }).join("&");
    }
}
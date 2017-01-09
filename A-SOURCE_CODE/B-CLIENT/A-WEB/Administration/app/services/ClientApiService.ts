import {Injectable} from "@angular/core";

/*
* Service which handles hyperlink of api.
* */
@Injectable()
export class ClientApiService{

    // Api which web application will consume the service.
    private apiUrl = "http://confession.azurewebsites.net";

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
    public constructor(){

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
}
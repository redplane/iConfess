import {Injectable} from "@angular/core";

/*
* Service which handles hyperlink of api.
* */
@Injectable()
export class HyperlinkService{

    // Api which web application will consume the service.
    public apiUrl = "http://192.168.1.101";

    // Hyperlink which is used for searching for categories.
    public apiFindCategory : string;

    // Hyperlink which is used for searching for categories for deleting 'em.
    public apiDeleteCategory : string;

    // Hyperlink which is used for changing category information.
    public apiChangeCategoryDetail: string;

    // Hyperlink which is used for searching for accounts.
    public apiFindAccount : string;

    // Initiate service with settings.
    public constructor(){

        // Find category api url.
        this.apiFindCategory = `${this.apiUrl}/api/category/find`;
        this.apiDeleteCategory = `${this.apiUrl}/api/category`;
        this.apiChangeCategoryDetail = `${this.apiUrl}/api/category`;

        // Find category account api url.
        this.apiFindAccount = `${this.apiUrl}/api/account/find`;
    }
}
import {Injectable} from "@angular/core";

/*
* Service which handles hyperlink of api.
* */
@Injectable()
export class HyperlinkService{

    // Hyperlink which is used for searching for categories.
    public apiFindCategory : string = "http://confession.azurewebsites.net/api/category/find";

    // Hyperlink which is used for searching for accounts.
    public apiFindAccount : string = "http://confession.azurewebsites.net/api/account/find";
}
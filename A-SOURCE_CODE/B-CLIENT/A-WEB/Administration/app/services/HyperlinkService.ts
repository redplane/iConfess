import {Injectable} from "@angular/core";

/*
* Service which handles hyperlink of api.
* */
@Injectable()
export class HyperlinkService{

    // Hyperlink which is used for searching for categories.
    public apiFindCategory : string = "http://confession.azurewebsites.net/api/category/find";
    //public apiFindCategory : string = "http://localhost:2101/api/category/find";
}
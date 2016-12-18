import {ICategoryService} from "../interfaces/services/ICategoryService";
import {CategoryDetailViewModel} from "../viewmodels/category/CategoryDetailViewModel";
import {Account} from "../models/Account";
import {Injectable} from '@angular/core';
import {CategorySearchViewModel} from "../viewmodels/category/CategorySearchViewModel";
import {HyperlinkService} from "./HyperlinkService";
import {Http, Headers, RequestOptions, Response} from "@angular/http";
import 'rxjs/add/operator/toPromise';

/*
* Service which handles category business.
* */
@Injectable()
export class CategoryService implements ICategoryService {

    // Service which handles hyperlink.
    private _hyperlinkService: HyperlinkService;

    // HttpClient which is used for handling request to web api service.
    private _httpClient: Http;

    // Initiate instance of category service.
    public constructor(hyperlinkService: HyperlinkService, httpClient: Http){

        this._hyperlinkService = hyperlinkService;
        this._httpClient = httpClient;
    }

    // Find categories by using specific conditions.
    findCategories(categorySearch: CategorySearchViewModel) {

        let requestOptions = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        let requestBody = {
            pagination: {
                index: 0,
                records: 20
            }
        };

        // Request to api to obtain list of available categories in system.
        return this._httpClient.post(this._hyperlinkService.apiFindCategory, requestBody, requestOptions)
            .toPromise();

    }
}
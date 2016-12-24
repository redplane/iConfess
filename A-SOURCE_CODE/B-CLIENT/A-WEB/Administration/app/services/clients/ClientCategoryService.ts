import {IClientCategoryService} from "../../interfaces/services/ICategoryService";
import {CategoryDetailViewModel} from "../../viewmodels/category/CategoryDetailViewModel";
import {Account} from "../../models/Account";
import {Injectable} from '@angular/core';
import {CategorySearchViewModel} from "../../viewmodels/category/FindCategoriesViewModel";
import {HyperlinkService} from "../HyperlinkService";
import {Http, Headers, RequestOptions, Response} from "@angular/http";
import 'rxjs/add/operator/toPromise';
import {UnixDateRange} from "../../viewmodels/UnixDateRange";
import {Pagination} from "../../viewmodels/Pagination";

/*
* Service which handles category business.
* */
@Injectable()
export class ClientCategoryService implements IClientCategoryService {

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
    public findCategories(categorySearch: CategorySearchViewModel) {

        // Page index should be decrease by one.
        let conditions = Object.assign({}, categorySearch);
        conditions['pagination'] = Object.assign({}, categorySearch.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;

        let requestOptions = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        // Request to api to obtain list of available categories in system.
        return this._httpClient.post(this._hyperlinkService.apiFindCategory, conditions, requestOptions)
            .toPromise();

    }

    // Reset categories search conditions.
    public resetFindCategoriesConditions(): CategorySearchViewModel{

        // Initiate find categories conditions.
        let conditions = new CategorySearchViewModel();

        if (conditions == null)
            conditions = new CategorySearchViewModel();

        conditions.creatorIndex = null;
        conditions.name = null;
        conditions.created = new UnixDateRange();
        conditions.lastModified = new UnixDateRange();

        return conditions;
    }
}
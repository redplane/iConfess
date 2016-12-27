import {IClientCategoryService} from "../../interfaces/services/IClientCategoryService";
import {Injectable} from '@angular/core';
import {FindCategoriesViewModel} from "../../viewmodels/category/FindCategoriesViewModel";
import {ClientApiService} from "../ClientApiService";
import {Http, Headers, RequestOptions} from "@angular/http";
import 'rxjs/add/operator/toPromise';
import {UnixDateRange} from "../../viewmodels/UnixDateRange";
import {Category} from "../../models/Category";

/*
* Service which handles category business.
* */
@Injectable()
export class ClientCategoryService implements IClientCategoryService {

    // Service which handles hyperlink.
    private _hyperlinkService: ClientApiService;

    // HttpClient which is used for handling request to web api service.
    private _httpClient: Http;

    // Initiate instance of category service.
    public constructor(hyperlinkService: ClientApiService, httpClient: Http){

        this._hyperlinkService = hyperlinkService;
        this._httpClient = httpClient;
    }

    // Find categories by using specific conditions.
    public findCategories(categorySearch: FindCategoriesViewModel) {

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

    // Find categories by using specific conditions and delete 'em.
    public deleteCategories(findCategoriesConditions: FindCategoriesViewModel){
        let requestOptions = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: findCategoriesConditions
        });

        // Request to api to obtain list of available categories in system.
        return this._httpClient.delete(this._hyperlinkService.apiDeleteCategory, requestOptions)
            .toPromise();
    }

    // Change category detail information by searching its index.
    public changeCategoryDetails(id: number, category: Category){

        let requestOptions = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        // Construct change category api.
        let changeCategoryDetailApi = `${this._hyperlinkService.apiChangeCategoryDetail}?index=${id}`;

        // Request to api to obtain list of available categories in system.
        return this._httpClient.put(changeCategoryDetailApi, category, requestOptions)
            .toPromise();
    }

    // Initiate category into system.
    public initiateCategory(category: any) : any {

        let requestOptions = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        // Construct change category api.
        let changeCategoryDetailApi = `${this._hyperlinkService.apiInitiateCategory}`;

        // Request to api to obtain list of available categories in system.
        return this._httpClient.put(changeCategoryDetailApi, category, requestOptions)
            .toPromise();
    }
    // Reset categories search conditions.
    public resetFindCategoriesConditions(): FindCategoriesViewModel{

        // Initiate find categories conditions.
        let conditions = new FindCategoriesViewModel();

        if (conditions == null)
            conditions = new FindCategoriesViewModel();

        conditions.creatorIndex = null;
        conditions.name = null;
        conditions.created = new UnixDateRange();
        conditions.lastModified = new UnixDateRange();

        return conditions;
    }
}
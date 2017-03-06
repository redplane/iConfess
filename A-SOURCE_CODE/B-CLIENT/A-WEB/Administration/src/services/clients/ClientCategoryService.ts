import {IClientCategoryService} from "../../interfaces/services/IClientCategoryService";
import {Injectable} from '@angular/core';
import {SearchCategoriesViewModel} from "../../viewmodels/category/SearchCategoriesViewModel";
import {ClientApiService} from "../ClientApiService";
import 'rxjs/add/operator/toPromise';
import {UnixDateRange} from "../../viewmodels/UnixDateRange";
import {Category} from "../../models/Category";
import {ClientAuthenticationService} from "./ClientAuthenticationService";
import {toPromise} from "rxjs/operator/toPromise";

/*
* Service which handles category business.
* */
@Injectable()
export class ClientCategoryService implements IClientCategoryService {

    // Service which handles hyperlink.
    private _clientApiService: ClientApiService;

    // Authentication service.
    private _clientAuthenticationService : ClientAuthenticationService;

    // Initiate instance of category service.
    public constructor(
        clientApiService: ClientApiService,
        clientAuthenticationService: ClientAuthenticationService){

        this._clientApiService = clientApiService;
        this._clientAuthenticationService = clientAuthenticationService;
    }

    // Find categories by using specific conditions.
    public findCategories(categorySearch: SearchCategoriesViewModel) {

        // Page index should be decrease by one.
        let conditions = Object.assign({}, categorySearch);
        conditions['pagination'] = Object.assign({}, categorySearch.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;

        return this._clientApiService.post(
            this._clientAuthenticationService.findClientAuthenticationToken(),
            this._clientApiService.apiFindCategory,
            null,
            conditions)
            .toPromise();
    }

    // Find categories by using specific conditions and delete 'em.
    public deleteCategories(findCategoriesConditions: SearchCategoriesViewModel){
        // Request to api to obtain list of available categories in system.
        return this._clientApiService.delete(
            this._clientAuthenticationService.findClientAuthenticationToken(),
            this._clientApiService.apiDeleteCategory,
            null,
            findCategoriesConditions)
            .toPromise();
    }

    // Change category detail information by searching its index.
    public changeCategoryDetails(id: number, category: Category){
        // Request to api to obtain list of available categories in system.
        return this._clientApiService.put(this._clientAuthenticationService.findClientAuthenticationToken(),
            this._clientApiService.apiChangeCategoryDetail, {index: id}, category)
            .toPromise();
    }

    // Initiate category into system.
    public initiateCategory(category: any) : any {
        // Request to api to obtain list of available categories in system.
        return this._clientApiService.post(this._clientAuthenticationService.findClientAuthenticationToken(),
            this._clientApiService.apiInitiateCategory, null, category)
            .toPromise();
    }
    // Reset categories search conditions.
    public resetFindCategoriesConditions(): SearchCategoriesViewModel{

        // Initiate find categories conditions.
        let conditions = new SearchCategoriesViewModel();

        if (conditions == null)
            conditions = new SearchCategoriesViewModel();

        conditions.creatorIndex = null;
        conditions.name = null;
        conditions.created = new UnixDateRange();
        conditions.lastModified = new UnixDateRange();

        return conditions;
    }
}
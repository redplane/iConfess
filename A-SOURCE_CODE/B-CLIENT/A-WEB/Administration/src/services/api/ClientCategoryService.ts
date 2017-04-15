import {IClientCategoryService} from "../../interfaces/services/api/IClientCategoryService";
import {Injectable, Inject} from '@angular/core';
import {SearchCategoriesViewModel} from "../../viewmodels/category/SearchCategoriesViewModel";
import {ClientApiService} from "../ClientApiService";
import {UnixDateRange} from "../../viewmodels/UnixDateRange";
import {Category} from "../../models/Category";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";

/*
* Service which handles category business.
* */
@Injectable()
export class ClientCategoryService implements IClientCategoryService {

    //#region Constructor

    // Initiate instance of category service.
    public constructor(
        @Inject("IClientAuthenticationService")public clientAuthenticationService: IClientAuthenticationService,
        public clientApiService: ClientApiService){
    }

    //#endregion

    //Region Methods

    // Find categories by using specific conditions.
    public getCategories(categorySearch: SearchCategoriesViewModel) {

        // Page index should be decrease by one.
        let conditions = Object.assign({}, categorySearch);
        conditions['pagination'] = Object.assign({}, categorySearch.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;

        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiFindCategory,
            null,
            conditions)
            .toPromise();
    }

    // Find categories by using specific conditions and delete 'em.
    public deleteCategories(findCategoriesConditions: SearchCategoriesViewModel){
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.delete(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiDeleteCategory,
            null,
            findCategoriesConditions)
            .toPromise();
    }

    // Change category detail information by searching its index.
    public editCategoryDetails(id: number, category: Category){
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.put(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiChangeCategoryDetail, {index: id}, category)
            .toPromise();
    }

    // Initiate category into system.
    public initiateCategory(category: any) : any {
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiInitiateCategory, null, category)
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

    //#endregion
}
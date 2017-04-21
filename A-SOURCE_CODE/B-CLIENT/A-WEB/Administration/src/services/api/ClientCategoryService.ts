import {IClientCategoryService} from "../../interfaces/services/api/IClientCategoryService";
import {Injectable, Inject} from '@angular/core';
import {SearchCategoriesViewModel} from "../../viewmodels/category/SearchCategoriesViewModel";
import {UnixDateRange} from "../../viewmodels/UnixDateRange";
import {Category} from "../../models/entities/Category";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";

/*
 * Service which handles category business.
 * */
@Injectable()
export class ClientCategoryService implements IClientCategoryService {

    //#region Properties

    // Url which is for searching for categories.
    public urlSearchCategory: string = "api/category/find";

    // Url which is for deleting for categories.
    public urlDeleteCategory: string = "api/category";

    // Url which is for changing for category detail.
    public urlChangeCategoryDetail: string = "api/category";

    // Url which is for initiating category.
    public urlInitiateCategory: string = "api/category";

    //#endregion

    //#region Constructor

    // Initiate instance of category service.
    public constructor(@Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService,
                       @Inject("IClientApiService") public clientApiService: IClientApiService) {
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
            this.clientAuthenticationService.getTokenCode(),
            `${this.clientApiService.getBaseUrl()}/${this.urlSearchCategory}`,
            null,
            conditions);
    }

    // Find categories by using specific conditions and delete 'em.
    public deleteCategories(findCategoriesConditions: SearchCategoriesViewModel) {
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.delete(
            this.clientAuthenticationService.getTokenCode(),
            `${this.clientApiService.getBaseUrl()}/${this.urlDeleteCategory}`,
            null,
            findCategoriesConditions);
    }

    // Change category detail information by searching its index.
    public editCategoryDetails(id: number, category: Category) {
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.put(
            this.clientAuthenticationService.getTokenCode(),
            `${this.clientApiService.getBaseUrl()}/${this.urlChangeCategoryDetail}`,
            {index: id}, category);
    }

    // Initiate category into system.
    public initiateCategory(category: any): any {
        // Initiate url.
        let url = `${this.clientApiService.getBaseUrl()}/${this.urlInitiateCategory}`;

        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(
            this.clientAuthenticationService.getTokenCode(),
            url, null, category);
    }

    // Reset categories search conditions.
    public resetFindCategoriesConditions(): SearchCategoriesViewModel {

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
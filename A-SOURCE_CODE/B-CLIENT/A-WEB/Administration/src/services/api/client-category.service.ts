import {IClientCategoryService} from "../../interfaces/services/api/IClientCategoryService";
import {Injectable, Inject} from '@angular/core';
import {SearchCategoriesViewModel} from "../../viewmodels/category/search-categories.view-model";
import {Category} from "../../models/entities/category";
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
    public getCategories(conditions: SearchCategoriesViewModel) {

        // Page page should be decrease by one.
        return this.clientApiService.post(
            this.clientAuthenticationService.getTokenCode(),
            `${this.clientApiService.getBaseUrl()}/${this.urlSearchCategory}`,
            null,
            conditions);
    }

    // Find categories by using specific conditions and delete 'em.
    public deleteCategories(conditions: SearchCategoriesViewModel) {
        // Request to api to obtain list of available categories in system.
        return this.clientApiService.delete(
            this.clientAuthenticationService.getTokenCode(),
            `${this.clientApiService.getBaseUrl()}/${this.urlDeleteCategory}`,
            null,
            conditions);
    }

    // Change category detail information by searching its page.
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

    //#endregion
}

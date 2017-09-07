import {Injectable, Inject} from '@angular/core';
import {SearchCategoriesViewModel} from "../../viewmodels/category/search-categories.view-model";
import {Category} from "../../models/entities/category";
import {ICategoryService} from "../../interfaces/services/api/category-service.interface";
import {IApiService} from "../../interfaces/services/api/api-service.interface";
import {environment} from "../../environments/environment";
import {ApiUrl} from "../../constants/api-url";

/*
 * Service which handles category business.
 * */
@Injectable()
export class CategoryService implements ICategoryService {

  //#region Constructor

  /*
  * Initiate instance of category service.
  * */
  public constructor(@Inject("IApiService") public clientApiService: IApiService) {
  }

  //#endregion

  //Region Methods

  /*
  * Find categories by using specific conditions.
  * */
  public getCategories(conditions: SearchCategoriesViewModel) {

    // Page page should be decrease by one.
    return this.clientApiService.post(environment.baseUrl, ApiUrl.getCategories,
      null,
      conditions);
  }

  /*
  * Find categories by using specific conditions and delete 'em.
  * */
  public deleteCategories(conditions: SearchCategoriesViewModel) {
    // Request to api to obtain list of available categories in system.
    return this.clientApiService.delete(
      environment.baseUrl,
      ApiUrl.deleteCategory,
      null,
      conditions);
  }

  /*
  * Change category detail information by searching its page.
  * */
  public editCategoryDetails(id: number, category: Category) {
    // Request to api to obtain list of available categories in system.
    return this.clientApiService.put(environment.baseUrl, ApiUrl.editCategoryDetails,
      {index: id}, category);
  }

  /*
  * Initiate category into system.
  * */
  public initiateCategory(category: any): any {
    // Request to api to obtain list of available categories in system.
    return this.clientApiService.post(environment.baseUrl,
      ApiUrl.initiateCategory, null, category);
  }

  //#endregion
}

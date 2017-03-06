import {SearchCategoriesViewModel} from "../../viewmodels/category/SearchCategoriesViewModel";
import 'rxjs/add/operator/toPromise';
import {Response} from "@angular/http";
import {Category} from "../../models/Category";

export interface IClientCategoryService{

    // Find categories by using specific conditions.
    findCategories(categorySearch: SearchCategoriesViewModel): any;

    // Find categories by using specific conditions and delete 'em all.
    deleteCategories(findCategoriesConditions: SearchCategoriesViewModel): any;

    // Find categories by using index and update their information.
    changeCategoryDetails(id: number, category: Category): any;

    // Initiate category into system.
    initiateCategory(category:any) : any;

    // Reset categories search conditions.
    resetFindCategoriesConditions(): SearchCategoriesViewModel;
}
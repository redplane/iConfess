import {FindCategoriesViewModel} from "../../viewmodels/category/FindCategoriesViewModel";
import 'rxjs/add/operator/toPromise';
import {Response} from "@angular/http";
import {Category} from "../../models/Category";

export interface IClientCategoryService{

    // Find categories by using specific conditions.
    findCategories(categorySearch: FindCategoriesViewModel): any;

    // Find categories by using specific conditions and delete 'em all.
    deleteCategories(findCategoriesConditions: FindCategoriesViewModel);

    // Find categories by using index and update their information.
    changeCategoryDetails(id: number, category: Category);

    // Initiate category into system.
    initiateCategory(category:any) : any;

    // Reset categories search conditions.
    resetFindCategoriesConditions(): FindCategoriesViewModel;
}
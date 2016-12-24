import {CategorySearchViewModel} from "../../viewmodels/category/FindCategoriesViewModel";
import 'rxjs/add/operator/toPromise';
import {Response} from "@angular/http";

export interface IClientCategoryService{

    // Find categories by using specific conditions.
    findCategories(categorySearch: CategorySearchViewModel): any;

    // Reset categories search conditions.
    resetFindCategoriesConditions(): CategorySearchViewModel;
}
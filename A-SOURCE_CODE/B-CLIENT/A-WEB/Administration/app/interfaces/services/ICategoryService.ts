import {CategorySearchViewModel} from "../../viewmodels/category/CategorySearchViewModel";
import 'rxjs/add/operator/toPromise';
import {Response} from "@angular/http";

export interface ICategoryService{

    // Find categories by using specific conditions.
    findCategories(categorySearch: CategorySearchViewModel);
}
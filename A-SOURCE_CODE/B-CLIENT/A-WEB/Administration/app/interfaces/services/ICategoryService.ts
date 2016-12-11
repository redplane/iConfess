import {CategorySearchDetailViewModel} from "../../viewmodels/category/CategorySearchDetailViewModel";
import {CategorySearchViewModel} from "../../viewmodels/category/CategorySearchViewModel";

export interface ICategoryService{

    // Find categories by using specific conditions.
    findCategories(categorySearch: CategorySearchViewModel): CategorySearchDetailViewModel;
}
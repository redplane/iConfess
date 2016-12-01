import {CategorySearchDetailViewModel} from "../../viewmodels/category/CategorySearchDetailViewModel";
export interface ICategoryService{

    // Find categories by using specific conditions.
    findCategories(): CategorySearchDetailViewModel;
}
import {CategoryDetailsViewModel} from "./CategoryDetailsViewModel";

/*
* Result instance of category search.
* */
export class SearchCategoriesResultViewModel{

    // List of detailed categories responded from service.
    public categories: CategoryDetailsViewModel[];

    // Total records which match with search conditions.
    public total: number;
}
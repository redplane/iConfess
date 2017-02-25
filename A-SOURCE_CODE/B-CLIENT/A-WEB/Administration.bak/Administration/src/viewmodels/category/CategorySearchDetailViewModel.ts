import {CategoryDetailViewModel} from "./CategoryDetailViewModel";

/*
* Result instance of category search.
* */
export class CategorySearchDetailViewModel{

    // List of detailed categories responded from service.
    public categories: CategoryDetailViewModel[];

    // Total records which match with search conditions.
    public total: number;
}
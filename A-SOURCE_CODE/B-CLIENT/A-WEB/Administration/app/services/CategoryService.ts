import {ICategoryService} from "../interfaces/services/ICategoryService";
import {CategorySearchDetailViewModel} from "../viewmodels/category/CategorySearchDetailViewModel";
import {CategoryDetailViewModel} from "../viewmodels/category/CategoryDetailViewModel";
import {Account} from "../models/Account";
import {AccountStatuses} from "../enumerations/AccountStatuses";
import {Injectable} from '@angular/core';
import {CategorySearchViewModel} from "../viewmodels/category/CategorySearchViewModel";

/*
* Service which handles category business.
* */
@Injectable()
export class CategoryService implements ICategoryService {

    // Who created list of categories.
    private creator: Account;

    // List of categories responded from service..
    private categories: Array<CategoryDetailViewModel>;

    // Initiate instance of category service.
    public constructor(){

        // Initiate account information.
        this.creator = new Account();
        this.creator.id = 1;
        this.creator.email = "linhndse03150@fpt.edu.vn";
        this.creator.nickname = "Linh Nguyen";
        this.creator.status = AccountStatuses.Active;
        this.creator.joined = 0;
        this.creator.lastModified = 0;

        // Initiate list of categories.
        this.categories = new Array<CategoryDetailViewModel>();

        for (let i = 0; i < 10; i++){
            var category = new CategoryDetailViewModel();
            category.id = i;
            category.creator = this.creator;
            category.name = `category[${i}]`;
            category.created = i;
            category.lastModified = i;

            this.categories.push(category);
        }
    }
    // Find categories by using specific conditions.
    findCategories(categorySearch: CategorySearchViewModel): CategorySearchDetailViewModel {

        // Initiate category search result.
        let categoriesSearchResult = new CategorySearchDetailViewModel();
        categoriesSearchResult.categories = this.categories;
        categoriesSearchResult.total = this.categories.length;

        return categoriesSearchResult;
    }

}
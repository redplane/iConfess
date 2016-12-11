import {ICategoryService} from "../interfaces/services/ICategoryService";
import {CategorySearchDetailViewModel} from "../viewmodels/category/CategorySearchDetailViewModel";
import {CategoryDetailViewModel} from "../viewmodels/category/CategoryDetailViewModel";
import {Account} from "../models/Account";
import {AccountStatuses} from "../enumerations/AccountStatuses";
import {Injectable} from '@angular/core';
import {CategorySearchViewModel} from "../viewmodels/category/CategorySearchViewModel";
import {HyperlinkService} from "./HyperlinkService";
import {Http, Headers, RequestOptions, Response} from "@angular/http";
import 'rxjs/add/operator/toPromise';

/*
* Service which handles category business.
* */
@Injectable()
export class CategoryService implements ICategoryService {

    // Who created list of categories.
    private creator: Account;

    // List of categories responded from service..
    private categories: Array<CategoryDetailViewModel>;

    // Service which handles hyperlink.
    private _hyperlinkService: HyperlinkService;

    // HttpClient which is used for handling request to web api service.
    private _httpClient: Http;

    // Initiate instance of category service.
    public constructor(hyperlinkService: HyperlinkService, httpClient: Http){

        this._hyperlinkService = hyperlinkService;
        this._httpClient = httpClient;

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

        let requestOptions = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        let requestBody = {};

        this._httpClient.post(this._hyperlinkService.apiFindCategory, requestBody, requestOptions)
            .toPromise()
            .then(this.processFindCategoriesResult)
            .catch(this.handleError);

        console.log(this._hyperlinkService.apiFindCategory);
        return categoriesSearchResult;
    }

    // This callback is called when data is sent back from server which find categories request was sent.
    private processFindCategoriesResult(response: Response){
        console.log(response);
    }

    // This callback is called when data is sent back from service due to its invalidity.
    private handleError(response: Response | any){
        console.log(response);
    }

}
import {Component} from '@angular/core'
import {CategorySearchDetailViewModel} from "../viewmodels/category/CategorySearchDetailViewModel";
import {CategoryService} from "../services/CategoryService";
import {ICategoryService} from "../interfaces/services/ICategoryService";
import {TimeService} from "../services/TimeService";
import {ITimeService} from "../interfaces/services/ITimeService";
import {CategoryDetailViewModel} from "../viewmodels/category/CategoryDetailViewModel";
import {CategoryEditBoxComponent} from "./content/category/category-edit-box.component";
import {CategoryDeleteBoxComponent} from "./content/category/category-delete-box.component";
import {CategorySearchViewModel} from "../viewmodels/category/CategorySearchViewModel";
import {HyperlinkService} from "../services/HyperlinkService";
import {Response} from "@angular/http";
import {ResponseAnalyzeService} from "../services/ResponseAnalyzeService";
import {ConfigurationService} from "../services/ConfigurationService";

declare var $:any;

@Component({
    selector: 'category-management',
    templateUrl: './app/html/pages/category-management.component.html',
    providers:[
        CategoryService,
        TimeService,
        HyperlinkService,
        ResponseAnalyzeService,
        ConfigurationService
    ],
})

export class CategoryManagementComponent{

    // List of categories responded from service.
    private _categorySearchResult: CategorySearchDetailViewModel;

    // Service which handles category business.
    private _categoryService: ICategoryService;

    // Service which handles time conversion.
    private _timeService: ITimeService;

    // Service which provides function to analyze response sent back from server.
    private _responseAnalyzeService: ResponseAnalyzeService;

    // Service which provides functions to access application configuration.
    private _configurationService: ConfigurationService;

    // Whether records are being searched or not.
    private _isLoading : boolean;

    // Conditions which are used for searching categories.
    private _categorySearchConditions: CategorySearchViewModel;

    // Initiate component with dependency injections.
    public constructor(categoryService: CategoryService, timeService: TimeService,
                       responseAnalyzeService : ResponseAnalyzeService, configurationService: ConfigurationService) {
        this._categoryService = categoryService;
        this._timeService = timeService;
        this._responseAnalyzeService = responseAnalyzeService;

        // Find configuration service in IoC.
        this._configurationService = configurationService;

        // Initiate categories search result.
        this._categorySearchResult = new CategorySearchDetailViewModel();

        // Initiate category search conditions.
        this._categorySearchConditions = new CategorySearchViewModel();
    }

    // Callback is fired when a category is created to be removed.
    public clickRemoveCategory(category:CategoryDetailViewModel, deleteCategoryBox: CategoryDeleteBoxComponent):void{
        // Update category information into box.
        deleteCategoryBox.setCategory(category);

        // Open delete category confirmation box.
        deleteCategoryBox.open();
    }

    // Callback which is fired when change category box is clicked.
    public clickChangeCategoryInfo(category:CategoryDetailViewModel, changeCategoryBox: CategoryEditBoxComponent): void{
        // Update category information into box.
        changeCategoryBox.setCategory(category);

        // Open change category information box.
        changeCategoryBox.open();
    }

    // Callback which is fired when search button of category search box is clicked.
    public clickSearch(categoriesSearchConditions: CategorySearchViewModel): void{

        // Update search conditions.
        this._categorySearchConditions = categoriesSearchConditions;

        // Freeze the find box.
        this._isLoading = true;

        // Find categories by using specific conditions.
        this._categoryService.findCategories(categoriesSearchConditions)
            .then((response: Response)=> {

                // Update categories list.
                this._categorySearchResult = response.json();

                // Unfreeze the category find box.
                this._isLoading = false;

                console.log(response);
            })
            .catch((response: Response | any) => {
               this._isLoading = false;
            });


    }
}
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

declare var $:any;

@Component({
    selector: 'category-management',
    templateUrl: './app/html/pages/category-management.component.html',
    providers:[
        CategoryService,
        TimeService,
        HyperlinkService
    ],
})

export class CategoryManagementComponent{

    // List of categories responded from service.
    private _categorySearchResult: CategorySearchDetailViewModel;

    // Service which handles category business.
    private _categoryService: ICategoryService;

    // Service which handles time conversion.
    private _timeService: ITimeService;

    // Whether records are being searched or not.
    private _isLoading : boolean;

    // Initiate component with dependency injections.
    public constructor(categoryService: CategoryService, timeService: TimeService) {
        this._categoryService = categoryService;
        this._timeService = timeService;
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
    public clickSearch(categorySearch: CategorySearchViewModel): void{
        // Find categories by using specific conditions.
        this._categorySearchResult =  this._categoryService.findCategories(categorySearch);
    }
}
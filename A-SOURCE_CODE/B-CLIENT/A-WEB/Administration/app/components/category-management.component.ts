import {Component, EventEmitter} from '@angular/core'
import {CategorySearchDetailViewModel} from "../viewmodels/category/CategorySearchDetailViewModel";
import {CategoryService} from "../services/CategoryService";
import {ICategoryService} from "../interfaces/services/ICategoryService";
import {TimeService} from "../services/TimeService";
import {ITimeService} from "../interfaces/services/ITimeService";
import {CategoryDetailViewModel} from "../viewmodels/category/CategoryDetailViewModel";

@Component({
    selector: 'category-management',
    templateUrl: './app/html/pages/category-management.component.html',
    providers:[
        CategoryService,
        TimeService
    ]
})

export class CategoryManagementComponent{

    // List of categories responded from service.
    private _categorySearchResult: CategorySearchDetailViewModel;

    // Service which handles category business.
    private _categoryService: ICategoryService;

    // Service which handles time conversion.
    private _timeService: ITimeService;

    // Event emitter which is fired when category is clicked to be removed.
    public clickCategoryRemove: EventEmitter<any>;

    public constructor(categoryService: CategoryService, timeService: TimeService){
        this._categoryService = categoryService;
        this._timeService = timeService;

        this._categorySearchResult = this._categoryService.findCategories();
        this.clickCategoryRemove = new EventEmitter();
    }

    public clickRemoveCategory(category: CategoryDetailViewModel): void{
        alert(category.name);
    }
}
import {Component, EventEmitter} from '@angular/core';
import {TimeService} from "../../../services/ClientTimeService";
import {ITimeService} from "../../../interfaces/services/ITimeService";
import {CategoryDetailViewModel} from "../../../viewmodels/category/CategoryDetailViewModel";

@Component({
    selector: 'category-detail',
    inputs: ['category'],
    outputs: ['clickRemoveCategory', 'clickChangeCategoryInfo'],
    templateUrl: './app/views/contents/category/category-detail-box.component.html',
    providers:[
        TimeService
    ]
})

export class CategoryDetailBoxComponent{

    // Service which handles time functions.
    private _timeService: ITimeService;

    // Event emitter which is fired when a category is clicked to be removed.
    private clickRemoveCategory: EventEmitter<any>;

    // Event emitter which is fired when a category is clicked to be changed.
    private clickChangeCategoryInfo: EventEmitter<any>;

    // Initiate category detail box with dependency injections.
    public constructor(timeService: TimeService){
        this._timeService = timeService;
        this.clickRemoveCategory = new EventEmitter();
        this.clickChangeCategoryInfo = new EventEmitter();
    }

    // Fired when a category is clicked to be removed.
    public deleteCategory(category: CategoryDetailViewModel): void{
        this.clickRemoveCategory.emit(category);
    }

    // Fired when a category is clicked to be changed.
    public changeCategoryInfo(category: CategoryDetailViewModel): void{
        this.clickChangeCategoryInfo.emit(category);
    }
}
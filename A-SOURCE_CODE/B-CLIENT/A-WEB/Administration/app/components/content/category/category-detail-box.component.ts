import {Component, EventEmitter} from '@angular/core';
import {TimeService} from "../../../services/TimeService";
import {ITimeService} from "../../../interfaces/services/ITimeService";
import {CategoryDetailViewModel} from "../../../viewmodels/category/CategoryDetailViewModel";

@Component({
    selector: 'category-detail',
    inputs: ['category'],
    outputs: ['clickRemoveCategory'],
    templateUrl: './app/html/content/category/category-detail-box.component.html',
    providers:[
        TimeService
    ]
})

export class CategoryDetailBoxComponent{

    // Service which handles time functions.
    private _timeService: ITimeService;

    // Event emitter which is fired when a category is clicked to be removed.
    private clickRemoveCategory: EventEmitter<any>;

    // Initiate category detail box with dependency injections.
    public constructor(timeService: TimeService){
        this._timeService = timeService;
        this.clickRemoveCategory = new EventEmitter();
    }

    // Fired when a category is clicked to be removed.
    public deleteCategory(category: CategoryDetailViewModel): void{
        this.clickRemoveCategory.emit(category);
    }
}
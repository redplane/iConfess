import {Component, EventEmitter} from '@angular/core';
import {ClientTimeService} from "../../services/ClientTimeService";
import {IClientTimeService} from "../../interfaces/services/ITimeService";
import {CategoryDetailsViewModel} from "../../viewmodels/category/CategoryDetailsViewModel";

@Component({
    selector: 'category-detail',
    inputs: ['category'],
    outputs: ['clickRemoveCategory', 'clickChangeCategoryInfo'],
    templateUrl: 'category-detail-box.component.html',
    providers:[
        ClientTimeService
    ]
})

export class CategoryDetailBoxComponent{

    // Service which handles time functions.
    private _timeService: IClientTimeService;

    // Event emitter which is fired when a category is clicked to be removed.
    private clickRemoveCategory: EventEmitter<any>;

    // Event emitter which is fired when a category is clicked to be changed.
    private clickChangeCategoryInfo: EventEmitter<any>;

    // Initiate category detail box with dependency injections.
    public constructor(timeService: ClientTimeService){
        this._timeService = timeService;
        this.clickRemoveCategory = new EventEmitter();
        this.clickChangeCategoryInfo = new EventEmitter();
    }

    // Fired when a category is clicked to be removed.
    public deleteCategory(category: CategoryDetailsViewModel): void{
        this.clickRemoveCategory.emit(category);
    }

    // Fired when a category is clicked to be changed.
    public changeCategoryInfo(category: CategoryDetailsViewModel): void{
        this.clickChangeCategoryInfo.emit(category);
    }
}
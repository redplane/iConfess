import {Component, EventEmitter, Inject} from '@angular/core';
import {IClientTimeService} from "../../interfaces/services/IClientTimeService";
import {CategoryDetailsViewModel} from "../../viewmodels/category/CategoryDetailsViewModel";

@Component({
    selector: 'category-detail',
    inputs: ['category'],
    outputs: ['clickRemoveCategory', 'clickChangeCategoryInfo'],
    templateUrl: 'category-detail-box.component.html'
})

export class CategoryDetailBoxComponent{

    //#region Properties

    // Event emitter which is fired when a category is clicked to be removed.
    private clickRemoveCategory: EventEmitter<any>;

    // Event emitter which is fired when a category is clicked to be changed.
    private clickChangeCategoryInfo: EventEmitter<any>;

    //#endregion

    //#region Constructor

    // Initiate category detail box with dependency injections.
    public constructor(@Inject("IClientTimeService") public clientTimeService: IClientTimeService){
        this.clickRemoveCategory = new EventEmitter();
        this.clickChangeCategoryInfo = new EventEmitter();
    }

    //#endregion

    //#region Methods

    // Fired when a category is clicked to be removed.
    public deleteCategory(category: CategoryDetailsViewModel): void{
        this.clickRemoveCategory.emit(category);
    }

    // Fired when a category is clicked to be changed.
    public changeCategoryInfo(category: CategoryDetailsViewModel): void{
        this.clickChangeCategoryInfo.emit(category);
    }

    //#endregion
}
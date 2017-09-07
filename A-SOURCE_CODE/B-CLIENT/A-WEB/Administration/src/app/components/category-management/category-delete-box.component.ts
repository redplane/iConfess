import {Component} from '@angular/core';
import * as _ from "lodash";
import {CategoryDetailsViewModel} from "../../../viewmodels/category/category-details.view-model";

@Component({
    selector: 'category-delete-box',
    templateUrl: 'category-delete-box.component.html',
    exportAs: 'category-delete-box'
})

export class CategoryDeleteBoxComponent{

    //#region Properties

    // Category information.
    private details: CategoryDetailsViewModel;

    //#endregion

    //#region Constructor

    // Initiate component with injectors.
    public constructor(){
        this.details = new CategoryDetailsViewModel();
    }
    //#endregion

    //#region Methods

    // Set details and attach to delete box.
    public setDetails(details: CategoryDetailsViewModel): void{
        this.details = _.cloneDeep(details);
    }

    // Get category details.
    public getDetails(): CategoryDetailsViewModel{
        return this.details;
    }

    //#endregion
}

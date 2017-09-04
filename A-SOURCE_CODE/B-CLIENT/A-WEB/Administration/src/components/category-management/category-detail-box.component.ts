import {Component, Inject} from '@angular/core';
import {IClientTimeService} from "../../interfaces/services/IClientTimeService";
import {CategoryDetailsViewModel} from "../../viewmodels/category/CategoryDetailsViewModel";
import * as _ from "lodash";


@Component({
    selector: 'category-detail-box',
    templateUrl: 'category-detail-box.component.html',
    exportAs: 'category-detail-box'
})

export class CategoryDetailBoxComponent{

    //#region Properties

    // Category details which should be displayed on box.
    private details: CategoryDetailsViewModel;

    //#endregion

    //#region Constructor

    // Initiate category detail box with dependency injections.
    public constructor(@Inject("IClientTimeService") public clientTimeService: IClientTimeService){
        this.details = new CategoryDetailsViewModel();
    }

    //#endregion

    //#region Methods

    // Set detail information and attach to component.
    public setDetails(details: CategoryDetailsViewModel): void{
        this.details = _.cloneDeep(details);
    }

    // Get detail information attached to component.
    public getDetails(): CategoryDetailsViewModel{
        return this.details;
    }

    //#endregion
}
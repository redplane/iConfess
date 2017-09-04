import {Component, ViewChild} from '@angular/core';
import {InitiateCategoryViewModel} from "../../viewmodels/category/InitiateCategoryViewModel";
import {NgForm} from "@angular/forms";

@Component({
    selector: 'category-initiate-box',
    templateUrl: 'category-initiate-box.component.html',
    exportAs: 'category-initiate-box'
})

export class CategoryInitiateBoxComponent {

    // Category initiator.
    private initiator: InitiateCategoryViewModel;

    // Form which contains input fields.
    @ViewChild('categoryInitiatorComponent')
    private categoryInitiatorComponent: NgForm;

    //#region Constructor

    // Initiate component with default dependency injection.
    public constructor() {
        // Initiate initiator.
        this.initiator = new InitiateCategoryViewModel();
    }

    //#endregion

    //#region Methods

    // Get initiator information.
    public getInitiator(): InitiateCategoryViewModel{
        return this.initiator;
    }

    public getForm(): NgForm{
        return this.categoryInitiatorComponent;
    }
    //#endregion
}
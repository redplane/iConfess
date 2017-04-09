import {Component, EventEmitter} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {ClientAccountService} from "../../services/api/ClientAccountService";
import {ClientDataConstraintService} from "../../services/ClientDataConstraintService";

@Component({
    selector: 'category-initiate-box',
    templateUrl: 'category-initiate-box.component.html',
    inputs:['isLoading'],
    outputs:['initiateCategory'],
    providers: [
        FormBuilder,

        ClientDataConstraintService,
        ClientConfigurationService,
        ClientAccountService
    ]
})

export class CategoryInitiateBoxComponent {

    // Whether records are being loaded from server or not.
    public isLoading: boolean;

    // Event which is emitted when search button is clicked.
    private initiateCategory: EventEmitter<any>;

    // Category find box which contains category.
    private initiateCategoryBox: FormGroup;

    // Initiate component with default dependency injection.
    public constructor(private formBuilder: FormBuilder,
                       public clientDataConstraintService: ClientDataConstraintService) {

        // Form control of find category box.
        this.initiateCategoryBox = this.formBuilder.group({
            name: [null]
        });

        // Initiate event emitters.
        this.initiateCategory = new EventEmitter()

        this.isLoading = false;
    }

    // Callback which is fired when confirm button is clicked.
    public clickInitiateCategory(category: any): void {
        this.initiateCategory.emit(category);
    }
}
import {Component, EventEmitter} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {ConfigurationService} from "../../../services/ClientConfigurationService";
import {ClientAccountService} from "../../../services/clients/ClientAccountService";

@Component({
    selector: 'category-initiate-box',
    templateUrl: './app/views/contents/category/category-initiate-box.component.html',
    inputs:['isLoading'],
    outputs:['initiateCategory'],
    providers: [
        FormBuilder,
        ConfigurationService,
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

    // Service which provides function to access application configuration.
    private _clientConfigurationService: ConfigurationService;

    // Initiate component with default dependency injection.
    public constructor(private formBuilder: FormBuilder, clientConfigurationService: ConfigurationService) {

        // Find configuration service from IoC.
        this._clientConfigurationService = clientConfigurationService;

        // Form control of find category box.
        this.initiateCategoryBox = this.formBuilder.group({
            name: [null]
        });

        // Initiate event emitters.
        this.initiateCategory = new EventEmitter()

        this.isLoading = false;
    }

    // Callback which is fired when search button is clicked.
    public clickInitiateCategory(): void {
        this.initiateCategory.emit(this.initiateCategoryBox.value);
    }
}
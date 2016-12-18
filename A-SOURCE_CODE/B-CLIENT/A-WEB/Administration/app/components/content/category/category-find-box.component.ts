import {Component, EventEmitter} from '@angular/core';
import {CategorySearchViewModel} from "../../../viewmodels/category/CategorySearchViewModel";
import {FormBuilder, FormGroup, Validators, FormControl} from '@angular/forms';
import {ConfigurationService} from "../../../services/ConfigurationService";
import {Pagination} from "../../../viewmodels/Pagination";

@Component({
    selector: 'category-find-box',
    templateUrl: './app/html/content/category/category-find-box.component.html',
    inputs:['categorySearch', 'isLoading'],
    outputs:['search'],
    providers:[
        CategorySearchViewModel,
        FormBuilder,
        ConfigurationService
    ]
})

export class CategoryFindBoxComponent{

    // Whether records are being loaded from server or not.
    public isLoading: boolean;

    // Event which is emitted when search button is clicked.
    private search: EventEmitter<any>;

    // Category find box which contains category.
    private findCategoryBox: FormGroup;

    // Service which provides function to access application configuration.
    private _configurationService: ConfigurationService;

    // Collection of conditions which are used for searching categories.
    private _categoriesSearchConditions: CategorySearchViewModel;

    // Initiate component with default dependency injection.
    public constructor(private formBuilder:FormBuilder, configurationService: ConfigurationService){

        // Find configuration service from IoC.
        this._configurationService = configurationService;

        // Form control of find category box.
        this.findCategoryBox = this.formBuilder.group({
            name: [null],
            creatorIndex: [null],
            created: this.formBuilder.group({
                from: [null, Validators.nullValidator],
                to: [null, Validators.nullValidator]
            }),
            lastModified: this.formBuilder.group({
                from: [null, Validators.nullValidator],
                to: [null, Validators.nullValidator]
            }),
            pagination: this.formBuilder.group({
                records: [null, Validators.nullValidator]
            }),
            sort: [null],
            direction: [null]
        });

        // Initiate conditions.
        this._categoriesSearchConditions = new CategorySearchViewModel();
        this._categoriesSearchConditions.pagination = new Pagination();
        this._categoriesSearchConditions.pagination.records = this._configurationService.pageRecords[0];
        this._categoriesSearchConditions.direction = this._configurationService.sortDirections.values()[0];

        // Initiate event emitters.
        this.search = new EventEmitter();
    }

    // Callback which is fired when search button is clicked.
    public clickSearch(event: any): void{
        console.log(event);
        //this.search.emit(this.findCategoryBox.value);
    }

}
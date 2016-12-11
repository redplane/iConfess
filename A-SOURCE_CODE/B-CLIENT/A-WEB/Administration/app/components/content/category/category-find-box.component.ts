import {Component, EventEmitter} from '@angular/core';
import {CategorySearchViewModel} from "../../../viewmodels/category/CategorySearchViewModel";

@Component({
    selector: 'category-find-box',
    templateUrl: './app/html/content/category/category-find-box.component.html',
    inputs:['categorySearch'],
    outputs:['search'],
    providers:[
        CategorySearchViewModel
    ]
})

export class CategoryFindBoxComponent{

    // Whether records are being loaded from server or not.
    private _isLoading: boolean;

    // Category search information.
    private _categorySearch : CategorySearchViewModel;

    // Event which is emitted when search button is clicked.
    private search: EventEmitter<any>;

    // Initiate component with default dependency injection.
    public constructor(){

        // Initiate category search view model.
        this._categorySearch = new CategorySearchViewModel();

        // Initiate event emitters.
        this.search = new EventEmitter();
    }

    // Callback which is fired when search button is clicked.
    public clickSearch(): void{

        this._isLoading = true;
        setTimeout(() => {
            this._isLoading = false;
            this.search.emit(this._categorySearch);
        }, 10000);


    }

}
import {Component} from '@angular/core'
import {CategoryDetailViewModel} from "../../../viewmodels/category/CategoryDetailViewModel";
import {Account} from "../../../models/Account";

// Allow $ symbol to be usable.
declare var $:any;

@Component({
    selector: 'category-edit-box',
    templateUrl: './app/html/content/category/category-edit-box.component.html',
    providers:[
        CategoryDetailViewModel,
        Account
    ]
})

export class CategoryEditBoxComponent{

    // Category detail.
    private _category: CategoryDetailViewModel;

    // Initiate category edit box component.
    public constructor(){
        // Category hasn't been initialized.
        if (this._category == null)
            this._category = new CategoryDetailViewModel();

        // Creator hasn't been initialized.
        if (this._category.creator == null)
            this._category.creator = new Account();

        this._category.name = 'Category 01';
    }

    // Update category which should be shown on screen.
    public setCategory(category: CategoryDetailViewModel){
        this._category = category;
    }

    public getCategory(){
        return this._category;
    }

    // Open the dialog.
    private open(): void{
        console.log(this);
    }
}
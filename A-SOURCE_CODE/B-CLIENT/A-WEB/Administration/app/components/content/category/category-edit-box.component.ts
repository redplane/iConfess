import {Component} from '@angular/core'
import {CategoryDetailViewModel} from "../../../viewmodels/category/CategoryDetailViewModel";

@Component({
    selector: 'category-edit-box',
    templateUrl: './app/html/content/category/category-edit-box.component.html',
    inputs: ['category'],
    output: ['clickConfirm', 'clickDeny']
})

export class CategoryEditBoxComponent{

    // Category detail.
    private _category: CategoryDetailViewModel;

    // Initiate category edit box component.
    public constructor(category: CategoryDetailViewModel){
        this._category = category;
    }
}
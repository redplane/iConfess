import {Component, ViewChild, ElementRef} from '@angular/core'
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

    @ViewChild('changeCategoryBox') private changeCategoryBox: ElementRef;

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

    // Display change category information modal.
    public open() : void {
        $(this.changeCategoryBox.nativeElement).modal('show');
    }

    // Close change category information modal.
    public close():void{
        $(this.changeCategoryBox.nativeElement).modal('close');
    }

    // Toggle change category information modal.
    public toggle(): void{
        $(this.changeCategoryBox.nativeElement).modal('toggle');
    }
}
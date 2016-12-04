import {Component, ViewChild, ElementRef} from '@angular/core'
import {CategoryDetailViewModel} from "../../../viewmodels/category/CategoryDetailViewModel";

// Allow $ symbol to be usable.
declare var $:any;

@Component({
    selector: 'category-delete-box',
    templateUrl: './app/html/content/category/category-delete-box.component.html',
    providers:[
        CategoryDetailViewModel
    ]
})

export class CategoryDeleteBoxComponent{

    @ViewChild('deleteCategoryBox') private deleteCategoryBox: ElementRef;

    // Category detail.
    private _category: CategoryDetailViewModel;

    // Initiate category edit box component.
    public constructor(){

        // Category hasn't been initialized.
        if (this._category == null)
            this._category = new CategoryDetailViewModel();
    }

    // Update category which should be shown on screen.
    public setCategory(category: CategoryDetailViewModel){
        this._category = category;
    }

    // Display change category information modal.
    public open() : void {
        $(this.deleteCategoryBox.nativeElement).modal('show');
    }

    // Close change category information modal.
    public close():void{
        $(this.deleteCategoryBox.nativeElement).modal('close');
    }

    // Toggle change category information modal.
    public toggle(): void{
        $(this.deleteCategoryBox.nativeElement).modal('toggle');
    }
}
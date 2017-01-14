import {Injectable} from "@angular/core";

/*
* Contains data constraint of application.
* */
@Injectable()
export class ClientDataConstraintService{

    /* Category section */
    public minLengthCategoryName: number; // Minimum length of category name.
    public maxLengthCategoryName: number; // Maximum length of category name.

    public constructor(){

        // Category section.
        this.minLengthCategoryName = 6;
        this.maxLengthCategoryName = 10;
    }
}
/*
* Category properties.
* */
import {Account} from "./Account";
export class Category{

    // Id of category.
    public id: number;

    // Category creator information.
    public creator: Account;

    // Name of category.
    public name: string;

    // When the category was created.
    public created: number;

    // When the category was lastly modified.
    public lastModified: number;
}
import {UnixDateRange} from "../UnixDateRange";

export class CategorySearchViewModel{

    // Name of category
    public name: string;

    // Index of category creator.
    public creatorIndex: number;

    // When the category was created.
    public created: UnixDateRange;

    // When the category was lastly modified.
    public lastModified: UnixDateRange;

    // Initiate view model with properties.
    public constructor(){
        this.name = null;
        this.creatorIndex = null;
        this.created = new UnixDateRange();
        this.lastModified = new UnixDateRange();
    }
}
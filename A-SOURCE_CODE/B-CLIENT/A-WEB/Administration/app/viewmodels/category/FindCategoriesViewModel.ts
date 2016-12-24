import {UnixDateRange} from "../UnixDateRange";
import {Pagination} from "../Pagination";
import {SortDirection} from "../../enumerations/SortDirection";
import {CategorySortProperty} from "../../enumerations/order/CategorySortProperty";
import {TextSearchMode} from "../../enumerations/TextSearchMode";
import {TextSearch} from "../TextSearch";

export class FindCategoriesViewModel{

    // Index of category.
    public id: number;

    // Name of category
    public name: TextSearch;

    // Index of category creator.
    public creatorIndex: number;

    // When the category was created.
    public created: UnixDateRange;

    // When the category was lastly modified.
    public lastModified: UnixDateRange;

    // Results pagination.
    public pagination: Pagination;

    // Results sorting.
    public direction: SortDirection;

    // Property which is used for sorting.
    public sort: CategorySortProperty;

    // Initiate view model with properties.
    public constructor(){
        this.name = new TextSearch();
        this.creatorIndex = null;
        this.created = new UnixDateRange();
        this.lastModified = new UnixDateRange();
        this.pagination = new Pagination();
        this.direction = SortDirection.Ascending;
        this.sort = CategorySortProperty.index;
    }
}
import {TextSearch} from "../TextSearch";
import {UnixDateRange} from "../UnixDateRange";
import {Pagination} from "../Pagination";
import {CommentsDetailsSortProperty} from "../../enumerations/order/CommentsDetailsSortProperty";
import {SortDirection} from "../../enumerations/SortDirection";

export class SearchCommentsDetailsViewModel{

    // Id of comment.
    public id: number;

    // Index of comment owner.
    public ownerIndex: number;

    // Index of post which comment belongs to.
    public postIndex: number;

    // Time range when the comment had been created.
    public created: UnixDateRange;

    // Time range when them comment was lastly modified.
    public lastModified: UnixDateRange;

    // Property which should be used for sorting.
    public sort: CommentsDetailsSortProperty;

    // Whether list should be sorted ascending or descending.
    public direction: SortDirection;

    // Pagination of comments search.
    public pagination: Pagination;
}
import {TextSearch} from "../TextSearch";
import {UnixDateRange} from "../UnixDateRange";
import {Pagination} from "../Pagination";

export class SearchCommentsViewModel{

    // Id of comment.
    public id: number;

    // Index of comment owner.
    public ownerIndex: number;

    // Index of post which comment belongs to.
    public postIndex: number;

    // Comment content.
    public content: TextSearch;

    // Time range when the comment had been created.
    public created: UnixDateRange;

    // Time range when them comment was lastly modified.
    public lastModified: UnixDateRange;

    // Pagination of comments search.
    public pagination: Pagination;
}
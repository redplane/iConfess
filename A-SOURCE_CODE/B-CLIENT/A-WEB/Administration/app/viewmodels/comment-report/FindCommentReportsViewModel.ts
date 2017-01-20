import {UnixDateRange} from "../UnixDateRange";
import {Pagination} from "../Pagination";
import {TextSearch} from "../TextSearch";
import {CommentReportSortProperty} from "../../enumerations/order/CommentReportSortProperty";
import {SortDirection} from "../../enumerations/SortDirection";
import {TextSearchMode} from "../../enumerations/TextSearchMode";
export class FindCommentReportsViewModel{

    // Index of comment report.
    public id: number;

    // Index of comment.
    public commentIndex: number;

    // Index of comment owner.
    public ownerIndex: number;

    // Index of comment reporter.
    public reporterIndex: number;

    // Body of comment.
    public body: TextSearch;

    // Reason of comment.
    public reason: TextSearch;

    // Property which should be used for results sorting.
    public sort: CommentReportSortProperty;

    // Direction of sorting.
    public direction: SortDirection;

    // Time when the comment was created.
    public created: UnixDateRange;

    // Results pagination.
    public pagination: Pagination;

    // Initiate filter with default settings.
    public constructor(){

        let commentBody = new TextSearch();
        commentBody.mode = TextSearchMode.contains;
        commentBody.value = '';
        this.body = commentBody;

        let commentReportReason = new TextSearch();
        commentReportReason.mode = TextSearchMode.contains;
        commentReportReason.value = '';
        this.reason = commentReportReason;

        this.sort = CommentReportSortProperty.Index;
        this.direction = SortDirection.Ascending;
        this.pagination = new Pagination();
    }
}
import {UnixDateRange} from "../UnixDateRange";
import {Pagination} from "../Pagination";
import {SortDirection} from "../../enumerations/SortDirection";
import {PostReportSortProperty} from "../../enumerations/order/PostReportSortProperty";
export class FindPostReportViewModel{

    // Id of report.
    public id: number;

    // Id of post.
    public postIndex: number;

    // Post owner index.
    public postOwnerIndex: number;

    // Post reporter index.
    public postReporterIndex: number;

    // Time range when post report was created.
    public created: UnixDateRange;

    // Whether record should be sorted ascendingly or descendingly.
    public direction: SortDirection;

    // Post report sort property.
    public sort : PostReportSortProperty;

    // Records pagination.
    public pagination: Pagination;
}
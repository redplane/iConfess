import {Comment} from '../../models/Comment'

export class SearchCommentsResultViewModel{
    // Comments which can be used for binding to view.
    public comments: Array<Comment>;

    // Total searched comments.
    public total: number;
}
import {SearchCommentsDetailsViewModel} from "./SearchCommentsDetailsViewModel";

export class SearchCommentsDetailsResultViewModel{

    // List of comments details which have been found.
    public commentsDetails: Array<SearchCommentsDetailsViewModel>;

    // Total record which match the condition.
    public total: number;
}
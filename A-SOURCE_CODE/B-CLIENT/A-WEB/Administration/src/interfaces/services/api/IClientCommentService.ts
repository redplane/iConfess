import {SearchCommentsDetailsViewModel} from "../../../viewmodels/comment/SearchCommentsDetailsViewModel";
import {SearchCommentsViewModel} from "../../../viewmodels/comment/SearchCommentsViewModel";
import {Response} from "@angular/http";

export interface IClientCommentService {

    //#region Methods

    // Get list of comments base on specific conditions.
    getComments(conditions: SearchCommentsViewModel): Promise<Response>;

    // Search for a specific comment's detail.
    getCommentDetails(conditions: SearchCommentsDetailsViewModel): Promise<Response>;

    //#endregion
}
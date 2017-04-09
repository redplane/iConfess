import {SearchPostsViewModel} from "../../../viewmodels/post/SearchPostsViewModel";
import {Response} from "@angular/http";

export interface IClientPostService {

    //#region Methods

    // Find posts by using specific conditions.
    getPosts(conditions: SearchPostsViewModel): Promise<Response>;

    // Find post details.
    getPostDetails(index: number): Promise<Response>;

    //#endregion
}
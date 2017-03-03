import {Injectable} from "@angular/core";
import {ClientApiService} from "../ClientApiService";
import {ClientAuthenticationService} from "./ClientAuthenticationService";
import {CommentSearchViewModel} from "../../viewmodels/comment/CommentSearchViewModel";
import {SearchCommentsDetailsViewModel} from "../../viewmodels/comment/SearchCommentsDetailsViewModel";

@Injectable()
export class ClientCommentService{

    // Initiate service with IoC.
    public constructor(private clientApiService: ClientApiService,
                       private clientAuthenticationService: ClientAuthenticationService){
    }

    // Search for comments by using specific conditions.
    public searchComments(conditions: CommentSearchViewModel): any{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiSearchComment, null, conditions)
            .toPromise();
    }

    // Search for a specific comment's detail.
    public searchCommentDetails(conditions: SearchCommentsDetailsViewModel){
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),this.clientApiService.apiSearchCommentDetails, null, conditions).toPromise();
    }
}
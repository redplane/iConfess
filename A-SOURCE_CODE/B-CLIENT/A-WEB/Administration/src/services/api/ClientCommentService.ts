import {Injectable, Inject} from "@angular/core";
import {SearchCommentsViewModel} from "../../viewmodels/comment/SearchCommentsViewModel";
import {SearchCommentsDetailsViewModel} from "../../viewmodels/comment/SearchCommentsDetailsViewModel";
import {IClientCommentService} from "../../interfaces/services/api/IClientCommentService";
import {Response} from "@angular/http";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";

@Injectable()
export class ClientCommentService implements IClientCommentService{

    //#region Properties

    // Url which is for searching comments.
    public urlSearchComment: string = "api/comment/find";

    // Url which is for searching for comment details.
    public urlSearchCommentDetails: string = "api/comments/details";

    //#endregion

    //#region Constructor

    // Initiate service with injectors.
    public constructor(@Inject("IClientApiService") public clientApiService: IClientApiService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService){
    }

    //#endregion

    //#region Methods

    // Search for comments by using specific conditions.
    public getComments(conditions: SearchCommentsViewModel): Promise<Response>{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            `${this.clientApiService.getBaseUrl()}/${this.urlSearchComment}`,
            null, conditions);
    }

    // Search for a specific comment's detail.
    public getCommentDetails(conditions: SearchCommentsDetailsViewModel): Promise<Response>{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            `${this.clientApiService.getBaseUrl()}/${this.urlSearchCommentDetails}`,
            null, conditions);
    }

    //#endregion
}
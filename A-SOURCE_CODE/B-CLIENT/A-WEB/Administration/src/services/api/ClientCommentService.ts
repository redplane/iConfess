import {Injectable, Inject} from "@angular/core";
import {ClientApiService} from "../ClientApiService";
import {SearchCommentsViewModel} from "../../viewmodels/comment/SearchCommentsViewModel";
import {SearchCommentsDetailsViewModel} from "../../viewmodels/comment/SearchCommentsDetailsViewModel";
import {IClientCommentService} from "../../interfaces/services/api/IClientCommentService";
import {Response} from "@angular/http";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";

@Injectable()
export class ClientCommentService implements IClientCommentService{

    //#region Constructor

    // Initiate service with IoC.
    public constructor(private clientApiService: ClientApiService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService){
    }

    //#endregion

    //#region Methods

    // Search for comments by using specific conditions.
    public getComments(conditions: SearchCommentsViewModel): Promise<Response>{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiSearchComment, null, conditions)
            .toPromise();
    }

    // Search for a specific comment's detail.
    public getCommentDetails(conditions: SearchCommentsDetailsViewModel): Promise<Response>{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),this.clientApiService.apiSearchCommentDetails, null, conditions).toPromise();
    }

    //#endregion
}
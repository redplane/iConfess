import {Inject, Injectable} from "@angular/core";
import {SearchPostsViewModel} from "../../viewmodels/post/SearchPostsViewModel";
import {ClientApiService} from "../ClientApiService";
import {Response} from "@angular/http";
import {IClientPostService} from "../../interfaces/services/api/IClientPostService";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";

@Injectable()
export class ClientPostService implements IClientPostService{

    //#region Constructor

    // Initiate instance of category service.
    public constructor(private clientApiService: ClientApiService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService){
    }

    //#endregion

    //#region Methods

    // Find categories by using specific conditions.
    public getPosts(conditions: SearchPostsViewModel): Promise<Response> {
        // Page index should be decrease by one.
        let localConditions = Object.assign({}, conditions);
        localConditions['pagination'] = Object.assign({}, localConditions.pagination);
        localConditions.pagination.index -= 1;
        if (localConditions.pagination.index < 0)
            localConditions.pagination.index = 0;


        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiFindPost,
            null,
            localConditions).toPromise();
    }

    // Find post details.
    public getPostDetails(index: number): Promise<Response>{
        // Build full url.
        let url = `${this.clientApiService.apiFindPostDetails}?index=${index}`;

        // Request to api to obtain list of available categories in system.
        return this.clientApiService.get(this.clientAuthenticationService.findClientAuthenticationToken(),
            url,
            null).toPromise();
    }

    //#endregion
}
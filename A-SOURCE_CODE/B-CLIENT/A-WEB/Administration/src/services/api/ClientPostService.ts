import {Inject, Injectable} from "@angular/core";
import {SearchPostsViewModel} from "../../viewmodels/post/SearchPostsViewModel";
import {Response} from "@angular/http";
import {IClientPostService} from "../../interfaces/services/api/IClientPostService";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";

@Injectable()
export class ClientPostService implements IClientPostService{

    //#region Properties

    // Search posts.
    public urlSearchPosts: string = "api/post/find";

    // Search post details.
    public urlSearchPostDetails: string = "api/post/details";

    //#endregion

    //#region Constructor

    // Initiate instance of category service.
    public constructor(@Inject("IClientApiService") private clientApiService: IClientApiService,
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


        // Initiate url.
        let url = `${this.clientApiService.getBaseUrl()}/${this.urlSearchPosts}`;

        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(
            this.clientAuthenticationService.getTokenCode(),
            url,
            null,
            localConditions);
    }

    // Find post details.
    public getPostDetails(index: number): Promise<Response>{
        // Build full url.
        let url = `${this.clientApiService.getBaseUrl()}/${this.urlSearchPostDetails}?index=${index}`;

        // Request to api to obtain list of available categories in system.
        return this.clientApiService.get(
            this.clientAuthenticationService.getTokenCode(),
            url,
            null);
    }

    //#endregion
}
import {Injectable} from "@angular/core";
import {SearchPostsViewModel} from "../../viewmodels/post/SearchPostsViewModel";
import {ClientApiService} from "../ClientApiService";
import {ClientAuthenticationService} from "./ClientAuthenticationService";

@Injectable()
export class ClientPostService{

    // Initiate instance of category service.
    public constructor(private clientApiService: ClientApiService,
                       public clientAuthenticationService: ClientAuthenticationService){
    }

    // Find categories by using specific conditions.
    public findPosts(findPostsCondition: SearchPostsViewModel) {
        // Page index should be decrease by one.
        let conditions = Object.assign({}, findPostsCondition);
        conditions['pagination'] = Object.assign({}, findPostsCondition.pagination);
        conditions.pagination.index -= 1;
        if (conditions.pagination.index < 0)
            conditions.pagination.index = 0;


        // Request to api to obtain list of available categories in system.
        return this.clientApiService.post(this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiFindPost,
            null,
            conditions).toPromise();
    }

    // Find post details.
    public findPostDetails(index: number){
        // Build full url.
        let url = `${this.clientApiService.apiFindPostDetails}?index=${index}`;

        // Request to api to obtain list of available categories in system.
        return this.clientApiService.get(this.clientAuthenticationService.findClientAuthenticationToken(),
            url,
            null).toPromise();
    }
}
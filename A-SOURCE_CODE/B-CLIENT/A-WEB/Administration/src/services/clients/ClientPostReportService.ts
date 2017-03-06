import {Injectable} from "@angular/core";
import {SearchPostReportsViewModel} from "../../viewmodels/post-report/SearchPostReportsViewModel";
import {ClientApiService} from "../ClientApiService";
import {ClientAuthenticationService} from "./ClientAuthenticationService";

@Injectable()
export class ClientPostReportService{

    // Initiate service with IoC.
    public constructor(private clientApiService: ClientApiService,
                       private clientAuthenticationService: ClientAuthenticationService){
    }

    // Find post reports by using specific conditions.
    public findPostReports(conditions: SearchPostReportsViewModel): any{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiFindPostReport, null, conditions)
            .toPromise();
    }

    // Delete post reports by using specific conditions.
    public deletePostReports(conditions: SearchPostReportsViewModel): any{
        return this.clientApiService.delete(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiDeletePostReport, null, conditions)
            .toPromise();
    }
}
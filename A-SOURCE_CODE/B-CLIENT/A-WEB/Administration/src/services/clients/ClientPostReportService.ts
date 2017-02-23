import {Injectable} from "@angular/core";
import {FindPostReportViewModel} from "../../viewmodels/post-report/FindPostReportViewModel";
import {ClientApiService} from "../ClientApiService";
import {ClientAuthenticationService} from "./ClientAuthenticationService";

@Injectable()
export class ClientPostReportService{

    // Initiate service with IoC.
    public constructor(private clientApiService: ClientApiService,
                       private clientAuthenticationService: ClientAuthenticationService){
    }

    // Find post reports by using specific conditions.
    public findPostReports(conditions: FindPostReportViewModel): any{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiFindPostReport, null, conditions)
            .toPromise();
    }

    // Delete post reports by using specific conditions.
    public deletePostReports(conditions: FindPostReportViewModel): any{
        return this.clientApiService.delete(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiDeletePostReport, null, conditions)
            .toPromise();
    }
}
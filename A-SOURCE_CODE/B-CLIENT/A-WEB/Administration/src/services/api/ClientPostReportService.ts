import {Injectable, Inject} from "@angular/core";
import {SearchPostReportsViewModel} from "../../viewmodels/post-report/SearchPostReportsViewModel";
import {ClientApiService} from "../ClientApiService";
import {Response} from "@angular/http";
import {IClientPostReportService} from "../../interfaces/services/api/IClientPostReportService";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";

@Injectable()
export class ClientPostReportService implements IClientPostReportService{

    //#region Constructor

    // Initiate service with injectors.
    public constructor(private clientApiService: ClientApiService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService){
    }

    //#endregion

    //#region Methods

    // Find post reports by using specific conditions.
    public getPostReports(conditions: SearchPostReportsViewModel): Promise<Response>{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiFindPostReport, null, conditions)
            .toPromise();
    }

    // Delete post reports by using specific conditions.
    public deletePostReports(conditions: SearchPostReportsViewModel): Promise<Response>{
        return this.clientApiService.delete(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            this.clientApiService.apiDeletePostReport, null, conditions)
            .toPromise();
    }

    //#endregion
}
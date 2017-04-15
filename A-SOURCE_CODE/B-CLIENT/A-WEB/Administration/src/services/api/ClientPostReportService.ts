import {Injectable, Inject} from "@angular/core";
import {SearchPostReportsViewModel} from "../../viewmodels/post-report/SearchPostReportsViewModel";
import {Response} from "@angular/http";
import {IClientPostReportService} from "../../interfaces/services/api/IClientPostReportService";
import {IClientAuthenticationService} from "../../interfaces/services/api/IClientAuthenticationService";
import {IClientApiService} from "../../interfaces/services/api/IClientApiService";

@Injectable()
export class ClientPostReportService implements IClientPostReportService{

    //#region Properties

    // Url which is for searching for post reports.
    public urlSearchPostReport: string = "api/report/post/find";

    // Url which is for deleting for post reports.
    public urlDeletePostReport: string = "api/report/post";

    //#endregion

    //#region Constructor

    // Initiate service with injectors.
    public constructor(@Inject("IClientApiService") private clientApiService: IClientApiService,
                       @Inject("IClientAuthenticationService") public clientAuthenticationService: IClientAuthenticationService){
    }

    //#endregion

    //#region Methods

    // Find post reports by using specific conditions.
    public getPostReports(conditions: SearchPostReportsViewModel): Promise<Response>{
        return this.clientApiService.post(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            `${this.clientApiService.getBaseUrl()}/${this.urlSearchPostReport}`,
            null, conditions);
    }

    // Delete post reports by using specific conditions.
    public deletePostReports(conditions: SearchPostReportsViewModel): Promise<Response>{
        return this.clientApiService.delete(
            this.clientAuthenticationService.findClientAuthenticationToken(),
            `${this.clientApiService.getBaseUrl()}/${this.urlDeletePostReport}`,
            null, conditions);
    }

    //#endregion
}
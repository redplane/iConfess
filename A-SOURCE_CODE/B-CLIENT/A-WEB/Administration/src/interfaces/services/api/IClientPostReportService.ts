import {SearchPostReportsViewModel} from "../../../viewmodels/post-report/SearchPostReportsViewModel";
import {Response} from "@angular/http";

export interface IClientPostReportService {

    //#region Methods

    // Find post reports by using specific conditions.
    getPostReports(conditions: SearchPostReportsViewModel): Promise<Response>;

    // Delete post reports by using specific conditions.
    deletePostReports(conditions: SearchPostReportsViewModel): Promise<Response>;

    //#endregion
}
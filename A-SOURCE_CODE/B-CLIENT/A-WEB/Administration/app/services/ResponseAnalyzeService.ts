import {Response} from "@angular/http";

/*
* Class which is used for analyze response sent back from server.
* */
export class ResponseAnalyzeService{

    public analyzeInvalidResponse(response: Response | any): void{
        console.log(response);
    }
}
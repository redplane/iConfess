import {Response} from "@angular/http";

/*
* Class which is used for analyze response sent back from server.
* */
export class ClientProceedResponseService{

    public proceedInvalidResponse(response: Response | any): void{

        // Response is invalid.
        if (response == null || response['status'] == null)
            return;

        switch (response.status){
            case 401: // Request is unauthorized.
                break;
        }
    }
}
import {Component} from "@angular/core";
import {ClientNotificationService} from "../../../services/ClientNotificationService";
import {ClientAuthenticationService} from "../../../services/clients/ClientAuthenticationService";

@Component({
    selector: 'comment-report-management',
    templateUrl: './app/views/views/comment-report-management/comment-report-management.component.html',
    providers:[
        ClientNotificationService,
        ClientAuthenticationService
    ]
})

export class CommentReportManagementComponent{

}
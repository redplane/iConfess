import {Component, OnInit} from "@angular/core";
import {ClientConfigurationService} from "../../../services/ClientConfigurationService";
import {FormBuilder, FormGroup} from "@angular/forms";
import {Account} from "../../../models/Account";
import {FindCommentReportsViewModel} from "../../../viewmodels/comment-report/FindCommentReportsViewModel";
import {FindAccountsViewModel} from "../../../viewmodels/accounts/FindAccountsViewModel";
import {TextSearch} from "../../../viewmodels/TextSearch";
import {ClientAccountService} from "../../../services/clients/ClientAccountService";
import {Pagination} from "../../../viewmodels/Pagination";
import {TextSearchMode} from "../../../enumerations/TextSearchMode";
import {ClientApiService} from "../../../services/ClientApiService";

@Component({
    selector: 'comment-report-find-box',
    templateUrl: '../../..//views/contents/comment-report/comment-report-find-box.component.html',
    inputs:['conditions'],
    providers:[
        ClientConfigurationService,
        ClientAccountService,
        ClientApiService
    ]
})

export class CommentReportFindBoxComponent implements OnInit{

    // Container of controls of comment report.
    private commentReportFindBox: FormGroup;

    //  List of comment owners which can be filtered on the find box.
    private commentOwnersList: Array<Account>;

    // List of comment reporters which can be filtered on the find box.
    private commentReportersList: Array<Account>;

    // Find comment report conditions.
    private conditions: FindCommentReportsViewModel;

    public constructor(private clientConfigurationService: ClientConfigurationService,
                       private clientAccountService: ClientAccountService,
                       private formBuilder: FormBuilder){

        // Initiate comment report find box components container.
        this.commentReportFindBox = this.formBuilder.group({
            commentOwner: [''],
            commentReporter: [''],
            body: this.formBuilder.group({
                mode: [''],
                value: ['']
            }),
            reason: this.formBuilder.group({
                mode: [''],
                value: []
            }),
            created: this.formBuilder.group({
                from: [''],
                to: ['']
            }),
            pagination: this.formBuilder.group({
                index: [''],
                records:['']
            }),
            sort: [''],
            direction:['']
        });

        this.conditions = new FindCommentReportsViewModel();
    }

    // Callback which is fired when component has been rendered successfully.
    public ngOnInit(): void {
        this.conditions.pagination.records = this.clientConfigurationService.findMaxPageRecords();
    }

    // Callback which is fired when control is starting to load data of accounts from service.
    private loadAccounts(email: string): any {

        // Initiate find account conditions.
        let findAccountsViewModel = new FindAccountsViewModel();

        // Update account which should be searched for.
        if (findAccountsViewModel.email == null)
            findAccountsViewModel.email = new TextSearch();
        findAccountsViewModel.email.value = email;
        findAccountsViewModel.email.mode = TextSearchMode.contains;

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.findMaxPageRecords();

        // Pagination update.
        findAccountsViewModel.pagination = pagination;

        return this.clientAccountService.findAccounts(findAccountsViewModel);
            //
            // .then((response: Response | any) => {
            //
            //     // Analyze find account response view model.
            //     let findAccountResult = response.json();
            //
            //     // Find list of accounts which has been responded from service.
            //     this._accounts = findAccountResult.accounts;
            // })
            // .catch((response: any) => {
            //
            // });
    }

}
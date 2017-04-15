import {Component, Inject, OnInit} from "@angular/core";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {FormBuilder, FormGroup} from "@angular/forms";
import {Account} from "../../models/entities/Account";
import {SearchCommentReportsViewModel} from "../../viewmodels/comment-report/SearchCommentReportsViewModel";
import {SearchAccountsViewModel} from "../../viewmodels/accounts/SearchAccountsViewModel";
import {TextSearch} from "../../viewmodels/TextSearch";
import {Pagination} from "../../viewmodels/Pagination";
import {TextSearchMode} from "../../enumerations/TextSearchMode";
import {ClientApiService} from "../../services/ClientApiService";
import {IClientAccountService} from "../../interfaces/services/api/IClientAccountService";

@Component({
    selector: 'comment-report-find-box',
    templateUrl: 'comment-report-find-box.component.html',
    inputs:['conditions'],
    providers:[
        ClientConfigurationService
    ]
})

export class CommentReportFindBoxComponent implements OnInit{

    //#region Properties

    // Container of controls of comment report.
    private commentReportFindBox: FormGroup;

    //  List of comment owners which can be filtered on the find box.
    private commentOwnersList: Array<Account>;

    // List of comment reporters which can be filtered on the find box.
    private commentReportersList: Array<Account>;

    // Find comment report conditions.
    private conditions: SearchCommentReportsViewModel;

    //#endregion

    //#region Constructor

    public constructor(private clientConfigurationService: ClientConfigurationService,
                       @Inject("IClientAccountService") private clientAccountService: IClientAccountService,
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

        this.conditions = new SearchCommentReportsViewModel();
    }

    //#endregion

    //#region Methods

    // Callback which is fired when component has been rendered successfully.
    public ngOnInit(): void {
        this.conditions.pagination.records = this.clientConfigurationService.getMaxPageRecords();
    }

    // Callback which is fired when control is starting to load data of accounts from service.
    private loadAccounts(email: string): any {

        // Initiate find account conditions.
        let findAccountsViewModel = new SearchAccountsViewModel();

        // Update account which should be searched for.
        if (findAccountsViewModel.email == null)
            findAccountsViewModel.email = new TextSearch();
        findAccountsViewModel.email.value = email;
        findAccountsViewModel.email.mode = TextSearchMode.contains;

        // Initiate pagination.
        let pagination = new Pagination();
        pagination.index = 0;
        pagination.records = this.clientConfigurationService.getMaxPageRecords();

        // Pagination update.
        findAccountsViewModel.pagination = pagination;

        return this.clientAccountService.getAccounts(findAccountsViewModel);
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

    //#endregion
}
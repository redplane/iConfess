import {Component, OnInit, EventEmitter} from "@angular/core";
import {Post} from "../../models/Post";
import {SearchCommentsResultViewModel} from "../../viewmodels/comment/SearchCommentsResultViewModel";
import {Category} from "../../models/Category";
import {Account} from "../../models/Account";
import {ClientTimeService} from "../../services/ClientTimeService";
import {ClientConfigurationService} from "../../services/ClientConfigurationService";
import {SearchPostsViewModel} from "../../viewmodels/post/SearchPostsViewModel";
import {Pagination} from "../../viewmodels/Pagination";
import {SearchCommentsDetailsResultViewModel} from "../../viewmodels/comment/SearchCommentsDetailsResultViewModel";

@Component({
    selector: 'post-detail-box',
    templateUrl: 'post-detail-box.component.html',
    inputs:['maxComments', 'postDetails', 'searchCommentsDetailsResult', 'isSearchingPost', 'isSearchingComments'],
    outputs:['changeCommentsPage'],
    providers: [
        ClientTimeService,
        ClientConfigurationService
    ]
})

export class PostDetailBoxComponent implements OnInit{

    // Whether component is searching for post information or not.
    private isSearchingPost: boolean;

    // Whether component is searching for comments list of a related post or not.
    private isSearchingComments: boolean;

    // List of comments search result.
    public searchCommentsDetailsResult: SearchCommentsDetailsResultViewModel;

    // Condition of post detail searching.
    public searchPostDetailCondition: SearchPostsViewModel;

    // Emitter which is used for emitting event when comments page is changed.
    private changeCommentsPage: EventEmitter<number>;

    public constructor(
        public clientTimeService: ClientTimeService,
        public clientConfigurationService: ClientConfigurationService) {

        // Event emitter initialization.
        this.changeCommentsPage = new EventEmitter();
    }

    // Callback which is fired when component has been initiated.
    public ngOnInit(): void {

        let condition = new SearchPostsViewModel();
        let pagination = new Pagination();

        pagination.index = 0;
        pagination.records = this.clientConfigurationService.getMinPageRecords();
        condition.pagination = pagination;

        this.searchPostDetailCondition = condition;
    }

    // Check whether post contains any comments or not.
    private hasComments(): boolean {
        // Result is blank.
        if (this.searchCommentsDetailsResult == null)
            return false;

        // Comments list is empty.
        let comments = this.searchCommentsDetailsResult.commentsDetails;
        if (comments == null || comments.length < 1)
            return false;
        if (this.searchCommentsDetailsResult.total < 1)
            return false;
        return true;
    }

    // Check whether component is running for search tasks or not.
    private isComponentBusy(): boolean{
        return (this.isSearchingPost || this.isSearchingComments);
    }

    // Callback which is fired when comments pagination button is clicked.
    private clickSearchComments(parameter: any): void{
        // Parameter is invalid.
        if (parameter == null || parameter['page'] == null) {
            this.changeCommentsPage.emit(0);
            return;
        }

        let page = parameter['page'];
        page--;
        if (page < 0)
            page = 0;
        this.changeCommentsPage.emit(page);
    }
}
import {Injectable} from "@angular/core";
import {Dictionary} from "../viewmodels/Dictionary";
import {SortDirection} from "../enumerations/SortDirection";
import {CategorySortProperty} from "../enumerations/order/CategorySortProperty";
import {AccountSortProperty} from "../enumerations/order/AccountSortProperty";
import {Account} from "../models/entities/Account";
import {AccountStatuses} from "../enumerations/AccountStatuses";
import {SelectionItem} from "../models/SelectionItem";
import {CommentReportSortProperty} from "../enumerations/order/CommentReportSortProperty";
import {TextSearchMode} from "../enumerations/TextSearchMode";
import {PostSortProperty} from "../enumerations/order/PostSortProperty";
import {PostReportSortProperty} from "../enumerations/order/PostReportSortProperty";

@Injectable()
export class ClientConfigurationService {

    // List of page record number which can be selected on the screen.
    public pageRecords: number[];

    // Modes of text search.
    public textSearchModes: Dictionary<TextSearchMode>;

    // List of direction which can be used in records sorting.
    public sortDirections: Dictionary<SortDirection>;

    // List of properties which can be used for accounts sorting.
    public accountSortProperties: Dictionary<AccountSortProperty>;

    // List of properties which can be used for categories sorting.
    public categorySortProperties: Dictionary<CategorySortProperty>;

    // List of properties which are used for sorting.
    public postSortProperties: Dictionary<PostSortProperty>;

    // List of properties which are used for sorting.
    public postReportSortProperties: Dictionary<PostReportSortProperty>;

    // List of properties which can be used for comments sorting.
    public commentReportSortProperties: Dictionary<CommentReportSortProperty>;// List of items which can be selected in accounts list.

    // List of account statuses which can be selected.
    public accountStatusSelections: Dictionary<AccountStatuses>;

    // Initiate instance of service with default settings.
    public constructor() {
        // Amount of records which can be displayed on the screen.
        this.pageRecords = [5, 10, 15, 20];

        // Initiate list of text search modes.
        this.textSearchModes = this.initiateTextSearchModes();

        // Initiate sort directions list.
        this.sortDirections = new Dictionary<SortDirection>();
        this.sortDirections.add('Ascending', SortDirection.Ascending);
        this.sortDirections.add('Descending', SortDirection.Descending);

        // Initiate account sort properties.
        this.accountSortProperties = new Dictionary<AccountSortProperty>();
        this.accountSortProperties.add('Index', AccountSortProperty.index);
        this.accountSortProperties.add('Email', AccountSortProperty.email);
        this.accountSortProperties.add('Nickname', AccountSortProperty.nickname);
        this.accountSortProperties.add('Status', AccountSortProperty.status);
        this.accountSortProperties.add('Joined', AccountSortProperty.joined);
        this.accountSortProperties.add('Last modified', AccountSortProperty.lastModified);

        // Initiate list of account statuses.
        this.accountStatusSelections = this.initializeAccountSelections();

        // Initiate category sort properties.
        this.categorySortProperties = new Dictionary<CategorySortProperty>();
        this.categorySortProperties.add('Index', CategorySortProperty.index);
        this.categorySortProperties.add('Creator', CategorySortProperty.creatorIndex);
        this.categorySortProperties.add('Name', CategorySortProperty.name);
        this.categorySortProperties.add('Created', CategorySortProperty.created);
        this.categorySortProperties.add('Last modified', CategorySortProperty.lastModified);

        // Initiate post sort properties.
        this.postSortProperties = new Dictionary<PostSortProperty>();
        this.postSortProperties.add('Index', PostSortProperty.id);
        this.postSortProperties.add('Owner', PostSortProperty.ownerIndex);
        this.postSortProperties.add('Category', PostSortProperty.categoryIndex);
        this.postSortProperties.add('Created', PostSortProperty.created);

        // Initiate post report sort properties list.
        this.postReportSortProperties = new Dictionary<PostReportSortProperty>();
        this.postReportSortProperties.add('Index', PostReportSortProperty.id);
        this.postReportSortProperties.add('Post', PostReportSortProperty.postIndex);
        this.postReportSortProperties.add('Post owner', PostReportSortProperty.postOwnerIndex);
        this.postReportSortProperties.add('Post reporter', PostReportSortProperty.postReporterIndex);
        this.postReportSortProperties.add('Created', PostReportSortProperty.created);

        // Initiate list of comment report sort properties.
        this.commentReportSortProperties = this.initiateCommentReportSortProperties();
    }

    // Maximum number of records which can be displayed on page.
    public getMaxPageRecords(): number {
        return 20;
    }

    // Get minimum number of records can be displayed per page.
    public getMinPageRecords(): number{
        return 5;
    }

    // Initiate list of account statuses selection.
    private initializeAccountSelections(): Dictionary<AccountStatuses> {
        let accountStatusItems = new Dictionary<AccountStatuses>();

        accountStatusItems.add('Inactive', AccountStatuses.Disabled);
        accountStatusItems.add('Pending', AccountStatuses.Pending);
        accountStatusItems.add('Active', AccountStatuses.Active);

        return accountStatusItems;
    }

    // Initiate comment report sort properties list.
    private initiateCommentReportSortProperties(): Dictionary<CommentReportSortProperty> {

        // Initiate properties list.
        let commentReportSortProperties = new Dictionary<CommentReportSortProperty>();
        commentReportSortProperties.add('Index', CommentReportSortProperty.Index);
        commentReportSortProperties.add('Created', CommentReportSortProperty.Index);

        return commentReportSortProperties;
    }

    // Initiate text search modes list.
    private initiateTextSearchModes(): Dictionary<TextSearchMode> {

        // Initiate text search modes.
        let textSearchModes = new Dictionary<TextSearchMode>();
        textSearchModes.add('Equals', TextSearchMode.equals);
        textSearchModes.add('Equals case-insensitively', TextSearchMode.equalsIgnoreCase);
        textSearchModes.add('Contains', TextSearchMode.contains);
        return textSearchModes;
    }
}
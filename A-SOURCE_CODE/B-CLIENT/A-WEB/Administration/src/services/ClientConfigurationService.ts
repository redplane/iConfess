import {Injectable} from "@angular/core";
import {Dictionary} from "../viewmodels/Dictionary";
import {SortDirection} from "../enumerations/SortDirection";
import {CategorySortProperty} from "../enumerations/order/CategorySortProperty";
import {AccountSortProperty} from "../enumerations/order/AccountSortProperty";
import {Account} from "../models/Account";
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

    // Pagination settings of ng2-bootstrap.
    public ngPaginationSettings: any;

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

        // Initate default settings of ng2-bootstrap pagination control.
        this.ngPaginationSettings = {
            rotate: true, //if true current page will in the middle of views list
            firstText: '<<',
            previousText: '<',
            nextText: '>',
            lastText: '>>'
        };

        // Initiate sort directions list.
        this.sortDirections = new Dictionary<SortDirection>();
        this.sortDirections.insert('Ascending', SortDirection.Ascending);
        this.sortDirections.insert('Descending', SortDirection.Descending);

        // Initiate account sort properties.
        this.accountSortProperties = new Dictionary<AccountSortProperty>();
        this.accountSortProperties.insert('Index', AccountSortProperty.index);
        this.accountSortProperties.insert('Email', AccountSortProperty.email);
        this.accountSortProperties.insert('Nickname', AccountSortProperty.nickname);
        this.accountSortProperties.insert('Status', AccountSortProperty.status);
        this.accountSortProperties.insert('Joined', AccountSortProperty.joined);
        this.accountSortProperties.insert('Last modified', AccountSortProperty.lastModified);

        // Initiate list of account statuses.
        this.accountStatusSelections = this.initializeAccountSelections();

        // Initiate category sort properties.
        this.categorySortProperties = new Dictionary<CategorySortProperty>();
        this.categorySortProperties.insert('Index', CategorySortProperty.index);
        this.categorySortProperties.insert('Creator', CategorySortProperty.creatorIndex);
        this.categorySortProperties.insert('Name', CategorySortProperty.name);
        this.categorySortProperties.insert('Created', CategorySortProperty.created);
        this.categorySortProperties.insert('Last modified', CategorySortProperty.lastModified);

        // Initiate post sort properties.
        this.postSortProperties = new Dictionary<PostSortProperty>();
        this.postSortProperties.insert('Index', PostSortProperty.id);
        this.postSortProperties.insert('Owner', PostSortProperty.ownerIndex);
        this.postSortProperties.insert('Category', PostSortProperty.categoryIndex);
        this.postSortProperties.insert('Created', PostSortProperty.created);

        // Initiate post report sort properties list.
        this.postReportSortProperties = new Dictionary<PostReportSortProperty>();
        this.postReportSortProperties.insert('Index', PostReportSortProperty.id);
        this.postReportSortProperties.insert('Post', PostReportSortProperty.postIndex);
        this.postReportSortProperties.insert('Post owner', PostReportSortProperty.postOwnerIndex);
        this.postReportSortProperties.insert('Post reporter', PostReportSortProperty.postReporterIndex);
        this.postReportSortProperties.insert('Created', PostReportSortProperty.created);

        // Initiate list of comment report sort properties.
        this.commentReportSortProperties = this.initiateCommentReportSortProperties();
    }

    // Maximum number of records which can be displayed on page.
    public findMaxPageRecords(): number {
        return this.pageRecords[this.pageRecords.length - 1];
    }

    // Initiate list of account statuses selection.
    private initializeAccountSelections(): Dictionary<AccountStatuses> {
        let accountStatusItems = new Dictionary<AccountStatuses>();

        accountStatusItems.insert('Inactive', AccountStatuses.Disabled);
        accountStatusItems.insert('Pending', AccountStatuses.Pending);
        accountStatusItems.insert('Active', AccountStatuses.Active);

        return accountStatusItems;
    }

    // Initiate comment report sort properties list.
    private initiateCommentReportSortProperties(): Dictionary<CommentReportSortProperty> {

        // Initiate properties list.
        let commentReportSortProperties = new Dictionary<CommentReportSortProperty>();
        commentReportSortProperties.insert('Index', CommentReportSortProperty.Index);
        commentReportSortProperties.insert('Created', CommentReportSortProperty.Index);

        return commentReportSortProperties;
    }

    // Initiate text search modes list.
    private initiateTextSearchModes(): Dictionary<TextSearchMode> {

        // Initiate text search modes.
        let textSearchModes = new Dictionary<TextSearchMode>();
        textSearchModes.insert('Equals', TextSearchMode.equals);
        textSearchModes.insert('Equals case-insensitively', TextSearchMode.equalsIgnoreCase);
        textSearchModes.insert('Contains', TextSearchMode.contains);
        return textSearchModes;
    }
}
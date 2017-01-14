import {Injectable} from "@angular/core";
import {Dictionary} from "../viewmodels/Dictionary";
import {SortDirection} from "../enumerations/SortDirection";
import {CategorySortProperty} from "../enumerations/order/CategorySortProperty";
import {AccountSortProperty} from "../enumerations/order/AccountSortProperty";
import {Account} from "../models/Account";

@Injectable()
export class ClientConfigurationService{

    // List of page record number which can be selected on the screen.
    public pageRecords : number[];

    // Pagination settings of ng2-bootstrap.
    public ngPaginationSettings: any;

    // List of direction which can be used in records sorting.
    public sortDirections: Dictionary<SortDirection>;

    // List of properties which can be used for accounts sorting.
    public accountSortProperties: Dictionary<AccountSortProperty>;

    // List of properties which can be used for categories sorting.
    public categorySortProperties: Dictionary<CategorySortProperty>;

    public constructor(){

        // Amount of records which can be displayed on the screen.
        this.pageRecords = [5, 10, 15, 20];

        // Initate default settings of ng2-bootstrap pagination control.
        this.ngPaginationSettings = {
            rotate: true, //if true current page will in the middle of pages list
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

        // Initiate category sort properties.
        this.categorySortProperties = new Dictionary<CategorySortProperty>();
        this.categorySortProperties.insert('Index', CategorySortProperty.index);
        this.categorySortProperties.insert('Creator', CategorySortProperty.creatorIndex);
        this.categorySortProperties.insert('Name', CategorySortProperty.name);
        this.categorySortProperties.insert('Created', CategorySortProperty.created);
        this.categorySortProperties.insert('Last modified', CategorySortProperty.lastModified);
    }

    // Maximum number of records which can be displayed on page.
    public findMaxPageRecords(): number{
        return this.pageRecords[this.pageRecords.length - 1];
    }
}
"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var Dictionary_1 = require("../viewmodels/Dictionary");
var SortDirection_1 = require("../enumerations/SortDirection");
var CategorySortProperty_1 = require("../enumerations/order/CategorySortProperty");
var AccountSortProperty_1 = require("../enumerations/order/AccountSortProperty");
var AccountStatuses_1 = require("../enumerations/AccountStatuses");
var CommentReportSortProperty_1 = require("../enumerations/order/CommentReportSortProperty");
var TextSearchMode_1 = require("../enumerations/TextSearchMode");
var PostSortProperty_1 = require("../enumerations/order/PostSortProperty");
var PostReportSortProperty_1 = require("../enumerations/order/PostReportSortProperty");
var ClientConfigurationService = (function () {
    // Initiate instance of service with default settings.
    function ClientConfigurationService() {
        // Amount of records which can be displayed on the screen.
        this.pageRecords = [5, 10, 15, 20];
        // Initiate list of text search modes.
        this.textSearchModes = this.initiateTextSearchModes();
        // Initate default settings of ng2-bootstrap pagination control.
        this.ngPaginationSettings = {
            rotate: true,
            firstText: '<<',
            previousText: '<',
            nextText: '>',
            lastText: '>>'
        };
        // Initiate sort directions list.
        this.sortDirections = new Dictionary_1.Dictionary();
        this.sortDirections.insert('Ascending', SortDirection_1.SortDirection.Ascending);
        this.sortDirections.insert('Descending', SortDirection_1.SortDirection.Descending);
        // Initiate account sort properties.
        this.accountSortProperties = new Dictionary_1.Dictionary();
        this.accountSortProperties.insert('Index', AccountSortProperty_1.AccountSortProperty.index);
        this.accountSortProperties.insert('Email', AccountSortProperty_1.AccountSortProperty.email);
        this.accountSortProperties.insert('Nickname', AccountSortProperty_1.AccountSortProperty.nickname);
        this.accountSortProperties.insert('Status', AccountSortProperty_1.AccountSortProperty.status);
        this.accountSortProperties.insert('Joined', AccountSortProperty_1.AccountSortProperty.joined);
        this.accountSortProperties.insert('Last modified', AccountSortProperty_1.AccountSortProperty.lastModified);
        // Initiate list of account statuses.
        this.accountStatusSelections = this.initializeAccountSelections();
        // Initiate category sort properties.
        this.categorySortProperties = new Dictionary_1.Dictionary();
        this.categorySortProperties.insert('Index', CategorySortProperty_1.CategorySortProperty.index);
        this.categorySortProperties.insert('Creator', CategorySortProperty_1.CategorySortProperty.creatorIndex);
        this.categorySortProperties.insert('Name', CategorySortProperty_1.CategorySortProperty.name);
        this.categorySortProperties.insert('Created', CategorySortProperty_1.CategorySortProperty.created);
        this.categorySortProperties.insert('Last modified', CategorySortProperty_1.CategorySortProperty.lastModified);
        // Initiate post sort properties.
        this.postSortProperties = new Dictionary_1.Dictionary();
        this.postSortProperties.insert('Index', PostSortProperty_1.PostSortProperty.id);
        this.postSortProperties.insert('Owner', PostSortProperty_1.PostSortProperty.ownerIndex);
        this.postSortProperties.insert('Category', PostSortProperty_1.PostSortProperty.categoryIndex);
        this.postSortProperties.insert('Created', PostSortProperty_1.PostSortProperty.created);
        // Initiate post report sort properties list.
        this.postReportSortProperties = new Dictionary_1.Dictionary();
        this.postReportSortProperties.insert('Index', PostReportSortProperty_1.PostReportSortProperty.id);
        this.postReportSortProperties.insert('Post', PostReportSortProperty_1.PostReportSortProperty.postIndex);
        this.postReportSortProperties.insert('Post owner', PostReportSortProperty_1.PostReportSortProperty.postOwnerIndex);
        this.postReportSortProperties.insert('Post reporter', PostReportSortProperty_1.PostReportSortProperty.postReporterIndex);
        this.postReportSortProperties.insert('Created', PostReportSortProperty_1.PostReportSortProperty.created);
        // Initiate list of comment report sort properties.
        this.commentReportSortProperties = this.initiateCommentReportSortProperties();
    }
    // Maximum number of records which can be displayed on page.
    ClientConfigurationService.prototype.findMaxPageRecords = function () {
        return this.pageRecords[this.pageRecords.length - 1];
    };
    // Initiate list of account statuses selection.
    ClientConfigurationService.prototype.initializeAccountSelections = function () {
        var accountStatusItems = new Dictionary_1.Dictionary();
        accountStatusItems.insert('Inactive', AccountStatuses_1.AccountStatuses.Disabled);
        accountStatusItems.insert('Pending', AccountStatuses_1.AccountStatuses.Pending);
        accountStatusItems.insert('Active', AccountStatuses_1.AccountStatuses.Active);
        return accountStatusItems;
    };
    // Initiate comment report sort properties list.
    ClientConfigurationService.prototype.initiateCommentReportSortProperties = function () {
        // Initiate properties list.
        var commentReportSortProperties = new Dictionary_1.Dictionary();
        commentReportSortProperties.insert('Index', CommentReportSortProperty_1.CommentReportSortProperty.Index);
        commentReportSortProperties.insert('Created', CommentReportSortProperty_1.CommentReportSortProperty.Index);
        return commentReportSortProperties;
    };
    // Initiate text search modes list.
    ClientConfigurationService.prototype.initiateTextSearchModes = function () {
        // Initiate text search modes.
        var textSearchModes = new Dictionary_1.Dictionary();
        textSearchModes.insert('Equals', TextSearchMode_1.TextSearchMode.equals);
        textSearchModes.insert('Equals case-insensitively', TextSearchMode_1.TextSearchMode.equalsIgnoreCase);
        textSearchModes.insert('Contains', TextSearchMode_1.TextSearchMode.contains);
        return textSearchModes;
    };
    return ClientConfigurationService;
}());
ClientConfigurationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ClientConfigurationService);
exports.ClientConfigurationService = ClientConfigurationService;
//# sourceMappingURL=ClientConfigurationService.js.map
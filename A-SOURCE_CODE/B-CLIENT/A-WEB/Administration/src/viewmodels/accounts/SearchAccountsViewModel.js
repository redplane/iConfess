"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var TextSearch_1 = require("../TextSearch");
var UnixDateRange_1 = require("../UnixDateRange");
var Pagination_1 = require("../Pagination");
var Sorting_1 = require("../../models/Sorting");
var SearchAccountsViewModel = (function () {
    //#endregion
    //#region Constructor
    // Initiate find accounts view model with default settings.
    function SearchAccountsViewModel() {
        this.email = new TextSearch_1.TextSearch();
        this.nickname = new TextSearch_1.TextSearch();
        this.statuses = [];
        this.joined = new UnixDateRange_1.UnixDateRange();
        this.lastModified = new UnixDateRange_1.UnixDateRange();
        this.pagination = new Pagination_1.Pagination();
        this.sorting = new Sorting_1.Sorting();
    }
    return SearchAccountsViewModel;
}());
exports.SearchAccountsViewModel = SearchAccountsViewModel;
//# sourceMappingURL=SearchAccountsViewModel.js.map
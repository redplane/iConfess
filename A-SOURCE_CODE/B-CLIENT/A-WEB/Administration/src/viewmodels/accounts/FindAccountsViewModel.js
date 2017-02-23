"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var TextSearch_1 = require("../TextSearch");
var UnixDateRange_1 = require("../UnixDateRange");
var AccountSortProperty_1 = require("../../enumerations/order/AccountSortProperty");
var Pagination_1 = require("../Pagination");
var SortDirection_1 = require("../../enumerations/SortDirection");
var FindAccountsViewModel = (function () {
    // Initiate find accounts view model with default settings.
    function FindAccountsViewModel() {
        this.email = new TextSearch_1.TextSearch();
        this.nickname = new TextSearch_1.TextSearch();
        this.statuses = [];
        this.joined = new UnixDateRange_1.UnixDateRange();
        this.lastModified = new UnixDateRange_1.UnixDateRange();
        this.pagination = new Pagination_1.Pagination();
        this.direction = SortDirection_1.SortDirection.Ascending;
        this.sort = AccountSortProperty_1.AccountSortProperty.index;
    }
    return FindAccountsViewModel;
}());
exports.FindAccountsViewModel = FindAccountsViewModel;
//# sourceMappingURL=FindAccountsViewModel.js.map
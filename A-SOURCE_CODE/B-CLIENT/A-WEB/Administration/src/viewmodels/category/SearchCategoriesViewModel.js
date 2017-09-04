"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var UnixDateRange_1 = require("../UnixDateRange");
var Pagination_1 = require("../Pagination");
var TextSearch_1 = require("../TextSearch");
var Sorting_1 = require("../../models/Sorting");
var SearchCategoriesViewModel = (function () {
    //#endregion
    //#region Constructor
    // Initiate view model with properties.
    function SearchCategoriesViewModel() {
        this.name = new TextSearch_1.TextSearch();
        this.creatorIndex = null;
        this.created = new UnixDateRange_1.UnixDateRange();
        this.lastModified = new UnixDateRange_1.UnixDateRange();
        this.pagination = new Pagination_1.Pagination();
        this.sorting = new Sorting_1.Sorting();
    }
    return SearchCategoriesViewModel;
}());
exports.SearchCategoriesViewModel = SearchCategoriesViewModel;
//# sourceMappingURL=SearchCategoriesViewModel.js.map
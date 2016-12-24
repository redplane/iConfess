"use strict";
var UnixDateRange_1 = require("../UnixDateRange");
var Pagination_1 = require("../Pagination");
var SortDirection_1 = require("../../enumerations/SortDirection");
var CategorySortProperty_1 = require("../../enumerations/order/CategorySortProperty");
var TextSearch_1 = require("../TextSearch");
var FindCategoriesViewModel = (function () {
    // Initiate view model with properties.
    function FindCategoriesViewModel() {
        this.name = new TextSearch_1.TextSearch();
        this.creatorIndex = null;
        this.created = new UnixDateRange_1.UnixDateRange();
        this.lastModified = new UnixDateRange_1.UnixDateRange();
        this.pagination = new Pagination_1.Pagination();
        this.direction = SortDirection_1.SortDirection.Ascending;
        this.sort = CategorySortProperty_1.CategorySortProperty.index;
    }
    return FindCategoriesViewModel;
}());
exports.FindCategoriesViewModel = FindCategoriesViewModel;
//# sourceMappingURL=FindCategoriesViewModel.js.map
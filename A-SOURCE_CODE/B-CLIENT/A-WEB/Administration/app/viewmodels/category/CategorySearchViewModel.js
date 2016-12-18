"use strict";
var UnixDateRange_1 = require("../UnixDateRange");
var Pagination_1 = require("../Pagination");
var SortDirection_1 = require("../../enumerations/SortDirection");
var CategorySortProperty_1 = require("../../enumerations/CategorySortProperty");
var CategorySearchViewModel = (function () {
    // Initiate view model with properties.
    function CategorySearchViewModel() {
        this.name = null;
        this.creatorIndex = null;
        this.created = new UnixDateRange_1.UnixDateRange();
        this.lastModified = new UnixDateRange_1.UnixDateRange();
        this.pagination = new Pagination_1.Pagination();
        this.direction = SortDirection_1.SortDirection.Ascending;
        this.sort = CategorySortProperty_1.CategorySortProperty.index;
    }
    return CategorySearchViewModel;
}());
exports.CategorySearchViewModel = CategorySearchViewModel;
//# sourceMappingURL=CategorySearchViewModel.js.map
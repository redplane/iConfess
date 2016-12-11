"use strict";
var UnixDateRange_1 = require("../UnixDateRange");
var CategorySearchViewModel = (function () {
    // Initiate view model with properties.
    function CategorySearchViewModel() {
        this.name = null;
        this.creatorIndex = null;
        this.created = new UnixDateRange_1.UnixDateRange();
        this.lastModified = new UnixDateRange_1.UnixDateRange();
    }
    return CategorySearchViewModel;
}());
exports.CategorySearchViewModel = CategorySearchViewModel;
//# sourceMappingURL=CategorySearchViewModel.js.map
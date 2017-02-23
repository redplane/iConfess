"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var TextSearchMode_1 = require("../enumerations/TextSearchMode");
/*
* Structure of text searching parameter.
* */
var TextSearch = (function () {
    // Initiate text searching parameter.
    function TextSearch() {
        this.value = null;
        this.mode = TextSearchMode_1.TextSearchMode.contains;
    }
    return TextSearch;
}());
exports.TextSearch = TextSearch;
//# sourceMappingURL=TextSearch.js.map
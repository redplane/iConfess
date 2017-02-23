"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Pagination_1 = require("../Pagination");
var TextSearch_1 = require("../TextSearch");
var CommentReportSortProperty_1 = require("../../enumerations/order/CommentReportSortProperty");
var SortDirection_1 = require("../../enumerations/SortDirection");
var TextSearchMode_1 = require("../../enumerations/TextSearchMode");
var FindCommentReportsViewModel = (function () {
    // Initiate filter with default settings.
    function FindCommentReportsViewModel() {
        var commentBody = new TextSearch_1.TextSearch();
        commentBody.mode = TextSearchMode_1.TextSearchMode.contains;
        commentBody.value = '';
        this.body = commentBody;
        var commentReportReason = new TextSearch_1.TextSearch();
        commentReportReason.mode = TextSearchMode_1.TextSearchMode.contains;
        commentReportReason.value = '';
        this.reason = commentReportReason;
        this.sort = CommentReportSortProperty_1.CommentReportSortProperty.Index;
        this.direction = SortDirection_1.SortDirection.Ascending;
        this.pagination = new Pagination_1.Pagination();
    }
    return FindCommentReportsViewModel;
}());
exports.FindCommentReportsViewModel = FindCommentReportsViewModel;
//# sourceMappingURL=FindCommentReportsViewModel.js.map
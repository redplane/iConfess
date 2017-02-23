"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
/*
* Contains common business calculation.
* */
var AccountStatuses_1 = require("../enumerations/AccountStatuses");
var ClientCommonService = (function () {
    function ClientCommonService() {
    }
    // Find the start record index by calculating page index and page records.
    ClientCommonService.prototype.findRecordStartIndex = function (pageIndex, pageRecords, subtractIndex) {
        if (subtractIndex)
            pageIndex -= 1;
        return (pageIndex * pageRecords);
    };
    // Find the start index of record by calculating pagination instance.
    ClientCommonService.prototype.findRecordStartIndexFromPagination = function (pagination, subtractIndex) {
        // Start from 0.
        if (pagination == null)
            return 0;
        // Page index calculation.
        var pageIndex = pagination.index;
        if (subtractIndex)
            pageIndex--;
        // Start index calculation.
        var startIndex = pageIndex * pagination.records;
        return startIndex;
    };
    // Find account display from enumeration.
    ClientCommonService.prototype.findAccountStatusDisplay = function (accountStatus) {
        switch (accountStatus) {
            case AccountStatuses_1.AccountStatuses.Active:
                return 'Active';
            case AccountStatuses_1.AccountStatuses.Disabled:
                return 'Disabled';
            case AccountStatuses_1.AccountStatuses.Pending:
                return 'Pending';
            default:
                return 'N/A';
        }
    };
    return ClientCommonService;
}());
exports.ClientCommonService = ClientCommonService;
//# sourceMappingURL=ClientCommonService.js.map
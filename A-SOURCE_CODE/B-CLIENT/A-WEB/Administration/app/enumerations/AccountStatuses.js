"use strict";
/*
* Collection of statuses that account can be.
* */
(function (AccountStatuses) {
    AccountStatuses[AccountStatuses["Disabled"] = 0] = "Disabled";
    AccountStatuses[AccountStatuses["Pending"] = 1] = "Pending";
    AccountStatuses[AccountStatuses["Active"] = 2] = "Active";
})(exports.AccountStatuses || (exports.AccountStatuses = {}));
var AccountStatuses = exports.AccountStatuses;
//# sourceMappingURL=AccountStatuses.js.map
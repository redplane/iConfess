"use strict";
var TimeService = (function () {
    function TimeService() {
    }
    // Convert number to unix time.
    TimeService.prototype.findUnixTime = function (milliseconds) {
        // Initiate date and set its millisecs.
        var date = new Date();
        date.setTime(milliseconds);
        return date;
    };
    return TimeService;
}());
exports.TimeService = TimeService;
//# sourceMappingURL=TimeService.js.map
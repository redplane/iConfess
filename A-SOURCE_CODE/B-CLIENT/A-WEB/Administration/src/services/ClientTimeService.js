"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
/*
* Client time service which handles time calculation.
* */
var ClientTimeService = (function () {
    function ClientTimeService() {
    }
    // Convert number to unix time.
    ClientTimeService.prototype.getUtc = function (milliseconds) {
        // Initiate date and set its millisecs.
        var date = new Date();
        date.setTime(milliseconds);
        return date;
    };
    return ClientTimeService;
}());
exports.ClientTimeService = ClientTimeService;
//# sourceMappingURL=ClientTimeService.js.map
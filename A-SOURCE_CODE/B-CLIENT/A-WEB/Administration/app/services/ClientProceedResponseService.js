"use strict";
/*
* Class which is used for analyze response sent back from server.
* */
var ClientProceedResponseService = (function () {
    function ClientProceedResponseService() {
    }
    ClientProceedResponseService.prototype.proceedInvalidResponse = function (response) {
        // Response is invalid.
        if (response == null || response['status'] == null)
            return;
        switch (response.status) {
            case 401:
                break;
        }
    };
    return ClientProceedResponseService;
}());
exports.ClientProceedResponseService = ClientProceedResponseService;
//# sourceMappingURL=ClientProceedResponseService.js.map
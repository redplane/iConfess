"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ToastrPosition_1 = require("../enumerations/ToastrPosition");
/*
 * Configuration of toast.
 * */
var ToastOptions = (function () {
    //#endregion
    //#region Constructor
    // Intiate toast option with default settings.
    function ToastOptions() {
        this.closeButton = true;
        this.debug = false;
        this.newestOnTop = true;
        this.progressBar = false;
        this.preventDuplicates = true;
        this.showDuration = 300;
        this.hideDuration = 1000;
        this.timeOut = 0;
        this.extendedTimeOut = 0;
        this.showEasing = 'swing';
        this.hideEasing = 'linear';
        this.showMethod = 'fadeIn';
        this.hideMethod = 'fadeOut';
        this.tapToDismiss = true;
        this.position = ToastrPosition_1.ToastPosition.topRight;
    }
    return ToastOptions;
}());
exports.ToastOptions = ToastOptions;
//# sourceMappingURL=ToastrOption.js.map
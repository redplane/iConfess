"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
/*
* Position of toast notification.
* */
var ToastPosition;
(function (ToastPosition) {
    ToastPosition[ToastPosition["topRight"] = 0] = "topRight";
    ToastPosition[ToastPosition["topLeft"] = 1] = "topLeft";
    ToastPosition[ToastPosition["bottomRight"] = 2] = "bottomRight";
    ToastPosition[ToastPosition["bottomLeft"] = 3] = "bottomLeft";
    ToastPosition[ToastPosition["topFullWidth"] = 4] = "topFullWidth";
    ToastPosition[ToastPosition["bottomFullWidth"] = 5] = "bottomFullWidth";
    ToastPosition[ToastPosition["topCenter"] = 6] = "topCenter";
    ToastPosition[ToastPosition["bottomCenter"] = 7] = "bottomCenter";
})(ToastPosition = exports.ToastPosition || (exports.ToastPosition = {}));
/*
* Configuration of toast.
* */
var ToastOptions = (function () {
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
        this.position = ToastPosition.topRight;
    }
    return ToastOptions;
}());
exports.ToastOptions = ToastOptions;
/*
* Service which handles business of toast notification on page.
* */
var ClientNotificationService = (function () {
    // Initiate toast notification service.
    function ClientNotificationService() {
        this._toastOptions = new ToastOptions();
    }
    // Display success message.
    ClientNotificationService.prototype.success = function (message, title, toastOption) {
        if (title === void 0) { title = ''; }
        if (toastOption === void 0) { toastOption = null; }
        return this.show(message, title, toastOption, 'success');
    };
    // Display info message.
    ClientNotificationService.prototype.info = function (message, title, toastOption) {
        if (title === void 0) { title = ''; }
        if (toastOption === void 0) { toastOption = null; }
        return this.show(message, title, toastOption, 'info');
    };
    // Display warning message.
    ClientNotificationService.prototype.warning = function (message, title, toastOption) {
        if (title === void 0) { title = ''; }
        if (toastOption === void 0) { toastOption = null; }
        return this.show(message, title, toastOption, 'warning');
    };
    // Display error message.
    ClientNotificationService.prototype.error = function (message, title, toastOption) {
        if (title === void 0) { title = ''; }
        if (toastOption === void 0) { toastOption = null; }
        return this.show(message, title, toastOption, 'error');
    };
    // Find default toast configuration.
    ClientNotificationService.prototype.getToastConfiguration = function () {
        return this._toastOptions;
    };
    // Display toast notification
    ClientNotificationService.prototype.show = function (message, title, toastOption, type) {
        var options = this._toastOptions;
        if (toastOption != null)
            options = toastOption;
        options['positionClass'] = this.getToastPosition(options.position);
        return toastr[type](message, title);
    };
    // Find toast notification display position.
    ClientNotificationService.prototype.getToastPosition = function (position) {
        switch (position) {
            case ToastPosition.bottomRight:
                return 'toast-bottom-right';
            case ToastPosition.bottomLeft:
                return 'toast-bottom-left';
            case ToastPosition.topLeft:
                return 'toast-top-left';
            case ToastPosition.topLeft:
                return 'toast-top-full-width';
            case ToastPosition.topFullWidth:
                return 'toast-top-full-width';
            case ToastPosition.bottomFullWidth:
                return 'toast-bottom-full-width';
            case ToastPosition.topCenter:
                return 'toast-top-center';
            case ToastPosition.bottomCenter:
                return 'toast-bottom-center';
            default:
                return 'toast-top-right';
        }
    };
    return ClientNotificationService;
}());
ClientNotificationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ClientNotificationService);
exports.ClientNotificationService = ClientNotificationService;
//# sourceMappingURL=ClientNotificationService.js.map
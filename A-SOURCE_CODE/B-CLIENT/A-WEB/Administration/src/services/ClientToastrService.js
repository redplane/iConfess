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
var ToastrPosition_1 = require("../enumerations/ToastrPosition");
var ToastrOption_1 = require("../models/ToastrOption");
/*
* Service which handles business of toast notification on page.
* */
var ClientToastrService = (function () {
    //#endregion
    //#region Constructor
    // Initiate toast notification service.
    function ClientToastrService() {
        this.options = new ToastrOption_1.ToastOptions();
    }
    //#endregion
    //#region Methods
    // Display success message.
    ClientToastrService.prototype.success = function (message, title, toastOption) {
        if (title === void 0) { title = ''; }
        if (toastOption === void 0) { toastOption = null; }
        return this.show(message, title, toastOption, 'success');
    };
    // Display info message.
    ClientToastrService.prototype.info = function (message, title, toastOption) {
        if (title === void 0) { title = ''; }
        if (toastOption === void 0) { toastOption = null; }
        return this.show(message, title, toastOption, 'info');
    };
    // Display warning message.
    ClientToastrService.prototype.warning = function (message, title, toastOption) {
        if (title === void 0) { title = ''; }
        if (toastOption === void 0) { toastOption = null; }
        return this.show(message, title, toastOption, 'warning');
    };
    // Display error message.
    ClientToastrService.prototype.error = function (message, title, toastOption) {
        if (title === void 0) { title = ''; }
        if (toastOption === void 0) { toastOption = null; }
        return this.show(message, title, toastOption, 'error');
    };
    // Find default toast configuration.
    ClientToastrService.prototype.getToastConfiguration = function () {
        return this.options;
    };
    // Display toast notification
    ClientToastrService.prototype.show = function (message, title, toastOption, type) {
        var options = this.options;
        if (toastOption != null)
            options = toastOption;
        options['positionClass'] = this.getToastPosition(options.position);
        toastr[type](message, title);
    };
    // Find toast notification display position.
    ClientToastrService.prototype.getToastPosition = function (position) {
        switch (position) {
            case ToastrPosition_1.ToastPosition.bottomRight:
                return 'toast-bottom-right';
            case ToastrPosition_1.ToastPosition.bottomLeft:
                return 'toast-bottom-left';
            case ToastrPosition_1.ToastPosition.topLeft:
                return 'toast-top-left';
            case ToastrPosition_1.ToastPosition.topLeft:
                return 'toast-top-full-width';
            case ToastrPosition_1.ToastPosition.topFullWidth:
                return 'toast-top-full-width';
            case ToastrPosition_1.ToastPosition.bottomFullWidth:
                return 'toast-bottom-full-width';
            case ToastrPosition_1.ToastPosition.topCenter:
                return 'toast-top-center';
            case ToastrPosition_1.ToastPosition.bottomCenter:
                return 'toast-bottom-center';
            default:
                return 'toast-top-right';
        }
    };
    return ClientToastrService;
}());
ClientToastrService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ClientToastrService);
exports.ClientToastrService = ClientToastrService;
//# sourceMappingURL=ClientToastrService.js.map
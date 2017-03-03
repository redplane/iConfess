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
* Contains data constraint of application.
* */
var ClientDataConstraintService = (function () {
    function ClientDataConstraintService() {
        // Category section.
        this.minLengthCategoryName = 6;
        this.maxLengthCategoryName = 64;
        // Account section.
        this.minLengthEmail = 1;
        this.maxLengthEmail = 128;
        this.patternEmail = new RegExp(/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/);
        this.patternAccountPassword = new RegExp(/^[a-zA-Z0-9_!@#$%^&*()]*$/);
        /* Token section */
        this.patternTokenCode = new RegExp(/^[0-9a-f]{32}$/);
    }
    return ClientDataConstraintService;
}());
ClientDataConstraintService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], ClientDataConstraintService);
exports.ClientDataConstraintService = ClientDataConstraintService;
//# sourceMappingURL=ClientDataConstraintService.js.map
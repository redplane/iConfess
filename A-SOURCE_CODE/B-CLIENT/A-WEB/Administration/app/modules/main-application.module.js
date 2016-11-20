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
var core_1 = require('@angular/core');
var platform_browser_1 = require('@angular/platform-browser');
var main_application_component_1 = require('../components/main-application.component');
var navigation_bar_component_1 = require("../components/navigation-bar.component");
var sidebar_component_1 = require("../components/sidebar.component");
var account_management_component_1 = require("../components/account-management.component");
var category_management_component_1 = require("../components/category-management.component");
var MainApplicationModule = (function () {
    function MainApplicationModule() {
    }
    MainApplicationModule = __decorate([
        core_1.NgModule({
            imports: [platform_browser_1.BrowserModule],
            declarations: [
                main_application_component_1.MainApplicationComponent,
                navigation_bar_component_1.NavigationBarComponent,
                sidebar_component_1.SidebarComponent,
                account_management_component_1.AccountManagementComponent,
                category_management_component_1.CategoryManagementComponent
            ],
            bootstrap: [main_application_component_1.MainApplicationComponent]
        }), 
        __metadata('design:paramtypes', [])
    ], MainApplicationModule);
    return MainApplicationModule;
}());
exports.MainApplicationModule = MainApplicationModule;
//# sourceMappingURL=main-application.module.js.map
"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var main_application_component_1 = require("../components/main-application.component");
var navigation_bar_component_1 = require("../components/navigation-bar.component");
var sidebar_component_1 = require("../components/sidebar.component");
var account_management_component_1 = require("../components/account-management.component");
var category_management_component_1 = require("../components/category-management.component");
var category_detail_box_component_1 = require("../components/content/category/category-detail-box.component");
var category_initiate_box_component_1 = require("../components/content/category/category-initiate-box.component");
var forms_1 = require("@angular/forms");
var angular2_select_1 = require("angular2-select");
var category_find_box_component_1 = require("../components/content/category/category-find-box.component");
var http_1 = require("@angular/http");
var ng2_bootstrap_1 = require("ng2-bootstrap/ng2-bootstrap");
var router_1 = require("@angular/router");
var login_component_1 = require("../components/login.component");
var account_detail_box_component_1 = require("../components/content/account/account-detail-box.component");
var account_find_box_component_1 = require("../components/content/account/account-find-box.component");
// Routing configuration.
var appRoutes = [
    {
        path: '',
        component: login_component_1.LoginComponent
    },
    {
        path: 'account-management',
        component: account_management_component_1.AccountManagementComponent,
    },
    {
        path: 'category-management',
        component: category_management_component_1.CategoryManagementComponent
    }
];
var MainApplicationModule = (function () {
    function MainApplicationModule() {
    }
    return MainApplicationModule;
}());
MainApplicationModule = __decorate([
    core_1.NgModule({
        imports: [
            platform_browser_1.BrowserModule,
            forms_1.FormsModule,
            forms_1.ReactiveFormsModule,
            angular2_select_1.SelectModule,
            http_1.HttpModule,
            http_1.JsonpModule,
            ng2_bootstrap_1.ModalModule,
            ng2_bootstrap_1.PaginationModule,
            ng2_bootstrap_1.TypeaheadModule,
            // Initiate application routing configuration.
            router_1.RouterModule.forRoot(appRoutes)
        ],
        declarations: [
            main_application_component_1.MainApplicationComponent,
            navigation_bar_component_1.NavigationBarComponent,
            sidebar_component_1.SidebarComponent,
            login_component_1.LoginComponent,
            account_management_component_1.AccountManagementComponent,
            account_detail_box_component_1.AccountDetailBoxComponent,
            account_find_box_component_1.AccountFindBoxComponent,
            category_management_component_1.CategoryManagementComponent,
            category_detail_box_component_1.CategoryDetailBoxComponent,
            category_find_box_component_1.CategoryFindBoxComponent,
            category_initiate_box_component_1.CategoryInitiateBoxComponent
        ],
        bootstrap: [
            main_application_component_1.MainApplicationComponent
        ]
    })
], MainApplicationModule);
exports.MainApplicationModule = MainApplicationModule;
//# sourceMappingURL=main-application.module.js.map
"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var main_application_component_1 = require("../components/main-application.component");
var navigation_bar_component_1 = require("../components/shared/navigation-bar.component");
var sidebar_component_1 = require("../components/shared/sidebar.component");
var account_management_component_1 = require("../components/account-management/account-management.component");
var category_management_component_1 = require("../components/category-management/category-management.component");
var category_detail_box_component_1 = require("../components/category-management/category-detail-box.component");
var category_initiate_box_component_1 = require("../components/category-management/category-initiate-box.component");
var account_forgot_password_component_1 = require("../components/account-management/account-forgot-password.component");
var forms_1 = require("@angular/forms");
var category_find_box_component_1 = require("../components/category-management/category-find-box.component");
var http_1 = require("@angular/http");
var ng2_bootstrap_1 = require("ng2-bootstrap");
var router_1 = require("@angular/router");
var account_find_box_component_1 = require("../components/account-management/account-find-box.component");
var comment_report_management_component_1 = require("../components/comment-report-management/comment-report-management.component");
var comment_report_find_box_component_1 = require("../components/comment-report-management/comment-report-find-box.component");
var account_login_component_1 = require("../components/account-management/account-login.component");
var account_submit_password_component_1 = require("../components/account-management/account-submit-password.component");
var TextPropertyComparisionValidator_1 = require("../validators/TextPropertyComparisionValidator");
var post_report_management_component_1 = require("../components/post-report-management/post-report-management.component");
var post_report_find_box_component_1 = require("../components/post-report-management/post-report-find-box.component");
var ng2_select_1 = require("ng2-select");
var angular2_moment_1 = require("angular2-moment");
// Routing configuration.
var appRoutes = [
    {
        path: '',
        component: account_login_component_1.AccountLoginComponent
    },
    {
        path: 'account-management',
        component: account_management_component_1.AccountManagementComponent,
    },
    {
        path: 'category-management',
        component: category_management_component_1.CategoryManagementComponent
    },
    {
        path: 'comment-report-management',
        component: comment_report_management_component_1.CommentReportManagementComponent
    },
    {
        path: 'post-report-management',
        component: post_report_management_component_1.PostReportManagementComponent
    },
    {
        path: 'forgot-password',
        component: account_forgot_password_component_1.AccountForgotPasswordComponent
    },
    {
        path: 'submit-password',
        component: account_submit_password_component_1.AccountSubmitPasswordComponent
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
            http_1.HttpModule,
            http_1.JsonpModule,
            ng2_bootstrap_1.ModalModule.forRoot(),
            ng2_bootstrap_1.PaginationModule.forRoot(),
            ng2_bootstrap_1.TypeaheadModule.forRoot(),
            ng2_select_1.SelectModule,
            // Moment module.
            angular2_moment_1.MomentModule,
            // Initiate application routing configuration.
            router_1.RouterModule.forRoot(appRoutes)
        ],
        declarations: [
            main_application_component_1.MainApplicationComponent,
            navigation_bar_component_1.NavigationBarComponent,
            sidebar_component_1.SidebarComponent,
            account_management_component_1.AccountManagementComponent,
            account_find_box_component_1.AccountFindBoxComponent,
            account_forgot_password_component_1.AccountForgotPasswordComponent,
            account_login_component_1.AccountLoginComponent,
            account_submit_password_component_1.AccountSubmitPasswordComponent,
            category_management_component_1.CategoryManagementComponent,
            category_detail_box_component_1.CategoryDetailBoxComponent,
            category_find_box_component_1.CategoryFindBoxComponent,
            category_initiate_box_component_1.CategoryInitiateBoxComponent,
            post_report_management_component_1.PostReportManagementComponent,
            post_report_find_box_component_1.PostReportFindBoxComponent,
            comment_report_management_component_1.CommentReportManagementComponent,
            comment_report_find_box_component_1.CommentReportFindBoxComponent,
            TextPropertyComparisionValidator_1.TextPropertyComparisionValidator
        ],
        bootstrap: [
            main_application_component_1.MainApplicationComponent
        ]
    })
], MainApplicationModule);
exports.MainApplicationModule = MainApplicationModule;
//# sourceMappingURL=main-application.module.js.map
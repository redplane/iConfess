import {NgModule}      from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {MainApplicationComponent}   from '../components/main-application.component';
import {NavigationBarComponent} from "../components/views/shared/navigation-bar.component";
import {SidebarComponent} from "../components/views/shared/sidebar.component";
import {AccountManagementComponent} from "../components/views/account-management/account-management.component";
import {CategoryManagementComponent} from "../components/views/category-management/category-management.component";
import {CategoryDetailBoxComponent} from '../components/content/category/category-detail-box.component';
import {CategoryInitiateBoxComponent} from '../components/content/category/category-initiate-box.component';
import {AccountForgotPasswordBoxComponent} from '../components/content/account/account-forgot-password-box';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CategoryFindBoxComponent} from "../components/content/category/category-find-box.component";
import {HttpModule, JsonpModule} from '@angular/http';
import {ModalModule, PaginationModule, TypeaheadModule} from 'ng2-bootstrap/ng2-bootstrap';
import {RouterModule, Routes} from '@angular/router';
import {AccountLoginBoxComponent} from "../components/content/account/account-login-box.component";
import {AccountFindBoxComponent} from "../components/content/account/account-find-box.component";
import {SelectModule} from 'ng2-select';
import {CommentReportManagementComponent} from "../components/views/comment-report-management/comment-report-management.component";
import {CommentReportFindBoxComponent} from "../components/content/comment-report/comment-report-find-box.component";
import {AccountLoginComponent} from "../components/views/account-management/account-login.component";
import {MomentModule} from "angular2-moment";
import {AccountForgotPasswordComponent} from "../components/views/account-management/account-forgot-password.component";
import {AccountSubmitPasswordComponent} from "../components/views/account-management/account-submit-password.component";
import {TextPropertyComparisionValidator} from "../validators/TextPropertyComparisionValidator";

// Routing configuration.
const appRoutes: Routes = [
    {
        path: '',
        component: AccountLoginComponent
    },
    {
        path: 'account-management',
        component: AccountManagementComponent,

    },
    {
        path: 'category-management',
        component: CategoryManagementComponent
    },
    {
        path: 'comment-report-management',
        component: CommentReportManagementComponent
    },
    {
        path: 'forgot-password',
        component: AccountForgotPasswordComponent
    },
    {
        path: 'submit-password',
        component: AccountSubmitPasswordComponent
    }
];


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        JsonpModule,

        ModalModule.forRoot(),
        PaginationModule.forRoot(),
        TypeaheadModule.forRoot(),
        SelectModule,

        // Moment module.
        MomentModule,

        // Initiate application routing configuration.
        RouterModule.forRoot(appRoutes)
    ],
    declarations: [
        MainApplicationComponent,
        NavigationBarComponent,
        SidebarComponent,

        AccountManagementComponent,
        AccountFindBoxComponent,
        AccountLoginBoxComponent,
        AccountForgotPasswordBoxComponent,
        AccountLoginComponent,
        AccountForgotPasswordComponent,
        AccountSubmitPasswordComponent,

        CategoryManagementComponent,
        CategoryDetailBoxComponent,
        CategoryFindBoxComponent,
        CategoryInitiateBoxComponent,

        CommentReportManagementComponent,
        CommentReportFindBoxComponent,

        TextPropertyComparisionValidator

    ],
    bootstrap: [
        MainApplicationComponent
    ]
})
export class MainApplicationModule {
}

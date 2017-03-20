import {NgModule}      from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {MainApplicationComponent}   from '../components/main-application.component';
import {NavigationBarComponent} from "../components/shared/navigation-bar.component";
import {SidebarComponent} from "../components/shared/sidebar.component";
import {AccountManagementComponent} from "../components/account-management/account-management.component";
import {CategoryManagementComponent} from "../components/category-management/category-management.component";
import {CategoryDetailBoxComponent} from '../components/category-management/category-detail-box.component';
import {CategoryInitiateBoxComponent} from '../components/category-management/category-initiate-box.component';
import {AccountForgotPasswordComponent} from '../components/account-management/account-forgot-password.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CategoryFindBoxComponent} from "../components/category-management/category-find-box.component";
import {HttpModule, JsonpModule} from '@angular/http';
import {ModalModule, PaginationModule, TypeaheadModule} from 'ng2-bootstrap';
import {RouterModule, Routes} from '@angular/router';
import {AccountFindBoxComponent} from "../components/account-management/account-find-box.component";
import {CommentReportManagementComponent} from "../components/comment-report-management/comment-report-management.component";
import {CommentReportFindBoxComponent} from "../components/comment-report-management/comment-report-find-box.component";
import {AccountLoginComponent} from "../components/account-management/account-login.component";
import {AccountSubmitPasswordComponent} from "../components/account-management/account-submit-password.component";
import {TextPropertyComparisionValidator} from "../validators/TextPropertyComparisionValidator";
import {PostReportManagementComponent} from "../components/post-report-management/post-report-management.component";
import {PostReportFindBoxComponent} from "../components/post-report-management/post-report-find-box.component";
import {SelectModule} from "ng2-select";
import {MomentModule} from "angular2-moment";
import {AccountProfileBoxComponent} from "../components/account-management/account-profile-box.component";
import {PostDetailBoxComponent} from "../components/post-management/post-detail-box.component";
import {ChartsModule} from "ng2-charts";

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
        path: 'post-report-management',
        component: PostReportManagementComponent
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

        // Chart module.
        ChartsModule,

        // Initiate application routing configuration.
        RouterModule.forRoot(appRoutes)
    ],
    declarations: [
        MainApplicationComponent,
        NavigationBarComponent,
        SidebarComponent,

        AccountManagementComponent,
        AccountFindBoxComponent,
        AccountForgotPasswordComponent,
        AccountLoginComponent,
        AccountSubmitPasswordComponent,
        AccountProfileBoxComponent,

        CategoryManagementComponent,
        CategoryDetailBoxComponent,
        CategoryFindBoxComponent,
        CategoryInitiateBoxComponent,

        PostReportManagementComponent,
        PostReportFindBoxComponent,

        PostDetailBoxComponent,

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

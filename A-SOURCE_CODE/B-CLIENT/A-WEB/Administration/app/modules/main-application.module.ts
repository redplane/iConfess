import {NgModule}      from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {MainApplicationComponent}   from '../components/main-application.component';
import {NavigationBarComponent} from "../components/navigation-bar.component";
import {SidebarComponent} from "../components/sidebar.component";
import {AccountManagementComponent} from "../components/account-management.component";
import {CategoryManagementComponent} from "../components/category-management.component";
import {CategoryDetailBoxComponent} from '../components/content/category/category-detail-box.component';
import {CategoryInitiateBoxComponent} from '../components/content/category/category-initiate-box.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CategoryFindBoxComponent} from "../components/content/category/category-find-box.component";
import {HttpModule, JsonpModule} from '@angular/http';
import {ModalModule, PaginationModule, TypeaheadModule} from 'ng2-bootstrap/ng2-bootstrap';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from "../components/login.component";
import {AccountDetailBoxComponent} from "../components/content/account/account-detail-box.component";
import {AccountFindBoxComponent} from "../components/content/account/account-find-box.component";
import {SelectModule} from 'ng2-select';import {CommentReportManagementComponent} from "../components/comment-report-management.component";
import {CommentReportFindBoxComponent} from "../components/content/comment-report/comment-report-find-box.component";

// Routing configuration.
const appRoutes: Routes = [
    {
        path: '',
        component: LoginComponent
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

        // Initiate application routing configuration.
        RouterModule.forRoot(appRoutes)
    ],
    declarations: [
        MainApplicationComponent,
        NavigationBarComponent,
        SidebarComponent,

        LoginComponent,

        AccountManagementComponent,
        AccountDetailBoxComponent,
        AccountFindBoxComponent,

        CategoryManagementComponent,
        CategoryDetailBoxComponent,
        CategoryFindBoxComponent,
        CategoryInitiateBoxComponent,

        CommentReportManagementComponent,
        CommentReportFindBoxComponent

    ],
    bootstrap: [
        MainApplicationComponent
    ]
})
export class MainApplicationModule {
}

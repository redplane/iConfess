import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {RouterModule, Routes} from "@angular/router";
import {AccountLoginComponent} from "./components/account-management/account-login.component";
import {IsAuthorizedGuard} from "../guards/is-authorized-guard";
import {AccountManagementComponent} from "./components/account-management/account-management.component";
import {AccountService} from "../services/api/account.service";
import {CategoryService} from "../services/api/category.service";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import {Http, HttpModule} from "@angular/http";
import {GlobalHttpInterceptor} from "../interceptors/global-http-interceptor";
import {FormsModule} from "@angular/forms";
import {AccountSearchBoxComponent} from "./components/account-management/account-search-box.component";
import {MomentModule} from "angular2-moment";
import {NgxOrdinaryPagerModule} from 'ngx-numeric-paginator';
import {ModalModule} from "ngx-bootstrap";
import {AccountProfileBoxComponent} from "./components/account-management/account-profile-box.component";
import {NgxMultiSelectorModule} from 'ngx-multi-selector';
import {AuthenticationService} from "../services/authentication.service";
import {AuthorizeLayoutComponent} from "./components/shared/authorize-layout/authorize-layout.component";
import {ApiService} from "../services/api.service";
import {ITimeService} from "../interfaces/services/time-service.interface";
import {TimeService} from "../services/time.service";
import {NavigationBarComponent} from "./components/shared/navigation-bar/navigation-bar.component";
import {SideBarComponent} from "./components/shared/side-bar/side-bar.component";
import {ConfigurationService} from "../services/configuration.service";
import {CalendarModule, DataTableModule,SharedModule} from "primeng/primeng";
import {CategorySearchBoxComponent} from "./components/category-management/category-search-box.component";
import {CategoryManagementComponent} from "./components/category-management/category-management.component";
import {CategoryDetailBoxComponent} from "./components/category-management/category-detail-box.component";
import {CategoryInitiateBoxComponent} from "./components/category-management/category-initiate-box.component";
import {Category} from "../models/entities/Category";
import {CategoryDeleteBoxComponent} from "./components/category-management/category-delete-box.component";
import {ProfileResolve} from "../resolvers/profile.resolve";
import {AccountForgotPasswordComponent} from "./components/account-management/account-forgot-password.component";
import {ConstraintService} from "../services/constraint.service";
import {AccountSubmitPasswordComponent} from "./components/account-management/account-submit-password.component";

//#region Route configuration

// Config application routes.
const appRoutes: Routes = [
  {
    path: 'account',
    component: AuthorizeLayoutComponent,
    canActivate: [IsAuthorizedGuard],
    resolve:{
      profile: ProfileResolve
    },
    children: [
      {
        path: 'management',
        component: AccountManagementComponent,
        pathMatch: 'full'
      },
      {
        path: '',
        redirectTo: 'management',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: 'category',
    component: AuthorizeLayoutComponent,
    canActivate: [IsAuthorizedGuard],
    resolve:{
      profile: ProfileResolve
    },
    children: [
      {
        path: 'management',
        component: CategoryManagementComponent,
        pathMatch: 'full'
      },
      {
        path: '',
        redirectTo: 'management',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'account/management'
  },
  {
    path: 'login',
    component: AccountLoginComponent,
    pathMatch: 'full'
  },
  {
    path: 'forgot-password',
    component: AccountForgotPasswordComponent,
    pathMatch: 'full'
  },
  {
    path: 'submit-password',
    component: AccountSubmitPasswordComponent,
    pathMatch: 'full'
  }
];

//#endregion

@NgModule({
  declarations: [
    AppComponent,

    // Layout
    AuthorizeLayoutComponent,

    // Shared components
    NavigationBarComponent,
    SideBarComponent,

    AccountLoginComponent,
    AccountForgotPasswordComponent,
    AccountSubmitPasswordComponent,
    AccountManagementComponent,
    AccountSearchBoxComponent,
    AccountProfileBoxComponent,

    CategorySearchBoxComponent,
    CategoryManagementComponent,
    CategoryDetailBoxComponent,
    CategoryInitiateBoxComponent,
    CategoryDeleteBoxComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    HttpModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added

    // Import router configuration.
    RouterModule.forRoot(appRoutes),

    MomentModule,
    ModalModule.forRoot(),
    CalendarModule,
    DataTableModule,
    SharedModule,
    NgxOrdinaryPagerModule,
    NgxMultiSelectorModule
  ],
  providers: [
    IsAuthorizedGuard,
    {provide: 'IApiService', useClass: ApiService},
    {provide: 'IAccountService', useClass: AccountService},
    {provide: 'ICategoryService', useClass: CategoryService},
    {provide: 'IAuthenticationService', useClass: AuthenticationService},
    {provide: 'ITimeService', useClass: TimeService},
    {provide: 'IConfigurationService', useClass: ConfigurationService},
    ConstraintService,

    // Handle common behaviour of http request / response.
    {provide: Http, useClass: GlobalHttpInterceptor},

    // Resolvers.
    ProfileResolve
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

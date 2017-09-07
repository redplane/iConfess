import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {RouterModule, Routes} from "@angular/router";
import {AccountLoginComponent} from "./components/account-management/account-login.component";
import {IsAuthorizedGuard} from "../guards/is-authorized-guard";
import {AccountManagementComponent} from "./components/account-management/account-management.component";
import {ApiService} from "../services/client-api.service";
import {AccountService} from "../services/api/account.service";
import {CategoryService} from "../services/api/category.service";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import {Http} from "@angular/http";
import {GlobalHttpInterceptor} from "../interceptors/global-http-interceptor";

//#region Route configuration

// Config application routes.
const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'account-management'
  },
  {
    path: 'account-management',
    component: AccountManagementComponent,
    canActivate: [IsAuthorizedGuard]
  },
  {
    path: 'login',
    component: AccountLoginComponent,
    pathMatch: 'full'
  }
];

//#endregion

@NgModule({
  declarations: [
    AppComponent,

    AccountManagementComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added

    // Import router configuration.
    RouterModule.forRoot(appRoutes),
  ],
  providers: [
    {provide: 'IApiService', useClass: ApiService},
    {provide: 'IAccountService', useClass: AccountService},
    {provide: 'ICategoryService', useClass: CategoryService},

    // Handle common behaviour of http request / response.
    {provide: Http, useClass: GlobalHttpInterceptor},
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

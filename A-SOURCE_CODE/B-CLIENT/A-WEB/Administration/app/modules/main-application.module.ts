import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MainApplicationComponent }   from '../components/main-application.component';
import {NavigationBarComponent} from "../components/navigation-bar.component";
import {SidebarComponent} from "../components/sidebar.component";
import {AccountManagementComponent} from "../components/account-management.component";
import {CategoryManagementComponent} from "../components/category-management.component";

@NgModule({
    imports:      [ BrowserModule ],
    declarations: [
        MainApplicationComponent ,
        NavigationBarComponent,
        SidebarComponent,
        AccountManagementComponent,
        CategoryManagementComponent
    ],
    bootstrap:    [ MainApplicationComponent ]
})
export class MainApplicationModule { }

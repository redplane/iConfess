import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MainApplicationComponent }   from '../components/main-application.component';
import {NavigationBarComponent} from "../components/navigation-bar.component";
import {SidebarComponent} from "../components/sidebar.component";
import {AccountManagementComponent} from "../components/account-management.component";
import {CategoryManagementComponent} from "../components/category-management.component";
import {CategoryDetailBoxComponent} from '../components/content/category/category-detail-box.component';
import {CategoryEditBoxComponent} from "../components/content/category/category-edit-box.component";
import {FormsModule} from '@angular/forms';

@NgModule({
    imports:      [
        BrowserModule,
        FormsModule
    ],
    declarations: [
        MainApplicationComponent ,
        NavigationBarComponent,
        SidebarComponent,
        AccountManagementComponent,
        CategoryManagementComponent,
        CategoryDetailBoxComponent,

        CategoryEditBoxComponent
    ],
    bootstrap:    [
        MainApplicationComponent
    ]
})
export class MainApplicationModule { }

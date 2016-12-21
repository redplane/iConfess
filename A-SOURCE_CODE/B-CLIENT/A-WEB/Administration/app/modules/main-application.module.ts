import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MainApplicationComponent }   from '../components/main-application.component';
import {NavigationBarComponent} from "../components/navigation-bar.component";
import {SidebarComponent} from "../components/sidebar.component";
import {AccountManagementComponent} from "../components/account-management.component";
import {CategoryManagementComponent} from "../components/category-management.component";
import {CategoryDetailBoxComponent} from '../components/content/category/category-detail-box.component';
import {CategoryEditBoxComponent} from "../components/content/category/category-edit-box.component";
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CategoryDeleteBoxComponent} from "../components/content/category/category-delete-box.component";
import {SelectModule} from "angular2-select";
import {CategoryFindBoxComponent} from "../components/content/category/category-find-box.component";
import { HttpModule, JsonpModule } from '@angular/http';
import { PaginationModule, TypeaheadModule  } from 'ng2-bootstrap/ng2-bootstrap';

@NgModule({
    imports:      [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        SelectModule,
        HttpModule,
        JsonpModule,

        PaginationModule,
        TypeaheadModule
    ],
    declarations: [
        MainApplicationComponent ,
        NavigationBarComponent,
        SidebarComponent,
        AccountManagementComponent,

        CategoryManagementComponent,
        CategoryDetailBoxComponent,
        CategoryEditBoxComponent,
        CategoryDeleteBoxComponent,
        CategoryFindBoxComponent
    ],
    bootstrap:    [
        MainApplicationComponent
    ]
})
export class MainApplicationModule { }

import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MainApplicationComponent }   from '../components/main-application.component';

@NgModule({
    imports:      [ BrowserModule ],
    declarations: [ MainApplicationComponent ],
    bootstrap:    [ MainApplicationComponent ]
})
export class MainApplicationModule { }

import { Component, EventEmitter } from '@angular/core';
import {Sections} from "../enumerations/Sections";

@Component({
    selector: 'sidebar',
    templateUrl: './app/html/sidebar.component.html',
    inputs:['selectArea'],
    outputs: ['onAreaSelected']
})
export class SidebarComponent {

    public sections: Sections;
    onAreaSelected: EventEmitter<Sections>;

    public constructor(){
        this.sections = Sections;
        this.onAreaSelected = new EventEmitter();
    }

    // Select an area to be displayed.
    public selectArea(section: Sections){
        console.log(`Area selected = ${section}`);
        this.onAreaSelected.emit(section);
    }
}

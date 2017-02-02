import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Dictionary} from "../viewmodels/Dictionary";

@Component({
    selector: 'main-application',
    templateUrl: './app/views/main-application.html'
})

export class MainApplicationComponent implements OnInit{

    // List of url where navigation bars should not be displayed.
    private unauthenticatedUrls: Dictionary<boolean>;

    // Initiate component with IoC.
    public constructor(public clientRoutingService: Router){
        this.unauthenticatedUrls = new Dictionary<boolean>();
    }

    // Find location of the current page.
    public getLocation(): string{
        return this.clientRoutingService.url;
    }

    // Check whether navigation bar should be displayed or not.
    public shouldNavigationBarsBeAvailable(): boolean{

        // Find the current page location.
        let location = this.getLocation();

        // If location is the prohibited list, navigation bars should not be displayed.
        if (this.unauthenticatedUrls.containsKey(location))
            return false;

        return true;
    }

    // Callback which is fired when component has been loaded successfully.
    public ngOnInit(): void {
        this.unauthenticatedUrls.insert("/", true);
        this.unauthenticatedUrls.insert("/forgot-password", true);
        this.unauthenticatedUrls.insert("/submit-password", true);
    }
}

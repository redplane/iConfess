import { Component } from '@angular/core';
import {Router} from '@angular/router';

@Component({
    selector: 'main-application',
    templateUrl: './app/views/main-application.html'
})

export class MainApplicationComponent {

    // Router service which is used for routing.
    private _router: Router;

    // Location of the current page.
    public location: string;

    // Initiate component with IoC.
    public constructor(router: Router){
        this._router = router;

        // Find the page location.
        this.location = router.url;

    }

}

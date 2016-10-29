import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { MainApplicationModule } from './modules/main-application.module';

const platform = platformBrowserDynamic();
platform.bootstrapModule(MainApplicationModule);
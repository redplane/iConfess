import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './navbar.module';
const platform = platformBrowserDynamic();
platform.bootstrapModule(AppModule);

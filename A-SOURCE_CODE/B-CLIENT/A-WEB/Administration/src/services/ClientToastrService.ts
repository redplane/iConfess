import {Injectable} from "@angular/core";
import {ToastPosition} from "../enumerations/ToastrPosition";
import {ToastOptions} from "../models/ToastrOption";
import {IClientToastrService} from "../interfaces/services/IClientToastrService";

// Toastr variable declaration.
// This is for calling toastr function of jquery plugin.
declare var toastr: any;

/*
* Service which handles business of toast notification on page.
* */
@Injectable()
export class ClientToastrService implements IClientToastrService{

    //#region Properties

    // Default toast configuration.
    private options : ToastOptions;

    //#endregion

    //#region Constructor

    // Initiate toast notification service.
    public constructor(){
        this.options = new ToastOptions();
    }

    //#endregion

    //#region Methods

    // Display success message.
    public success(message: string, title: string = '', toastOption: ToastOptions = null): void{
        return this.show(message, title, toastOption, 'success');
    }

    // Display info message.
    public info(message: string, title: string = '', toastOption: ToastOptions = null): void{
        return this.show(message, title, toastOption, 'info');
    }

    // Display warning message.
    public warning(message: string, title: string = '', toastOption: ToastOptions = null): void{
        return this.show(message, title, toastOption, 'warning');
    }

    // Display error message.
    public error(message: string, title: string = '', toastOption: ToastOptions = null): void{
        return this.show(message, title, toastOption, 'error');
    }

    // Find default toast configuration.
    public getToastConfiguration(): ToastOptions{
        return this.options;
    }

    // Display toast notification
    public show(message: string, title: string, toastOption: ToastOptions, type: string): void{
        let options = this.options;
        if (toastOption != null)
            options = toastOption;

        options['positionClass'] = this.getToastPosition(options.position);
        toastr[type](message, title);
    }

    // Find toast notification display position.
    private getToastPosition(position: ToastPosition): string{
        switch (position){
            case ToastPosition.bottomRight:
                return 'toast-bottom-right';
            case ToastPosition.bottomLeft:
                return 'toast-bottom-left';
            case ToastPosition.topLeft:
                return 'toast-top-left';
            case ToastPosition.topLeft:
                return 'toast-top-full-width';
            case ToastPosition.topFullWidth:
                return 'toast-top-full-width';
            case ToastPosition.bottomFullWidth:
                return 'toast-bottom-full-width';
            case ToastPosition.topCenter:
                return 'toast-top-center';
            case ToastPosition.bottomCenter:
                return 'toast-bottom-center';
            default:
                return 'toast-top-right';
        }
    }

    //#endregion
}
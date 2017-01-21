import {Injectable} from "@angular/core";

// Toastr variable declaration.
// This is for calling toastr function of jquery plugin.
declare var toastr: any;


/*
* Position of toast notification.
* */
export enum ToastPosition{
    topRight,
    topLeft,
    bottomRight,
    bottomLeft,
    topFullWidth,
    bottomFullWidth,
    topCenter,
    bottomCenter
}

/*
* Configuration of toast.
* */
export class ToastOptions {

    // Whether close button should appear in toast.
    public closeButton: boolean;

    // Whether debug mode is on.
    public debug: boolean;

    // Whether newest toast be on top.
    public newestOnTop: boolean;

    // Whether progress bar should be displayed or not.
    public progressBar: boolean;

    // Whether similar toast cannot be displayed or not.
    public preventDuplicates: boolean;

    // Life-time of toast.
    public showDuration: number;

    // How many milliseconds does it take to make toast disappear.
    public hideDuration: number;

    // Timeout of toast.
    public timeOut: number;

    public extendedTimeOut: number;

    // Mode of easing.
    public showEasing: string;

    // Easing hide mode.
    public hideEasing: string;

    // Display method.
    public showMethod: string;

    // Way that toast disappear.
    public hideMethod: string;

    // Whether toast can be dismissed by tapping.
    public tapToDismiss: boolean;

    // Place where toast should be shown.
    public position: ToastPosition;

    // Intiate toast option with default settings.
    public constructor() {
        this.closeButton = true;
        this.debug = false;
        this.newestOnTop = true;
        this.progressBar = false;
        this.preventDuplicates = true;
        this.showDuration = 300;
        this.hideDuration = 1000;
        this.timeOut = 0;
        this.extendedTimeOut = 0;
        this.showEasing = 'swing';
        this.hideEasing = 'linear';
        this.showMethod = 'fadeIn';
        this.hideMethod = 'fadeOut';
        this.tapToDismiss = true;
        this.position = ToastPosition.topRight;
    }
}

/*
* Service which handles business of toast notification on page.
* */
@Injectable()
export class ClientNotificationService{

    // Default toast configuration.
    private _toastOptions : ToastOptions;

    // Initiate toast notification service.
    public constructor(){
        this._toastOptions = new ToastOptions();
    }

    // Display success message.
    public success(message: string, title: string = '', toastOption: ToastOptions = null){
        return this.show(message, title, toastOption, 'success');
    }

    // Display info message.
    public info(message: string, title: string = '', toastOption: ToastOptions = null){
        return this.show(message, title, toastOption, 'info');
    }

    // Display warning message.
    public warning(message: string, title: string = '', toastOption: ToastOptions = null){
        return this.show(message, title, toastOption, 'warning');
    }

    // Display error message.
    public error(message: string, title: string = '', toastOption: ToastOptions = null){
        return this.show(message, title, toastOption, 'error');
    }

    // Find default toast configuration.
    public getToastConfiguration(){
        return this._toastOptions;
    }

    // Display toast notification
    private show(message: string, title: string, toastOption: ToastOptions, type: string){
        let options = this._toastOptions;
        if (toastOption != null)
            options = toastOption;

        options['positionClass'] = this.getToastPosition(options.position);
        return toastr[type](message, title);
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

}
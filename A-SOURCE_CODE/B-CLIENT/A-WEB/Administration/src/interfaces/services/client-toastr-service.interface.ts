import {ToastOptions} from "../../models/toastr-option";

export interface IClientToastrService {

    // Display success message.
    success(message: string, title: string, toastOption: ToastOptions): void;

    // Display info message.
    info(message: string, title: string, toastOption: ToastOptions): void;

    // Display warning message.
    warning(message: string, title: string, toastOption: ToastOptions): void;

    // Display error message.
    error(message: string, title: string, toastOption: ToastOptions): void;

    // Display toast notification
    show(message: string, title: string, toastOption: ToastOptions, type: string): void;
}

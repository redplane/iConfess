import {AccountStatuses} from "../enumerations/AccountStatuses";

/*
* Account and its properties.
* */
export class Account{

    // Account index.
    public id: number;

    // Email which is used for account registration.
    public email: string;

    // Nickname of account.
    public nickname: string;

    // Status of account.
    public status: AccountStatuses;

    // Photo of account.
    public photoRelativeUrl: string;

    // Time when account was created on server.
    public joined: number;

    // Time when account was lastly modified.
    public lastModified: number;
}
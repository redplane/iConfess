export interface IClientTimeService{
    //#region Methods

    // Convert number to unix time.
    getUtc(milliseconds: number) : Date;

    //#endregion
}
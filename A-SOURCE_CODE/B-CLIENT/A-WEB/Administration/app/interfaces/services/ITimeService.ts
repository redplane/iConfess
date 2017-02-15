export interface IClientTimeService{

    // Convert number to unix time.
    findUnixTime(milliseconds: number) : Date;

}
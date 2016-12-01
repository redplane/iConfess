export interface ITimeService{

    // Convert number to unix time.
    findUnixTime(milliseconds: number) : Date;

}
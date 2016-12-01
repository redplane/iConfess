import {ITimeService} from "../interfaces/services/ITimeService";
export class TimeService implements ITimeService{

    // Convert number to unix time.
    findUnixTime(milliseconds: number): Date {

        // Initiate date and set its millisecs.
        var date = new Date();
        date.setTime(milliseconds);
        return date;
    }

}
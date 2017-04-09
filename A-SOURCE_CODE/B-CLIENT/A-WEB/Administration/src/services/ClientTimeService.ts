import {IClientTimeService} from "../interfaces/services/IClientTimeService";

/*
* Client time service which handles time calculation.
* */
export class ClientTimeService implements IClientTimeService{

    // Convert number to unix time.
    getUtc(milliseconds: number): Date {

        // Initiate date and set its millisecs.
        let date = new Date();
        date.setTime(milliseconds);
        return date;
    }

}
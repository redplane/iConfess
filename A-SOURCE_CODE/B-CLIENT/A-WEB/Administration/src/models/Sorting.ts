import {SortDirection} from "../enumerations/SortDirection";

export class Sorting<T>{

    // Whether record should be sorted ascending or descending.
    public direction: SortDirection;

    // Property which should be sorted.
    public property: T;
}
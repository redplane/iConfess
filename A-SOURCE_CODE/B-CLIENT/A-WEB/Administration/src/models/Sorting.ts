import {SortDirection} from "../enumerations/sort-direction";

export class Sorting<T>{

    // Whether record should be sorted ascending or descending.
    public direction: SortDirection;

    // Property which should be sorted.
    public property: T;
}

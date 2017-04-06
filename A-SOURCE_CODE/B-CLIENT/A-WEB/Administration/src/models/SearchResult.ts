// This class contains search results which obtained from web api.
export class SearchResult<T> {

    // List of records.
    public records: Array<T>;

    // Total condition matched records.
    public total: number;
}
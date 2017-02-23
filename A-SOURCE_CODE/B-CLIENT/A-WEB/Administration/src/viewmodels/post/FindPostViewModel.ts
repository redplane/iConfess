import {TextSearch} from "../TextSearch";
import {UnixDateRange} from "../UnixDateRange";
import {Pagination} from "../Pagination";

export class FindPostViewModel{

    // Id of post
    public id: number;

    // Owner index of post.
    public ownerIndex: number;

    // Category index of post.
    public categoryIndex: number;

    // Title of post
    public title: TextSearch;

    // Body of post
    public body: TextSearch;

    // Time when post is created.
    public created: UnixDateRange;

    // Time when post was lastly modified.
    public lastModified: UnixDateRange;

    // Pagination of records.
    public pagination: Pagination;
}
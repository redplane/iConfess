import {Account} from "../../models/Account";
import {Category} from "../../models/Category";
export class PostDetailsViewModel{

    // Id of post.
    public id: number;

    // Post owner.
    public owner: Account;

    // Category information which post belongs to.
    public category: Category;

    // Title of post.
    public title: string;

    // Post content.
    public body: string;

    // When the post was created.
    public created: number;

    // When the post was lastly modified.
    public lastModified: number;
}
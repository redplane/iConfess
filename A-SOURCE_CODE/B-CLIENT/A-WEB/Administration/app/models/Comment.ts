import {Account} from "./Account";
import {Post} from "./Post";

/*
* Comment and its Information
* */
export class Comment{

    // Comment index.
    public id: number;

    // Information of comment creator.
    public owner: Account;

    // Information of post which contains comment.
    public post: Post;

    // Comment content.
    public content: string;

    // When the comment was created.
    public created: number;

    // When the comment was lastly modified.
    public lastModified: number;
}
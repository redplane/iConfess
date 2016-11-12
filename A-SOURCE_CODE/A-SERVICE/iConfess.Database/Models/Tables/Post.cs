namespace iConfess.Database.Models.Tables
{
    public class Post
    {
        #region Properties

        /// <summary>
        /// Id of post.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Who owns the post.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        /// Which category the post belongs to.
        /// </summary>
        public int CategoryIndex { get; set; }

        /// <summary>
        /// Title of post.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Post body.
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// When the post was created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        /// When the post was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships
        
        /// <summary>
        /// Who create the post.
        /// </summary>
        public Account Owner { get; set; }

        /// <summary>
        /// One post can be monitored by follow post.
        /// </summary>
        public FollowPost BeFollowed { get; set; }

        /// <summary>
        /// Which notification comment post belongs to.
        /// </summary>
        public NotificationComment NotificationComment { get; set; }

        /// <summary>
        /// Which notification post the post belongs to.
        /// </summary>
        public NotificationPost NotificationPost { get; set; }

        #endregion

    }
}
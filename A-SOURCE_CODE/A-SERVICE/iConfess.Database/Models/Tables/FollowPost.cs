namespace iConfess.Database.Models.Tables
{
    public class FollowPost
    {
        #region Properties

        /// <summary>
        /// Who is the follower of post.
        /// </summary>
        public int FollowerIndex { get; set; }

        /// <summary>
        /// Which post is being followed by the follower.
        /// </summary>
        public int PostIndex { get; set; }

        /// <summary>
        /// When the following action was created.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// Who is following the post.
        /// </summary>
        public Account Follower { get; set; }

        /// <summary>
        /// Post which is being monitored by this relationship.
        /// </summary>
        public Post Post { get; set; }

        #endregion
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace Administration.Models.Tables
{
    [Table(nameof(FollowPost))]
    public class FollowPost
    {
        #region Properties

        /// <summary>
        ///     The account which is following the specific post.
        /// </summary>
        public int Follower { get; set; }

        /// <summary>
        ///     Index of post which is being followed by the follower.
        /// </summary>
        public int Post { get; set; }

        /// <summary>
        ///     When the follow was started.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Foreign keys

        /// <summary>
        ///     Detail information of follower.
        /// </summary>
        [ForeignKey(nameof(Follower))]
        public virtual Account FollowerInfo { get; set; }

        /// <summary>
        ///     Detail information of post.
        /// </summary>
        [ForeignKey(nameof(Post))]
        public virtual Post PostInfo { get; set; }

        #endregion
    }
}
using Database.Models.Entities;

namespace Administration.ViewModels.ApiPost
{
    public class PostDetailViewModel
    {
        #region Properties.

        /// <summary>
        ///     Id of post.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Who owns the post.
        /// </summary>
        public Account Owner { get; set; }

        /// <summary>
        ///     Which category the post belongs to.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        ///     Title of post.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Post body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     When the post was created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the post was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion
    }
}
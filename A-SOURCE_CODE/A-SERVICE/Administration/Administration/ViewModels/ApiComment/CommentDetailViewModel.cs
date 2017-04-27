using Database.Models.Entities;

namespace Administration.ViewModels.ApiComment
{
    public class CommentDetailViewModel
    {
        /// <summary>
        ///     Page of comment (Auto incremented)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Who wrote the comment.
        /// </summary>
        public Account Owner { get; set; }
        
        /// <summary>
        ///     Comment content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     When was the comment created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the comment was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }
    }
}
using System.Linq;

namespace iConfess.Admin.ViewModels.ApiComment
{
    public class ResponseCommentsDetailsViewModel
    {
        /// <summary>
        /// List of comments details.
        /// </summary>
        public IQueryable<CommentDetailViewModel> CommentsDetails { get; set; }

        /// <summary>
        /// Total records match the specific conditions.
        /// </summary>
        public int Total { get; set; }
    }
}
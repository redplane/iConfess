using System.Linq;
using iConfess.Database.Models.Tables;

namespace Shared.ViewModels.Comments
{
    public class ResponseCommentsViewModel
    {
        /// <summary>
        /// List of comments which match with the conditions.
        /// </summary>
        public IQueryable<Comment> Comments { get; set; }

        /// <summary>
        /// Total of record number.
        /// </summary>
        public int Total { get; set; }
    }
}
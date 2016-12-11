using System.Linq;
using iConfess.Database.Models.Tables;

namespace Shared.ViewModels.Posts
{
    public class ResponsePostsViewModel
    {
        /// <summary>
        ///     Posts filtered by using specific conditions.
        /// </summary>
        public IQueryable<Post> Posts { get; set; }

        /// <summary>
        ///     Total records match with specific conditions.
        /// </summary>
        public int Total { get; set; }
    }
}
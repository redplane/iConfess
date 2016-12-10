using System.Linq;
using iConfess.Database.Models.Tables;

namespace Shared.ViewModels.PostReports
{
    public class ResponsePostReportsViewModel
    {
        /// <summary>
        /// List of filtered post reports.
        /// </summary>
        public IQueryable<PostReport> PostReports { get; set; }

        /// <summary>
        /// Total record number.
        /// </summary>
        public int Total { get; set; }
    }
}
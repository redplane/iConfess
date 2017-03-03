using System.Linq;

namespace iConfess.Admin.ViewModels.ApiPostReport
{
    public class ResponseFindPostReportViewModel
    {
        /// <summary>
        /// Find post reports.
        /// </summary>
        public IQueryable<PostReportViewModel> PostReports { get; set; }

        /// <summary>
        /// Total of reports.
        /// </summary>
        public int Total { get; set; }
    }
}
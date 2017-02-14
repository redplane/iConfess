using iConfess.Database.Models.Tables;

namespace iConfess.Admin.ViewModels.ApiPostReport
{
    public class PostReportViewModel
    {
        /// <summary>
        /// Index of post report.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Post index.
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// Post report owner.
        /// </summary>
        public Account Owner { get; set; }

        /// <summary>
        /// Who created report.
        /// </summary>
        public Account Reporter { get; set; }

        /// <summary>
        /// Body of report.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Reason of report.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Time when the report is created.
        /// </summary>
        public double Created { get; set; }
    }
}
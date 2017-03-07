using System.ComponentModel.DataAnnotations;
using Shared.Resources;

namespace Shared.ViewModels.CommentReports
{
    public class InitiateCommentReportViewModel
    {
        /// <summary>
        ///     Index of comment which should be reported.
        /// </summary>
        public int CommentIndex { get; set; }

        /// <summary>
        ///     Reason why the comment should be reported.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(HttpMessages), ErrorMessageResourceName = "InformationIsRequired")]
        public string Reason { get; set; }
    }
}
using System;
using System.Linq;
using Database.Interfaces;
using Database.Models.Entities;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.CommentReports;

namespace Shared.Repositories
{
    public class RepositoryCommentReport : ParentRepository<CommentReport>, IRepositoryCommentReport
    {
        #region Constructors

        /// <summary>
        ///     Initiate repository of comment reports.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        public RepositoryCommentReport(
            IDbContextWrapper dbContextWrapper) : base(dbContextWrapper)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search comment reports by using specific conditions.
        /// </summary>
        /// <param name="commentReports"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<CommentReport> Search(IQueryable<CommentReport> commentReports,
            SearchCommentReportViewModel conditions)
        {
            // Comment index is specified.
            if (conditions.CommentIndex != null)
                commentReports = commentReports.Where(x => x.CommentIndex == conditions.CommentIndex.Value);

            // Comment owner is specified.
            if (conditions.CommentOwnerIndex != null)
                commentReports = commentReports.Where(x => x.CommentOwnerIndex == conditions.CommentOwnerIndex.Value);

            // Reporter index is specified.
            if (conditions.CommentReporterIndex != null)
                commentReports =
                    commentReports.Where(x => x.CommentReporterIndex == conditions.CommentReporterIndex.Value);

            // Comment body is specified.
            if (conditions.Body != null && !string.IsNullOrEmpty(conditions.Body.Value))
            {
                var szBody = conditions.Body;
                switch (szBody.Mode)
                {
                    case TextComparision.Contain:
                        commentReports = commentReports.Where(x => x.Body.Contains(szBody.Value));
                        break;
                    case TextComparision.Equal:
                        commentReports = commentReports.Where(x => x.Body.Equals(szBody.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        commentReports =
                            commentReports.Where(
                                x => x.Body.Equals(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        commentReports = commentReports.Where(x => x.Body.StartsWith(szBody.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        commentReports =
                            commentReports.Where(
                                x => x.Body.StartsWith(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        commentReports = commentReports.Where(x => x.Body.EndsWith(szBody.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        commentReports =
                            commentReports.Where(
                                x => x.Body.EndsWith(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        commentReports = commentReports.Where(x => x.Body.ToLower().Contains(szBody.Value.ToLower()));
                        break;
                }
                return commentReports;
            }

            // Comment reason is specified.
            if (conditions.Reason != null && !string.IsNullOrEmpty(conditions.Reason.Value))
            {
                var szReason = conditions.Reason;
                switch (szReason.Mode)
                {
                    case TextComparision.Contain:
                        commentReports = commentReports.Where(x => x.Reason.Contains(szReason.Value));
                        break;
                    case TextComparision.Equal:
                        commentReports = commentReports.Where(x => x.Reason.Equals(szReason.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        commentReports =
                            commentReports.Where(
                                x => x.Reason.Equals(szReason.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        commentReports = commentReports.Where(x => x.Reason.StartsWith(szReason.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        commentReports =
                            commentReports.Where(
                                x => x.Reason.StartsWith(szReason.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        commentReports = commentReports.Where(x => x.Reason.EndsWith(szReason.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        commentReports =
                            commentReports.Where(
                                x => x.Reason.EndsWith(szReason.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        commentReports = commentReports.Where(x => x.Reason.ToLower().Contains(szReason.Value.ToLower()));
                        break;
                }
            }

            // Created is specified.
            if (conditions.Created != null)
            {
                // Comment created time.
                var created = conditions.Created;

                // From is defined.
                if (created.From != null)
                    commentReports = commentReports.Where(x => x.Created >= created.From.Value);

                // To is defined.
                if (created.To != null)
                    commentReports = commentReports.Where(x => x.Created <= created.To.Value);
            }

            return commentReports;
        }

        #endregion
    }
}
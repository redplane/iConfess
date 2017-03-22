using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.CommentReports;

namespace Shared.Repositories
{
    public class RepositoryCommentReport : IRepositoryCommentReport
    {
        #region Properties

        /// <summary>
        ///     Provides access to database.
        /// </summary>
        private readonly ConfessDbContext _iConfessDbContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository of comment reports.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryCommentReport(ConfessDbContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Delete comment reports by searching for specific conditions.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public void Delete(FindCommentReportsViewModel parameters)
        {
            // Find all comment reports first.
            var commentReports = _iConfessDbContext.CommentReports.AsQueryable();

            // Find all comment reports with specific conditions.
            commentReports = FindCommentReports(commentReports, parameters);

            // Delete all found comment reports.
            _iConfessDbContext.CommentReports.RemoveRange(commentReports);
        }
        
        /// <summary>
        ///     Initiate / update comment report.
        /// </summary>
        /// <param name="commentReport"></param>
        /// <returns></returns>
        public async Task<CommentReport> InitiateCommentReportAsync(CommentReport commentReport)
        {
            // Insert / update comment report.
            _iConfessDbContext.CommentReports.AddOrUpdate(commentReport);

            // Save changes into database.
            await _iConfessDbContext.SaveChangesAsync();

            return commentReport;
        }

        /// <summary>
        /// Find all comment reports in database.
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommentReport> FindCommentReports()
        {
            return _iConfessDbContext.CommentReports.AsQueryable();
        }

        /// <summary>
        ///     Find comment reports by using specific conditions.
        /// </summary>
        /// <param name="commentReports"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<CommentReport> FindCommentReports(IQueryable<CommentReport> commentReports,
            FindCommentReportsViewModel conditions)
        {
            // Index is specified.
            if (conditions.Id != null)
                commentReports = commentReports.Where(x => x.Id == conditions.Id.Value);

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
            if ((conditions.Body != null) && !string.IsNullOrEmpty(conditions.Body.Value))
            {
                var commentBody = conditions.Body;
                switch (commentBody.Mode)
                {
                    case TextComparision.Contain:
                        commentReports = commentReports.Where(x => x.Body.Contains(commentBody.Value));
                        break;
                    case TextComparision.Equal:
                        commentReports = commentReports.Where(x => x.Body.Equals(commentBody.Value));
                        break;
                    default:
                        commentReports =
                            commentReports.Where(
                                x => x.Body.Equals(commentBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                }
            }

            // Comment reason is specified.
            if ((conditions.Reason != null) && !string.IsNullOrEmpty(conditions.Reason.Value))
            {
                var commentReason = conditions.Reason;
                switch (commentReason.Mode)
                {
                    case TextComparision.Contain:
                        commentReports = commentReports.Where(x => x.Reason.Contains(commentReason.Value));
                        break;
                    case TextComparision.Equal:
                        commentReports = commentReports.Where(x => x.Reason.Equals(commentReason.Value));
                        break;
                    default:
                        commentReports =
                            commentReports.Where(
                                x => x.Reason.Equals(commentReason.Value, StringComparison.InvariantCultureIgnoreCase));
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
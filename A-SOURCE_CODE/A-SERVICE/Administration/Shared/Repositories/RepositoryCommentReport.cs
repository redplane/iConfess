using System;
using System.Linq;
using iConfess.Database.Interfaces;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
using Shared.ViewModels.CommentReports;

namespace Shared.Repositories
{
    public class RepositoryCommentReport : ParentRepository<CommentReport>, IRepositoryCommentReport
    {
        #region Properties

        /// <summary>
        ///     Provides access to database.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;
        
        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository of comment reports.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        public RepositoryCommentReport(
            IDbContextWrapper dbContextWrapper) : base(dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
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
            if (conditions.Body != null && !string.IsNullOrEmpty(conditions.Body.Value))
                commentReports = SearchPropertyText(commentReports, x => x.Body,
                    conditions.Body);
            
            // Comment reason is specified.
            if (conditions.Reason != null && !string.IsNullOrEmpty(conditions.Reason.Value))
                commentReports = SearchPropertyText(commentReports, x => x.Reason,
                    conditions.Reason);

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
using System.Linq;
using iConfess.Database.Interfaces;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
using Shared.ViewModels.PostReports;

namespace Shared.Repositories
{
    public class RepositoryPostReport : ParentRepository<PostReport>, IRepositoryPostReport
    {
        #region Constructors

        /// <summary>
        ///     Initiate repository with inversion of control.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        /// <param name="commonRepositoryService"></param>
        public RepositoryPostReport(
            IDbContextWrapper dbContextWrapper,
            ICommonRepositoryService commonRepositoryService) : base(dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
            _commonRepositoryService = commonRepositoryService;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search posts by using specific conditions.
        /// </summary>
        /// <param name="postReports"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<PostReport> Search(IQueryable<PostReport> postReports,
            SearchPostReportViewModel conditions)
        {
            // Id is specified.
            if (conditions.Id != null)
                postReports = postReports.Where(x => x.Id == conditions.Id.Value);

            // Post index is specified.
            if (conditions.PostIndex != null)
                postReports = postReports.Where(x => x.PostIndex == conditions.PostIndex.Value);

            // Owner index is specified.
            if (conditions.PostOwnerIndex != null)
                postReports = postReports.Where(x => x.PostOwnerIndex == conditions.PostOwnerIndex.Value);

            // Reporter index is specified.
            if (conditions.PostReporterIndex != null)
                postReports = postReports.Where(x => x.PostReporterIndex == conditions.PostReporterIndex.Value);

            // Body of post.
            if (conditions.Body != null && !string.IsNullOrEmpty(conditions.Body.Value))
                postReports = _commonRepositoryService.SearchPropertyText(postReports, x => x.Body, conditions.Body);

            // Reason is specified.
            if (conditions.Reason != null && !string.IsNullOrEmpty(conditions.Reason.Value))
                postReports = _commonRepositoryService.SearchPropertyText(postReports, x => x.Reason, conditions.Reason);

            // Created is specified.
            if (conditions.Created != null)
            {
                var created = conditions.Created;

                // From is defined.
                if (created.From != null)
                    postReports = postReports.Where(x => x.Created >= created.From.Value);

                // To is defined.
                if (created.To != null)
                    postReports = postReports.Where(x => x.Created <= created.To.Value);
            }

            return postReports;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Instance which is used for accessing database context.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        /// <summary>
        ///     Service which handles common businesses of repositories.
        /// </summary>
        private readonly ICommonRepositoryService _commonRepositoryService;

        #endregion
    }
}
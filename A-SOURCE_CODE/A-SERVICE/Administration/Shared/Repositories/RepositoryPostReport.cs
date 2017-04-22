using System;
using System.Linq;
using Database.Interfaces;
using Database.Models.Entities;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
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
        public RepositoryPostReport(
            IDbContextWrapper dbContextWrapper) : base(dbContextWrapper)
        {
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
            // Owner index is specified.
            if (conditions.PostOwnerIndex != null)
                postReports = postReports.Where(x => x.PostOwnerIndex == conditions.PostOwnerIndex.Value);

            // Reporter index is specified.
            if (conditions.PostReporterIndex != null)
                postReports = postReports.Where(x => x.PostReporterIndex == conditions.PostReporterIndex.Value);

            // Body of post.
            if (conditions.Body != null && !string.IsNullOrEmpty(conditions.Body.Value))
            {
                var szBody = conditions.Body;
                switch (szBody.Mode)
                {
                    case TextComparision.Contain:
                        postReports = postReports.Where(x => x.Body.Contains(szBody.Value));
                        break;
                    case TextComparision.Equal:
                        postReports = postReports.Where(x => x.Body.Equals(szBody.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        postReports =
                            postReports.Where(
                                x => x.Body.Equals(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        postReports = postReports.Where(x => x.Body.StartsWith(szBody.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        postReports =
                            postReports.Where(
                                x => x.Body.StartsWith(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        postReports = postReports.Where(x => x.Body.EndsWith(szBody.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        postReports =
                            postReports.Where(
                                x => x.Body.EndsWith(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        postReports = postReports.Where(x => x.Body.ToLower().Contains(szBody.Value.ToLower()));
                        break;
                }
            }
            // Reason is specified.
            if (conditions.Reason != null && !string.IsNullOrEmpty(conditions.Reason.Value))
            {
                var szReason = conditions.Reason;
                switch (szReason.Mode)
                {
                    case TextComparision.Contain:
                        postReports = postReports.Where(x => x.Reason.Contains(szReason.Value));
                        break;
                    case TextComparision.Equal:
                        postReports = postReports.Where(x => x.Reason.Equals(szReason.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        postReports =
                            postReports.Where(
                                x => x.Reason.Equals(szReason.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        postReports = postReports.Where(x => x.Reason.StartsWith(szReason.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        postReports =
                            postReports.Where(
                                x => x.Reason.StartsWith(szReason.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        postReports = postReports.Where(x => x.Reason.EndsWith(szReason.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        postReports =
                            postReports.Where(
                                x => x.Reason.EndsWith(szReason.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        postReports = postReports.Where(x => x.Reason.ToLower().Contains(szReason.Value.ToLower()));
                        break;
                }
            }

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
    }
}
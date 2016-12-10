using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.PostReports;

namespace Shared.Repositories
{
    public class RepositoryPostReport : IRepositoryPostReport
    {
        #region Properties

        /// <summary>
        ///     Instance which is used for accessing database context.
        /// </summary>
        private readonly ConfessionDbContext _iConfessDbContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with inversion of control.
        /// </summary>
        public RepositoryPostReport(ConfessionDbContext iConfessionDbContext)
        {
            _iConfessDbContext = iConfessionDbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initiate / update a post report information.
        /// </summary>
        /// <param name="postReport"></param>
        /// <returns></returns>
        public async Task<PostReport> InitiatePostReportAsync(PostReport postReport)
        {
            // Add or update the record.
            _iConfessDbContext.PostReports.AddOrUpdate(postReport);

            // Save changes into database.
            await _iConfessDbContext.SaveChangesAsync();

            return postReport;
        }

        /// <summary>
        ///     Find posts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        public async Task<ResponsePostReportsViewModel> FindPostReportsAsync(FindPostReportsViewModel conditions)
        {
            // Response initialization.
            var responsePostReportsViewModel = new ResponsePostReportsViewModel();
            responsePostReportsViewModel.PostReports = _iConfessDbContext.PostReports.AsQueryable();

            // Find posts by using specific conditions.
            responsePostReportsViewModel.PostReports = FindPostReports(responsePostReportsViewModel.PostReports,
                conditions);

            // Find total records which match with specific conditions.
            responsePostReportsViewModel.Total = await responsePostReportsViewModel.PostReports.CountAsync();

            // Do pagination.
            if (conditions.Pagination != null)
            {
                // Find pagination.
                var pagination = conditions.Pagination;

                responsePostReportsViewModel.PostReports = responsePostReportsViewModel.PostReports.Skip(
                        pagination.Index*pagination.Record)
                    .Take(pagination.Record);
            }

            return responsePostReportsViewModel;
        }

        /// <summary>
        ///     Delete pots by using specific conditions.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<int> DeletePostsAsync(FindPostReportsViewModel conditions)
        {
            // Find posts by using specific conditions.
            var postReports = FindPostReports(_iConfessDbContext.PostReports.AsQueryable(), conditions);

            // Delete all searched records.
            _iConfessDbContext.PostReports.RemoveRange(postReports);

            // Submit changes and count total affected records.
            var totalRecords = await _iConfessDbContext.SaveChangesAsync();

            return totalRecords;
        }

        /// <summary>
        ///     Find posts by using specific conditions.
        /// </summary>
        /// <param name="postReports"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private IQueryable<PostReport> FindPostReports(IQueryable<PostReport> postReports,
            FindPostReportsViewModel conditions)
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
            if ((conditions.Body != null) && !string.IsNullOrEmpty(conditions.Body.Value))
            {
                var body = conditions.Body;
                switch (body.Mode)
                {
                    case TextComparision.Contain:
                        postReports = postReports.Where(x => x.Body.Contains(body.Value));
                        break;
                    case TextComparision.Equal:
                        postReports = postReports.Where(x => x.Body.Equals(body.Value));
                        break;
                    default:
                        postReports =
                            postReports.Where(
                                x => x.Body.Equals(body.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                }
            }

            // Reason is specified.
            if ((conditions.Reason != null) && !string.IsNullOrEmpty(conditions.Reason.Value))
            {
                var reason = conditions.Reason;
                switch (reason.Mode)
                {
                    case TextComparision.Contain:
                        postReports = postReports.Where(x => x.Body.Contains(reason.Value));
                        break;
                    case TextComparision.Equal:
                        postReports = postReports.Where(x => x.Body.Equals(reason.Value));
                        break;
                    default:
                        postReports =
                            postReports.Where(
                                x => x.Body.Equals(reason.Value, StringComparison.InvariantCultureIgnoreCase));
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
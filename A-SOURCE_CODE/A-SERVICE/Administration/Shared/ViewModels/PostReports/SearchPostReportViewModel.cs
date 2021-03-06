﻿using Shared.Enumerations.Order;
using Shared.Models;

namespace Shared.ViewModels.PostReports
{
    public class SearchPostReportViewModel
    {
        /// <summary>
        ///     Page of post report.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Page of post which is reported.
        /// </summary>
        public int? PostIndex { get; set; }

        /// <summary>
        ///     Page of post owner.
        /// </summary>
        public int? PostOwnerIndex { get; set; }

        /// <summary>
        ///     Page of report.
        /// </summary>
        public int? PostReporterIndex { get; set; }

        /// <summary>
        ///     Post report body.
        /// </summary>
        public TextSearch Body { get; set; }

        /// <summary>
        ///     Post report reason.
        /// </summary>
        public TextSearch Reason { get; set; }

        /// <summary>
        ///     When the report was created.
        /// </summary>
        public DoubleRange Created { get; set; }

        /// <summary>
        /// Direction of result sorting.
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        /// Which property should be used for sorting.
        /// </summary>
        public PostReportSort Sort { get; set; }

        /// <summary>
        ///     Result pagination.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}
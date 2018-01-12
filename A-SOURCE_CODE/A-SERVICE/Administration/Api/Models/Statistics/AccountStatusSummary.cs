using SystemDatabase.Enumerations;

namespace Administration.Models.Statistics
{
    public class AccountStatusSummary
    {
        /// <summary>
        /// Status of account.
        /// </summary>
        public Statuses Status { get; set; }

        /// <summary>
        /// Total account which match specific status.
        /// </summary>
        public int Total { get; set; }
    }
}
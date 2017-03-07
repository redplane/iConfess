using iConfess.Database.Enumerations;

namespace iConfess.Admin.Models.Statistics
{
    public class AccountStatusSummary
    {
        /// <summary>
        /// Status of account.
        /// </summary>
        public AccountStatus Status { get; set; }

        /// <summary>
        /// Total account which match specific status.
        /// </summary>
        public int Total { get; set; }
    }
}
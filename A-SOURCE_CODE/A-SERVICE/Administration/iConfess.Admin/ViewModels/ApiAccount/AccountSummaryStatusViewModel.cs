using Shared.Models;

namespace iConfess.Admin.ViewModels.ApiAccount
{
    public class AccountSummaryStatusViewModel
    {
        /// <summary>
        /// Time range when account had been created.
        /// </summary>
        public UnixDateRange Joined { get; set; }

        /// <summary>
        /// Time range when account was lastly modified.
        /// </summary>
        public UnixDateRange LastModified { get; set; }
    }
}
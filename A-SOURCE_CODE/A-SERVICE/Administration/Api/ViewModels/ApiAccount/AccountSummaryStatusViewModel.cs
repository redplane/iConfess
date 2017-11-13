using Shared.Models;

namespace Administration.ViewModels.ApiAccount
{
    public class AccountSummaryStatusViewModel
    {
        /// <summary>
        /// Time range when account had been created.
        /// </summary>
        public DoubleRange Joined { get; set; }

        /// <summary>
        /// Time range when account was lastly modified.
        /// </summary>
        public DoubleRange LastModified { get; set; }
    }
}
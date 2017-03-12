using Shared.ViewModels.Accounts;

namespace iConfess.Admin.ViewModels.ApiSystemMessage
{
    public class PublishSystemMessageViewModel
    {
        /// <summary>
        /// Search accounts conditions.
        /// </summary>
        public FindAccountsViewModel Search { get; set; }

        /// <summary>
        /// Message which should be broadcasted to specific clients.
        /// </summary>
        public string Message { get; set; }
    }
}
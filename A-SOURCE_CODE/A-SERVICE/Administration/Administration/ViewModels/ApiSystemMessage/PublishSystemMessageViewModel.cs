using Shared.ViewModels.Accounts;

namespace Administration.ViewModels.ApiSystemMessage
{
    public class PublishSystemMessageViewModel
    {
        /// <summary>
        /// Search accounts conditions.
        /// </summary>
        public SearchAccountViewModel Search { get; set; }

        /// <summary>
        /// Message which should be broadcasted to specific clients.
        /// </summary>
        public string Message { get; set; }
    }
}
using iConfess.Database.Enumerations;
using Shared.Enumerations;
using Shared.Models;

namespace iConfess.Admin.ViewModels.ApiAccount
{
    public class FindAccountViewModel
    {
        /// <summary>
        /// Id of account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email which is used for account registration.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Name which will be displayed on article.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Statuses of account.
        /// </summary>
        public AccountStatus[] Statuses { get; set; }

        /// <summary>
        /// Range of time when account joined in system.
        /// </summary>
        public UnixDateRange Joined { get; set; }
        
        /// <summary>
        /// Range of time when account was modified its information.
        /// </summary>
        public UnixDateRange LastModified { get; set; }
    }
}
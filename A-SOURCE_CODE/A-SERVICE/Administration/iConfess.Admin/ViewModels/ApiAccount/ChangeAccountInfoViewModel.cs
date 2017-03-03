using iConfess.Database.Enumerations;

namespace iConfess.Admin.ViewModels.ApiAccount
{
    public class ChangeAccountInfoViewModel
    {
        /// <summary>
        /// Nickname of account.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Status of account.
        /// </summary>
        public AccountStatus? Status { get; set; }
    }
}
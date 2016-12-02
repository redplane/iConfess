using System.ComponentModel.DataAnnotations;
using Shared.Resources;

namespace iConfess.Admin.ViewModels.ApiAccount
{
    public class LoginViewModel
    {
        /// <summary>
        ///     Email of account.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "InformationRequired")]
        public string Email { get; set; }

        /// <summary>
        ///     Password related to account.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "InformationRequired")]
        public string Password { get; set; }
    }
}
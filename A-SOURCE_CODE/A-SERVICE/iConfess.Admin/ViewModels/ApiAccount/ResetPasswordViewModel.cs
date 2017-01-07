using System.ComponentModel.DataAnnotations;
using Shared.Resources;

namespace iConfess.Admin.ViewModels.ApiAccount
{
    public class ResetPasswordViewModel
    {
        /// <summary>
        ///     Email of account.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "InformationRequired")]
        public string Email { get; set; }

        /// <summary>
        ///     Request token use to rết password.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "InformationRequired")]
        public string Token { get; set; }

        /// <summary>
        ///     New password to set.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "InformationRequired")]
        public string NewPassword { get; set; }
    }
}
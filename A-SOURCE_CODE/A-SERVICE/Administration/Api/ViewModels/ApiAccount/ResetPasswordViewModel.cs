using System.ComponentModel.DataAnnotations;
using Shared.Constants;
using Shared.Resources;

namespace Administration.ViewModels.ApiAccount
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
        [MinLength(DataConstraints.MinLengthPassword, ErrorMessageResourceType = typeof(HttpMessages), ErrorMessageResourceName = "PasswordMinLengthInvalid")]
        [MaxLength(DataConstraints.MaxLengthPassword, ErrorMessageResourceType = typeof(HttpMessages), ErrorMessageResourceName = "PasswordMaxLengthInvalid")]
        [RegularExpression(Regexes.Password, ErrorMessageResourceType = typeof(HttpMessages), ErrorMessageResourceName = "PasswordFormatInvalid")]
        public string NewPassword { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using Shared.Constants;
using Shared.Resources;

namespace Administration.ViewModels.ApiAccount
{
    public class LoginViewModel
    {
        /// <summary>
        ///     Email of account.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "InformationRequired")]
        [MaxLength(DataConstraints.MaxLengthEmail, ErrorMessageResourceType = typeof(HttpValidationMessages), ErrorMessageResourceName = "DataMaxLengthExceeded")]
        [RegularExpression(Regexes.Email, ErrorMessageResourceType = typeof(HttpValidationMessages), ErrorMessageResourceName = "InvalidDataFormat")]
        public string Email { get; set; }

        /// <summary>
        ///     Password related to account.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "InformationRequired")]
        [MaxLength(DataConstraints.MaxLengthPassword, ErrorMessageResourceType = typeof(HttpValidationMessages), ErrorMessageResourceName = "DataMaxLengthExceeded")]
        [MinLength(DataConstraints.MinLengthPassword, ErrorMessageResourceType = typeof(HttpValidationMessages), ErrorMessageResourceName = "DataMinLengthInvalid")]
        [RegularExpression(Regexes.Password, ErrorMessageResourceType = typeof(HttpValidationMessages), ErrorMessageResourceName = "InvalidDataFormat")]
        public string Password { get; set; }
    }
}
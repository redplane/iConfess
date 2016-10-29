using System.ComponentModel.DataAnnotations;
using Administration.Resources;

namespace Administration.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// Email which is used for logging into system.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "EmailIsRequired")]
        public string Email { get; set; }

        /// <summary>
        /// Password of related account.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PasswordIsRequired")]
        public string Password { get; set; }
    }
}
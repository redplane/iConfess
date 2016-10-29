using System.ComponentModel.DataAnnotations;
using Administration.Resources;

namespace Administration.ViewModels
{
    public class AccountRegisterViewModel
    {
        /// <summary>
        /// Email which is used for registering account into system
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "EmailIsRequired")]
        public string Email { get; set; }

        /// <summary>
        /// Nickname of account.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "NicknameIsRequired")]
        public string Nickname { get; set; }

        /// <summary>
        /// Password of account.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PasswordIsRequired")]
        public string Password { get; set; }
    }
}
namespace Core.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// Email which is used for logging into system.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of related account.
        /// </summary>
        public string Password { get; set; }
    }
}
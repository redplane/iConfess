namespace Shared.Constants
{
    public class DataConstraints
    {
        /// <summary>
        ///     Number of characters that email can contains.
        /// </summary>
        public const int MaxLengthEmail = 128;

        /// <summary>
        ///     Number of characters that nickname can contains.
        /// </summary>
        public const int MaxLengthNickName = 32;

        /// <summary>
        /// Maximum length of account password.
        /// </summary>
        public const int MaxLengthPassword = 16;

        /// <summary>
        /// Minimum length of password.
        /// </summary>
        public const int MinLengthPassword = 6;
    }
}
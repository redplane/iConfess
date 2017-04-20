namespace Administration.Models
{
    public class JwtResponse
    {
        /// <summary>
        ///     Token which is used for accessing into system.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     Type of token.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     When should the token be expired.
        /// </summary>
        public double Expire { get; set; }
    }
}
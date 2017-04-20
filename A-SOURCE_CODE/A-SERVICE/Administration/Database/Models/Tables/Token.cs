using Database.Enumerations;
using Newtonsoft.Json;

namespace Database.Models.Tables
{
    public class Token
    {
        #region Relationships

        /// <summary>
        ///     One category have one owner.
        /// </summary>
        [JsonIgnore]
        public Account Owner { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Who this token belongs to.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        ///     Type of Token.
        /// </summary>
        public TokenType Type { get; set; }

        /// <summary>
        ///     Code of token
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Time when the token was issued.
        /// </summary>
        public double Issued { get; set; }

        /// <summary>
        ///     Time when the token should be expired.
        /// </summary>
        public double Expired { get; set; }

        #endregion
    }
}
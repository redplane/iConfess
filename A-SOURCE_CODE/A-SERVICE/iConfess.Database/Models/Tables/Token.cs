using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iConfess.Database.Enumerations;
using Newtonsoft.Json;

namespace iConfess.Database.Models.Tables
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
        ///     Id of token.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Who this token belongs to.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        ///     Code of token
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Type of Token.
        /// </summary>
        public TokenType Type { get; set; }

        /// <summary>
        ///     When the token expires.
        /// </summary>
        public DateTime Expire { get; set; }

        #endregion
    }
}